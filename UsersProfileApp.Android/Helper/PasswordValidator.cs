using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UsersProfileApp.Android.Helper
{
    public class PasswordValidator
    {
        public static string ERROR_PASSWORD_LENGTH = "Password must be 7 to 16 characters long.";
        public static string ERROR_LETTER_DIGIT_CASE = "Password must contain atleast one uppercase, lowercase letters and a digit.";
        public static string ERROR_SAMEAS_USERNAME = "Password cannot be password, default or same as username";

        internal static string validate(string password, string username)
        {
            List<string> failures = new List<string>();
            checkLength(password, failures);
            checkIsSameAsUsername(password, username, failures);
            CheckLetterAndDigit(password, failures);
            return errorDetails(failures);

        }
        public static string errorDetails(List<string> failures)

        {
            string error = null;
            if (failures != null)
            {
                for (int i = 0; i < failures.Count; i++)
                {
                    error += failures[i] + "\n";
                }
            }
            return error;
        }

        private static void CheckLetterAndDigit(string password, List<string> failures)
        {
            // Include a digit, uppercase letter and lowercase letter
            if (!Regex.IsMatch(password, @"((?=.*[A-Z])(?=.*[a-z])(?=.*\d))"))
            {
                failures.Add(ERROR_LETTER_DIGIT_CASE);
            }

        }

        private static void checkIsSameAsUsername(string password, string username, List<string> failures)
        {
            // Is the password is password, default and same as username
            bool isValid = Regex.IsMatch(password, $@"^((?!{username}|password|default).)*$", RegexOptions.IgnoreCase);
            if (!isValid)
            {
                failures.Add(ERROR_SAMEAS_USERNAME);
            }

        }

        private static void checkLength(string password, List<string> failures)
        {
            // Is length between 7 and 16 characters
            if (!Regex.IsMatch(password, "^.{7,16}$"))
            {
                failures.Add(ERROR_PASSWORD_LENGTH);
            }
        }

    }
}
