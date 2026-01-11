using Service.Model.External.CommonMasters;
using Service.Model.External.CommonReports;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.IRepository.External.CommonReports
{
    public interface ICommonReportMISRepository
    {
        List<LstPaidInformation> GetPaymentCollection(int a, int b, string dtFrom, string dtTo);
    }
}
