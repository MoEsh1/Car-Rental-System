using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental_DataAccess
{
    public class clsReturnData
    {
        public static bool GetReturnInfoByReturnID(int ReturnID, ref int BookingID, ref DateTime ActualReturnDate,
                                  ref int MileageAdded, ref int ConsumedDays, ref decimal FinalTotal)
        {
            bool isFound = false;
            string query = @"SELECT BookingID, ActualReturnDate, MileageAdded, ConsumedDays, FinalTotal 
                             FROM Returns WHERE ReturnID = @ReturnID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
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

        public static bool GetReturnInfoByBookingID(int BookingID, ref int ReturnID, ref DateTime ActualReturnDate,
                                  ref int MileageAdded, ref int ConsumedDays, ref decimal FinalTotal)
        {
            bool isFound = false;
            string query = @"SELECT ReturnID, ActualReturnDate, MileageAdded, ConsumedDays, FinalTotal 
                             FROM Returns WHERE BookingID = @BookingID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
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

        public static int AddNewReturns(int BookingID, DateTime ActualReturnDate,
                                   int MileageAdded, int ConsumedDays, decimal FinalTotal)
        {
            int ReturnID = -1; 

            string query = @"INSERT INTO Returns (BookingID, ActualReturnDate, MileageAdded, ConsumedDays, FinalTotal)
                             VALUES (@BookingID, @ActualReturnDate, @MileageAdded, @ConsumedDays, @FinalTotal);
                             SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BookingID", BookingID);
                    command.Parameters.AddWithValue("@ActualReturnDate", ActualReturnDate);
                    command.Parameters.AddWithValue("@MileageAdded", MileageAdded);
                    command.Parameters.AddWithValue("@ConsumedDays", ConsumedDays);
                    command.Parameters.AddWithValue("@FinalTotal", FinalTotal);

                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        ReturnID = insertedID;
                    }
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }

            return ReturnID;
        }

        public static bool UpdateReturn(int ReturnID, int BookingID, DateTime ActualReturnDate,
                                   int MileageAdded, int ConsumedDays, decimal FinalTotal)
        {
            int rowsAffected = 0;

            string query = @"Update Returns 
                             set BookingID = @BookingID,
                                 ActualReturnDate = @ActualReturnDate,
                                 MileageAdded = @MileageAdded,
                                 ConsumedDays = @ConsumedDays,
                                 FinalTotal = @FinalTotal
                             where ReturnID = @ReturnID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
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

        public static bool DeleteReturn(int ReturnID)
        {
            int rowsAffected = 0;
            string query = @"Delete From Returns where ReturnID = @ReturnID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
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

        public static DataTable GetAllReturns()
        {
            DataTable dt = new DataTable();
            string query = @"SELECT * From Returns_View";

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

        public static bool IsReturnExist(int ReturnID)
        {
            bool isFound = false;
            string query = "SELECT Found=1 FROM Returns WHERE ReturnID = @ReturnID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReturnID", ReturnID);

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
    }
}