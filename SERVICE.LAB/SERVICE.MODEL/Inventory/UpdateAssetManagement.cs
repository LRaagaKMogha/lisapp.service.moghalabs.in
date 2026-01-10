using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public class UpdateAssetManagement
    {
        // public int TotalRecords { get; set; }
        public int pageIndex { get; set; }
        public int instrumentsNo { get; set; }
        public string instrumentsName { get; set; }
        public string installationDate { get; set; }
        public string modificationDate { get; set; }
        public string remark { get; set; }
        //public string documentPath { get; set; }
        public int venueNo { get; set; }
        public int venueBranchno { get; set; }
        public int userNo { get; set; }
        public string manufacturerName { get; set; }
        //
        public string ContactPersonName { get; set; }
        public string MobileNo { get; set; }
        public string CallCenterContactNumber { get; set; }
        public string Email { get; set; }
        public string RequestRaised { get; set; }
        public string ResolvedDate { get; set; }
        public string RequestRemarks { get; set; }
        public bool? Status { get; set; }

    }

    public class postAssetManagementDTO
    {
        // public int TotalRecords { get; set; }
        public int InstrumentsNo { get; set; }
        public string InstrumentsName { get; set; }
        public int venueNo { get; set; }
        public int venueBranchno { get; set; }
        public int userNo { get; set; }
        public int branchNo { get; set; }
        public int DepartmentNo { get; set; }
        public string InstallationDate { get; set; }
        public string ModificationDate { get; set; }
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
        public bool? Status { get; set; }
        public int pageIndex { get; set; }
    }

}

