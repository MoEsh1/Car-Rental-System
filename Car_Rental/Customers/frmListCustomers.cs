using BuisnessLayer;
using Car_Rental.Global_Classes;
using Car_Rental.Users;
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
    public partial class frmListCustomers : Form
    {
        private static DataTable _dtAllCustomers;
        public frmListCustomers()
        {
            InitializeComponent();

            // Set the title bar color 
            Color darkerOldLace = Color.FromArgb(240, 230, 210);

            clsUtil.SetTitleBarColor(this, darkerOldLace);
        }

        private void frmListCustomers_Load(object sender, EventArgs e)
        {
            _dtAllCustomers = clsCustomer.GetAllCustomers();
            dgvCustomers.DataSource = _dtAllCustomers;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvCustomers.Rows.Count.ToString();

            dgvCustomers.Columns[0].HeaderText = "Customer ID";
            dgvCustomers.Columns[0].Width = 110;

            dgvCustomers.Columns[1].HeaderText = "Person ID";
            dgvCustomers.Columns[1].Width = 120;

            dgvCustomers.Columns[2].HeaderText = "Full Name";
            dgvCustomers.Columns[2].Width = 350;

            dgvCustomers.Columns[3].HeaderText = "Driver License No.";
            dgvCustomers.Columns[3].Width = 120;
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if (cbFilterBy.Text == "None")
            {
                txtFilterValue.Enabled = false;
            }
            else
                txtFilterValue.Enabled = true;

            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }
        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Customer ID":
                    FilterColumn = "CustomerID";
                    break;
                case "Driver License No.":
                    FilterColumn = "DriverLicenseNo";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllCustomers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvCustomers.Rows.Count.ToString();
                return;
            }


            if (FilterColumn != "FullName" && FilterColumn != "DriverLicenseNo")
                //in this case we deal with numbers not string.
                _dtAllCustomers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllCustomers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = _dtAllCustomers.Rows.Count.ToString();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            frmUserInfo Frm1 = new frmUserInfo((int)dgvUsers.CurrentRow.Cells[0].Value);
            Frm1.ShowDialog();
             
             */
            MessageBox.Show("to be implemented");

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            btnAddCustomer_Click(sender, e);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            frmAddUpdateUser Frm1 = new frmAddUpdateUser((int)dgvUsers.CurrentRow.Cells[0].Value);
            Frm1.ShowDialog();
            frmListUsers_Load(null, null);
             */
            MessageBox.Show("to be implemented");

        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            //frmAddUpdatCustomer Frm = new frmAddUpdatCustomer();
            //Frm.ShowDialog();
            //frmListCustomers_Load(null, null);
            MessageBox.Show("to be implemented");
        }

        private void dgvCustomers_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            frmUserInfo Frm1 = new frmUserInfo((int)dgvUsers.CurrentRow.Cells[0].Value);
            Frm1.ShowDialog();
             */
            MessageBox.Show("to be implemented");

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int CustomerID = (int)dgvCustomers.CurrentRow.Cells[0].Value;
            if (clsCustomer.DeleteCustomer(CustomerID))
            {
                MessageBox.Show("Customer has been deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmListCustomers_Load(null, null);
            }

            else
                MessageBox.Show("Customer is not delted due to data connected to it.", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id or customer id is selected.
            if (cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "Customer ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }


    }
}
