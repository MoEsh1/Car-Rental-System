using BuisnessLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

using static Car_Rental.Global_Classes.clsEventLogger;
namespace Car_Rental.Global_Classes
{

    internal static class clsGlobal
    {
        public static clsUser CurrentUser;

        public static bool RememberUsernameAndPassword(string Username, string Password)
        {

            try
            {

                //this will get the current project directory folder.
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();


                // Define the path to the text file where you want to save the data
                string filePath = currentDirectory + "\\data.txt";

                //incase the username is empty, delete the file
                if (Username == "" && File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;

                }

                // concatonate username and passwrod withe seperator.
                string dataToSave = Username + "#//#" + Password;

                // Create a StreamWriter to write to the file
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write the data to the file
                    writer.WriteLine(dataToSave);

                    return true;
                }
            }
            catch (Exception ex)
            {
                LogError($"An error occurred: {ex.Message}");
                return false;
            }

        }
        // to save username and passowrd into registry
        public static bool RememberUsernameAndPasswordAtReg(string Username, string Password)
        {
            string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\CarRental";
            string ValueName1 = "username";
            string ValueData1 = Username;
            string ValueName2 = "password";
            string ValueData2 = Password;
            try
            {

                Registry.SetValue(KeyPath, ValueName1, ValueData1, RegistryValueKind.String);
                Registry.SetValue(KeyPath, ValueName2, ValueData2, RegistryValueKind.String);
                
                LogInfo($"Value {ValueName1} = {ValueData1} successfully written to the Registry.");
                return true;
            }
            catch (Exception ex)
            {
                LogError($"An error occurred: {ex.Message}");
                return false;
            }

        }

        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            //this will get the stored username and password and will return true if found and false if not found.
            try
            {
                //gets the current project's directory
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();

                // Path for the file that contains the credential.
                string filePath = currentDirectory + "\\data.txt";

                // Check if the file exists before attempting to read it
                if (File.Exists(filePath))
                {
                    // Create a StreamReader to read from the file
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        // Read data line by line until the end of the file
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(line); // Output each line of data to the console
                            string[] result = line.Split(new string[] { "#//#" }, StringSplitOptions.None);

                            Username = result[0];
                            Password = result[1];
                        }
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogError($"An error occurred: {ex.Message}");
                return false;
            }

        }
        public static bool GetStoredCredentialFromReg(ref string Username, ref string Password)
        {
            //this will get the stored username and password and will return true if found and false if not found.

            string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\CarRental";
            string ValueName1 = "username";
            string ValueName2 = "password";

            try
            {
                // Read BOTH values first
                string UsernameValue = Registry.GetValue(KeyPath, ValueName1, null) as string;
                string PasswordValue = Registry.GetValue(KeyPath, ValueName2, null) as string;

                // Check if BOTH were found successfully
                if (UsernameValue != null && PasswordValue != null)
                {
                    Username = UsernameValue;
                    Password = PasswordValue;
                    return true; 
                }
                else
                {
                    
                    if (UsernameValue == null)
                    {
                      LogError($"{ValueName1} not found in the Registry.");
                    }

                    if (PasswordValue == null)
                    {
                      LogError($"{ValueName2} not found in the Registry."); 
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                LogError($"An error occurred: {ex.Message}");
                return false;
            }
        }
    }
}
