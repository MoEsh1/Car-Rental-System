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
        /*******************************************************************************************/
        public static bool GetVehicleInfoByID(int VehicleID, ref string PlateNo, ref string Make, ref string Model,
                                     ref int Year, ref int Mileage, ref Decimal DailyRate, ref bool IsAvailable)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetVehicleInfoByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
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
        /*******************************************************************************************/
        public static bool GetVehicleInfoByPlateNo(string PlateNo, ref int VehicleID, ref string Make, ref string Model,
                             ref int Year, ref int Mileage, ref Decimal DailyRate, ref bool IsAvailable)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetVehicleInfoByPlateNo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
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
        /*******************************************************************************************/
        public static int AddNewVehicle(string PlateNo, string Make, string Model, int Year,
                                        int Mileage, Decimal DailyRate, bool IsAvailable)
        {
            int VehicleID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_AddNewVehicle", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PlateNo", PlateNo);
                    command.Parameters.AddWithValue("@Make", Make);
                    command.Parameters.AddWithValue("@Model", Model);
                    command.Parameters.AddWithValue("@Year", Year);
                    command.Parameters.AddWithValue("@Mileage", Mileage);
                    command.Parameters.AddWithValue("@DailyRate", DailyRate);
                    command.Parameters.AddWithValue("@IsAvailable", IsAvailable);

                    SqlParameter outputIdParam = new SqlParameter("@NewVehicleID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);

                    connection.Open();
                    command.ExecuteNonQuery();

                    VehicleID = (int)command.Parameters["@NewVehicleID"].Value;
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }

            return VehicleID;
        }
        /*******************************************************************************************/
        public static bool UpdateVehicle(int VehicleID, string PlateNo, string Make, string Model, int Year,
                                        int Mileage, Decimal DailyRate, bool IsAvailable)
        {
            int rowAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_UpdateVehicle", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

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
        /*******************************************************************************************/
        public static bool DeleteVehicle(int VehicleID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_DeleteVehicle", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
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
        /*******************************************************************************************/
        public static DataTable GetAllVehicles()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetAllVehicles", connection))
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
        public static DataTable GetAllAvailableVehicles()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetAllAvailableVehicles", connection))
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
        public static bool IsVehicleExist(int VehicleID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_IsVehicleExist", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@VehicleID", VehicleID);

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
        public static bool IsVehicleExist(string PlateNo)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_IsVehicleExistByPlateNo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PlateNo", PlateNo);

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
        public static bool IsVehicleAvailable(int VehicleID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_IsVehicleAvailable", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@VehicleID", VehicleID);

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