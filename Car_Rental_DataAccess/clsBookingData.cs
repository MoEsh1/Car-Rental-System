using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class clsBookingData
    {
        /*******************************************************************************************/
        public static bool GetBookingInfoByBookingID(int BookingID, ref int CustomerID, ref int VehicleID,
            ref DateTime StartDate, ref DateTime EndDate, ref decimal InitialTotal)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetBookingInfoByBookingID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BookingID", BookingID);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            CustomerID = (int)reader["CustomerID"];
                            VehicleID = (int)reader["VehicleID"];
                            StartDate = (DateTime)reader["StartDate"];
                            EndDate = (DateTime)reader["EndDate"];
                            InitialTotal = (decimal)reader["InitialTotal"];
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
        public static int AddNewBooking(int CustomerID, int VehicleID, DateTime StartDate,
            DateTime EndDate, decimal InitialTotal)
        {
            int BookingID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_AddNewBooking", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CustomerID", CustomerID);
                    command.Parameters.AddWithValue("@VehicleID", VehicleID);
                    command.Parameters.AddWithValue("@StartDate", StartDate);
                    command.Parameters.AddWithValue("@EndDate", EndDate);
                    command.Parameters.AddWithValue("@InitialTotal", InitialTotal);

                    SqlParameter outputIdParam = new SqlParameter("@NewBookingID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);

                    connection.Open();
                    command.ExecuteNonQuery();

                    BookingID = (int)command.Parameters["@NewBookingID"].Value;
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }

            return BookingID;
        }
        /*******************************************************************************************/
        public static bool UpdateBooking(int BookingID, int CustomerID, int VehicleID,
            DateTime StartDate, DateTime EndDate, decimal InitialTotal)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_UpdateBooking", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BookingID", BookingID);
                    command.Parameters.AddWithValue("@CustomerID", CustomerID);
                    command.Parameters.AddWithValue("@VehicleID", VehicleID);
                    command.Parameters.AddWithValue("@StartDate", StartDate);
                    command.Parameters.AddWithValue("@EndDate", EndDate);
                    command.Parameters.AddWithValue("@InitialTotal", InitialTotal);

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
        public static bool DeleteBooking(int BookingID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_DeleteBooking", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BookingID", BookingID);

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
        public static DataTable GetAllBookings()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetAllBookings", connection))
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
        public static bool IsBookingExist(int BookingID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_IsBookingExist", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BookingID", BookingID);

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