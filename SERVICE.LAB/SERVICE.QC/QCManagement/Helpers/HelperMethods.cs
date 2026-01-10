using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using QC.Services.StartupServices;
using ErrorOr;
using FluentValidation;

namespace QC.Helpers
{
    public static class HelperMethods
    {
        public static bool LessThanOrEqualToToday(DateTime date)
        {
            return date <= DateTime.Now;
        }
        public static bool GreaterThanOrEqualToToday(DateTime date)
        {
            return date >= DateTime.Now;
        }

        public static bool checkCsvVulnerableCharactersValidator(string input)
        {
            if (string.IsNullOrEmpty(input)) return true;
            Regex regex = new Regex(@"^(=|\+|\-|@)");
            return !regex.IsMatch(input);
        }
        public static bool CalculateAge(DateTime birthdate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthdate.Year;
            if (birthdate.Date > today.AddYears(-age))
            {
                age--;
            }
            return age <= 120;
        }

        public static bool IsNumeric(string input)
        {
            return double.TryParse(input, out _);
        }
    }
}