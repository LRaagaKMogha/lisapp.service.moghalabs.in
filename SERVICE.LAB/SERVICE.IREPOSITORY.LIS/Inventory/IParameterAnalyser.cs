using DEV.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository.Inventory
{
    public interface IParameterAnalyserRepositoty
    {
        List<GetParameterAnalyserResponse> GetAllParameterAnalyser(GetAllParameterAnalyserRequest request);
        CommonAdminResponse InsertParameterAnalyser(InsertParameterAnalyser insertConsumption);
    }
}

