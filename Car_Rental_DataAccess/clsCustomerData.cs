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
        /*******************************************************************************************/
        public static bool GetCustomerInfoByCustomerID(int CustomerID, ref int PersonID, ref string DriverLicenseNo)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetCustomerInfoByCustomerID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
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
        /*******************************************************************************************/
        public static bool GetCustomerInfoByPersonID(int PersonID, ref int CustomerID, ref string DriverLicenseNo)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetCustomerInfoByPersonID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
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
        /*******************************************************************************************/
        public static bool GetCustomerInfoByDriverLicenseNo(string DriverLicenseNo, ref int CustomerID, ref int PersonID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetCustomerInfoByDriverLicenseNo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
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
        /*******************************************************************************************/
        public static int AddNewCustomer(int PersonID, string DriverLicenseNo)
        {
            int CustomerID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_AddNewCustomer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@DriverLicenseNo", DriverLicenseNo);

                    SqlParameter outputIdParam = new SqlParameter("@NewCustomerID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);

                    connection.Open();
                    command.ExecuteNonQuery();

                    CustomerID = (int)command.Parameters["@NewCustomerID"].Value;
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }

            return CustomerID;
        }
        /*******************************************************************************************/
        public static bool UpdateCustomer(int CustomerID, int PersonID, string DriverLicenseNo)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_UpdateCustomer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
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
        /*******************************************************************************************/
        public static DataTable GetAllCustomers()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetAllCustomers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
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
        /*******************************************************************************************/
        public static bool DeleteCustomer(int CustomerID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_DeleteCustomer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
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
        /*******************************************************************************************/
        public static bool IsCustomerExist(int CustomerID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_IsCustomerExistByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CustomerID", CustomerID);

                    connection.Open();

                    int result = Convert.ToInt32(command.ExecuteScalar());
                    isFound = (result == 1);
                }
            }
            catch (Exception ex)
            {
                isFound = false;
            }

            return isFound;
        }
        /*******************************************************************************************/
        public static bool IsCustomerExist(string DriverLicenseNo)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_IsCustomerExistByDriverLicenseNo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DriverLicenseNo", DriverLicenseNo);

                    connection.Open();

                    int result = Convert.ToInt32(command.ExecuteScalar());
                    isFound = (result == 1);
                }
            }
            catch (Exception ex)
            {
                isFound = false;
            }

            return isFound;
        }
        /*******************************************************************************************/
        public static bool IsCustomerExistForPersonID(int PersonID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_IsCustomerExistByPersonID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    connection.Open();

                    int result = Convert.ToInt32(command.ExecuteScalar());
                    isFound = (result == 1);
                }
            }
            catch (Exception ex)
            {
                isFound = false;
            }

            return isFound;
        }
        /*******************************************************************************************/
        public static bool DoesPersonHaveCustomer(int PersonID)
        {
            return IsCustomerExistForPersonID(PersonID);
        }
        /*******************************************************************************************/
    }
}