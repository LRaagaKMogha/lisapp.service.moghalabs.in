using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace Service.Model.Integration
{
    public class FinanceIntegrationMaster
    {
        public int FinanceNo { get; set; } 
        public string FieldName { get; set; }
        public int SeqNo { get; set; }
        public string DefaultValue { get; set; }
        public string FinanceType { get; set; }
    }
    public class FinanceFileExport
    {
        public string FileName { get; set; }
        public string ExportPath { get; set; }
        public DateTime ExportedDateTime { get; set; }
    }
    public class SaleExportRequest
    {
        public DateTime PostingDate { get; set; }

    }
    public class InvoiceExportRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
    public class CustomerExportRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
    public class SaleExportResponse
    {
        public DateTime VisitDTTM { get; set; }
        public int PatientVisitNo { get; set; }
        public int PatientBillNo { get; set; }
        public char ServiceType { get; set; }
        public int ServiceNo { get; set; }
        public decimal Amount { get; set; }
        public int CustomerNo { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public bool IsTaxable { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public bool Status { get; set; }
        public bool IsCancelled { get; set; }

    }
    public class InvoiceExportResponse
    {
        public DateTime VisitDttm { get; set; }
        public DateTime GenerateDTTM { get; set; }
        public int PatientVisitNo { get; set; }
        public int PatientNo { get; set; }
        public char ServiceType { get; set; }
        public string ReceiptNo { get; set; }
        public decimal Amount { get; set; }
        public int CustomerType { get; set; }
        public string VisitID { get; set; }
        public string PatientName { get; set; }
        public string CustomerCode { get; set; }
        public bool IsInternal { get; set; }
        public bool IsTaxable { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
    public class FinanceCustomer
    {
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public long Id { get; set; }
        public string CompanyCode { get; set; }
        public string IdentificationforUniqueID { get; set; }
        public string UniqueID1 { get; set; }
        public string UniqueID2 { get; set; }
        public string Dateofbirth { get; set; }
        public string PatientName { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string BlkHseLotNo { get; set; }
        public string FloorNo { get; set; }
        public string UnitNo { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Firsttelephonenumber { get; set; }
        public string MobileNumber { get; set; }
        public string IsVIP { get; set; }
        public string SendEmailOutstandingAccount { get; set; }
        public string FreezeDateEmailOutstandingAccount { get; set; }
        public string BlockEmailOutstandingReason { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

    }

    public class FinanceSales
    {
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public long Id { get; set; }
        public DateTime PostingDate { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal PackageAmount { get; set; }
        public decimal NonPackageAmount { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

    }
}
