using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace DEV.Common
{
    /// <summary>  
    /// Different types of exceptions.  
    /// </summary>  

    public enum Exceptions
    {
        NullReferenceException = 1,
        FileNotFoundException = 2,
        OverflowException = 3,
        OutOfMemoryException = 4,
        InvalidCastException = 5,
        ObjectDisposedException = 6,
        UnauthorizedAccessException = 7,
        NotImplementedException = 8,
        NotSupportedException = 9,
        InvalidOperationException = 10,
        TimeoutException = 11,
        ArgumentException = 12,
        FormatException = 13,
        StackOverflowException = 14,
        SqlException = 15,
        IndexOutOfRangeException = 16,
        IOException = 17
    }
    public enum TableType
    {
        Tooltable = 1,
        ContentTable = 2

    }
    public enum ExceptionPriority
    {
        High = 1,
        Medium = 2,
        Low = 3
    }
    public enum ApplicationType
    {
        APPSERVICE = 1,
        WINSERVICE = 2,
        REPOSITORY = 3,
        EXTERNALAPPSERVICE = 4,
    }
    public static class CacheKeys
    {
        public const string DepartmentMaster = "tbl_DepartmentMaster";
        public const string SpecimenMaster = "tbl_SpecimenMaster";
        public const string ContainerMaster = "CONTAINER";
        public const string UserMaster = "tblUserMaster";
        public const string VenueMaster = "tblVenueMaster";
        public const string CommonMaster = "CommonMaster";
        public const string ConfigurationMaster = "ConfigurationMaster";
        public const string CountryMaster = "CountryMaster";
        public const string StateMaster = "StateMaster";
        public const string CityMaster = "CityMaster";
        public const string DiscountMaster = "DiscountMaster";
        public const string UnitMaster = "tbl_UnitMaster";
        public const string CurrencyMaster = "TblCurrency";
        public const string CustomerMaster = "tblCustomer";
        public const string tblRiderList = "tblRiderList";
        public const string ServiceList = "ServiceList";
        public const string tblDepartmentList = "tblDepartmentList";
        public const string tblMethodList = "tblMethodList";
        public const string tblUnitsList = "tblUnitsList";
        public const string tblOrganismList = "tblOrganismList";
        public const string tblOrgTypeAntiMapList = "tblOrgTypeAntiMapList";
        public const string tblTemplateList = "tblTemplateList";
        public const string PhysicianMaster = "PhysicianMaster";
        public const string TariffMaster = "TariffMaster";
        public const string SampleMaster = "tbl_Sample";
        public const string UserdetailMaster = "UserdetailMaster";
        public const string UserMenu = "UserMenu";
        public const string tblGroupList = "tblGroupList";
        public const string RouteMaster = "tbl_Route";
        public const string POBySupplier = "POBySupplier";
        public const string Tblspecialization = "Tblspecialization";
        public const string TblMainDepartment = " TblMainDepartment";
        public const string AnalyzerMaster = "tbl_Analyzer";
        public const string UserMenuCode = "UserMenuCode";
        public const string PARAMNAME = "PARAMNAME";
        public const string RefTypeList = "RefTypeList";
    }
    public static class FileFormat
    {
        public const string PDF = "PDF";
        public const string EXCEL = "EXCEL";
    }
    public class Suffix
    {
        public string? option { get; set; }
    }
    public class PostGoogleUrl
    {
        public string? longDynamicLink { get; set; }
        public Suffix? suffix { get; set; }
    }
    public class GoogleResponse
    {
        public string? shortLink { get; set; }
        public string? previewLink { get; set; }
    }
    public static class ConfigKeys
    {
        public const string DefaultConnection = "DefaultConnection";
        public const string ArchiveDefaultConnection = "ArchiveDefaultConnection";
        public const string ReportServiceURL = "ReportServiceURL";
        public const string FireBaseAPIkey = "FireBaseAPIkey";
        public const string PortalUrl = "portalURL";
        public const string Defaultpassword = "defaultPassword";
        public const string cacheMemoryTime = "CacheMemoryTime";
        public const string MasterFilePath = "MasterFilePath";
        public const string TransFilePath = "TransFilePath";
        public const string TransTemplateFilePath = "TransTemplateFilePath";
        public const string PatientportalURL = "PatientportalURL";
        public const string BulkRegistrationTimeout = "BulkRegistrationTimeout";
        public const string UploadPathInit = "UploadPathInit";
        public const string DevExpressEditorConfig = "DevExpressEditorConfig";
        public const string MultiTemplateFormat = "MultiTemplateFormat";
        public const string ResultAckUpload = "ResultAckUpload";
        public const string PatientMasterUpload = "PatientMasterUpload";
        public const string HCPrescriptionPath = "HCPrescriptionPath";
        public const string MachineImagePath = "MachineImagePath";
        public const string MachineImagePathWidth = "MachineImagePathWidth";
        public const string MachineImageSeparation = "MachineImageSeparation";
        public const string IsReportWithBilAtach = "IsReportWithBilAtach";
        public const string UploadPhysicianDoc = "UploadPhysicianDoc";
        public const string UploadClientDoc = "UploadClientDoc";
        public const string UploadPatientDiseasePath = "PatientDisease";
        public const string UploadOPDImaging = "UploadOPDImaging";
        public const string OPDMasterTemplateFilePath = "OPDMasterTemplateFilePath";
        public const string OPDTransTemplateFilePath = "OPDTransTemplateFilePath";
        public const string DocumentDBConnection = "DocumentDBConnection";
    }
    public static class FileExtension
    {
        public const string PDF = ".pdf";
        public const string EXCEL = ".xls";
        public const string RDLC = ".rdlc";
        public const string HTML = ".html";
        public const string TXT = ".txt";
        public const string PNG = ".png";
        public const string CLASS = ".cs";
    }
    public static class ReportKey
    {
        public const string PATIENTBILL = "PATIENTBILL";
        public const string PATIENTREPORT = "PATIENTREPORT";
        public const string MBPATIENTREPORT = "MBPATIENTREPORT";
        public const string TEMPPATIENTREPORT = "TEMPPATIENTREPORT";
        public const string TRANSACTIONMISREPORT = "TRANSACTIONMISREPORT";
        public const string MTEMPPATIENTREPORT = "MTEMPPATIENTREPORT";
        public const string LANGPATIENTREPORT = "LANGPATIENTREPORT";
        public const string LANGMBPATIENTREPORT = "LANGMBPATIENTREPORT";
        public const string LANGTEMPPATIENTREPORT = "LANGTEMPPATIENTREPORT";
        public const string LANGMTEMPPATIENTREPORT = "LANGMTEMPPATIENTREPORT";
        public const string TRNDPATIENTREPORT = "TRNDPATIENTREPORT";
        public const string AMENDEDPATIENTREPORT = "AMENDEDPATIENTREPORT";
        public const string AMENDEDMBPATIENTREPORT = "AMENDEDMBPATIENTREPORT";
        public const string AMENDEDTEMPPATIENTREPORT = "AMENDEDTEMPPATIENTREPORT";
        public const string PATIENTHCBILL = "PATIENTHCBILL";
    }
    public static class Constants
    {
        public const string LocaleTN = "LANGTN";
    }
    public static class ReportType
    {
        public const string BILL = "BILL";
    }
    public static class SessionContext
    {
        public const int VenueNo = 1;
        public const int VenueBranchNo = 1;
    }
    public enum LoginType
    {
        CUSTOMER = 1,
        LABORATORY = 2,
        PATIENT = 3,
        FRANCHISEE = 4,
        CUSTOMERSUBUSER = 5,
        PHYSICIAN = 7
    }
    public static class CommonExtension
    {
        public static string ValidateEmpty(this string value)
        {
            return string.IsNullOrEmpty(value) ? string.Empty : value;
        }
        public static List<Dictionary<String, Object>> DatableToDicionary(DataTable dataTable)
        {
            List<Dictionary<String, Object>> tableRows = new List<Dictionary<String, Object>>();
            Dictionary<String, Object> row;
            foreach (DataRow dr in dataTable.Rows)
            {
                row = new Dictionary<String, Object>();
                foreach (DataColumn col in dataTable.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                tableRows.Add(row);
            }
            return tableRows;
        }
        public static string ToSubstring(this string s, int length)
        {
            if (s != null)
            {
                if (s.Length > length)
                    return s.Substring(0, length);
            }
            else
                s = string.Empty;
            return s;
        }
    }
    public enum Weekdays
    {
        Sun = 1,
        Mon = 2,
        Tue = 3,
        Wed = 4,
        Thu = 5,
        Fri = 6,
        Sat = 7
    }
    public static class OPDReportKey
    {
        public const string OPDCONSULTATIONREPORT = "OPDCONSULTATIONREPORT";
        public const string TMPOPDCONSULTATIONREPORT = "TMPOPDCONSULTATIONREPORT";
    }
}
