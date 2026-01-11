using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public partial class requestquotation
    {
        public Int16 venueno { get; set; }
        public int venuebranchno { get; set; }
        public int quotationMasterNo { get; set; }
        public int pageIndex { get; set; }
    }
    public class returnquotationlst
    {
        public int quotationMasterNo { get; set; }
        public string quotationDtTm { get; set; }
        public string quotationId { get; set; }
        public int patientNo { get; set; }
        public string titleCode { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string fullName { get; set; }
        public string gender { get; set; }
        //public DateTime dOB { get; set; }
        public Byte ageY { get; set; }
        public Byte ageM { get; set; }
        public Byte ageD { get; set; }
        public string mobileNumber { get; set; }
        public string emailID { get; set; }
        public Int16 venueno { get; set; }
        public int venuebranchno { get; set; }
        public bool status { get; set; }
        public int userno { get; set; }
        public int refferralTypeNo { get; set; }
        public int customerNo { get; set; }
        public int physicianNo { get; set; }
        public string physicianName { get; set; }
        public string customerName { get; set; }
        public string referer { get; set; }
        public int pageIndex { get; set; }
        public int TotalRecords { get; set; }

    }

    public class responselst
    {
        public int quotationMasterNo { get; set; }
        public string quotationDtTm { get; set; }
        public string quotationId { get; set; }
        public int patientNo { get; set; }
        public string titleCode { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string fullName { get; set; }
        public string gender { get; set; }
        public string dOB { get; set; }
        public Byte ageY { get; set; }
        public Byte ageM { get; set; }
        public Byte ageD { get; set; }
        public string mobileNumber { get; set; }
        public string emailID { get; set; }
        public Int16 venueno { get; set; }
        public int venuebranchno { get; set; }
        public bool status { get; set; }
        public int userno { get; set; }
        public int refferralTypeNo { get; set; }
        public int customerNo { get; set; }
        public int physicianNo { get; set; }
        public string physicianName { get; set; }
        public string customerName { get; set; }
        public List<gettestlst> gettestlst { get; set; }
        public int quotationOrderListNo { get; set; }
        public string quotationNo { get; set; }
    }
    public class gettestlst
    {
        public int quotationOrderListNo { get; set; }
        public int quotationMasterNo { get; set; }
        public int serviceno { get; set; }
        public Decimal amount { get; set; }
        public string servicetype { get; set; }
        public bool status { get; set; }
    }
    public class storequotationlst
    {
        public int quotationMasterNo { get; set; }
       
    }
     
}
