using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer
{
    public class clsCustomer
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int CustomerID { set; get; }
        public int PersonID { set; get; }
        public clsPerson PersonInfo;
        public string DriverLicenseNo { set; get; }

        /****************************************************************************************************************************************/
        public clsCustomer()
        {
            this.CustomerID = -1;
            this.DriverLicenseNo = "";
            Mode = enMode.AddNew;
        }

        /****************************************************************************************************************************************/

        private clsCustomer(int CustomerID, int PersonID, string DriverLicenseNo)
        {
            this.CustomerID = CustomerID;
            this.PersonID = PersonID;
            this.PersonInfo = clsPerson.Find(PersonID);
            this.DriverLicenseNo = DriverLicenseNo;
            Mode = enMode.Update;
        }
        /****************************************************************************************************************************************/
        private bool _AddNewCustomer()
        {
            this.CustomerID = clsCustomerData.AddNewCustomer(this.PersonID, this.DriverLicenseNo);
            return (this.CustomerID != -1);
        }
        /****************************************************************************************************************************************/
        private bool _UpdateCustomer()
        {
            return clsCustomerData.UpdateCustomer(this.CustomerID, this.PersonID, this.DriverLicenseNo);
        }

        /****************************************************************************************************************************************/
        public static clsCustomer FindByCustomerID(int CustomerID)
        {
            int PersonID = -1;
            string DriverLicenseNo = "";
            bool IsFound = clsCustomerData.GetCustomerInfoByCustomerID(CustomerID, ref PersonID, ref DriverLicenseNo);
            if (IsFound)
                return new clsCustomer(CustomerID, PersonID, DriverLicenseNo);
            else
                return null;
        }
        /****************************************************************************************************************************************/
        public static clsCustomer FindByPersonID(int PersonID)
        {
            int CustomerID = -1;
            string DriverLicenseNo = "";
            bool IsFound = clsCustomerData.GetCustomerInfoByPersonID(PersonID, ref CustomerID, ref DriverLicenseNo);
            if (IsFound)
                return new clsCustomer(CustomerID, PersonID, DriverLicenseNo);
            else
                return null;
        }
        /****************************************************************************************************************************************/
        public static clsCustomer FindByDriverLicenseNo(string DriverLicenseNo)
        {
            int CustomerID = -1;
            int PersonID = -1;

            bool IsFound = clsCustomerData.GetCustomerInfoByDriverLicenseNo
                                (DriverLicenseNo, ref CustomerID, ref PersonID);

            if (IsFound)
                return new clsCustomer(CustomerID, PersonID, DriverLicenseNo);
            else
                return null;
        }
        /****************************************************************************************************************************************/
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCustomer())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateCustomer();
            }

            return false;
        }
        /****************************************************************************************************************************************/
        public static DataTable GetAllCustomers()
        {
            return clsCustomerData.GetAllCustomers();
        }
        /****************************************************************************************************************************************/
        public static bool DeleteCustomer(int CustomerID)
        {
            return clsCustomerData.DeleteCustomer(CustomerID);
        }
        /****************************************************************************************************************************************/
        public static bool isCustomerExist(int CustomerID)
        {
            return clsCustomerData.IsCustomerExist(CustomerID);
        }
        /****************************************************************************************************************************************/
        public static bool isCustomerExist(string DriverLicenseNo)
        {
            return clsCustomerData.IsCustomerExist(DriverLicenseNo);
        }
        /****************************************************************************************************************************************/
        public static bool isCustomerExistForPersonID(int PersonID)
        {
            return clsCustomerData.IsCustomerExistForPersonID(PersonID);
        }
        /****************************************************************************************************************************************/

    }
}
