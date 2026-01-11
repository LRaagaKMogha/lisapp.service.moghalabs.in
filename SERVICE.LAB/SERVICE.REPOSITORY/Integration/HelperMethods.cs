using Service.Model.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Repository.Integration
{
    public class HelperMethods
    {
        public HelperMethods() { }
        public static List<string> bbTests = new List<string>() { "420101", "499020" };
        public static int getReferralTypeName(IntegrationOrderDetailsResponse responseData)
        {
            //if (responseData.IntegrationOrderClientDetails != null && !string.IsNullOrEmpty(responseData.IntegrationOrderClientDetails.clientcode))
            //    return responseData.IntegrationOrderClientDetails.clientname;
            //if (responseData.IntegrationOrderDoctorDetails != null && !string.IsNullOrEmpty(responseData.IntegrationOrderDoctorDetails.doctorname))
            //    return responseData.IntegrationOrderDoctorDetails.doctorname;
            return 0;

        }

        public static bool isBBTestOrder(string testCode)
        {
            return bbTests.Any(tes => tes == testCode);
        }

        public static string getReferralType(IntegrationOrderDetailsResponse responseData)
        {
            if (responseData.IntegrationOrderClientDetails != null && !string.IsNullOrEmpty(responseData.IntegrationOrderClientDetails.clientcode))
                return "2";
            if (responseData.IntegrationOrderDoctorDetails != null && !string.IsNullOrEmpty(responseData.IntegrationOrderDoctorDetails.doctorname))
                return "3";
            return "1";

        }
        public static string getAlternateURNType(string urnType)
        {
            if (urnType == "M") return "713";
            else if (urnType == "N") return "714";
            else if (urnType == "P") return "715";
            else if (urnType == "O") return "716";
            else if (urnType == "C") return "783";
            return "0";
        }
        public static string getURNType(string urnType)
        {
            if (urnType == "P") return "1";
            else if (urnType == "B") return "2";
            else if (urnType == "F") return "3";
            else if (urnType == "O") return "12";
            else if (urnType == "X") return "5";
            else if (urnType == "XX") return "4";
            else if (urnType == "Z") return "13";
            else if (urnType == "C") return "6";
            else if (urnType == "D") return "7";
            else if (urnType == "E") return "8";
            else if (urnType == "L") return "9";
            else if (urnType == "M") return "10";
            else if (urnType == "S") return "11";

            return "0";
        }
        public static int getMaritalStatus(string maritalStatus = "")
        {
            return maritalStatus?.ToLower() == "married" || maritalStatus?.ToLower() == "M" ? 1 : 0;
        }

        public static string GetAgeDescription(DateTime birthDate)
        {
            DateTime currentDate = DateTime.Now;
            TimeSpan difference = currentDate - birthDate;
            int daysDifference = difference.Days;
            if (daysDifference < 30)
            {
                return $"{daysDifference} Days";
            }
            int years = currentDate.Year - birthDate.Year;
            int months = currentDate.Month - birthDate.Month;
            int days = currentDate.Day - birthDate.Day;
            if(days < 0)
            {
                months--;
                days += DateTime.DaysInMonth(currentDate.Year, currentDate.Month == 1 ? 12 : currentDate.Month - 1);
            }
            if(months < 0)
            {
                years--;
                months += 12;
            }
            if(years < 1)
            {
                return $"{months} Months";
            }
            return $"{years} Years";
        }
        public static int CalculateAge(DateTime dateOfBirth)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - dateOfBirth.Year;

            // Check if the birthday has occurred this year
            if (dateOfBirth.Date > today.AddYears(-age))
                age--;

            return age;
        }

        public static string getResulType(String s )
        {
            switch(s)
            {
                case "TX":
                case "TA":
                case "CU":
                case "TE":
                    return "C";
                case "NU":
                    return "R";
            }
            return string.Empty;
        }

        public static string getSourceSystem(string sourceSystemId)
        {
            var id = Int32.Parse(sourceSystemId);
            nsourcesystem enumValue = (nsourcesystem)id;
            return Enum.GetName(typeof(nsourcesystem), enumValue);
        }

        public static string getGender(string genderId)
        {
            var id = 3;
            if (genderId == "1" || genderId == "M" || genderId?.ToUpper() == "MALE") id = 1;
            else if (genderId == "2" || genderId == "F" || genderId?.ToUpper() == "FEMALE") id = 2;

            ngender enumValue = (ngender)id;
            return Enum.GetName(typeof(ngender), enumValue);
        }

        public static string getGenderTitle(string genderId)
        {
            ;
            if (genderId == "1" || genderId == "M" || genderId?.ToUpper() == "MALE") return "Mr.";
            else if (genderId == "2" || genderId == "F" || genderId?.ToUpper() == "FEMALE") return "Ms.";
            return "Mr.";
        }
        public static int getAdditionalId(List<TestAdditionalInformation> testAdditionalInformation, string input)
        {
            var data = testAdditionalInformation.FirstOrDefault(x => x.DataType == input);
            if (data != null) return Int32.Parse(data.ID.ToString());
            return 0;

        }
        public static string getAdditionalDescription(List<TestAdditionalInformation> testAdditionalInformation, string input)
        {
            var data = testAdditionalInformation.FirstOrDefault(x => x.DataType == input);
            if (data != null) return data.Details;
            return input;

        }
    }
}
