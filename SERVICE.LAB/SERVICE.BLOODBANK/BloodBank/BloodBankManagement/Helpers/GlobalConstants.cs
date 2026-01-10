using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodBankManagement.Contracts;
using MasterManagement.Contracts;

namespace BloodBankManagement.Services.StartupServices
{
    public static class GlobalConstants
    {
        public static long BloodGroupingRHId { get; set; } = 0;
        public static long BloodGroupingRHAutoId { get; set; } = 0;
        public static long BabyBloodGroupingRHAutoId { get; set; } = 0;
        public static long ABOConfirmationId { get; set; } = 0;
        public static long BabyABOConfirmationId { get; set; } = 0;
        public static long AntibodyScreeningId { get; set; } = 0;
        public static long CrossMatchingXMId { get; set; } = 0;
        public static long CrossMatchingManualXMId { get; set; } = 0;
        public static long CrossMatchingImmediateSpinXMId { get; set; } = 0;
        public static long AntibodyIdentifiedId { get; set; } = 0;
        public static long AntibodyIdentifiedId2 { get; set; } = 0; 
        public static long RedCellCategoryId { get; set; } = 0;
        public static long CrossMatchingInterpretationTestId { get; set; } = 0;
        public static long CrossMatchingManualInterpretationTestId { get; set; } = 0;
        public static long CrossMatchingImmediateSpinInterpretationTestId { get; set; } = 0;
        public static long BloodGroupingRHInterpretationId { get; set; } = 0;
        public static long BloodGroupingRHAutoInterpretationId { get; set; } = 0;
        public static long BabyBloodGroupingRHAutoInterpretationId { get; set; } = 0;
        public static long AntibodyScreeningInterpretationId { get; set; } = 0;
        public static long AntibodyIdentifiedInterpretationId { get; set; } = 0;
        public static long ColdAntibodyIdentifiedInterpretationId { get; set; } = 0;
        public static long AntibodyIdentified2InterpretationId { get; set; } = 0;
        public static long ColdAntibodyIdentified2InterpretationId { get; set; } = 0;
        
        public static long ABOConfirmationInterpretationId { get; set; } = 0;
        public static long BabyABOConfirmationInterpretationId { get; set; } = 0;
        public static List<TestResponse> Tests { get; set; } = new List<Contracts.TestResponse>();
        public static List<SubTestResponse> SubTests { get; set; } = new List<Contracts.SubTestResponse>();
        public static List<GroupResponse> Groups { get; set; } = new List<Contracts.GroupResponse>();

        public static List<Lookup> Lookups { get; set; } = new List<Lookup>();
        public static List<Tariff> Tariffs { get; set; } = new List<Tariff>();
        public static List<Product> Products { get; set; } = new List<Product>();
        public static List<Product> RedCellsProducts { get; set; } = new List<Product>();
        public static List<string> ThawedPlazmaCodes { get; set; } = new List<string>() { "E8807V00" };
        public static List<string> ThawedCryoprecipitateCodes { get; set; } = new List<string>() { "E8808V00" };
        public static Dictionary<string, string> BloodGroupMappings { get; set; } = new Dictionary<string, string>()
        {
            { "A Pos", "A +" },
            { "A Neg", "A -" },
            { "B Pos", "B +" },
            { "B Neg", "B -" },
            { "AB Pos", "AB+" },
            { "AB Neg", "AB-" },
            { "O Pos", "O +" },
            { "O Neg", "O -" },
            { "-", "   "}
        };
        public static string FacilityCode { get; set; } = "000173";
        public static string DepotCode { get; set; } = "0173";
        public static string CTMCode { get; set; } = "2701";
        public static string HospitalFirstLetterCode { get; set; } = "R";
    }
}