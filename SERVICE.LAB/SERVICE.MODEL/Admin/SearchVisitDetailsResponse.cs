using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
   public class SearchVisitDetailsResponse
    {
        public Int64 Row_num { get; set; }
        public string? PrimaryId { get; set; }
        public string? PatientName { get; set; }
        public string? Age { get; set; }
        public int VisitNo { get; set; }
        public string? VisitId { get; set; }
        public string? RegistrationDate { get; set; }
        public string? RefferedBy { get; set; }
        public int RefferedByNo { get; set; }
        public string? CustomerName { get; set; }
        public int TestNo { get; set; }
        public string? TestName { get; set; }     
        public string? OrderStatus { get; set; }
        public string? collectionDt { get; set; }
        public string? approvalDt { get; set; }

    }

    public class SearchUpdateDatesResponse
    {
        public Int64 Row_num { get; set; }
        public string? PrimaryId { get; set; }
        public string? PatientName { get; set; }
        public int PatientVisitNo { get; set; }
        public string? VisitId { get; set; }
        public string? RegistrationDate { get; set; }
        public string? collectionDt { get; set; }
        public string? approvalDt { get; set; }
        public int RefferralTypeNo { get; set; }
        public int RefferralNo { get; set; }
        public int PhysicianNo { get; set; }

    }


}


