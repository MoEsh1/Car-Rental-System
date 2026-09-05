using Car_Rental.Global_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Rental.Users
{
    public partial class frmUserInfo : Form
    {
        private int _UserID;

        public frmUserInfo(int UserID)
        {
            InitializeComponent();

            _UserID = UserID;

            // Set the title bar color 
            Color darkerOldLace = Color.FromArgb(240, 230, 210);

            clsUtil.SetTitleBarColor(this, darkerOldLace);


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUserInfo_Load(object sender, EventArgs e)
        {
            ctrlUserCard1.LoadUserInfo(_UserID);
        }
    }
}
