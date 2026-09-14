using BuisnessLayer;
using Car_Rental.Global_Classes;
using Car_Rental.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Rental.Bookings
{
    public partial class frmAddUpdateBooking : Form
    {
        public delegate void DataBackEventHandler(object sender, int BookingID);
        public event DataBackEventHandler DataBack;
        private enum enMode { AddNew = 0, Update };
        private enMode _Mode;
        private int _BookingID;
        clsBooking _Booking;
        public frmAddUpdateBooking()
        {
            InitializeComponent();
            // Set the title bar color 
            Color darkerOldLace = Color.FromArgb(240, 230, 210);
            clsUtil.SetTitleBarColor(this, darkerOldLace);

            _Mode = enMode.AddNew;
        }

        public frmAddUpdateBooking(int BookingID)
        {
            InitializeComponent();
            // Set the title bar color 
            Color darkerOldLace = Color.FromArgb(240, 230, 210);
            clsUtil.SetTitleBarColor(this, darkerOldLace);

            _Mode = enMode.Update;
            _BookingID = BookingID;
        }

        private void _ResetDefaultValues()
        {
            _FillVehiclesInComoboBox();


            if( _Mode == enMode.AddNew )
            {
                lblTitle.Text = "Add New Booking";
                this.Text = lblTitle.Text;
                _Booking = new clsBooking();
                tpBookingInfo.Enabled = false;
                ctrlCustomerCardWithFilter1.FilterFocus();            
            }
            else
            {
                lblTitle.Text = "Update Booking";
                this.Text = lblTitle.Text;
                tpBookingInfo.Enabled = true;
                btnSave.Enabled = true;
            }
            if (cbVehicles.Items.Count > 0)
                cbVehicles.SelectedIndex = 0;

            dtpStartDate.Value = DateTime.Now;
            dtpEndDate.Value = DateTime.Now.AddDays(1);
            lblInitialTotal.Text = "0";
            lblBookingID.Text = "????";
        }

        private void _LoadData()
        {
            _Booking = clsBooking.FindByBookingID( _BookingID );
            ctrlCustomerCardWithFilter1.FilterEnabled = false;
            if ( _Booking == null )
            {
                MessageBox.Show("No Booking With ID" + _BookingID, "Booking Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }
            lblBookingID.Text = _Booking.BookingID.ToString();
            
            // In update mode, we must load the car they already booked, even if it is currently marked as 'Not Available'
            clsVehicle bookedVehicle = clsVehicle.FindByVehicleID(_Booking.VehicleID);
            if (bookedVehicle != null && !cbVehicles.Items.Contains(bookedVehicle.PlateNo))
            {
                cbVehicles.Items.Add(bookedVehicle.PlateNo);
            }

            cbVehicles.SelectedIndex = cbVehicles.FindString(bookedVehicle.PlateNo);
            dtpStartDate.Value = _Booking.StartDate;
            dtpEndDate.Value = _Booking.EndDate;
            lblInitialTotal.Text = _Booking.InitialTotal.ToString();
            ctrlCustomerCardWithFilter1.LoadCustomerInfo(_Booking.CustomerID);
        }

        private void _FillVehiclesInComoboBox()
        {
            DataTable dtVehicles = clsVehicle.GetAllAvailableVehicles();

            foreach (DataRow row in dtVehicles.Rows)
            {
                cbVehicles.Items.Add(row["PlateNo"]);
            }
        }

        private void _CalculateTotalFees()
        {
            {
                if (cbVehicles.SelectedIndex == -1) return;

                bool isError = false;

                // Rule 1: Start Date cannot be in the past (only validate this for AddNew mode)
                if (_Mode == enMode.AddNew && dtpStartDate.Value.Date < DateTime.Now.Date)
                {
                    errorProvider1.SetError(dtpStartDate, "Start Date cannot be in the past.");
                    isError = true;
                }
                else
                {
                    errorProvider1.SetError(dtpStartDate, null);
                }

                // Rule 2: End Date must be strictly greater than Start Date
                if (dtpEndDate.Value.Date <= dtpStartDate.Value.Date)
                {
                    errorProvider1.SetError(dtpEndDate, "End Date must be at least 1 day after Start Date.");
                    isError = true;
                }
                else
                {
                    errorProvider1.SetError(dtpEndDate, null);
                }

                if (isError)
                {
                    lblInitialTotal.Text = "0";
                    return;
                }

                // Rule 3: Math Calculation
                int totalDays = (dtpEndDate.Value.Date - dtpStartDate.Value.Date).Days;
                clsVehicle selectedVehicle = clsVehicle.FindByPlateNo(cbVehicles.Text);

                if (selectedVehicle != null)
                {
                    lblInitialTotal.Text =( totalDays * selectedVehicle.DailyRate).ToString();
                }
            }
        }

        private void btnPersonInfoNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpBookingInfo.Enabled = true;
                tcCustomerInfo.SelectedTab = tcCustomerInfo.TabPages["tpBookingInfo"];
                return;
            }

            if (ctrlCustomerCardWithFilter1.CustomerID != -1)
            {
                btnSave.Enabled = true;
                tpBookingInfo.Enabled = true;
                tcCustomerInfo.SelectedTab = tcCustomerInfo.TabPages["tpBookingInfo"];
            }
            else
            {
                MessageBox.Show("Please Select a Customer First.", "Select a Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlCustomerCardWithFilter1.FilterFocus();
            }
        }


        private void frmAddUpdateBooking_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid! Put the mouse over the red icon(s) to see the error.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Final validation before saving
            if (lblInitialTotal.Text == "0")
            {
                MessageBox.Show("Cannot save. Please ensure dates are valid and vehicle is selected.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _Booking.CustomerID = ctrlCustomerCardWithFilter1.CustomerID;
            _Booking.StartDate = dtpStartDate.Value;
            _Booking.EndDate = dtpEndDate.Value;
            _Booking.InitialTotal =Convert.ToDecimal(lblInitialTotal.Text);
            _Booking.VehicleID = clsVehicle.FindByPlateNo(cbVehicles.Text).VehicleID;

            if (_Booking.Save())
            {
                lblBookingID.Text = _Booking.BookingID.ToString();
                _Mode = enMode.Update;
                lblTitle.Text = "Update Booking";
                this.Text = lblTitle.Text;

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

 
                DataBack?.Invoke(this, _Booking.BookingID);
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddUpdateBooking_Activated(object sender, EventArgs e)
        {
            ctrlCustomerCardWithFilter1.FilterFocus();
        }

        private void btnClaculate_Click(object sender, EventArgs e)
        {
            _CalculateTotalFees();

        }
    }
}
