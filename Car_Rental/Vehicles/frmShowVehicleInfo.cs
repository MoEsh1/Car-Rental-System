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

namespace Car_Rental.Vehicles
{
    public partial class frmShowVehicleInfo : Form
    {
        public frmShowVehicleInfo(int VehicleID)
        {
            InitializeComponent();
            ctrlVehicleCard1.LoadVehicleInfo(VehicleID);
            // Set the title bar color 
            Color darkerOldLace = Color.FromArgb(240, 230, 210);

            clsUtil.SetTitleBarColor(this, darkerOldLace);
        }

        public frmShowVehicleInfo(string PlateNo)
        {
            InitializeComponent();
            ctrlVehicleCard1.LoadVehicleInfo(PlateNo);
            
            // Set the title bar color 
            Color darkerOldLace = Color.FromArgb(240, 230, 210);

            clsUtil.SetTitleBarColor(this, darkerOldLace);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
