using DEV.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository
{
    public interface IExternalRepository
    {
        int PostResult(ExternalResultDTO results);
        int PostCultureResult(ExternalBulkCultureResultDTO results);
        List<ExternalResultCalculation> GetExternalFormulaOrderDetails(ExternalResultCalculationRequest req);
        double GetCalResult(string req);
        int PostDifCultureResult(ExternalDifBulkCultureResultDTO results);
        Boolean CheckFormulaIsAvailable_ForCalculation(ExternalResultCalculationRequest req);
        int ValidateAutoApporval(ExternalResultDTO req);
        List<CommonMasterDto> GetEGFRList(int venueno, int venuebranchno, string MasterKey);
    }
}