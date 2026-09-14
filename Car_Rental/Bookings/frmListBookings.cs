using BuisnessLayer;
using Car_Rental.Customers;
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

namespace Car_Rental.Bookings
{
    public partial class frmListBookings : Form
    {
        private static DataTable _dtAllBookings;
        public frmListBookings()
        {
            InitializeComponent();

            // Set the title bar color 
            Color darkerOldLace = Color.FromArgb(240, 230, 210);

            clsUtil.SetTitleBarColor(this, darkerOldLace);

        }

        private void frmListBookings_Load(object sender, EventArgs e)
        {
            _dtAllBookings = clsBooking.GetAllBookings();
            dgvBookings.DataSource = _dtAllBookings;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvBookings.Rows.Count.ToString();

            dgvBookings.Columns[0].HeaderText = "Booking ID";
            dgvBookings.Columns[0].Width = 100;

            dgvBookings.Columns[1].HeaderText = "Full Name";
            dgvBookings.Columns[1].Width = 200;

            dgvBookings.Columns[2].HeaderText = "National No.";
            dgvBookings.Columns[2].Width = 100;

            dgvBookings.Columns[3].HeaderText = "Plate No.";
            dgvBookings.Columns[3].Width = 100;

            dgvBookings.Columns[4].HeaderText = "Make";
            dgvBookings.Columns[4].Width = 100;

            dgvBookings.Columns[5].HeaderText = "Model";
            dgvBookings.Columns[5].Width = 100;

            dgvBookings.Columns[6].HeaderText = "Year";
            dgvBookings.Columns[6].Width = 50;

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowBookingInfo Frm1 = new frmShowBookingInfo((int)dgvBookings.CurrentRow.Cells[0].Value);
            Frm1.ShowDialog();
           
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            btnAddBooking_Click(sender, e);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            frmAddUpdateBooking Frm1 = new frmAddUpdateBooking((int)dgvBookings.CurrentRow.Cells[0].Value);
            Frm1.ShowDialog();
            frmListBookings_Load(null, null);
 
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            int BookingID = (int)dgvBookings.CurrentRow.Cells[0].Value;
            if (clsBooking.DeleteBooking(BookingID))
            {
                MessageBox.Show("Booking has been deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmListBookings_Load(null, null);
            }

            else
                MessageBox.Show("Booking is not delted due to data connected to it.", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
       
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddBooking_Click(object sender, EventArgs e)
        {
            
            frmAddUpdateBooking Frm = new frmAddUpdateBooking();
            Frm.ShowDialog();
            frmListBookings_Load(null, null); 
             
        }

        private void dgvBookings_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            showDetailsToolStripMenuItem_Click(sender, e);

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            txtFilterValue.Enabled = (cbFilterBy.Text != "None");
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Booking ID":
                    FilterColumn = "BookingID";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "Plate No":
                    FilterColumn = "PlateNo.";
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

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllBookings.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvBookings.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "BookingID" || FilterColumn == "Year")
                //in this case we deal with numbers not string.
                _dtAllBookings.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllBookings.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = _dtAllBookings.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase Booking id or year is selected.
            if (cbFilterBy.Text == "Booking ID" || cbFilterBy.Text == "Year")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void issueReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateReturn Frm1 = new frmAddUpdateReturn((int)dgvBookings.CurrentRow.Cells[0].Value, frmAddUpdateReturn.enMode.AddNew);
            Frm1.ShowDialog();
        }
    }
}
