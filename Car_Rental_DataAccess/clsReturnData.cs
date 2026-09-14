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

    /*
     * 
       ReturnID
       BookingID
       ActualReturnDate
       MileageAdded
       ConsumedDays
       FinalTotal
     
     */
    public class clsReturnData
    {
        /***********************************************************************************************************/
        public static bool GetReturnInfoByReturnID(int ReturnID, ref int BookingID, ref DateTime ActualReturnDate,
                                  ref int MileageAdded, ref int ConsumedDays, ref decimal FinalTotal)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Returns WHERE ReturnID = @ReturnID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ReturnID", ReturnID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    BookingID = (int)reader["BookingID"];
                    ActualReturnDate = (DateTime)reader["ActualReturnDate"];
                    MileageAdded = (int)reader["MileageAdded"];
                    ConsumedDays = (int)reader["ConsumedDays"];
                    FinalTotal = (decimal)reader["FinalTotal"];
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

        /***********************************************************************************************************/
        public static bool GetReturnInfoByBookingID(int BookingID, ref int ReturnID, ref DateTime ActualReturnDate,
                                  ref int MileageAdded, ref int ConsumedDays, ref decimal FinalTotal)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Returns WHERE BookingID = @BookingID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BookingID", BookingID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    ReturnID = (int)reader["ReturnID"];
                    ActualReturnDate = (DateTime)reader["ActualReturnDate"];
                    MileageAdded = (int)reader["MileageAdded"];
                    ConsumedDays = (int)reader["ConsumedDays"];
                    FinalTotal = (decimal)reader["FinalTotal"];
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
        /***********************************************************************************************************/
        public static int AddNewReturns(int BookingID,  DateTime ActualReturnDate,
                                   int MileageAdded,  int ConsumedDays,  decimal FinalTotal)

        {
            int RetrunID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO Returns (BookingID, ActualReturnDate, MileageAdded, ConsumedDays, FinalTotal)
                             VALUES (@BookingID, @ActualReturnDate, @MileageAdded, @ConsumedDays, @FinalTotal);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BookingID", BookingID);
            command.Parameters.AddWithValue("@ActualReturnDate", ActualReturnDate);
            command.Parameters.AddWithValue("@MileageAdded", MileageAdded);
            command.Parameters.AddWithValue("@ConsumedDays", ConsumedDays);
            command.Parameters.AddWithValue("@FinalTotal", FinalTotal);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    RetrunID = insertedID;
                }
            }

            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }

            return RetrunID;
        }
        /***********************************************************************************************************/
        public static bool UpdateReturn(int ReturnID, int BookingID, DateTime ActualReturnDate,
                                   int MileageAdded, int ConsumedDays, decimal FinalTotal)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update Returns 
                            set BookingID = @BookingID,
                                ActualReturnDate = @ActualReturnDate,
                                MileageAdded = @MileageAdded,
                                ConsumedDays = @ConsumedDays,
                                FinalTotal = @FinalTotal
                                where ReturnID = @ReturnID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ReturnID", ReturnID);
            command.Parameters.AddWithValue("@BookingID", BookingID);
            command.Parameters.AddWithValue("@ActualReturnDate", ActualReturnDate);
            command.Parameters.AddWithValue("@MileageAdded", MileageAdded);
            command.Parameters.AddWithValue("@ConsumedDays", ConsumedDays);
            command.Parameters.AddWithValue("@FinalTotal", FinalTotal);
            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }
        /***********************************************************************************************************/
        // this suppose not to be used, but just incase!!!
        public static bool DeleteReturn(int ReturnID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Delete Returns 
                                where ReturnID = @ReturnID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ReturnID", ReturnID);
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

        /***********************************************************************************************************/
        public static DataTable GetAllReturns()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * From Returns_View";

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
        /***********************************************************************************************************/

        public static bool IsReturnExist(int ReturnID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found=1 FROM Returns WHERE ReturnID = @ReturnID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ReturnID", ReturnID);
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
        /***********************************************************************************************************/
    }
}
