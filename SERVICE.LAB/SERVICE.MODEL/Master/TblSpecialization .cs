using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.Model
{
    public partial class Tblspecialization
    {
        public int specializationNo { get; set; }
        public string specialization { get; set; }
        public bool? status { get; set; }
        public int venueNo { get; set; }
        public int venueBranchno { get; set; }
        public int userNo { get; set; }
    }
    public class SpecializationMasterRequest
    {
        public Int16 specializationNo { get; set; }
        public Int16 venueNo { get; set; }
    }
    public class SpecializationMasterResponse
    {
        public int specializationNo { get; set; }      
    }
}
