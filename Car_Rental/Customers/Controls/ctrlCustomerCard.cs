using BuisnessLayer;
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
    public partial class ctrlCustomerCard : UserControl
    {

        private clsCustomer _Customer;
        private int _CustomerID = -1;
        public int CustomerID
        {
            get { return _CustomerID; }
        }

        public clsCustomer SelectedCustomerInfo
        {
            get { return _Customer; }
        }
        public ctrlCustomerCard()
        {
            InitializeComponent();
        }

        public void LoadCustomerInfo(int CustomerID)
        {
            _Customer = clsCustomer.FindByCustomerID(CustomerID);
            if (_Customer == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("No User with CustomerID = " + CustomerID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillCustomerInfo();
        }


        public void LoadCustomerInfoByPersonID(int PersonID)
        {
            _Customer = clsCustomer.FindByPersonID(PersonID);
            if (_Customer == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("No Customer with PersonID = " + PersonID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillCustomerInfo();
        }


        public void LoadCustomerInfoByDriverLicenseNo(string CustomerID)
        {
            _Customer = clsCustomer.FindByDriverLicenseNo(CustomerID);
            if (_Customer == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("No customer with DriverLicenseNo = " + _Customer.DriverLicenseNo, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillCustomerInfo();
        }


        private void _FillCustomerInfo()
        {

            ctrlPersonCard1.LoadPersonInfo(_Customer.PersonID);
            lblCustomerID.Text = _Customer.CustomerID.ToString();
            _CustomerID = _Customer.CustomerID;
            lblDriverLicenseNo.Text = _Customer.DriverLicenseNo;

        }
        private void _ResetPersonInfo()
        {

            ctrlPersonCard1.ResetPersonInfo();
            lblCustomerID.Text = "[???]";
            lblDriverLicenseNo.Text = "[???]";
        }
    }
}
