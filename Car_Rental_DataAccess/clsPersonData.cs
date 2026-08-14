using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DataAccess
{
    internal class clsPersonData
    {
        /****************************************************************************************************************************************/
        public static bool GetPersonInfoByID(int PersonID, ref string NationalNo, ref string FirstName, ref string LastName,
                                             ref DateTime DateOfBirth, ref short Gender, ref string Phone, ref string Email, 
                                             ref string Address, ref int CountryID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM People WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    NationalNo = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = (byte)reader["Gender"];

                    //Email: allows null in database so we should handle null
                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = (string)reader["Email"];
                    }
                    else
                    {
                        Email = "";
                    }
                    //Address: allows null in database so we should handle null
                    if (reader["Address"] != DBNull.Value)
                    {
                        Address = (string)reader["Address"];
                    }
                    else
                    {
                        Address = "";
                    }

                    CountryID = (int)reader["CountryID"];
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

        /****************************************************************************************************************************************/

        public static bool GetPersonInfoByNationalNo(string NationalNo, ref int PersonID, ref string FirstName, ref string LastName,
                                       ref DateTime DateOfBirth, ref short Gender, ref string Phone, ref string Email,
                                       ref string Address, ref int CountryID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM People WHERE NationalNo = @NationalNo";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    PersonID = (int)reader["PersonID"];
                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = (byte)reader["Gender"];

                    //Email: allows null in database so we should handle null
                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = (string)reader["Email"];
                    }
                    else
                    {
                        Email = "";
                    }
                    //Address: allows null in database so we should handle null
                    if (reader["Address"] != DBNull.Value)
                    {
                        Address = (string)reader["Address"];
                    }
                    else
                    {
                        Address = "";
                    }

                    CountryID = (int)reader["CountryID"];
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

        /****************************************************************************************************************************************/

        public static int AddNewPerson(string NationalNo, string FirstName, string LastName, DateTime DateOfBirth,
                                       short Gender, string Phone, string Email, string Address, int CountryID)
        {
            int PersonID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO People (NationalNo, FirstName, LastName, DateOfBirth,
                                                 Gender, Phone, Email, Address, CountryID)
                             VALUES (@NationalNo, @FirstName, @LastName, @DateOfBirth,
                                                 @Gender, @Phone, @Email, @Address, @CountryID);
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gender", Gender);
            if (Address != "" && Address != null)
                command.Parameters.AddWithValue("@Address", Address);
            else
                command.Parameters.AddWithValue("@Address", System.DBNull.Value);
            
            if (Email != "" && Email != null)
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@CountryID", CountryID);


            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if(result!= null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    PersonID = InsertedID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return PersonID;
        }

        /****************************************************************************************************************************************/

        public static bool UpdatePerson(int PersonID, string NationalNo, string FirstName, string LastName,
                                        DateTime DateOfBirth, short Gender, string Phone, string Email, string Address, int CountryID)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"UPDATE People
                             SET NationalNo = @NationalNo,
                                 FirstName = @FirstName,
                                 LastName = @LastName,
                                 DateOfBirth = @DateOfBirth,
                                 Gender = @Gender,
                                 Phone = @Phone,
                                 Email = @Email,
                                 Address = @Address,
                                 CountryID = @CountryID
                            WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@NationaNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@CountryID", CountryID);

            if (Email != "" && Email != null)
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            if (Address != "" && Address != null)
                command.Parameters.AddWithValue("@Address", Address);
            else
                command.Parameters.AddWithValue("@Address", System.DBNull.Value);


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

        /****************************************************************************************************************************************/
        public static bool DeletePerson(int PersonID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Delete People 
                                where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
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
        /****************************************************************************************************************************************/

        public static DataTable GetAllPeople()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query =
              @"SELECT People.PersonID, People.NationalNo, People.FirstName, People.LastName,
				       People.DateOfBirth, People.Gender, CASE
                                                                WHEN People.Gender = 0 THEN 'Male'
                                                                ELSE 'Female'
                                                                END as GenderCaption ,
			            People.Address, People.Phone, People.Email, 
                        People.CountryID, Countries.CountryName
               FROM     People INNER JOIN
                         Countries ON People.CountryID = Countries.CountryID
               ORDER BY People.LastName";

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

        /****************************************************************************************************************************************/

        public static bool IsPersonExist(int PersonID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Found=1 FROM People WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
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

        /****************************************************************************************************************************************/
        public static bool IsPersonExist(string NationalNo)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM People WHERE NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        /****************************************************************************************************************************************/
    }
}
