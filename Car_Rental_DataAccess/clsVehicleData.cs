using DataAccess;
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
        /**************************************************************************************************************************/
        public static bool GetVehicleInfoByID(int VehicleID, ref string PlateNo, ref string Make, ref string Model,
                                     ref int Year,ref int Mileage, ref Decimal DailyRate, ref bool IsAvailable)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Vehicles WHERE VehicleID = @VehicleID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@VehicleID", VehicleID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    PlateNo = (string)reader["PlateNo"];
                    Make = (string)reader["Make"];
                    Model = (string)reader["Model"];
                    Year = (int)reader["Year"];
                    Mileage = (int)reader["Mileage"];
                    DailyRate = (Decimal)reader["DailyRate"];
                    IsAvailable = (bool)reader["IsAvailable"];
                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        /**************************************************************************************************************************/
        public static bool GetVehicleInfoByPlateNo(string PlateNo, ref int VehicleID, ref string Make, ref string Model,
                             ref int Year, ref int Mileage, ref Decimal DailyRate, ref bool IsAvailable)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Vehicles WHERE PlateNo = @PlateNo";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PlateNo", PlateNo);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    VehicleID = (int)reader["VehicleID"];
                    Make = (string)reader["Make"];
                    Model = (string)reader["Model"];
                    Year = (int)reader["Year"];
                    Mileage = (int)reader["Mileage"];
                    DailyRate = (Decimal)reader["DailyRate"];
                    IsAvailable = (bool)reader["IsAvailable"];
                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;

        }
        /**************************************************************************************************************************/
        public static int AddNewVehicle(string PlateNo, string Make, string Model, int Year, 
                                        int Mileage, Decimal DailyRate, bool IsAvailable)
        {
            int VehicleID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO Vehicles (PlateNo, Make, Model, Year,
                                                 Mileage, DailyRate, IsAvailable)
                             VALUES (@PlateNo, @Make, @Model, @Year,
                                                 @Mileage, @DailyRate, @IsAvailable);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PlateNo", PlateNo);
            command.Parameters.AddWithValue("@Make", Make);
            command.Parameters.AddWithValue("@Model", Model);
            command.Parameters.AddWithValue("@Year", Year);
            command.Parameters.AddWithValue("@Mileage", Mileage);
            command.Parameters.AddWithValue("@DailyRate", DailyRate);
            command.Parameters.AddWithValue("@IsAvailable", IsAvailable);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    VehicleID = InsertedID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return VehicleID;
        }

        /**************************************************************************************************************************/
        public static bool UpdateVehicle(int VehicleID, string PlateNo, string Make, string Model, int Year,
                                        int Mileage, Decimal DailyRate, bool IsAvailable)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"UPDATE Vehicles
                             SET PlateNo = @PlateNo,
                                 Make = @Make,
                                 Model = @Model,
                                 Year = @Year,
                                 Mileage = @Mileage,
                                 DailyRate = @DailyRate,
                                 IsAvailable = @IsAvailable
                            WHERE VehicleID = @VehicleID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@VehicleID", VehicleID);
            command.Parameters.AddWithValue("@PlateNo", PlateNo);
            command.Parameters.AddWithValue("@Make", Make);
            command.Parameters.AddWithValue("@Model", Model);
            command.Parameters.AddWithValue("@Year", Year);
            command.Parameters.AddWithValue("@Mileage", Mileage);
            command.Parameters.AddWithValue("@DailyRate", DailyRate);
            command.Parameters.AddWithValue("@IsAvailable", IsAvailable);
            try
            {
                connection.Open();
                rowAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
            return rowAffected > 0;
        }

        /**************************************************************************************************************************/
        public static bool DeleteVehicle(int VehicleID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Delete Vehicles 
                                Where VehicleID = @VehicleID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@VehicleID", VehicleID);
            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }
        /**************************************************************************************************************************/
        public static DataTable GetAllVehicles()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * From Vehicles";

            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();
            }

            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }
        /**************************************************************************************************************************/
        public static DataTable GetAllAvailableVehicles()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * From Vehicles Where IsAvailable = 1";

            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();
            }

            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }
        /**************************************************************************************************************************/
        public static bool IsVehicleExist(int VehicleID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found=1 FROM Vehicles WHERE VehicleID = @VehicleID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@VehicleID", VehicleID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        
        /**************************************************************************************************************************/
        public static bool IsVehicleExist(string PlateNo)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found=1 FROM Vehicles WHERE PlateNo = @PlateNo";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PlateNo", PlateNo);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        /**************************************************************************************************************************/
        public static bool IsVehicleAvailable(int VehicleID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT Found=1 FROM Vehicles WHERE VehicleID = @VehicleID  
                                                                        AND IsAvailable = 1";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@VehicleID", VehicleID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        /**************************************************************************************************************************/
        //does Customer Has vehicle reserved implement it if needed that customer has only one vehcile to reserve at time
        //or maybe implement it at reservation section


    }
}
