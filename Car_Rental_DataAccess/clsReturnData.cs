using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Car_Rental_DataAccess
{
    public class clsReturnData
    {
        /*******************************************************************************************/
        public static bool GetReturnInfoByReturnID(int ReturnID, ref int BookingID, ref DateTime ActualReturnDate,
                                  ref int MileageAdded, ref int ConsumedDays, ref decimal FinalTotal)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetReturnInfoByReturnID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ReturnID", ReturnID);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            BookingID = (int)reader["BookingID"];
                            ActualReturnDate = (DateTime)reader["ActualReturnDate"];
                            MileageAdded = (int)reader["MileageAdded"];
                            ConsumedDays = (int)reader["ConsumedDays"];
                            FinalTotal = (decimal)reader["FinalTotal"];
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
        public static bool GetReturnInfoByBookingID(int BookingID, ref int ReturnID, ref DateTime ActualReturnDate,
                                  ref int MileageAdded, ref int ConsumedDays, ref decimal FinalTotal)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetReturnInfoByBookingID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BookingID", BookingID);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            ReturnID = (int)reader["ReturnID"];
                            ActualReturnDate = (DateTime)reader["ActualReturnDate"];
                            MileageAdded = (int)reader["MileageAdded"];
                            ConsumedDays = (int)reader["ConsumedDays"];
                            FinalTotal = (decimal)reader["FinalTotal"];
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
        public static int AddNewReturns(int BookingID, DateTime ActualReturnDate,
                                   int MileageAdded, int ConsumedDays, decimal FinalTotal)
        {
            int ReturnID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_AddNewReturn", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BookingID", BookingID);
                    command.Parameters.AddWithValue("@ActualReturnDate", ActualReturnDate);
                    command.Parameters.AddWithValue("@MileageAdded", MileageAdded);
                    command.Parameters.AddWithValue("@ConsumedDays", ConsumedDays);
                    command.Parameters.AddWithValue("@FinalTotal", FinalTotal);

                    SqlParameter outputIdParam = new SqlParameter("@NewReturnID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);

                    connection.Open();
                    command.ExecuteNonQuery();

                    ReturnID = (int)command.Parameters["@NewReturnID"].Value;
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }

            return ReturnID;
        }
        /*******************************************************************************************/
        public static bool UpdateReturn(int ReturnID, int BookingID, DateTime ActualReturnDate,
                                   int MileageAdded, int ConsumedDays, decimal FinalTotal)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_UpdateReturn", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ReturnID", ReturnID);
                    command.Parameters.AddWithValue("@BookingID", BookingID);
                    command.Parameters.AddWithValue("@ActualReturnDate", ActualReturnDate);
                    command.Parameters.AddWithValue("@MileageAdded", MileageAdded);
                    command.Parameters.AddWithValue("@ConsumedDays", ConsumedDays);
                    command.Parameters.AddWithValue("@FinalTotal", FinalTotal);

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
        public static bool DeleteReturn(int ReturnID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_DeleteReturn", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ReturnID", ReturnID);

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
        public static DataTable GetAllReturns()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetAllReturns", connection))
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

        public static bool IsReturnExist(int ReturnID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_IsReturnExist", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ReturnID", ReturnID);

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
    }
}