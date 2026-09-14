using BuisnessLayer;
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

namespace Car_Rental.Controls
{
    public partial class ctrlReturnCard : UserControl
    {
        private int _ReturnID = -1;
        private clsReturn _Return;
        public int ReturnID
        {
            get { return _ReturnID; }
        }
        public clsReturn SelectedReturnInfo
        {
            get { return _Return; }
        }

        public ctrlReturnCard()
        {
            InitializeComponent();
        }

        public void LoadReturnInfo(int ReturnID)
        {
            _Return = clsReturn.Find(ReturnID);
            if (_Return == null)
            {
                _ResetReturnInfo();
                MessageBox.Show("No Return with ID = " + ReturnID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillReturnInfo();
        }


        private void _ResetReturnInfo()
        {
            _ReturnID = -1;
            lblReturnID.Text = "?????";
            lblBookingID.Text = "?????";
            lblActualRetunDate.Text = "?????";
            lblMileageAdded.Text = "?????";
            lblConsumedDays.Text = "?????";
            lblFinalTotal.Text = "?????";
        }

        private void _FillReturnInfo()
        {
            _ReturnID = _Return.ReturnID;
            lblReturnID.Text = _Return.ReturnID.ToString();
            lblBookingID.Text = _Return.BookingID.ToString();
            lblActualRetunDate.Text = _Return.ActualReturnDate.ToString();
            lblMileageAdded.Text = _Return.MileageAdded.ToString();
            lblConsumedDays.Text = _Return.ConsumedDays.ToString();
            lblFinalTotal.Text = _Return.FinalTotal.ToString();
        }
    }
}
