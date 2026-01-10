using DEV.Model.External.CommonMasters;
using DEV.Model.External.CommonReports;
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
