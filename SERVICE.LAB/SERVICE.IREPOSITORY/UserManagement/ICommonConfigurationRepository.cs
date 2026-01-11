using Service.Model;
using Service.Model.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.IRepository.UserManagement
{
    public interface ICommonConfigurationRepository
    {
        List<CommonConfigurationResponseDTO> GetCommonConfiguration(CommonConfigurationRequestDTO request);
        int InsertCommonConfiguration(CommonConfigurationInsertDTO request);
        List<CommonMasterDto> GetAllBranches(CommonMasterRequestDTO request);
    }
}
