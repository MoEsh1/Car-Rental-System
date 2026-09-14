using BuisnessLayer;
using Car_Rental_DataAccess;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental_BuisnessLayer
{
    public class clsReturn
    {
        /*int ReturnID, int BookingID, DateTime ActualReturnDate,
                                   int MileageAdded, int ConsumedDays, decimal FinalTotal*/
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int ReturnID { get; set; }
        public int BookingID { get; set; }
        public DateTime ActualReturnDate { get; set; }
        public int MileageAdded { get; set; }
        public int ConsumedDays { get; set; }
        public Decimal FinalTotal { get; set; }

        public static DataTable GetAllReturns()
        {
            return clsReturnData.GetAllReturns();
        }

        public clsReturn()
        {
            ReturnID = 0;
            BookingID = 0;
            ActualReturnDate = DateTime.Now;
            MileageAdded = 0;
            ConsumedDays = 0;
            FinalTotal = 0;
            Mode = enMode.AddNew;
        }
        public clsReturn(int ReturnID, int BookingID, DateTime ActualReturnDate, int MileageAdded, int ConsumedDays, decimal FinalTotal)
        {
            this.ReturnID = ReturnID;
            this.BookingID = BookingID;
            this.ActualReturnDate = ActualReturnDate; 
            this.MileageAdded = MileageAdded;
            this.ConsumedDays = ConsumedDays;
            this.FinalTotal = FinalTotal;
            Mode = enMode.Update;

        }

        private bool _AddNewReturn()
        {
            this.ReturnID = clsReturnData.AddNewReturns(BookingID, ActualReturnDate, MileageAdded, ConsumedDays, FinalTotal);

            if (this.ReturnID != -1)
            {
                // Release the vehicle and update its mileage
                clsBooking booking = clsBooking.FindByBookingID(this.BookingID);
                if (booking != null)
                {
                    clsVehicle vehicle = clsVehicle.FindByVehicleID(booking.VehicleID);
                    if (vehicle != null)
                    {
                        vehicle.IsAvailable = true;
                        vehicle.Mileage += this.MileageAdded; // Add the trip mileage to the car's total
                        vehicle.Save(); // Save vehicle changes to the database
                    }
                }
                return true;
            }
            return false;
        }

        private bool _UpdateReturn()
        {
            return clsReturnData.UpdateReturn(ReturnID, BookingID, ActualReturnDate, MileageAdded, ConsumedDays, FinalTotal);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewReturn())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateReturn();
            }

            return false;
        }
        public static clsReturn Find(int ReturnID)
        {
            int BookingID = 0, MileageAdded = 0, ConsumedDays = 0;
            DateTime ActualReturnDate = DateTime.Now;
            decimal FinalTotal = 0;

            bool IsFound = clsReturnData.GetReturnInfoByReturnID(ReturnID, ref BookingID, ref ActualReturnDate, ref MileageAdded, ref ConsumedDays, ref FinalTotal);
            if (IsFound)
                return new clsReturn(ReturnID, BookingID, ActualReturnDate, MileageAdded, ConsumedDays, FinalTotal);

            else 
                return null;

        }

        public static bool DeletReturn(int ID)
        {
            return clsReturnData.DeleteReturn(ID);
        }
        
        public static bool IsReturnExist(int ID)
        {
            return clsReturnData.IsReturnExist(ID);
        }

    }
}
