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

namespace Car_Rental.People
{
    public partial class frmShowPersonInfo : Form
    {
        public frmShowPersonInfo(int PersonID)
        {
            InitializeComponent();
            ctrlPersonCard1.LoadPersonInfo(PersonID);
            // Set the title bar color 
            Color darkerOldLace = Color.FromArgb(240, 230, 210);

            clsUtil.SetTitleBarColor(this, darkerOldLace);

        }

        public frmShowPersonInfo(string NationalNo)
        {
            InitializeComponent();
            ctrlPersonCard1.LoadPersonInfo(NationalNo);

            // Set the title bar color 
            Color darkerOldLace = Color.FromArgb(240, 230, 210);

            clsUtil.SetTitleBarColor(this, darkerOldLace);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
