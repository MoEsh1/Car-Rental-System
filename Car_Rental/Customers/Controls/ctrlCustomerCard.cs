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
            get { return -CustomerID; }  
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
                MessageBox.Show("No User with UserID = " + _Customer.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillCustomerInfo();
        }

        private void _FillCustomerInfo()
        {

            ctrlPersonCard1.LoadPersonInfo(_Customer.PersonID);
            lblCustomerID.Text = _Customer.CustomerID.ToString();
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
