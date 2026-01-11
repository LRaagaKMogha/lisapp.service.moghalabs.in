using Service.Model;
using Service.Model.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Dev.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DEV.Common;
using Serilog;
using Microsoft.Data.SqlClient;

namespace Dev.Repository
{
    public class TitleRepository : ITitleRepository
    {
        private IConfiguration _config;
        public TitleRepository(IConfiguration config) { _config = config; }
        public List<TblTitle> GettitleDetails(TitlemasterRequest titlemaster)
        {
            List<TblTitle> titleresult = new List<TblTitle>();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    
                    var _commonBranchNo = new SqlParameter("commonBranchNo", titlemaster?.commonBranchNo);
                    var _venueNo = new SqlParameter("venueNo", titlemaster?.venueNo);
                    var _venueBranchno = new SqlParameter("venueBranchno", titlemaster?.venueBranchno);
                    var _CommonNo = new SqlParameter("CommonNo", titlemaster?.CommonNo);
                   

                    titleresult = context.GetTitle.FromSqlRaw(
                       "Execute dbo.pro_GetCommanMaster @commonBranchNo,@venueNo,@venueBranchno,@CommonNo", 
                        _commonBranchNo, _venueNo, _venueBranchno,_CommonNo).ToList();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TitleRepository. GettitleDetails" + titlemaster.commonBranchNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, titlemaster.venueNo, titlemaster.venueBranchno, 0);
            }
            return titleresult;
        }


        public Titlemasterresponse InsertTitlemaster(TblName tbltitle)
        {
            Titlemasterresponse objresult = new Titlemasterresponse();
            try
            {
                using (var context = new LIMSContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _CommonNo = new SqlParameter("CommonNo", tbltitle?.CommonNo);
                    var _CommonCode= new SqlParameter("CommonCode", tbltitle?.CommonCode);
                    var _IsDefault = new SqlParameter("IsDefault", tbltitle?.IsDefault);
                    var _commonBranchNo = new SqlParameter("commonBranchNo", tbltitle?.commonBranchNo);
                    var _venueNo = new SqlParameter("venueNo", tbltitle?.venueNo);
                    var _venueBranchno = new SqlParameter("venueBranchno", tbltitle?.venueBranchno);
                    var _userNo = new SqlParameter("userNo", tbltitle?.userNo);
                    var _commonValue = new SqlParameter("commonValue", tbltitle?.commonValue);
                    var _sequenceNo = new SqlParameter("sequenceNo", tbltitle?.sequenceNo);
                    var _status = new SqlParameter("status", tbltitle?.status);

                    
                    var obj = context.InsertTitle.FromSqlRaw(
                           "Execute dbo.pro_InsertCommanmaster @CommonNo, @CommonCode,@IsDefault, @commonBranchNo,@venueNo,@venueBranchno,@userNo,@commonValue,@sequenceNo,@status",
                               _CommonNo,_CommonCode, _IsDefault,_commonBranchNo, _venueNo, _venueBranchno, _userNo, _commonValue,
                                _sequenceNo, _status).ToList();


                    objresult.commonBranchNo = obj[0].commonBranchNo;
                }
            }
            catch (Exception ex)
            {

                MyDevException.Error(ex, "TitleRepository.InsertTitlemaster" + tbltitle.CommonNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, tbltitle.venueNo, tbltitle.venueBranchno, 0);
            }
            return objresult;

        }
    }
}




