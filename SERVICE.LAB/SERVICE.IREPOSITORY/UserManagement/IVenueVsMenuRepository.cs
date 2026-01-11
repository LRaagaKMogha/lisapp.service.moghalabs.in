using Service.Model.Inventory;
using Service.Model.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.IRepository.UserManagement
{
    public interface IVenueVsMenuRepository
    {
        List<VenueVsMenuResponseDTO> GetVenueVsMenu(VenueVsMenuRequestDTO request);
        int InsertVenueVsMenu(VenueVsMenuInsertDTO request);
    }
}
