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
    public partial class frmCustomerInfo : Form
    {

        private int _CustomerID;
        public frmCustomerInfo(int CustomerID)
        {
            InitializeComponent();
            _CustomerID = CustomerID;

            // Set the title bar color 
            Color darkerOldLace = Color.FromArgb(240, 230, 210);

            clsUtil.SetTitleBarColor(this, darkerOldLace);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCustomerInfo_Load(object sender, EventArgs e)
        {
            ctrlCustomerCard1.LoadCustomerInfo(_CustomerID);
        }
    }
}
