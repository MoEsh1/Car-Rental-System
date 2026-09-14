using BuisnessLayer;
using Car_Rental.Classes;
using Car_Rental.Global_Classes;
using Car_Rental_BuisnessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Rental.Bookings
{
    public partial class frmAddUpdateReturn : Form
    {
        public enum enMode { AddNew = 0, Update=1};
        private enMode _Mode;
        private int _BookingID;
        private int _ReturnID;

        private clsReturn _Return;

        //Declare a delegate
        public delegate void DataBackEventHandler(object sender, int PersonID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;

        public frmAddUpdateReturn(int ID, enMode Mode)
        {
            InitializeComponent();

            Color darkerOldLace = Color.FromArgb(240, 230, 210);
            clsUtil.SetTitleBarColor(this, darkerOldLace);
            _Mode = Mode;
            if (Mode == enMode.AddNew)
            {
                _BookingID = ID;
            }
            else
            {
                _ReturnID = ID;
            }
            
        }
        // like this we recieve booking Id or return Id, see what todo next
        // 
        private void _ResetDefualtValues()
        {
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Return";
                _Return = new clsReturn();
                lblBookingID.Text = _BookingID.ToString();
            }
            else
            {
                lblTitle.Text = "Update Return";
            }

            dtpReturnDate.Value = DateTime.Now;
            txtMileageAdded.Text = "";
            lblConsumedDays.Text = "";
            lblFinalTotal.Text = "N/A";
        }

        private void _LoadData()
        {
            _Return = clsReturn.Find(_ReturnID);
            if (_Return == null)
            {
                MessageBox.Show("No Return with ID = " + _ReturnID, "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            lblReturnID.Text = _ReturnID.ToString();
            lblBookingID.Text = _Return.BookingID.ToString();
            dtpReturnDate.Value = _Return.ActualReturnDate;
            txtMileageAdded.Text = _Return.MileageAdded.ToString();
            lblConsumedDays.Text = _Return.ConsumedDays.ToString();
            lblFinalTotal.Text = _Return.FinalTotal.ToString();

        }

        private void frmAddUpdateReturn_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            _Return.BookingID = Convert.ToInt32(lblBookingID.Text.Trim());
            _Return.MileageAdded = Convert.ToInt32(txtMileageAdded.Text.Trim());
            _Return.ActualReturnDate = dtpReturnDate.Value;
            _Return.ConsumedDays = Convert.ToInt32(lblConsumedDays.Text.Trim());
            _Return.FinalTotal = clsValidation.MyStrToDecimal(lblFinalTotal.Text.Trim());

            if (_Return.Save())
            {
                lblReturnID.Text = _Return.ReturnID.ToString();
                _Mode = enMode.Update;
                lblTitle.Text = "Update Return";
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);


                // Trigger the event to send data back to the caller form.
                DataBack?.Invoke(this, _Return.ReturnID);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMileageAdded_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtMileageAdded.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtMileageAdded, "Mileage cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtMileageAdded, null);

            }



            if (!clsValidation.IsNumber(txtMileageAdded.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtMileageAdded, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtMileageAdded, null);
            }

        }


        private void btnCalculate_Click(object sender, EventArgs e)
        {
            _CalculateTotalFees();
        }

        private void _CalculateTotalFees()
        {
            //Get the original booking
            clsBooking booking = clsBooking.FindByBookingID(Convert.ToInt32(lblBookingID.Text));
            if (booking == null) 
                return;

            //Calculate Consumed Days (Actual Return Date - Start Date)
            int consumedDays = (dtpReturnDate.Value.Date - booking.StartDate.Date).Days;

            // Minimum 1 day charge, even if returned on the exact same day
            if (consumedDays == 0) 
                consumedDays = 1;

            // Validation: Return date cannot be before Start date
            if (consumedDays < 0)
            {
                MessageBox.Show("Return date cannot be before the booking start date.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblConsumedDays.Text = "";
                lblFinalTotal.Text = "N/A";
                return;
            }

            lblConsumedDays.Text = consumedDays.ToString();

            // Calculate Final Total (Consumed Days * Vehicle's Daily Rate)
            clsVehicle vehicle = clsVehicle.FindByVehicleID(booking.VehicleID);
            if (vehicle != null)
            {
                decimal finalTotal = consumedDays * vehicle.DailyRate;
                lblFinalTotal.Text = finalTotal.ToString();
            }
        }

    }
}
