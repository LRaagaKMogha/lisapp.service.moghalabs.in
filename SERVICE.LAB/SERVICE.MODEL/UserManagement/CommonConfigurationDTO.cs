using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEV.Model.UserManagement
{
    public class CommonConfigurationRequestDTO
    {
        public int VenueNo {  get; set; }
        public int VenueBranchNo {  get; set; }
        public int UserNo {  get; set; }
    }
    public class CommonConfigurationResponseDTO
    {
        public int BranchConfigurationNo {  get; set; }
        public int ConfigurationNo {  get; set; }
        public string ConfigurationKey {  get; set; }
        public string Description { get; set; }
        public int ConfigValue {  get; set; }
    }

    public class CommonConfigurationInsertDTO
    {
        public int UserNo { get; set; }
        public List<CommonConfigurationInsertRowDTO> CommonConfigurationList { get; set; }
    }
    public class CommonConfigurationInsertRowDTO
    {
        public int BranchConfigurationNo{ get; set; }
        public int ConfigurationNo{ get; set; }
        public int ConfigValue { get; set; }
        public int VenueNo { get; set; }
        public int VenueBranchNo { get;set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
    public class CommonMasterRequestDTO
    {
        public int VenueNo { get; set; }
    }
}
