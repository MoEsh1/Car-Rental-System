using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Car_Rental.Classes
{
    public class clsValidation
    {
        /***********************************************************************************************/
        public static bool ValidateEmail(string emailAddress)
        {
            var pattern = @"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";
            var regex = new Regex(pattern);
            return regex.IsMatch(emailAddress);
        }

        /*********************************************************************************************************************/
        public static bool ValidateInteger(string Number)
        {
            var pattern = @"^[0-9]*$";
            var regex = new Regex(pattern);
            return regex.IsMatch(Number);
        }

        /*********************************************************************************************************************/
        public static bool ValidateFloat(string Number)
        {
            // Accepts both commas and dots for regional flexibility
            var pattern = @"^[0-9]*(?:[\.\,][0-9]*)?$";
            var regex = new Regex(pattern);
            return regex.IsMatch(Number);
        }
        /*********************************************************************************************************************/
        public static bool IsNumber(string Number)
        {
            return (ValidateInteger(Number) || ValidateFloat(Number));
        }

        /*********************************************************************************************************************/
        public static bool MyTryStrToDecimal(string input, out decimal result)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                result = 0m;
                return false;
            }
            string normalized = input.Replace(",", ".");
            return decimal.TryParse(normalized, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
        }

        /*********************************************************************************************************************/
        public static decimal MyStrToDecimal(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return 0m;
            string normalized = input.Replace(",", ".");
            return decimal.Parse(normalized, CultureInfo.InvariantCulture);
        }

        /*********************************************************************************************************************/
        public static decimal MyStrToDecimalDef(string input, decimal defValue)
        {
            if (MyTryStrToDecimal(input, out decimal result))
                return result;
            return defValue;
        }

        /*********************************************************************************************************************/
        public static bool MyTryStrToDouble(string input, out double result)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                result = 0d;
                return false;
            }
            string normalized = input.Replace(",", ".");
            return double.TryParse(normalized, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
        }

        /*********************************************************************************************************************/
        public static double MyStrToDouble(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return 0d;
            string normalized = input.Replace(",", ".");
            return double.Parse(normalized, CultureInfo.InvariantCulture);
        }

        /*********************************************************************************************************************/
        public static double MyStrToDoubleDef(string input, double defValue)
        {
            if (MyTryStrToDouble(input, out double result))
                return result;
            return defValue;
        }

        /*********************************************************************************************************************/
        public static bool MyTryStrToFloat(string input, out float result)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                result = 0f;
                return false;
            }
            string normalized = input.Replace(",", ".");
            return float.TryParse(normalized, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
        }

        /*********************************************************************************************************************/
        public static float MyStrToFloat(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return 0f;
            string normalized = input.Replace(",", ".");
            return float.Parse(normalized, CultureInfo.InvariantCulture);
        }

        /*********************************************************************************************************************/
        public static float MyStrToFloatDef(string input, float defValue)
        {
            if (MyTryStrToFloat(input, out float result))
                return result;
            return defValue;
        }


        /*********************************************************************************************************************/
        // Returns a string containing only the allowed characters
        public static string AcceptChars(string s, char[] allowedChars)
        {
            if (string.IsNullOrEmpty(s)) return s;
            var allowed = new HashSet<char>(allowedChars);
            return new string(s.Where(c => allowed.Contains(c)).ToArray());
        }

        /*********************************************************************************************************************/
        // Returns a string with the prohibited characters removed
        public static string RemoveChars(string s, char[] prohibitedChars)
        {
            if (string.IsNullOrEmpty(s)) return s;
            var prohibited = new HashSet<char>(prohibitedChars);
            return new string(s.Where(c => !prohibited.Contains(c)).ToArray());
        }

        /*********************************************************************************************************************/

        // Fast check if a string is made of only digits
        public static bool IsNumberStr(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;
            foreach (char c in s)
            {
                if (!char.IsDigit(c)) return false;
            }
            return true;
        }
        /*********************************************************************************************************************/
        // Fast check if a string is made of only digits, optionally prefixed by '+' or '-'
        public static bool IsSignedNumberStr(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;
            if (s[0] == '+' || s[0] == '-')
            {
                return s.Length > 1 && IsNumberStr(s.Substring(1));
            }
            return IsNumberStr(s);
        }

        /*********************************************************************************************************************/
        // Splits a delimited string into an integer array, substituting failed parses with a default value
        public static int[] SplitIntegerListString(string values, char delim, int defValue)
        {
            if (string.IsNullOrWhiteSpace(values)) return new int[0];

            return values.Split(delim)
                         .Select(v => int.TryParse(v.Trim(), out int res) ? res : defValue)
                         .ToArray();
        }

        /*********************************************************************************************************************/
        // Removes trailing zeros from a decimal string, and the decimal point itself if no fractions remain
        public static string RemoveTrailingDecimalZero(string s)
        {
            if (string.IsNullOrEmpty(s) || !s.Contains(".")) return s;

            s = s.TrimEnd('0');
            if (s.EndsWith("."))
            {
                s = s.TrimEnd('.');
            }
            return s;
        }

        /*********************************************************************************************************************/
        // Attempts to parse a date string from multiple predefined formats, returning a default on failure
        public static DateTime MyStrToDateTimeDef(string strValue, DateTime defValue)
        {
            if (string.IsNullOrWhiteSpace(strValue)) return defValue;

            string[] formats = {
                "dd/MM/yyyy",
                "HH:mm:ss",
                "dd/MM/yyyy HH:mm:ss",
                "yyyyMMdd",
                "yyyyMMdd.HHmmss"
            };

            if (DateTime.TryParseExact(strValue, formats,
                                       CultureInfo.InvariantCulture,
                                       DateTimeStyles.None,
                                       out DateTime result))
            {
                return result;
            }

            return defValue;
        }
        /*********************************************************************************************************************/
    }
}