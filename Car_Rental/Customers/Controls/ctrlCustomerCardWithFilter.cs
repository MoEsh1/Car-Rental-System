using BuisnessLayer;
using Car_Rental.Customers;
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

namespace Car_Rental.Controls
{
    public partial class ctrlCustomerCardWithFilter : UserControl
    {
        /**********************************************************************************/
        // Define a custom event handler delegate with parameters
        public event Action<int> OnCustomerSelected;
        // Create a protected method to raise the event with a parameter
        protected virtual void CustomerSelected(int CustomerID)
        {
            Action<int> handler = OnCustomerSelected;
            if (handler != null)
            {
                handler(CustomerID); // Raise the event with the parameter
            }
        }
        /**********************************************************************************/
        private bool _ShowAddCustomer = true;

        public bool ShowAddCustomer
        {
            get
            {
                return _ShowAddCustomer;
            }
            set
            {
                _ShowAddCustomer = value;
                btnAddNewCustomer.Enabled = _ShowAddCustomer;
            }
        }
        /**********************************************************************************/
        private bool _FilterEnabled = true;

        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilters.Enabled = _FilterEnabled;
            }
        }
        /**********************************************************************************/
        public int CustomerID
        {
            get
            {
                return ctrlCustomerCard1.CustomerID;
            }
        }

        /**********************************************************************************/
        public clsCustomer SelectedCustomerInfo
        {
            get { return ctrlCustomerCard1.SelectedCustomerInfo; }
        }
        /**********************************************************************************/

        public void LoadCustomerInfo(int CustomerID)
        {
            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = CustomerID.ToString();
            FindNow();
        }
        /**********************************************************************************/
        private void FindNow()
        {
            switch (cbFilterBy.Text)
            {
                case "Customer ID":
                    ctrlCustomerCard1.LoadCustomerInfo(int.Parse(txtFilterValue.Text));
                    break;

                case "Person ID":
                    ctrlCustomerCard1.LoadCustomerInfoByPersonID(int.Parse(txtFilterValue.Text));
                    break;
                case "Driver License No.":
                    ctrlCustomerCard1.LoadCustomerInfoByDriverLicenseNo(txtFilterValue.Text);
                    break;

                default:
                    break;
            }
            if (OnCustomerSelected != null && FilterEnabled)
                // Raise the event with a parameter
                OnCustomerSelected(ctrlCustomerCard1.CustomerID);
        }
        /**********************************************************************************/
        public ctrlCustomerCardWithFilter()
        {
            InitializeComponent();
        }
        /**********************************************************************************/
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }
        /**********************************************************************************/
        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FindNow();
        }

        private void ctrlCustomerCard1_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Focus();
        }

        /**********************************************************************************/
        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }
        /**********************************************************************************/
        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
            {
                btnFind.PerformClick();
            }

            //this will allow only digits if person id or customer id is selected
            if (cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "Customer ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilterValue.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtFilterValue, null);
            }
        }
        /**********************************************************************************/

        private void btnAddNewCustomer_Click(object sender, EventArgs e)
        {
            frmAddUpdateCustomer frm = new frmAddUpdateCustomer();
            frm.DataBack += DataBackEvent;
            frm.ShowDialog();
        }

        /**********************************************************************************/
        private void DataBackEvent(object sender, int CustomerID)
        {
            // handle the data received
            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = CustomerID.ToString();
            ctrlCustomerCard1.LoadCustomerInfo(CustomerID);
        }
    }
}
