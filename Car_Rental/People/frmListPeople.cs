using BuisnessLayer;
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

namespace Car_Rental.People
{
    public partial class frmListPeople : Form
    {

        private static DataTable _dtAllPeople = clsPerson.GetAllPeople();
        
        // only select the columns that you wabt to show in the grid
        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo", "FirstName", "LastName",
                                                                        "GenderCaption", "DateOfBirth", "CountryName", "Phone", "Email");
        /****************************************************************************************************************/
        private void _RefreshPeopleList()
        {
            _dtAllPeople = clsPerson.GetAllPeople();
            _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "LastName", "GenderCaption", "DateOfBirth", 
                                                       "CountryName", "Phone", "Email");

            dgvPeople.DataSource = _dtPeople;
            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();

        }
        /****************************************************************************************************************/
        public frmListPeople()
        {

            InitializeComponent();

            // Set the title bar color 
            Color darkerOldLace = Color.FromArgb(240, 230, 210);

            clsUtil.SetTitleBarColor(this, darkerOldLace);

        }
        /****************************************************************************************************************/
        private void frmListPeople_Load(object sender, EventArgs e)
        {

            dgvPeople.DataSource = _dtPeople;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
            if (dgvPeople.Rows.Count > 0)
            {

                dgvPeople.Columns[0].HeaderText = "Person ID";
                dgvPeople.Columns[0].Width = 70;

                dgvPeople.Columns[1].HeaderText = "National No.";
                dgvPeople.Columns[1].Width = 70;

                dgvPeople.Columns[2].HeaderText = "Last Name";
                dgvPeople.Columns[2].Width = 100;

                dgvPeople.Columns[3].HeaderText = "Gender";
                dgvPeople.Columns[3].Width = 100;

                dgvPeople.Columns[4].HeaderText = "Date Of Birth";
                dgvPeople.Columns[4].Width = 120;

                dgvPeople.Columns[5].HeaderText = "Nationality";
                dgvPeople.Columns[5].Width = 100;


                dgvPeople.Columns[6].HeaderText = "Phone";
                dgvPeople.Columns[6].Width = 100;


                dgvPeople.Columns[7].HeaderText = "Email";
                dgvPeople.Columns[7].Width = 170;
            }
        }

        /****************************************************************************************************************/
        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {

            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Nationality":
                    FilterColumn = "CountryName";
                    break;

                case "Gender":
                    FilterColumn = "GenderCaption";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "PersonID")
                //in this case we deal with integer not string.

                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();

        }

        /****************************************************************************************************************/
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        /****************************************************************************************************************/
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /****************************************************************************************************************/
        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            Form frm1 = new frmAddUpdatePerson();
            frm1.ShowDialog();
            _RefreshPeopleList();
        }

        /****************************************************************************************************************/

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvPeople.CurrentRow.Cells[0].Value;
            Form frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
        }

        /****************************************************************************************************************/
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            showDetailsToolStripMenuItem_Click(sender, e);
        }

        /****************************************************************************************************************/
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdatePerson((int)dgvPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshPeopleList();
        }

        /****************************************************************************************************************/
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete Person [" + dgvPeople.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)

            {

                //Perform Delele and refresh
                if (clsPerson.DeletePerson((int)dgvPeople.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshPeopleList();
                }

                else
                    MessageBox.Show("Person was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        /****************************************************************************************************************/
        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        /****************************************************************************************************************/
        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        /****************************************************************************************************************/
        private void dgvPeople_DoubleClick(object sender, EventArgs e)
        {
            Form frm = new frmShowPersonInfo((int)dgvPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        /****************************************************************************************************************/
        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id is selected.
            if (cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        /****************************************************************************************************************/
    }
}
