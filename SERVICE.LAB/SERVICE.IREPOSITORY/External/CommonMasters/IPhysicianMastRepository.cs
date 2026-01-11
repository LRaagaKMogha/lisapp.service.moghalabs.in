using Service.Model.External.CommonMasters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository.External.CommonMasters
{
    public interface IPhysicianMastRepository
    {
        List<LstPhysician> GetPhysician(int a, int b);
        List<LstInternalPhysician> GetInternalPhysician(int a, int b);
    }
}
