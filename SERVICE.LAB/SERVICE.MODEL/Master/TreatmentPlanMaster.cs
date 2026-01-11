using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model.Master
{
    public partial class TreatmentPlanMaster
    {
        public int treatmentNo { get; set; }
        public string? treatmentName { get; set; }
        public int diseaseNo { get; set; }
        public string? diseaseName { get; set; }
        public List<TreatmentPlanProMaster> lstProcedures { get; set; }
        public List<TreatmentPlanPrmMaster> lstpharmacy { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int UserNo { get; set; }
        public decimal rate { get; set; }
    }
    public partial class TreatmentPlanProMaster
    {
        public int treatmentPlanProceduresNo { get; set; }
        public string? type { get; set; }
        public int testNo { get; set; }
        public string? testName { get; set; }
        public int scheduleEveryNo { get; set; }
        public string? scheduleEveryName { get; set; }
        public int frequencyNo { get; set; }
        public string? frequencyName { get; set; }
        public int daySunday { get; set; }
        public int dayMonday { get; set; }
        public int dayTuesday { get; set; }
        public int dayWednesday { get; set; }
        public int dayThursday { get; set; }
        public int dayFriday { get; set; }
        public int daySaturday { get; set; }
        public int totalTreatments { get; set; }
        public int performPhysicianNo { get; set; }
        public string? performPhysicianName { get; set; }
        public decimal rate { get; set; }
        public decimal totalRate { get; set; }
    }
    public partial class TreatmentPlanMasterResponse
    {
        public int treatmentNo { get; set; }
    }
    public partial class TreatmentPlanPrmMaster
    {
        public int treatmentPlanPharmacyNo { get; set; }
        public string? type { get; set; }
        public int productMasterNo { get; set; }
        public string? productMasterName { get; set; }
        public int daily { get; set; }
        public int am { get; set; }
        public int pm { get; set; }
        public int weekly { get; set; }
        public int asNeeded { get; set; }
    }
    public partial class TreatmentDelMaster
    {
        public int treatmentNo { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int UserNo { get; set; }
    }

    public partial class reqTreatmentMaster
    {
        public int treatmentNo { get; set; }
        public string? treatmentName { get; set; }
        public int diseaseNo { get; set; }
        public string? diseaseName { get; set; }
        public bool status { get; set; }
        public Int16 VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public decimal Rate { get; set; }
    }
}
