using Service.Model;
using Service.Model.UserManagement;
using System.Collections.Generic;

namespace Service.IRepository.UserManagement
{
    public interface ICommonConfigurationRepository
    {
        List<CommonConfigurationResponseDTO> GetCommonConfiguration(CommonConfigurationRequestDTO request);
        int InsertCommonConfiguration(CommonConfigurationInsertDTO request);
        List<CommonMasterDto> GetAllBranches(CommonMasterRequestDTO request);
    }
}
