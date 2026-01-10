using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEV.Model.UserManagement
{
    public class VenueVsMenuRequestDTO
    {
        public int ModuleId { get; set; }
        public int VenueNo { get; set; }
        public int UserNo { get; set; }
    }
    public class VenueVsMenuResponseDTO
    {
        public string ModuleName { get; set; }
        public int MenuNo { get; set; }
        public string MenuName { get; set; }
        public bool Status { get; set; }
    }
    public class VenueVsMenuInsertDTO
    {
        public int UserNo { get; set; }
        public List<VenueVsMenuInsertRowDTO> MenuVenueList { get; set; }
    }
    public class VenueVsMenuInsertRowDTO
    {
        public int VenueNo { get; set; }
        public int MenuNo { get; set; }
        public bool Status { get; set; }
    }
}
