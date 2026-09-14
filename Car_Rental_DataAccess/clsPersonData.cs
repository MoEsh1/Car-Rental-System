using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class clsPersonData
    {
        public static bool GetPersonInfoByID(int PersonID, ref string NationalNo, ref string FirstName, ref string LastName,
                                             ref DateTime DateOfBirth, ref short Gender, ref string Phone, ref string Email,
                                             ref string Address, ref int CountryID)
        {
            bool isFound = false;
            string query = @"SELECT NationalNo, FirstName, LastName, DateOfBirth, Gender, 
                                    Phone, Email, Address, CountryID 
                             FROM People 
                             WHERE PersonID = @PersonID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            NationalNo = (string)reader["NationalNo"];
                            FirstName = (string)reader["FirstName"];
                            LastName = (string)reader["LastName"];
                            DateOfBirth = (DateTime)reader["DateOfBirth"];
                            Gender = (byte)reader["Gender"];
                            Phone = (string)reader["Phone"];

                            if (reader["Email"] != DBNull.Value)
                                Email = (string)reader["Email"];
                            else
                                Email = "";

                            if (reader["Address"] != DBNull.Value)
                                Address = (string)reader["Address"];
                            else
                                Address = "";

                            CountryID = (int)reader["CountryID"];
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

        public static bool GetPersonInfoByNationalNo(string NationalNo, ref int PersonID, ref string FirstName, ref string LastName,
                                       ref DateTime DateOfBirth, ref short Gender, ref string Phone, ref string Email,
                                       ref string Address, ref int CountryID)
        {
            bool isFound = false;

            string query = @"SELECT PersonID, FirstName, LastName, DateOfBirth, Gender, 
                                    Phone, Email, Address, CountryID 
                             FROM People 
                             WHERE NationalNo = @NationalNo";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            PersonID = (int)reader["PersonID"];
                            FirstName = (string)reader["FirstName"];
                            LastName = (string)reader["LastName"];
                            DateOfBirth = (DateTime)reader["DateOfBirth"];
                            Gender = (byte)reader["Gender"];

                            // BUG FIX: Phone was entirely missing in the original code!
                            Phone = (string)reader["Phone"];

                            if (reader["Email"] != DBNull.Value)
                                Email = (string)reader["Email"];
                            else
                                Email = "";

                            if (reader["Address"] != DBNull.Value)
                                Address = (string)reader["Address"];
                            else
                                Address = "";

                            CountryID = (int)reader["CountryID"];
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

        public static int AddNewPerson(string NationalNo, string FirstName, string LastName, DateTime DateOfBirth,
                                       short Gender, string Phone, string Email, string Address, int CountryID)
        {
            int PersonID = -1;

            string query = @"INSERT INTO People (NationalNo, FirstName, LastName, DateOfBirth,
                                                 Gender, Phone, Email, Address, CountryID)
                             VALUES (@NationalNo, @FirstName, @LastName, @DateOfBirth,
                                     @Gender, @Phone, @Email, @Address, @CountryID);
                             SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@Gender", Gender);
                    command.Parameters.AddWithValue("@Phone", Phone);
                    command.Parameters.AddWithValue("@CountryID", CountryID);

                    // Cleaned up null checks using string.IsNullOrEmpty
                    if (!string.IsNullOrEmpty(Address))
                        command.Parameters.AddWithValue("@Address", Address);
                    else
                        command.Parameters.AddWithValue("@Address", DBNull.Value);

                    if (!string.IsNullOrEmpty(Email))
                        command.Parameters.AddWithValue("@Email", Email);
                    else
                        command.Parameters.AddWithValue("@Email", DBNull.Value);

                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                    {
                        PersonID = InsertedID;
                    }
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
            }

            return PersonID;
        }

        public static bool UpdatePerson(int PersonID, string NationalNo, string FirstName, string LastName,
                                        DateTime DateOfBirth, short Gender, string Phone, string Email, string Address, int CountryID)
        {
            int rowsAffected = 0;

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

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@Gender", Gender);
                    command.Parameters.AddWithValue("@Phone", Phone);
                    command.Parameters.AddWithValue("@CountryID", CountryID);

                    if (!string.IsNullOrEmpty(Email))
                        command.Parameters.AddWithValue("@Email", Email);
                    else
                        command.Parameters.AddWithValue("@Email", DBNull.Value);

                    if (!string.IsNullOrEmpty(Address))
                        command.Parameters.AddWithValue("@Address", Address);
                    else
                        command.Parameters.AddWithValue("@Address", DBNull.Value);

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return rowsAffected > 0;
        }

        public static bool DeletePerson(int PersonID)
        {
            int rowsAffected = 0;
            string query = @"Delete People where PersonID = @PersonID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);

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

        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT People.PersonID, People.NationalNo, People.FirstName, People.LastName,
                                    People.DateOfBirth, People.Gender, 
                                    CASE
                                        WHEN People.Gender = 0 THEN 'Male'
                                        ELSE 'Female'
                                    END as GenderCaption,
                                    People.Address, People.Phone, People.Email, 
                                    People.CountryID, Countries.CountryName
                             FROM People 
                             INNER JOIN Countries ON People.CountryID = Countries.CountryID
                             ORDER BY People.LastName";

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

        public static bool IsPersonExist(int PersonID)
        {
            bool isFound = false;
            string query = "SELECT Found=1 FROM People WHERE PersonID = @PersonID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);

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

        public static bool IsPersonExist(string NationalNo)
        {
            bool isFound = false;
            string query = "SELECT Found=1 FROM People WHERE NationalNo = @NationalNo";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);

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