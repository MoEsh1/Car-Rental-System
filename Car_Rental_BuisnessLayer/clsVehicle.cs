using BuisnessLayer;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer
{
    public class clsVehicle
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int VehicleID { set; get; }
        public string PlateNo { get; set; }
        public string Make { set; get; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public decimal DailyRate { get; set; }
        public bool IsAvailable { get; set; }

        /****************************************************************************************************************************************/
        public clsVehicle()
        {
            this.VehicleID = -1;
            this.PlateNo = "";
            this.Make = "";
            this.Model = "";
            this.Year = -1;
            this.Mileage = -1;
            this.DailyRate = -1;
            this.IsAvailable = false;
            Mode = enMode.AddNew;
        }

        /****************************************************************************************************************************************/

        private clsVehicle(int VehicleID, string PlateNo, string Make, string Model,
                           int Year, int Mileage, decimal DailyRate, bool IsAvailable)
        {
            this.VehicleID=VehicleID;
            this.PlateNo=PlateNo;
            this.Make=Make;
            this.Model=Model;
            this.Year=Year;
            this.Mileage=Mileage;
            this.DailyRate=DailyRate;
            this.IsAvailable = IsAvailable;
            Mode = enMode.Update;
        }
        /****************************************************************************************************************************************/
        private bool _AddNewVehicle()
        {
            this.VehicleID = clsVehicleData.AddNewVehicle( PlateNo, Make, Model, Year, Mileage, DailyRate, IsAvailable);
            return (this.VehicleID != -1);
        }
        /****************************************************************************************************************************************/
        private bool _UpdateVehicle()
        {
            return clsVehicleData.UpdateVehicle(VehicleID, PlateNo, Make, Model, Year, Mileage, DailyRate,IsAvailable);
        }

        /****************************************************************************************************************************************/
        public static clsVehicle FindByVehicleID(int VehicleID)
        {
            string PlateNo = "";
            string Make = "";
            string Model = "";
            int Year = -1;
            int Mileage = -1;
            decimal DailyRate = -1;
            bool IsAvailable = false;

            bool IsFound = clsVehicleData.GetVehicleInfoByID(VehicleID, ref PlateNo, ref Make, ref Model, ref Year,
                                                             ref Mileage, ref DailyRate, ref IsAvailable);
            if (IsFound)
                return new clsVehicle(VehicleID, PlateNo, Make, Model, Year, Mileage, DailyRate, IsAvailable);
            else
                return null;
        }
        /****************************************************************************************************************************************/
        public static clsVehicle FindByPlateNo(string PlateNo)
        {
            int VehicleID = -1;
            string Make = "";
            string Model = "";
            int Year = -1;
            int Mileage = -1;
            decimal DailyRate = -1;
            bool IsAvailable = false;

            bool IsFound = clsVehicleData.GetVehicleInfoByPlateNo(PlateNo, ref VehicleID, ref Make, ref Model, ref Year,
                                                             ref Mileage, ref DailyRate, ref IsAvailable);
            if (IsFound)
                return new clsVehicle(VehicleID, PlateNo, Make, Model, Year, Mileage, DailyRate, IsAvailable);
            else
                return null;
        }

        /****************************************************************************************************************************************/
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewVehicle())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateVehicle();
            }

            return false;
        }
        /****************************************************************************************************************************************/
        public static DataTable GetAllVehicles()
        {
            return clsVehicleData.GetAllVehicles();
        }

        /****************************************************************************************************************************************/
        public static DataTable GetAllAvailableVehicles()
        {
            return clsVehicleData.GetAllAvailableVehicles();
        }

        /****************************************************************************************************************************************/
        public static bool DeletVehicle(int VehicleID)
        {
            return clsVehicleData.DeleteVehicle(VehicleID);
        }
        /****************************************************************************************************************************************/
        public static bool IsVehicleExist(int VehicleID)
        {
            return clsVehicleData.IsVehicleExist(VehicleID);
        }
        /****************************************************************************************************************************************/
        public static bool IsVehicleExist(string PlateNo)
        {
            return clsVehicleData.IsVehicleExist(PlateNo);
        }
        /****************************************************************************************************************************************/




    }
}
