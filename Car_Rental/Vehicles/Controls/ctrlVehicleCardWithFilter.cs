using BuisnessLayer;
using Car_Rental.Controls;
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

namespace Car_Rental.Vehicles.Controls
{
    public partial class ctrlVehicleCardWithFilter : UserControl
    {
        public delegate void OneVehicleSelected(int VehicleID);
        public event OneVehicleSelected OneVehicleIDSelected;
        protected virtual void VehicleSelected(int VehicleID)
        {
            OneVehicleIDSelected(VehicleID);
        }

        private bool _ShowAddVehicle = true;
        public bool ShowAddVehicle
        {
            get
            {
                return _ShowAddVehicle;
            }
            set
            {
                _ShowAddVehicle = value;
                btnAddNewVehicle.Enabled = _ShowAddVehicle;
            }
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilters.Enabled = _FilterEnabled;
            }
        }

        public int VehicleID
        {
            get
            {
                return ctrlVehicleCard1.VehicleID;
            }
        }

        public clsVehicle SelectedVehicleInfo
        {
            get { return ctrlVehicleCard1.SelectedVehicleInfo; }
        }

        public void LoadVehicleInfo(int  VehicleID)
        {
            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = VehicleID.ToString();
            _FindNow();
        }

        private void _FindNow()
        {
            switch (cbFilterBy.Text)
            {
                case "Vehicle ID":
                    ctrlVehicleCard1.LoadVehicleInfo(int.Parse(txtFilterValue.Text));
                    break;

                case "Plate No.":
                    ctrlVehicleCard1.LoadVehicleInfo(txtFilterValue.Text);
                    break;

                default:
                    break;
            }
            if (OneVehicleIDSelected != null && FilterEnabled)
                // Raise the event with a parameter
                OneVehicleIDSelected(ctrlVehicleCard1.VehicleID);
        }
        public ctrlVehicleCardWithFilter()
        {
            InitializeComponent();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FindNow();
        }

        private void ctrlVehicleCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Focus();
        }

        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
            {
                btnFind.PerformClick();
            }

            //this will allow only digits if person id is selected
            if (cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilterValue.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtFilterValue, null);
            }
        }

        private void btnAddNewVehicle_Click(object sender, EventArgs e)
        {
            frmAddUpdateVehicle frm = new frmAddUpdateVehicle();
            frm.DataBack += DataBackEvent;
            frm.ShowDialog();
        }

        private void DataBackEvent(object sender, int VehicleID)
        {
            // handle the data received
            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = VehicleID.ToString();
            ctrlVehicleCard1.LoadVehicleInfo(VehicleID);
        }
    }
}
