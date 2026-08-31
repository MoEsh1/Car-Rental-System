using BuisnessLayer;
using Car_Rental.Global_Classes;
using Car_Rental.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Rental.Vehicles
{
    public partial class frmListVehicles : Form
    {
        private static DataTable _dtAllVehicles = clsVehicle.GetAllVehicles();

        private void _RefreshVehiclesList()
        {
            _dtAllVehicles = clsVehicle.GetAllVehicles();
            dgvVehicles.DataSource = _dtAllVehicles;
            lblRecordsCount.Text = dgvVehicles.Rows.Count.ToString();
        }
        public frmListVehicles()
        {
            InitializeComponent();

            // Set the title bar color 
            Color darkerOldLace = Color.FromArgb(240, 230, 210);

            clsUtil.SetTitleBarColor(this, darkerOldLace);

        }

        private void frmListVehicles_Load(object sender, EventArgs e)
        {
            //dgvVehicles.DataSource = _dtAllVehicles;
            //cbFilterBy.SelectedIndex = 0;
            //lblRecordsCount.Text = dgvVehicles.Rows.Count.ToString();
            _RefreshVehiclesList();

            dgvVehicles.Columns[0].HeaderText = "Vehicle ID";
            dgvVehicles.Columns[0].Width = 120;

            dgvVehicles.Columns[1].HeaderText = "Plate No.";
            dgvVehicles.Columns[1].Width = 120;

            dgvVehicles.Columns[2].HeaderText = "Make";
            dgvVehicles.Columns[2].Width = 120;

            dgvVehicles.Columns[3].HeaderText = "Model";
            dgvVehicles.Columns[3].Width = 120;

            dgvVehicles.Columns[4].HeaderText = "Year";
            dgvVehicles.Columns[4].Width = 120;

            dgvVehicles.Columns[5].HeaderText = "Mileage";
            dgvVehicles.Columns[5].Width = 120;


            dgvVehicles.Columns[6].HeaderText = "Daily Rate";
            dgvVehicles.Columns[6].Width = 120;


            dgvVehicles.Columns[7].HeaderText = "IsAvailable";
            dgvVehicles.Columns[7].Width = 120;


        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int VehicleID = (int)dgvVehicles.CurrentRow.Cells[0].Value;
            Form frm = new frmShowVehicleInfo(VehicleID);
            frm.ShowDialog();

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            showDetailsToolStripMenuItem_Click(sender, e);

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdateVehicle((int)dgvVehicles.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshVehiclesList();


        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete Vehicle [" + dgvVehicles.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)

            {

                //Perform Delele and refresh
                if (clsVehicle.DeletVehicle((int)dgvVehicles.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Vehicle Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshVehiclesList();
                }

                else
                    MessageBox.Show("Vehicle was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void btnAddVehicle_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdateVehicle();
            frm.ShowDialog();
            _RefreshVehiclesList();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Vehicle ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Vehicle ID":
                    FilterColumn = "VehicleID";
                    break;

                case "Plate No.":
                    FilterColumn = "PlateNo";
                    break;

                case "Make":
                    FilterColumn = "Make";
                    break;

                case "Model":
                    FilterColumn = "Model";
                    break;

                case "Year":
                    FilterColumn = "Year";
                    break;

                case "Mileage":
                    FilterColumn = "Mileage";
                    break;

                case "Daily Rate":
                    FilterColumn = "DailyRate";
                    break;

                case "Is Available":
                    FilterColumn = "IsAvailable";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllVehicles.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvVehicles.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "VehicleID")
                //in this case we deal with integer not string.

                _dtAllVehicles.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllVehicles.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

        }

        private void dgvVehicles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Form frm = new frmShowVehicleInfo((int)dgvVehicles.CurrentRow.Cells[0].Value);
            //frm.ShowDialog();
        }
    }
}
