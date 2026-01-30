// Utils/ValidationHelper.cs
using System;
using System.Linq;

namespace BusTicketBookingSystem.Utils
{
    public static class ValidationHelper
    {
        /// <summary>
        /// Validates and formats a mobile number with +88 prefix
        /// </summary>
        /// <param name="mobile">Raw mobile number input</param>
        /// <returns>Formatted mobile number with +88 prefix</returns>
        /// <exception cref="Exception">Thrown when mobile number format is invalid</exception>
        public static string ValidateAndFormatMobileNumber(string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile))
                throw new Exception("Mobile Number Cannot Be Empty.");
            
            if (mobile.StartsWith("+88"))
            {
                // Already has +88 prefix
                if (mobile.Length != 14 || !mobile.Substring(3).All(char.IsDigit) || mobile[3] != '0')
                    throw new Exception("Invalid Mobile Number. Must Be +88 Followed By 11 Digits Starting With 0.");
                return mobile;
            }
            else
            {
                // No prefix, validate and add +88
                if (mobile.Length != 11 || !mobile.All(char.IsDigit) || !mobile.StartsWith("0"))
                    throw new Exception("Invalid Mobile Number. Must Be 11 Digits Starting With 0.");
                return "+88" + mobile;
            }
        }

        /// <summary>
        /// Validates email format
        /// </summary>
        /// <param name="email">Email address to validate</param>
        /// <exception cref="Exception">Thrown when email format is invalid</exception>
        public static void ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("Email Cannot Be Empty.");
            
            if (!email.Contains("@") || !email.Contains("."))
                throw new Exception("Invalid Email Address. Must Contain '@' And '.'");
        }

        /// <summary>
        /// Validates city name
        /// </summary>
        /// <param name="cityName">City name to validate</param>
        /// <param name="fieldName">Field name for error messages (e.g., "Departure City")</param>
        /// <exception cref="Exception">Thrown when city name is invalid</exception>
        public static void ValidateCityName(string cityName, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(cityName))
                throw new Exception($"{fieldName} Cannot Be Empty.");
            
            if (cityName.Any(char.IsDigit))
                throw new Exception($"{fieldName} Cannot Contain Numbers.");
            
            if (cityName.Length < 2)
                throw new Exception($"{fieldName} Must Be At Least 2 Characters Long.");
        }

        /// <summary>
        /// Validates and parses date/time string
        /// </summary>
        /// <param name="dateTimeInput">Date/time string in format yyyy-MM-dd HH:mm</param>
        /// <returns>Parsed DateTime object</returns>
        /// <exception cref="Exception">Thrown when date/time format is invalid or in the past</exception>
        public static DateTime ValidateDateTime(string dateTimeInput)
        {
            if (!DateTime.TryParseExact(dateTimeInput, "yyyy-MM-dd HH:mm", 
                System.Globalization.CultureInfo.InvariantCulture, 
                System.Globalization.DateTimeStyles.None, out DateTime result))
            {
                throw new Exception("Invalid Date/Time Format. Use yyyy-MM-dd HH:mm (e.g., 2026-12-31 14:30).");
            }
            
            if (result <= DateTime.Now)
            {
                throw new Exception("Departure Date/Time Must Be In The Future.");
            }
            
            return result;
        }

        /// <summary>
        /// Validates ticket price
        /// </summary>
        /// <param name="price">Price to validate</param>
        /// <exception cref="Exception">Thrown when price is invalid</exception>
        public static void ValidateTicketPrice(decimal price)
        {
            if (price < 0)
                throw new Exception("Ticket Price Cannot Be Negative.");
            
            if (price > 9999.99m)
                throw new Exception("Ticket Price Cannot Exceed 9999.99.");
        }

        /// <summary>
        /// Validates bus type input
        /// </summary>
        /// <param name="input">String input to parse as bus type</param>
        /// <returns>Parsed integer (0 or 1)</returns>
        /// <exception cref="Exception">Thrown when input is invalid</exception>
        public static int ValidateBusTypeInput(string? input)
        {
            if (!int.TryParse(input, out int busTypeInt) || (busTypeInt != 0 && busTypeInt != 1))
                throw new Exception("Invalid Bus Type. Enter 0 For Business Or 1 For Economy.");
            
            return busTypeInt;
        }

        /// <summary>
        /// Validates integer ID input
        /// </summary>
        /// <param name="input">String input to parse</param>
        /// <param name="fieldName">Field name for error messages</param>
        /// <returns>Parsed integer ID</returns>
        /// <exception cref="Exception">Thrown when input cannot be parsed as integer</exception>
        public static int ValidateIntegerId(string? input, string fieldName)
        {
            if (!int.TryParse(input, out int id))
                throw new Exception($"Invalid {fieldName}.");
            
            return id;
        }

        /// <summary>
        /// Validates decimal input
        /// </summary>
        /// <param name="input">String input to parse</param>
        /// <param name="fieldName">Field name for error messages</param>
        /// <returns>Parsed decimal value</returns>
        /// <exception cref="Exception">Thrown when input cannot be parsed as decimal</exception>
        public static decimal ValidateDecimal(string? input, string fieldName)
        {
            if (!decimal.TryParse(input, out decimal value))
                throw new Exception($"Invalid {fieldName}.");
            
            return value;
        }

        /// <summary>
        /// Validates that a string is not empty or whitespace
        /// </summary>
        /// <param name="input">String to validate</param>
        /// <param name="fieldName">Field name for error messages</param>
        /// <exception cref="Exception">Thrown when input is empty or whitespace</exception>
        public static void ValidateNotEmpty(string? input, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new Exception($"{fieldName} Cannot Be Empty.");
        }

        /// <summary>
        /// Validates a person name (non-empty, no digits)
        /// </summary>
        /// <param name="name">Name to validate</param>
        /// <exception cref="Exception">Thrown when name is invalid</exception>
        public static void ValidateName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Name Cannot Be Empty.");

            if (name.Any(char.IsDigit))
                throw new Exception("Name Cannot Contain Numbers.");
        }
    }
}
