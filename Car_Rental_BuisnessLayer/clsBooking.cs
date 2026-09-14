using System;
using System.Data;
using DataAccess;

namespace BuisnessLayer
{
    public class clsBooking
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int BookingID { get; set; }
        public int CustomerID { get; set; }
        public int VehicleID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal InitialTotal { get; set; }

        public clsBooking()
        {
            this.BookingID = -1;
            this.CustomerID = -1;
            this.VehicleID = -1;
            this.StartDate = DateTime.Now;
            this.EndDate = DateTime.Now.AddDays(1);
            this.InitialTotal = 0;
            Mode = enMode.AddNew;
        }

        public clsBooking(int BookingID, int CustomerID, int VehicleID, DateTime StartDate, DateTime EndDate, decimal InitialTotal)
        {
            this.BookingID = BookingID;
            this.CustomerID = CustomerID;
            this.VehicleID = VehicleID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.InitialTotal = InitialTotal;
            Mode = enMode.Update;
        }

        private bool _AddNewBooking()
        {
            // 1. Call DAL to insert booking
            this.BookingID = clsBookingData.AddNewBooking(CustomerID, VehicleID, StartDate, EndDate, InitialTotal);

            if (this.BookingID != -1)
            {
                // 2. STRICT BUSINESS RULE: Mark vehicle as unavailable immediately
                clsVehicle RentedVehicle = clsVehicle.FindByVehicleID(this.VehicleID);
                if (RentedVehicle != null)
                {
                    RentedVehicle.IsAvailable = false;
                    RentedVehicle.Save(); // Updates vehicle in DB
                }
                return true;
            }
            return false;
        }

        private bool _UpdateBooking()
        {
            return clsBookingData.UpdateBooking(BookingID, CustomerID, VehicleID, StartDate, EndDate, InitialTotal);
        }

        public static clsBooking FindByBookingID(int BookingID)
        {
            int CustomerID = -1, VehicleID = -1;
            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;
            decimal InitialTotal = 0;

            bool IsFound = clsBookingData.GetBookingInfoByBookingID(BookingID, ref CustomerID, ref VehicleID,
                                                                    ref StartDate, ref EndDate, ref InitialTotal);
            if (IsFound)
                return new clsBooking(BookingID, CustomerID, VehicleID, StartDate, EndDate, InitialTotal);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewBooking())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateBooking();
            }

            return false;
        }

        public static bool DeleteBooking(int BookingID)
        {
            return clsBookingData.DeleteBooking(BookingID);
        }

        public static bool IsBookingExist(int BookingID)
        {
            return clsBookingData.IsBookingExist(BookingID);
        }

        public static DataTable GetAllBookings()
        {
            return clsBookingData.GetAllBookings();
        }

        public static DataTable GetAllAvailableVehicles()
        {
            return clsVehicle.GetAllAvailableVehicles();
        }
    }
}