using BuisnessLayer;
using Car_Rental.Global_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Rental.Customers
{
    public partial class frmAddUpdateCustomer : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        private int _CustomerID = -1;
        clsCustomer _Customer;


        //Declare a delegate
        public delegate void DataBackEventHandler(object sender, int CustomerID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;


        public frmAddUpdateCustomer()
        {
            InitializeComponent();

            // Set the title bar color 
            Color darkerOldLace = Color.FromArgb(240, 230, 210);
            clsUtil.SetTitleBarColor(this, darkerOldLace);

            _Mode = enMode.AddNew;

        }

        public frmAddUpdateCustomer(int CustomerID)
        {
            InitializeComponent();

            // Set the title bar color 
            Color darkerOldLace = Color.FromArgb(240, 230, 210);
            clsUtil.SetTitleBarColor(this, darkerOldLace);
            _Mode = enMode.Update;
            _CustomerID = CustomerID;

        }

        private void _ResetDefualtValues()
        {
            //this will initialize the reset the defaule values

            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Customer";
                this.Text = "Add New Customer";
                _Customer = new clsCustomer();
                ctrlPersonCardWithFilter1.FilterFocus();
            }
            else
            {
                lblTitle.Text = "Update Customer";
                this.Text = "Update Customer";

            }

            txtDriverLicenseNo.Text = "";

        }

        private void _LoadData()
        {

            _Customer = clsCustomer.FindByCustomerID(_CustomerID);
            ctrlPersonCardWithFilter1.FilterEnabled = false;

            if (_Customer == null)
            {
                MessageBox.Show("No Customer with ID = " + _CustomerID, "Customer Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }

            //the following code will not be executed if the person was not found
            lblCustomerID.Text = _Customer.CustomerID.ToString();
            txtDriverLicenseNo.Text = _Customer.DriverLicenseNo;
            ctrlPersonCardWithFilter1.LoadPersonInfo(_Customer.PersonID);




        }

        private void frmAddUpdateCustomer_Load(object sender, EventArgs e)
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
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            _Customer.PersonID = ctrlPersonCardWithFilter1.PersonID;
            _Customer.DriverLicenseNo = txtDriverLicenseNo.Text.Trim();

            if (_Customer.Save())
            {
                lblCustomerID.Text = _Customer.CustomerID.ToString();
                //change form mode to update.
                _Mode = enMode.Update;
                lblTitle.Text = "Update Customer";
                this.Text = "Update Customer";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Trigger the event to send data back to the caller form.
                DataBack?.Invoke(this, _Customer.CustomerID);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void txtDriverLicenseNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDriverLicenseNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDriverLicenseNo, "Driver License No. cannot be blank");
                return;
            }
            else
            {
                errorProvider1.SetError(txtDriverLicenseNo, null);
            }


            if (_Mode == enMode.AddNew)
            {

                if (clsCustomer.isCustomerExist(txtDriverLicenseNo.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtDriverLicenseNo, "Driver License No. is used by another Customer");
                }
                else
                {
                    errorProvider1.SetError(txtDriverLicenseNo, null);
                }
                ;
            }
            else
            {
                //incase update make sure not to use anothers Customer
                if (_Customer.DriverLicenseNo != txtDriverLicenseNo.Text.Trim())
                {
                    if (clsCustomer.isCustomerExist(txtDriverLicenseNo.Text.Trim()))
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(txtDriverLicenseNo, "Driver License No. is used by another Customer");
                        return;
                    }
                    else
                    {
                        errorProvider1.SetError(txtDriverLicenseNo, null);
                    }
                    ;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddUpdateCustomer_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {

            //incase of add new mode.
            if (obj != -1)
            {

                if (clsCustomer.isCustomerExistForPersonID(obj))
                {

                    MessageBox.Show("Selected Person already has a customer, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    groupBox1.Enabled = false;
                    ctrlPersonCardWithFilter1.FilterFocus();
                }

                else
                {
                    groupBox1.Enabled = true;
                }
            }

            else

            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();

            }

        }
    }
}
