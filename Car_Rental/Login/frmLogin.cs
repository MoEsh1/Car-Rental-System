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
using static Car_Rental.Global_Classes.clsEventLogger;
namespace Car_Rental.Login
{
    public partial class frmLogin : Form
    {
        /********************************************************************************************************/
        public frmLogin()
        {
            InitializeComponent();
        }

        /********************************************************************************************************/
        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsUser user = clsUser.FindByUsernameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());

            if (user != null)
            {

                if (chkRememberMe.Checked)
                {
                    //store username and password
                    clsGlobal.RememberUsernameAndPasswordAtReg(txtUserName.Text.Trim(), txtPassword.Text.Trim());

                }
                else
                {
                    //store empty username and password
                    clsGlobal.RememberUsernameAndPasswordAtReg("", "");

                }

                //incase the user is not active
                if (!user.IsActive)
                {

                    txtUserName.Focus();
                    MessageBox.Show("Your accound is not Active, Contact Admin.", "In Active Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LogError("Your accound is not Active, Contact Admin.");
                    return;
                }

                clsGlobal.CurrentUser = user;
                this.Hide();
                frmMain frm = new frmMain(this);
                frm.ShowDialog();


                // If CurrentUser is not null, it means close 'X'  at frmMain was clicked to exit the app.
                if (clsGlobal.CurrentUser != null)
                {
                    Application.Exit();
                }

            }
            else
            {
                txtUserName.Focus();
                MessageBox.Show("Invalid Username/Password.", "Wrong Credintials", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogError("Invalid Username/Password.");
            }

        }
        /********************************************************************************************************/
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /********************************************************************************************************/
        private void frmLogin_Load(object sender, EventArgs e)
        {
            string UserName = "", Password = "";

            if (clsGlobal.GetStoredCredentialFromReg(ref UserName, ref Password))
            {
                txtUserName.Text = UserName;
                txtPassword.Text = Password;
                chkRememberMe.Checked = true;
            }
            else
                chkRememberMe.Checked = false;

        }
        /********************************************************************************************************/
    }
}
