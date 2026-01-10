using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public class GetAssetManagementResponse
    {
        public int TotalRecords { get; set; }        
        public int InstrumentsNo { get; set; }
        public string InstrumentsName { get; set; }
        public string InstallationDate { get; set; }
        public string ModificationDate { get; set; }
        public int branchNo { get; set; }
        public string branchName { get; set; }
        public int DepartmentNo { get; set; }
        public string DepartmentName { get; set; }
        public string Remark { get; set; }
       // public string DocumentPath { get; set; }
        public string manufacturerName { get; set; }
        //

        public string ContactPersonName { get; set; }
        public string MobileNo { get; set; }
        public string CallCenterContactNumber { get; set; }
        public string Email { get; set; }
        public string RequestRaised { get; set; }
        public string ResolvedDate { get; set; }
        public string RequestRemarks { get; set; }
        public string AssetNo { get; set; }
        public string MachineSerialNo { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public bool? Status { get; set; }
        public int pageIndex { get; set; }

    }
}


