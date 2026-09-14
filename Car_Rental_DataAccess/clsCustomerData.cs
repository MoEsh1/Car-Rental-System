using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class clsCustomerData
    {
        public static bool GetCustomerInfoByCustomerID(int CustomerID, ref int PersonID, ref string DriverLicenseNo)
        {
            bool isFound = false;
            string query = "SELECT PersonID, DriverLicenseNo FROM Customers WHERE CustomerID = @CustomerID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerID", CustomerID);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            PersonID = (int)reader["PersonID"];
                            DriverLicenseNo = (string)reader["DriverLicenseNo"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isFound = false;
            }

            return isFound;
        }

        public static bool GetCustomerInfoByPersonID(int PersonID, ref int CustomerID, ref string DriverLicenseNo)
        {
            bool isFound = false;
            string query = "SELECT CustomerID, DriverLicenseNo FROM Customers WHERE PersonID = @PersonID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            CustomerID = (int)reader["CustomerID"];
                            DriverLicenseNo = (string)reader["DriverLicenseNo"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isFound = false;
            }

            return isFound;
        }

        public static bool GetCustomerInfoByDriverLicenseNo(string DriverLicenseNo, ref int CustomerID, ref int PersonID)
        {
            bool isFound = false;
            string query = "SELECT CustomerID, PersonID FROM Customers WHERE DriverLicenseNo = @DriverLicenseNo;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DriverLicenseNo", DriverLicenseNo);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            CustomerID = (int)reader["CustomerID"];
                            PersonID = (int)reader["PersonID"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isFound = false;
            }

            return isFound;
        }

        public static int AddNewCustomer(int PersonID, string DriverLicenseNo)
        {
            int CustomerID = -1;

            string query = @"INSERT INTO Customers (PersonID, DriverLicenseNo)
                             VALUES (@PersonID, @DriverLicenseNo);
                             SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@DriverLicenseNo", DriverLicenseNo);

                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        CustomerID = insertedID;
                    }
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }

            return CustomerID;
        }

        public static bool UpdateCustomer(int CustomerID, int PersonID, string DriverLicenseNo)
        {
            int rowsAffected = 0;
            string query = @"Update Customers  
                             set PersonID = @PersonID,
                                 DriverLicenseNo = @DriverLicenseNo
                             where CustomerID = @CustomerID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@DriverLicenseNo", DriverLicenseNo);
                    command.Parameters.AddWithValue("@CustomerID", CustomerID);

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return (rowsAffected > 0);
        }

        public static DataTable GetAllCustomers()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT Customers.CustomerID, Customers.PersonID,
                                    FullName = People.FirstName + ' ' + People.LastName,
                                    Customers.DriverLicenseNo
                             FROM Customers INNER JOIN
                                  People ON Customers.PersonID = People.PersonID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }

            return dt;
        }

        public static bool DeleteCustomer(int CustomerID)
        {
            int rowsAffected = 0;
            string query = @"Delete Customers 
                             where CustomerID = @CustomerID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerID", CustomerID);

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }

            return (rowsAffected > 0);
        }

        public static bool IsCustomerExist(int CustomerID)
        {
            bool isFound = false;
            string query = "SELECT Found=1 FROM Customers WHERE CustomerID = @CustomerID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerID", CustomerID);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        isFound = reader.HasRows;
                    }
                }
            }
            catch (Exception ex)
            {
                isFound = false;
            }

            return isFound;
        }

        public static bool IsCustomerExist(string DriverLicenseNo)
        {
            bool isFound = false;
            string query = "SELECT Found=1 FROM Customers WHERE DriverLicenseNo = @DriverLicenseNo";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DriverLicenseNo", DriverLicenseNo);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        isFound = reader.HasRows;
                    }
                }
            }
            catch (Exception ex)
            {
                isFound = false;
            }

            return isFound;
        }

        public static bool IsCustomerExistForPersonID(int PersonID)
        {
            bool isFound = false;
            string query = "SELECT Found=1 FROM Customers WHERE PersonID = @PersonID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        isFound = reader.HasRows;
                    }
                }
            }
            catch (Exception ex)
            {
                isFound = false;
            }

            return isFound;
        }

        public static bool DoesPersonHaveCustomer(int PersonID)
        {
            return IsCustomerExistForPersonID(PersonID);
        }
    }
}