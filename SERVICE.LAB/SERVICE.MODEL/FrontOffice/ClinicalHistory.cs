using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public class ClinicalHistory
    {
        public int PatientVisitNo { get; set; }
        public int GroupNo { get; set; }
        public string GroupName { get; set; }
        public List<ClinicalHistoryMaster> ClinicalHistoryMasters { get; set; }
      
    }
    public class ClinicalHistoryMaster
    {      
        public int MasterNo { get; set; }
        public string MasterName { get; set; }
        public string MasterValue { get; set; }
        public string ControlType { get; set; }

    }
    public class ClinicalHistoryResponse
    {
        public int GroupNo { get; set; }
        public string GroupName { get; set; }
        public int HistoryNo { get; set; }
        public int MasterNo { get; set; }
        public string MasterName { get; set; }
        public string ControlType { get; set; }
        public string MasterValue { get; set; }

    }
    public class InsertClinicalHistory
    {
        public int PatientVisitNo { get; set; }
        public List<ClinicalHistory> ClinicalHistories  { get; set; }
        public int UserNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }

    }
    public class InsertSkinHistory
    {
        public int PatientVisitNo { get; set; }
        public string? ApptNo { get; set; }
        public List<ClinicalHistory>? ClinicalHistories { get; set; }
        public int UserNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }

    }

}
