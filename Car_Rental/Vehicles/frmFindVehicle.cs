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
    public partial class frmFindVehicle : Form
    {
        public delegate void DataBackEventHandler(object sender, int VehicleID);
        public DataBackEventHandler DataBack;
        public frmFindVehicle()
        {
            InitializeComponent();

            // Set the title bar color 
            Color darkerOldLace = Color.FromArgb(240, 230, 210);

            clsUtil.SetTitleBarColor(this, darkerOldLace);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Trigger the event to send data back to the caller form.
            DataBack?.Invoke(this, ctrlVehicleCardWithFilter1.VehicleID);

            this.Close();
        }
    }
}
