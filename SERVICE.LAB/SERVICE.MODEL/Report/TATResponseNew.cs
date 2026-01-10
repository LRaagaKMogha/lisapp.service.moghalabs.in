using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
   public class TATResponseNew
    {
        public int departmentNo { get; set; }
        public string departmentName { get; set; }
        public int total { get; set; }
        public int pIntat { get; set; }
        public int pOutTat { get; set; }
        public int cIntat { get; set; }
        public int cOutTat { get; set; }
        public int pNearTat { get; set; }
        public string type { get; set; }
    }

    public class TATReportDetailsResponse
    {
        public int Sno { get; set; }
        public string type { get; set; }
        public string departmentName { get; set; }
        public string patientNo { get; set; }
        public string patientVisitNo { get; set; }
        public string primaryId { get; set; }
        public string patientName { get; set; }
        public string age { get; set; }
        public string gender { get; set; }
        public string refferedBy { get; set; }
        public string regDate { get; set; }
        public string collectionDate { get; set; }
        public int serviceNo { get; set; }
        public string serviceName { get; set; }
    }
}

