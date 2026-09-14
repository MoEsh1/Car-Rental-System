using BuisnessLayer;
using Car_Rental.Classes;
using Car_Rental.Global_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Rental.Vehicles
{
    public partial class frmAddUpdateVehicle : Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);
        public event DataBackEventHandler DataBack;
        public enum enMode { AddNew = 0, Update };
        enMode _Mode;
        private int _VehicleID = -1;
        clsVehicle _Vehicle;

        /*********************************************************************************************************************/
        public frmAddUpdateVehicle()
        {
            InitializeComponent();
            // Set the title bar color 
            Color darkerOldLace = Color.FromArgb(240, 230, 210);

            clsUtil.SetTitleBarColor(this, darkerOldLace);


            _Mode = enMode.AddNew;
        }

        /*********************************************************************************************************************/
        public frmAddUpdateVehicle(int VehicleID)
        {
            InitializeComponent();
           
            // Set the title bar color 
            Color darkerOldLace = Color.FromArgb(240, 230, 210);

            clsUtil.SetTitleBarColor(this, darkerOldLace);


            _Mode = enMode.Update;
            _VehicleID = VehicleID;
        }

        /*********************************************************************************************************************/
        private void _ResetDefaultValues()
        {
            if( _Mode == enMode.AddNew )
            {
                lblTitle.Text = "Add New Vehicle";
                _Vehicle = new clsVehicle();
            }
            else
            {
                lblTitle.Text = "Update Vehicle";
            }
            txtPlateNo.Text = "";
            txtMake.Text = "";
            txtModel.Text = "";
            dtpYear.Value = DateTime.Now;
            txtMileage.Text = "";
            txtDailyRate.Text = "";
            chkAvailable.Checked = false;
        }

        /*********************************************************************************************************************/
        private void _LoadData()
        {
            _Vehicle = clsVehicle.FindByVehicleID( _VehicleID );
            if ( _Vehicle == null )
            {
                MessageBox.Show("No Vehicle with Id = " + _VehicleID, "Vehicle Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }
            lblVehicleID.Text = _VehicleID.ToString();
            txtPlateNo.Text = _Vehicle.PlateNo;
            txtMake.Text = _Vehicle.Make;
            txtModel.Text= _Vehicle.Model;
            if (_Vehicle.Year > 1800) // Sanity check to avoid invalid dates
            {
                dtpYear.Value = new DateTime(_Vehicle.Year, 1, 1);
            }
            else
            {
                dtpYear.Value = DateTime.Now; // Safe fallback for new/empty records
            }

            txtMileage.Text = _Vehicle.Mileage.ToString();
            txtDailyRate.Text = _Vehicle.DailyRate.ToString();
            chkAvailable.Checked = _Vehicle.IsAvailable;
        }

        /*********************************************************************************************************************/
        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_Mode == enMode.Update)
                _LoadData();
        }

        /*********************************************************************************************************************/
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /*********************************************************************************************************************/

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            _Vehicle.PlateNo = txtPlateNo.Text.Trim();
            _Vehicle.Make = txtMake.Text.Trim();
            _Vehicle.Model = txtModel.Text.Trim();
            _Vehicle.Year = dtpYear.Value.Year;
            _Vehicle.Mileage = Convert.ToInt32(txtMileage.Text.Trim());
            _Vehicle.DailyRate = Convert.ToDecimal(txtDailyRate.Text.Trim());
            _Vehicle.DailyRate = clsValidation.MyStrToDecimalDef(txtDailyRate.Text.Trim(),0m);
            _Vehicle.IsAvailable = chkAvailable.Checked;


            if (_Vehicle.Save())
            {
                lblVehicleID.Text = _Vehicle.VehicleID.ToString();
                //change form mode to update.
                _Mode = enMode.Update;
                lblTitle.Text = "Update Vehicle";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);


                // Trigger the event to send data back to the caller form.
                DataBack?.Invoke(this, _Vehicle.VehicleID);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        
        /*********************************************************************************************************************/
        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            TextBox Temp = (TextBox)sender;
            if (string.IsNullOrEmpty(Temp.Text.Trim())) 
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                errorProvider1.SetError(Temp, null);
            }
        }

        /*********************************************************************************************************************/
        private void txtMileage_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtMileage.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtMileage, "Mileage cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtMileage, null);

            }
            


            if (!clsValidation.IsNumber(txtMileage.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtMileage, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtMileage, null);
            }
            
        }

        /*********************************************************************************************************************/
        private void txtDailyRate_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtMileage.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDailyRate, "Daily Rate cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtDailyRate, null);

            }
            
            if (!clsValidation.IsNumber(txtDailyRate.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDailyRate, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtDailyRate, null);
            }

        }
        /*********************************************************************************************************************/
    }
}
