using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public class AuditLogDTO
    {
        public int pageIndex { get; set; }
        public Int32 TotalRecords { get; set; }
        public Int64 RowNum { get; set; }
        public int AuditLogNo { get; set; }
        public string PatientId { get; set; }
        public int PatientNo { get; set; }
        public int PatientVisitNo { get; set; }
        public string VisitID { get; set; }
        public string VisitDTTM { get; set; }
        public string PatientName { get; set; }
        public int AuditTypeNo { get; set; }
        public string AuditTypeName { get; set; }
        public string AuditLogDate { get; set; }
        public string Comments { get; set; }
    }
    public class AuditLogResponse
    {
        public int pageIndex { get; set; }
        public Int32 TotalRecords { get; set; }
        public Int64 AuditLogNo { get; set; }
        public string PatientId { get; set; }
        public int PatientNo { get; set; }
        public int PatientVisitNo { get; set; }
        public string VisitID { get; set; }
        public string VisitDTTM { get; set; }
        public string PatientName { get; set; }
        public List<AuditDetailDTO> Auditdetail { get; set; }
    }
    public class AuditDetailDTO
    {
        public Int64 AuditLogNo { get; set; }
        public string AuditLogDate { get; set; }
        public int AuditTypeNo { get; set; }
        public string AuditTypeName { get; set; }
        public string Comments { get; set; } 
     
    }
    public class AuditHistory
    {
        public Int32 AuditLogNo { get; set; }
        public string LogData { get; set; }
    }
    public class ApprovalHistory
    {
        public Int32 ApprovalHistoryNo { get; set; }
        public string LogData { get; set; }
    }
}


