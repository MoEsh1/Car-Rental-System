using Car_Rental.Controls;
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

namespace Car_Rental.Returns
{
    public partial class frmShowReturnInfo : Form
    {
        public frmShowReturnInfo(int ReturnID)
        {
            InitializeComponent();

            // Set the title bar color 
            Color darkerOldLace = Color.FromArgb(240, 230, 210);

            clsUtil.SetTitleBarColor(this, darkerOldLace);
            ctrlReturnCard1.LoadReturnInfo(ReturnID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
