using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
   public class TATResponse
    {
        public int VisitNo { get; set; }
        public string PatientID { get; set; }
        public string VisitID { get; set; }
        public string FullName { get; set; }
        public string AgeGender { get; set; }
        public string ReferredBy { get; set; }
        public string ServiceName { get; set; }
        public string VisitDTTM { get; set; }
        public string RegisteredBy { get; set; }
        public string SampleCollectedDTTM { get; set; }
        public string CollectedBy { get; set; }
        public string EnteredOn { get; set; }
        public string EnteredBy { get; set; }
        public string ApprovedOn { get; set; }
        public string ApprovedBy { get; set; }
        public string TATTime { get; set; }
        public string ActualTime { get; set; }
        public string DifferenceTime { get; set; }
      public Int16  maindeptNo { get; set; }

    }
}
