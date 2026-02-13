using Service.Model.External.CommonMasters;
using System.Collections.Generic;

namespace Service.IRepository.External.CommonMasters
{
    public interface IPhysicianMastRepository
    {
        List<LstPhysician> GetPhysician(int a, int b);
        List<LstInternalPhysician> GetInternalPhysician(int a, int b);
    }
}
