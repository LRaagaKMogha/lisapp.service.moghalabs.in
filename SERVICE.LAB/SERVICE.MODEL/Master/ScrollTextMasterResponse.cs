using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEV.Model.Master
{
    public class ScrollTextMasterResponse
    {
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public int Sno { get; set; }
        public int ScrollTextNo { get; set; }
        public string MessageType { get; set; }
        public int BranchNo { get; set; }
        public string BranchName { get; set; }
        public int MainDepartmentNo { get; set; }
        public string MainDeptName { get; set; }
        public int SubDepartmentNo { get; set; }
        public string SubDeptName { get; set; }
        public string Content { get; set; }
        public string EffFrom { get; set; }
        public string EffTo { get; set; }
        public bool Status { get; set; }

    }
    public class GetScrollTextMasterRequest
    {
        public int userNo { get; set; }
        public int venueNo { get; set; }
        public int venueBranchno { get; set; }
        public int PageIndex { get; set; } = 1;

    }
    public class SaveScrollTextMasterRequest
    {
        public int Sno { get; set; }
        public int ScrollTextNo { get; set; }
        public string MessageType { get; set; }
        public int BranchNo { get; set; }
        public string BranchName { get; set; }
        public int MainDepartmentNo { get; set; }
        public string MainDeptName { get; set; }
        public int SubDepartmentNo { get; set; }
        public string SubDeptName { get; set; }
        public string Content { get; set; }
        public string EffFrom { get; set; }
        public string EffTo { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
    }
    public class SaveScrollTextMasterResponse
    {
        public int Result { get; set; }

    }
}
