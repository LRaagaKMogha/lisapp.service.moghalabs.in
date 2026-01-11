using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public class GetArchivePatientResponse
    {        
        public int PatientNo { get; set; }
        public string? PatientID { get; set; }
        public string? FullName { get; set; }
        public string? MobileNumber { get; set; }
        public int PatientVisitNo { get; set; }
        public string? VisitID { get; set; }
        public string? VisitDTTM { get; set; }
        public string? RefferralType { get; set; }
        public string? CustomerName { get; set; }
        public string? PhysicianName { get; set; }
        public string? ResultTypeNo { get; set; }
    }

    public class GetArchivePatientRequest
    {
       
        public int PatientVisitNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int UserNo { get; set; }

    }
}


