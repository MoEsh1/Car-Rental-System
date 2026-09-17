using BuisnessLayer;
using Car_Rental.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Car_Rental.People;
using static Car_Rental.Global_Classes.clsEventLogger;
namespace Car_Rental.Controls
{

    public partial class ctrlPersonCard : UserControl
    {
        private int _PersonID = -1;
        private clsPerson _Person;

        public int PersonID 
        { 
            get { return _PersonID; } 
        }

        public clsPerson SelectedPersonInfo
        {
            get { return _Person; }
        }

        /****************************************************************************************************************************************/
        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        /****************************************************************************************************************************************/
        public void LoadPersonInfo(int PersonID)
        {
            _Person = clsPerson.Find(PersonID);
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person with PersonID = " + PersonID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogError("No Person with PersonID = " + PersonID.ToString());
                return;
            }

            _FillPersonInfo();
        }

        /****************************************************************************************************************************************/
        public void LoadPersonInfo(string NationalNo)
        {
            _Person = clsPerson.Find(NationalNo);
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person with National No. = " + NationalNo.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogError("No Person with National No. = " + NationalNo.ToString());
                return;
            }

            _FillPersonInfo();
        }

        /****************************************************************************************************************************************/
        public void ResetPersonInfo()
        {
            _PersonID = -1;
            lblPersonID.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblFullName.Text = "[????]";
            pbGender.Image = Resources.Man_32;
            lblGender.Text = "[????]";
            lblEmail.Text = "[????]";
            lblPhone.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblCountry.Text = "[????]";
            lblAddress.Text = "[????]";

        }

        /****************************************************************************************************************************************/
        private void _FillPersonInfo()
        {
            llEditPersonInfo.Enabled = true;
            _PersonID = _Person.PersonID;
            lblPersonID.Text = _Person.PersonID.ToString();
            lblNationalNo.Text = _Person.NationalNo;
            lblFullName.Text = _Person.FullName;
            lblGender.Text = _Person.Gender == 0 ? "Male" : "Female";
            lblEmail.Text = _Person.Email;
            lblPhone.Text = _Person.Phone;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
            lblCountry.Text = clsCountry.Find(_Person.CountryID).CountryName;
            lblAddress.Text = _Person.Address;

        }

        /****************************************************************************************************************************************/
        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson(_PersonID);
            frm.ShowDialog();

            //refresh
            LoadPersonInfo(_PersonID);
        }

        /****************************************************************************************************************************************/

    }
}
