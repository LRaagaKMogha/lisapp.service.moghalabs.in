using Service.Model.External.CommonReports;
using System.Collections.Generic;

namespace Service.IRepository.External.CommonReports
{
    public interface ICommonReportMISRepository
    {
        List<LstPaidInformation> GetPaymentCollection(int a, int b, string dtFrom, string dtTo);
    }
}
