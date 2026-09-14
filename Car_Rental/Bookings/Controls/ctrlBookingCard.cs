using BuisnessLayer;
using Car_Rental.Customers;
using Car_Rental.Vehicles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Rental.Controls
{
    public partial class ctrlBookingCard : UserControl
    {
        private int _BookingID = -1;
        private clsBooking _Booking;
        public int BookingID
        {
            get { return _BookingID; }
        }
        public clsBooking SelectedBookingInfo
        {
            get { return _Booking; }
        }
        public ctrlBookingCard()
        {
            InitializeComponent();
        }

        public void LoadBookingInfo(int BookingID)
        {
            _Booking = clsBooking.FindByBookingID(BookingID);
            if( _Booking == null )
            {
                _ResetBookingInfo();
                MessageBox.Show("No Booking with ID = " + BookingID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillBookingInfo();
        }
        private void _ResetBookingInfo()
        {
            _BookingID = -1;
            lblBookingID.Text = "?????";
            lblCustomerID.Text = "?????";
            lblVehicleID.Text = "?????";
            lblStartDate.Text = "?????";
            lblEndDate.Text = "?????";
            lblInitialTotal.Text = "?????";
        }

        private void _FillBookingInfo()
        {
            llblShowCustomer.Enabled = true;
            llShowVehicleInfo.Enabled = true;
            _BookingID = _Booking.BookingID;
            lblBookingID.Text = _BookingID.ToString();
            lblCustomerID.Text = _Booking.CustomerID.ToString();
            lblVehicleID.Text = _Booking.VehicleID.ToString();
            lblStartDate.Text = _Booking.StartDate.ToString();
            lblEndDate.Text = _Booking.EndDate.ToString();
            lblInitialTotal.Text = _Booking.InitialTotal.ToString();
        }

        private void llblShowCustomer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmCustomerInfo frm = new frmCustomerInfo(_Booking.CustomerID);
            frm.ShowDialog();
            LoadBookingInfo(_Booking.BookingID);
        }

        private void llShowVehicleInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdateVehicle frm = new frmAddUpdateVehicle(_Booking.VehicleID);
            frm.ShowDialog();

            //refresh
            LoadBookingInfo(_Booking.BookingID);
        }
    }
}
