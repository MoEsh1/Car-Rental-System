using BuisnessLayer;
using Car_Rental.Bookings;
using Car_Rental.Global_Classes;
using Car_Rental_BuisnessLayer;
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
    public partial class frmListReturns : Form
    {
        private static DataTable _dtAllReturns;
        public frmListReturns()
        {
            InitializeComponent();

            // Set the title bar color 
            Color darkerOldLace = Color.FromArgb(240, 230, 210);

            clsUtil.SetTitleBarColor(this, darkerOldLace);

        }

        private void frmListReturns_Load(object sender, EventArgs e)
        {
            _dtAllReturns = clsReturn.GetAllReturns();
            dgvReturns.DataSource = _dtAllReturns;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvReturns.Rows.Count.ToString();

            dgvReturns.Columns[0].HeaderText = "Return ID";
            dgvReturns.Columns[0].Width = 100;

            dgvReturns.Columns[1].HeaderText = "Booking ID";
            dgvReturns.Columns[1].Width = 100;

            dgvReturns.Columns[2].HeaderText = "Plate No.";
            dgvReturns.Columns[2].Width = 100;

            dgvReturns.Columns[3].HeaderText = "Start Date";
            dgvReturns.Columns[3].Width = 120;

            dgvReturns.Columns[4].HeaderText = "Actual Return Date";
            dgvReturns.Columns[4].Width = 120;

            dgvReturns.Columns[5].HeaderText = "Mileage Added";
            dgvReturns.Columns[5].Width = 100;

            dgvReturns.Columns[6].HeaderText = "Consumed Days";
            dgvReturns.Columns[6].Width = 50;

            dgvReturns.Columns[7].HeaderText = "Final Total";
            dgvReturns.Columns[7].Width = 50;


            dgvReturns.Columns[8].HeaderText = "Full Name";
            dgvReturns.Columns[8].Width = 200;


            dgvReturns.Columns[9].HeaderText = "National No.";
            dgvReturns.Columns[9].Width = 100;
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            frmAddUpdateReturn frm = new frmAddUpdateReturn((int)dgvReturns.CurrentRow.Cells[0].Value, frmAddUpdateReturn.enMode.Update);
            frm.ShowDialog();
            frmListReturns_Load(null, null);
            
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvReturns_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            showDetailsToolStripMenuItem_Click(sender, e);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowReturnInfo frm = new frmShowReturnInfo((int)dgvReturns.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            
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
                case "Return ID":
                    FilterColumn = "ReturnID";
                    break;

                case "Booking ID":
                    FilterColumn = "BookingID";
                    break;

                case "Plate No.":
                    FilterColumn = "PlateNo";
                    break;

                case "Start Date":
                    FilterColumn = "StartDate";
                    break;

                case "Actual Return Date":
                    FilterColumn = "ActualReturnDate";
                    break;
               
                case "Consumed Days":
                    FilterColumn = "ConsumedDays";
                    break;
               
                case "Final Total":
                    FilterColumn = "FinalTotal";
                    break;
                
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "National No.":
                    FilterColumn = "NationalNo";
                    break;
                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllReturns.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvReturns.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "ReturnID" || FilterColumn == "BookingID" || FilterColumn == "Year" || 
                FilterColumn == "ConsumedDays" || FilterColumn == "FinalTotal")
                //in this case we deal with numbers not string.
                _dtAllReturns.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllReturns.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = _dtAllReturns.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase Booking id or year is selected.
            if (cbFilterBy.Text == "Return ID" || cbFilterBy.Text == "Booking ID" || cbFilterBy.Text == "Consumed Days"
                || cbFilterBy.Text == "Final Total")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
