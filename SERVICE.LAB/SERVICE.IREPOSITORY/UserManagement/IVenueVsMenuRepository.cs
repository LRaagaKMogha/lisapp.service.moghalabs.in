using Service.Model.UserManagement;
using System.Collections.Generic;

namespace Service.IRepository.UserManagement
{
    public interface IVenueVsMenuRepository
    {
        List<VenueVsMenuResponseDTO> GetVenueVsMenu(VenueVsMenuRequestDTO request);
        int InsertVenueVsMenu(VenueVsMenuInsertDTO request);
    }
}
