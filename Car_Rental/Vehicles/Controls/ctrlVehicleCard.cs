using BuisnessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Car_Rental.Vehicles;

namespace Car_Rental.Controls
{
    public partial class ctrlVehicleCard : UserControl
    {
        private int _VehicleID = -1;
        private clsVehicle _Vehicle;

        public int VehicleID
        {
            get { return _VehicleID; }
        }
        public clsVehicle  SelectedVehicleInfo
        {
            get { return _Vehicle; } 
        }

        public ctrlVehicleCard()
        {
            InitializeComponent();
        }

        public void LoadVehicleInfo(int VehicleID)
        {
            _Vehicle = clsVehicle.FindByVehicleID(VehicleID);
            if( _Vehicle == null )
            {
                _ResetVehicleInfo();
                MessageBox.Show("No Vehicle with ID = " +  VehicleID.ToString(), "Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillVehicleInfo();
        }

        public void LoadVehicleInfo(string PlateNo)
        {
            _Vehicle = clsVehicle.FindByPlateNo(PlateNo);
            if (_Vehicle == null)
            {
                _ResetVehicleInfo();
                MessageBox.Show("No Vehicle with Plate No. = " + PlateNo.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillVehicleInfo();
        }

        private void _ResetVehicleInfo()
        {
            _VehicleID = -1;
            lblPlateNo.Text = "[????]";
            lblMake.Text = "[????]";
            lblModel.Text = "[????]";
            lblYear.Text = "[????]";
            lblMileage.Text = "[????]";
            lblDailyRate.Text = "[????]";
            lblAvailable.Text = "[????]";
        }
        private void _FillVehicleInfo()
        {
            llEditVehicleInfo.Enabled = true;
            _VehicleID = _Vehicle.VehicleID;
            lblVehicleID.Text = _Vehicle.VehicleID.ToString();
            lblPlateNo.Text = _Vehicle.PlateNo;
            lblMake.Text = _Vehicle.Make;
            lblModel.Text = _Vehicle.Model;
            lblYear.Text = _Vehicle.Year.ToString();
            lblMileage.Text = _Vehicle.Mileage.ToString();
            lblDailyRate.Text = _Vehicle.DailyRate.ToString();
            lblAvailable.Text = _Vehicle.IsAvailable.ToString();
        }

        private void llEditVehicleInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdateVehicle frm = new frmAddUpdateVehicle(_VehicleID);
            frm.ShowDialog();

            //refresh
            LoadVehicleInfo(_VehicleID);
        }
    }
}
