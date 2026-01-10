using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DEV.Model
{
    public partial class TblIPSetting
    {
        [Key]
        public int IPSettingNo { get; set; }
        public int? RCNo { get; set; }
        public int? RCPNo { get; set; }
        public int? PhysicianNo { get; set; }
        public int? DepartmentNo { get; set; }
        public int? ServiceNo { get; set; }
        public string ServiceType { get; set; }
        public decimal? MRPPrice { get; set; }
        public decimal? IPPrice { get; set; }
        public decimal? IPPercentage { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get; set; }
        
    }
    public class IPSettingRequest: TblIPSetting
    {
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
    }
}
