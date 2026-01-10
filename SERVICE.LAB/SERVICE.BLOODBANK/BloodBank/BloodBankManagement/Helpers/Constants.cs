using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagement.Helpers
{
    public static class Constants
    {
        private static readonly string connectionUrl = "WebApiDatabase";
        private static readonly string bloodGroupingRH = "ABOTMA";
        private static readonly string bloodGroupingRHAuto = "ABOTAT";
        private static readonly string babyBloodGroupingRHAuto = "BABYABO";
        private static readonly string babyABOConfirmation = "BABYCFM";
        private static readonly string aBOConfirmation = "ABOTCM";
        private static readonly string antibodyScreening = "ABSCT";
        private static readonly string crossMatchingXM = "EXMAT";
        private static readonly string crossMatchingManualXM = "EXM";
        private static readonly string crossMatchingImmediateSpinXM = "XMIS";
        private static readonly string antibodyIdentified = "ABID";
        private static readonly string antibodyIdentified2 = "ABID2";
        private static readonly string iNTERPRETATION = "INT";

        private static readonly string productIssued = "ProductIssued";
        private static readonly string uploadPathInit = "UploadPathInit";
        private static readonly string masterUrl = "MasterUrl";
        private static readonly string reportServiceURL = "ReportServiceURL";
        private static readonly string uploadedPath = "UploadedPath";
        public static string ConnectionUrl { get => connectionUrl; }
        public static string BloodGroupingRH { get => bloodGroupingRH; }
        public static string BloodGroupingRHAuto { get => bloodGroupingRHAuto; }
        public static string BabyBloodGroupingRHAuto { get => babyBloodGroupingRHAuto; }
        public static string ABOConfirmation { get => aBOConfirmation; }
        public static string BabyABOConfirmation { get => babyABOConfirmation; }

        public static string AntibodyScreening { get => antibodyScreening; }
        public static string CrossMatchingXM { get => crossMatchingXM; }
        public static string CrossMatchingManualXM { get => crossMatchingManualXM; }
        public static string CrossMatchingImmediateSpinXM { get => crossMatchingImmediateSpinXM; }
        public static string AntibodyIdentified { get => antibodyIdentified; }
        public static string AntibodyIdentified2 { get => antibodyIdentified2; }
        public static string INTERPRETATION { get => iNTERPRETATION; }
        public static string ProductIssued { get => productIssued; }
        public static string UploadPathInit { get => uploadPathInit; }
        public static string MasterUrl { get => masterUrl; }
        public static string ReportServiceURL { get => reportServiceURL; }
        public static string UploadedPath { get => uploadedPath; }
    }
}