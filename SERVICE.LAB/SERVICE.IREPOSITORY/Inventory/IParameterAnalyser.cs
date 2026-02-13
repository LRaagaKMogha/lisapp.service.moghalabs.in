using Service.Model;
using System.Collections.Generic;

namespace Service.IRepository.Inventory
{
    public interface IParameterAnalyserRepositoty
    {
        List<GetParameterAnalyserResponse> GetAllParameterAnalyser(GetAllParameterAnalyserRequest request);
        CommonAdminResponse InsertParameterAnalyser(InsertParameterAnalyser insertConsumption);
    }
}

