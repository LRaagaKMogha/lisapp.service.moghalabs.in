using System;
using System.Collections.Generic;
using System.Text;
using Dev.IRepository;
using DEV.Model;
using DEV.Model.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using DEV.Common;
using Microsoft.Data.SqlClient;

namespace Dev.Repository
{
    public class SubtestheaderRepository : ISubtestheaderRepository
    {
        private IConfiguration _config;
        public SubtestheaderRepository(IConfiguration config) { _config = config; }

        public List<TblSubtestheader> GetSubtestheadermaster(SubtestheaderMasterRequest subtestheaderMasterRequest)
        {
            List<TblSubtestheader> objresult = new List<TblSubtestheader>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _headerNo = new SqlParameter("headerNO", subtestheaderMasterRequest?.headerNo);
                    var _venueNo = new SqlParameter("venueNo", subtestheaderMasterRequest?.venueNo);
                    var _venueBranchno = new SqlParameter("venueBranchno", subtestheaderMasterRequest?.venueBranchno);
                    var _pageIndex = new SqlParameter("pageIndex", subtestheaderMasterRequest?.pageIndex);

                    objresult = context.Getheader.FromSqlRaw(
                        "Execute dbo.pro_GetSubtestheadermaster @headerNO,@venueNo,@venueBranchno,@pageIndex",
                         _headerNo, _venueNo, _venueBranchno, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "SubtestheaderRepository.GetSubtestheadermaster" + subtestheaderMasterRequest.headerNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, subtestheaderMasterRequest.venueNo, subtestheaderMasterRequest.venueBranchno, 0);
            }
            return objresult;
        }
        public SubtestheaderMasterResponse InsertSubtestheadermaster(TblSubtestheader testheader)
        {
            SubtestheaderMasterResponse objresult = new SubtestheaderMasterResponse();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _headerNo = new SqlParameter("headerNo", testheader?.headerNo);
                    var _headerdisplaytext = new SqlParameter("headerdisplaytext", testheader?.headerdisplaytext);
                    var _headerName = new SqlParameter("headerName", testheader?.headerName);
                    var _venueBranchno = new SqlParameter("venueBranchno", testheader?.venueBranchno);
                    var _venueNo = new SqlParameter("venueNo", testheader?.venueNo);
                    var _sequenceNo = new SqlParameter("sequenceNo", testheader?.sequenceNo);
                    var _status = new SqlParameter("status", testheader?.status);
                    var _userNo = new SqlParameter("userNo", testheader?.userNo);

                    objresult = context.Insertheader.FromSqlRaw(
                    "Execute dbo.pro_InsertSubtestheader @headerNo,@headerdisplaytext,@headerName," +
                    "@venueBranchNo,@venueNo, @sequenceno,@status,@userNo",
                    _headerNo, _headerdisplaytext, _headerName, _venueBranchno, _venueNo, _sequenceNo, _status, _userNo).AsEnumerable().FirstOrDefault();                    
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "SubtestheaderRepository.InsertSubtestheadermaster - " + testheader.headerNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, testheader.venueNo, testheader.venueBranchno, 0);
            }
            return objresult;
        }
    }
}