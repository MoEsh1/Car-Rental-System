using Car_Rental.Global_Classes;
using Car_Rental.People;
using Car_Rental.Users;
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

namespace Car_Rental
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            // Set the title bar color 
            Color darkerOldLace = Color.FromArgb(240, 230, 210);

            clsUtil.SetTitleBarColor(this, darkerOldLace);

        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListPeople frm = new frmListPeople();
            frm.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListUsers();
            frm.ShowDialog();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {/*
            clsGlobal.CurrentUser = null;
            _frmLogin.Show();
            this.Close();*/
            MessageBox.Show("To be implmented");
        }
    }
}
