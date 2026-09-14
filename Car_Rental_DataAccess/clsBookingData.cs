using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class clsBookingData
    {
        public static bool GetBookingInfoByBookingID(int BookingID, ref int CustomerID, ref int VehicleID,
            ref DateTime StartDate, ref DateTime EndDate, ref decimal InitialTotal)
        {
            bool isFound = false;
            string query = @"SELECT CustomerID, VehicleID, StartDate, EndDate, InitialTotal 
                             FROM Bookings WHERE BookingID = @BookingID";

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

        public static int AddNewBooking(int CustomerID, int VehicleID, DateTime StartDate,
            DateTime EndDate, decimal InitialTotal)
        {
            int BookingID = -1;

            string query = @"INSERT INTO Bookings (CustomerID, VehicleID, StartDate, EndDate, InitialTotal)
                             VALUES (@CustomerID, @VehicleID, @StartDate, @EndDate, @InitialTotal);
                             SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerID", CustomerID);
                    command.Parameters.AddWithValue("@VehicleID", VehicleID);
                    command.Parameters.AddWithValue("@StartDate", StartDate);
                    command.Parameters.AddWithValue("@EndDate", EndDate);
                    command.Parameters.AddWithValue("@InitialTotal", InitialTotal);

                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        BookingID = insertedID;
                    }
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }

            return BookingID;
        }

        public static bool UpdateBooking(int BookingID, int CustomerID, int VehicleID,
            DateTime StartDate, DateTime EndDate, decimal InitialTotal)
        {
            int rowsAffected = 0;

            string query = @"Update Bookings 
                             set CustomerID = @CustomerID,
                                 VehicleID = @VehicleID,
                                 StartDate = @StartDate,
                                 EndDate = @EndDate,
                                 InitialTotal = @InitialTotal
                             where BookingID = @BookingID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
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

        public static bool DeleteBooking(int BookingID)
        {
            int rowsAffected = 0;
            string query = @"Delete Bookings where BookingID = @BookingID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
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

        public static DataTable GetAllBookings()
        {
            DataTable dt = new DataTable();
            string query = @"SELECT * From Bookings_View";

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

        public static bool IsBookingExist(int BookingID)
        {
            bool isFound = false;
            string query = "SELECT Found=1 FROM Bookings WHERE BookingID = @BookingID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BookingID", BookingID);
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