using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer
{
    public class clsPerson
    {
        public enum enMode { AddNew = 0, Update  = 1 };
        public enMode Mode = enMode.AddNew;
        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public DateTime DateOfBirth { get; set; }
        public short Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int CountryID { get; set; }
        public clsCountryData CountryInfo { get; set; }

        /****************************************************************************************************************************************/
        public clsPerson()
        {
            PersonID = -1;
            NationalNo = "";
            FirstName = "";
            LastName = "";
            DateOfBirth = DateTime.Now;
            Address = "";
            Gender = 0;
            Phone = "";
            Email = "";
            CountryID = -1;
            Mode = enMode.AddNew;
        }
        /****************************************************************************************************************************************/
        private clsPerson(int PersonID, string NationalNo, string FirstName, string LastName, DateTime DateOfBirth, short Gender,
                            string Phone, string Email,  string Address,int CountryID)
        {
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;

            this.Gender = Gender;
            this.Phone = Phone;
            this.Email = Email;
            this.Address = Address;
            this.CountryID = CountryID;
            this.CountryInfo = clsCountry.Find(CountryID);
            Mode = enMode.Update;
        }
        /****************************************************************************************************************************************/
        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonData.AddNewPerson(this.NationalNo, this.FirstName, this.LastName, this.DateOfBirth, this.Gender, 
                                                       this.Phone, this.Email, this.Address, this.CountryID);

            return (this.PersonID != -1);
        }
        /****************************************************************************************************************************************/
        private bool _UpdatePerson()
        {

            return clsPersonData.UpdatePerson(this.PersonID, this.NationalNo, this.FirstName, this.LastName,
                                        this.DateOfBirth, this.Gender, this.Phone, this.Email, this.Address, this.CountryID);
        }
        /****************************************************************************************************************************************/
        public static clsPerson Find(int PersonID)
        {

            string FirstName = "",LastName = "", NationalNo = "", Email = "", Phone = "", Address = "";
            DateTime DateOfBirth = DateTime.Now;
            int CountryID = -1;
            short Gender = 0;

            bool IsFound = clsPersonData.GetPersonInfoByID(PersonID, ref NationalNo, ref FirstName, ref LastName,
                                             ref DateOfBirth, ref Gender, ref Phone, ref Email,
                                             ref Address, ref CountryID);

            if (IsFound)
                //we return new object of that person with the right data
                return new clsPerson(PersonID, NationalNo, FirstName, LastName, DateOfBirth, Gender,
                                        Phone, Email, Address, CountryID);
            else
                return null;
        }
        /****************************************************************************************************************************************/
        public static clsPerson Find(string NationalNo)
        {
            string FirstName = "", LastName = "", Email = "", Phone = "", Address = "";
            DateTime DateOfBirth = DateTime.Now;
            int PersonID = -1, CountryID = -1;
            short Gender = 0;

            bool IsFound = clsPersonData.GetPersonInfoByNationalNo(NationalNo, ref PersonID, ref FirstName, ref LastName,
                                       ref DateOfBirth, ref Gender, ref Phone, ref Email,
                                       ref Address, ref CountryID);

            if (IsFound)

                return new clsPerson(PersonID, NationalNo, FirstName, LastName, DateOfBirth, Gender,
                                        Phone, Email, Address, CountryID);
            else
                return null;
        }

        /****************************************************************************************************************************************/
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdatePerson();
            }

            return false;
        }
        /****************************************************************************************************************************************/
        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();
        }
        /****************************************************************************************************************************************/
        public static bool DeletePerson(int ID)
        {
            return clsPersonData.DeletePerson(ID);
        }
        /****************************************************************************************************************************************/
        public static bool isPersonExist(int ID)
        {
            return clsPersonData.IsPersonExist(ID);
        }
        /****************************************************************************************************************************************/
        public static bool isPersonExist(string NationlNo)
        {
            return clsPersonData.IsPersonExist(NationlNo);
        }
        /****************************************************************************************************************************************/
    }
}
