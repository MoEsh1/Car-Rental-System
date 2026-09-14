using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class clsVehicleData
    {
        public static bool GetVehicleInfoByID(int VehicleID, ref string PlateNo, ref string Make, ref string Model,
                                     ref int Year, ref int Mileage, ref Decimal DailyRate, ref bool IsAvailable)
        {
            bool isFound = false;

            string query = @"SELECT PlateNo, Make, Model, Year, Mileage, DailyRate, IsAvailable 
                             FROM Vehicles WHERE VehicleID = @VehicleID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VehicleID", VehicleID);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            PlateNo = (string)reader["PlateNo"];
                            Make = (string)reader["Make"];
                            Model = (string)reader["Model"];
                            Year = (int)reader["Year"];
                            Mileage = (int)reader["Mileage"];
                            DailyRate = Convert.ToDecimal(reader["DailyRate"]);
                            IsAvailable = (bool)reader["IsAvailable"];
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

        public static bool GetVehicleInfoByPlateNo(string PlateNo, ref int VehicleID, ref string Make, ref string Model,
                             ref int Year, ref int Mileage, ref Decimal DailyRate, ref bool IsAvailable)
        {
            bool isFound = false;
            string query = @"SELECT VehicleID, Make, Model, Year, Mileage, DailyRate, IsAvailable 
                             FROM Vehicles WHERE PlateNo = @PlateNo";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PlateNo", PlateNo);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            VehicleID = (int)reader["VehicleID"];
                            Make = (string)reader["Make"];
                            Model = (string)reader["Model"];
                            Year = (int)reader["Year"];
                            Mileage = (int)reader["Mileage"];
                            DailyRate = Convert.ToDecimal(reader["DailyRate"]);
                            IsAvailable = (bool)reader["IsAvailable"];
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

        public static int AddNewVehicle(string PlateNo, string Make, string Model, int Year,
                                        int Mileage, Decimal DailyRate, bool IsAvailable)
        {
            int VehicleID = -1;

            string query = @"INSERT INTO Vehicles (PlateNo, Make, Model, Year,
                                                 Mileage, DailyRate, IsAvailable)
                             VALUES (@PlateNo, @Make, @Model, @Year,
                                     @Mileage, @DailyRate, @IsAvailable);
                             SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PlateNo", PlateNo);
                    command.Parameters.AddWithValue("@Make", Make);
                    command.Parameters.AddWithValue("@Model", Model);
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@Mileage", Mileage);
                    command.Parameters.AddWithValue("@DailyRate", DailyRate);
                    command.Parameters.AddWithValue("@IsAvailable", IsAvailable);

                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                    {
                        VehicleID = InsertedID;
                    }
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }

            return VehicleID;
        }

        public static bool UpdateVehicle(int VehicleID, string PlateNo, string Make, string Model, int Year,
                                        int Mileage, Decimal DailyRate, bool IsAvailable)
        {
            int rowAffected = 0;

            string query = @"UPDATE Vehicles
                             SET PlateNo = @PlateNo,
                                 Make = @Make,
                                 Model = @Model,
                                 Year = @Year,
                                 Mileage = @Mileage,
                                 DailyRate = @DailyRate,
                                 IsAvailable = @IsAvailable
                             WHERE VehicleID = @VehicleID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VehicleID", VehicleID);
                    command.Parameters.AddWithValue("@PlateNo", PlateNo);
                    command.Parameters.AddWithValue("@Make", Make);
                    command.Parameters.AddWithValue("@Model", Model);
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@Mileage", Mileage);
                    command.Parameters.AddWithValue("@DailyRate", DailyRate);
                    command.Parameters.AddWithValue("@IsAvailable", IsAvailable);

                    connection.Open();
                    rowAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return rowAffected > 0;
        }

        public static bool DeleteVehicle(int VehicleID)
        {
            int rowsAffected = 0;
            string query = @"DELETE FROM Vehicles 
                             WHERE VehicleID = @VehicleID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VehicleID", VehicleID);

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

        public static DataTable GetAllVehicles()
        {
            DataTable dt = new DataTable();
            string query = @"SELECT * FROM Vehicles";

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

        public static DataTable GetAllAvailableVehicles()
        {
            DataTable dt = new DataTable();
            string query = @"SELECT * FROM Vehicles WHERE IsAvailable = 1";

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

        public static bool IsVehicleExist(int VehicleID)
        {
            bool isFound = false;
            string query = "SELECT Found=1 FROM Vehicles WHERE VehicleID = @VehicleID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VehicleID", VehicleID);

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

        public static bool IsVehicleExist(string PlateNo)
        {
            bool isFound = false;
            string query = "SELECT Found=1 FROM Vehicles WHERE PlateNo = @PlateNo";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PlateNo", PlateNo);

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

        public static bool IsVehicleAvailable(int VehicleID)
        {
            bool isFound = false;
            string query = @"SELECT Found=1 FROM Vehicles 
                             WHERE VehicleID = @VehicleID AND IsAvailable = 1";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VehicleID", VehicleID);

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