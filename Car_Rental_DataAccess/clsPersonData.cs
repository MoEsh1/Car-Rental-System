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
        
        /*******************************************************************************************/
        public static bool GetPersonInfoByID(int PersonID, ref string NationalNo, ref string FirstName, ref string LastName,
                                             ref DateTime DateOfBirth, ref short Gender, ref string Phone, ref string Email,
                                             ref string Address, ref int CountryID)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetPersonInfoByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
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
        /*******************************************************************************************/
        public static bool GetPersonInfoByNationalNo(string NationalNo, ref int PersonID, ref string FirstName, ref string LastName,
                                       ref DateTime DateOfBirth, ref short Gender, ref string Phone, ref string Email,
                                       ref string Address, ref int CountryID)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetPersonInfoByNationalNo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
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
        /*******************************************************************************************/
        public static int AddNewPerson(string NationalNo, string FirstName, string LastName, DateTime DateOfBirth,
                                       short Gender, string Phone, string Email, string Address, int CountryID)
        {
            int PersonID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_AddNewPerson", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
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

                    SqlParameter outputIdParam = new SqlParameter("@NewPersonID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);
                    connection.Open();
                    command.ExecuteNonQuery();

                    PersonID = (int)command.Parameters["@NewPersonID"].Value;


                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
            }

            return PersonID;
        }

        /*******************************************************************************************/
        public static bool UpdatePerson(int PersonID, string NationalNo, string FirstName, string LastName,
                                        DateTime DateOfBirth, short Gender, string Phone, string Email, string Address, int CountryID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_UpdatePerson", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

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

        /*******************************************************************************************/
        public static bool DeletePerson(int PersonID)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_DeletePerson", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
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
        /*******************************************************************************************/
        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetAllPeople", connection))
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
        public static bool IsPersonExist(int PersonID)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_IsPersonExist", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    connection.Open();

                    int result = (int)command.ExecuteScalar();

                    
                     isFound = result == 1;
                    
                }
            }
            catch (Exception ex)
            {
                isFound = false;
            }

            return isFound;
        }
        /*******************************************************************************************/
        public static bool IsPersonExist(string NationalNo)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_IsPersonExistByNationalNo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);

                    connection.Open();

                    int result = (int)command.ExecuteScalar();
                    isFound = result == 1;
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