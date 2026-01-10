using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BloodBankManagement.Services.StartupServices;
using ErrorOr;
using FluentValidation;

namespace BloodBankManagement.Helpers
{
    public static class HelperMethods
    {
        public static List<string> RegistrationStatus = new List<string>() { "Completed", "Expired", "InventoryAssigned", "ProductIssued", "Registered", "Rejected", "ResultValidated", "SampleReceived" };
        public static List<string> BloodGroups = new List<string> { "A Pos", "B Pos", "AB Pos", "O Pos", "A Neg", "B Neg", "AB Neg", "O Neg" };
        public static List<string> InventoryStatuses = new List<string> {"Assigned", "Returned", "Quarantine", "Acknowledged", "Disposed", "Expired", "Issued", "OnHold", "readyfortesting", "rejected", "ReturnedtoBSG", "Transfused", "uncheck", "available", "CaseCancel" };
        public static List<string> BloodSampleInventoryStatuses = new List<string> {"OnHold", "SampleProcessed", "Completed", "Expired", "InventoryAssigned", "ProductIssued", "Registered", "Rejected", "ResultValidated", "SampleReceived", "Returned", "Transfused", "CaseCancel" };
        public static List<string> BloodSampleInventoryNonExpiryStatuses = new List<string> { "Completed", "Expired", "ProductIssued", "Rejected", "Returned", "Transfused", "CaseCancel" };
        public static List<string> BloodSampleResultStatuses = new List<string> { "SampleProcessed", "SampleReceived", "Registered", "SentToHSA", "ResultValidated" };
        public static List<string> Gradings = new List<string> { "4+", "3+", "0", "-" };
        public static List<string> AboResult = new List<string> { "A", "B", "O", "AB" };
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
            if(string.IsNullOrEmpty(input)) return true;
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

        public static bool IsPresentIntheLookup(long id, string type)
        {
            return GlobalConstants.Lookups.Any(x => x.Identifier == id && x.Type == type);
        }

        public static bool IsNumeric(string input)
        {
            return double.TryParse(input, out _);
        }
        
        public static bool IsCrosMatchingTest(Int64 testId)
        {
            return testId == GlobalConstants.CrossMatchingXMId || testId == GlobalConstants.CrossMatchingManualXMId || testId == GlobalConstants.CrossMatchingImmediateSpinXMId;
        }
        public static Int64? ParseStringToLong(string input)
        {
            if (Int64.TryParse(input, out Int64 result))
            {
                return result;
            }
            else return null;
        }

        public static DateTime? ParseStringToDateTime(string input)
        {
            if (DateTime.TryParse(input, out DateTime result))
            {
                return result;
            }
            else return null;
        }
        public static Int64 GetCrossMatchingInterpretationTestId(Int64 crossMatchingTestId)
        {
            if (crossMatchingTestId == GlobalConstants.CrossMatchingXMId) return GlobalConstants.CrossMatchingInterpretationTestId;
            else if (crossMatchingTestId == GlobalConstants.CrossMatchingManualXMId) return GlobalConstants.CrossMatchingManualInterpretationTestId;
            else if (crossMatchingTestId == GlobalConstants.CrossMatchingImmediateSpinXMId) return GlobalConstants.CrossMatchingImmediateSpinInterpretationTestId;
            return GlobalConstants.CrossMatchingInterpretationTestId;
        }
    }
}