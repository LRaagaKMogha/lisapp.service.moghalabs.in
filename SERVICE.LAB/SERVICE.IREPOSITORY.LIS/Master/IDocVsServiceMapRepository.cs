using System;
using System.Collections.Generic;
using System.Text;
using DEV.Model;

namespace Dev.IRepository
{
    public interface IDocVsServiceMapRepository
    {

        List<DocVsSerResponse> Getdoctorlst(DocVsSerRequest Req);
        List<DocVsSerGetRes> GetdocVsSerlst(DocVsSerGetReq Req);
        int InsertdocVsSer(DocVsSerInsReq Req);
        List<DocVsSerAppRes> GetdocVsSerApproval(DocVsSerAppReq Req);
        List<DocVsSerAppdetailsRes> GetdocVsSerAppDetails(DocVsSerAppdetailsReq Req);
        int InsertdocVsSerProf(DocVsSerProfInsReq Req);
    }

}
