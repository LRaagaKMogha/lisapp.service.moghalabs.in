using Dev.IRepository.Inventory;
using DEV.Common;
using Service.Model;
using Service.Model.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dev.Repository.Inventory
{
    public class ParameterAnalyserRepositoty: IParameterAnalyserRepositoty
    {
        private IConfiguration _config;
        public ParameterAnalyserRepositoty(IConfiguration config) { _config = config; }
        public List<GetParameterAnalyserResponse> GetAllParameterAnalyser(GetAllParameterAnalyserRequest request)
        {
            List<GetParameterAnalyserResponse> objresult = new List<GetParameterAnalyserResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", request.venueno);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", request.venuebranchno);
                    var _branchNo = new SqlParameter("BranchNo", request.BranchNo);
                    var _analyserNo = new SqlParameter("AnalyserNo", request.AnalyserNo);
                    var _parameterNo = new SqlParameter("ParameterNo", request.ParameterNo);
                    var _testNo = new SqlParameter("TestNo", request.TestNo);
                    var _subTestNo = new SqlParameter("SubTestNo", request.SubTestNo);
                    var _userNo = new SqlParameter("UserNo", request.userno);
                    var _pageIndex = new SqlParameter("PageIndex", request.pageIndex);

                    objresult = context.GetParameterAnalyserDTO.FromSqlRaw(
                    "Execute dbo.pro_GetAllParameterAnalyser @VenueNo,@VenueBranchNo,@BranchNo,@AnalyserNo,@ParameterNo,@TestNo,@SubTestNo,@UserNo,@PageIndex",
                    _venueNo, _venueBranchNo, _branchNo, _analyserNo, _parameterNo, _testNo, _subTestNo, _userNo, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ParameterAnalyserRepositoty.GetAllParameterAnalyser", ExceptionPriority.High, ApplicationType.REPOSITORY, request.venueno, (int)request.venuebranchno, (int)request.masterNo);
            }
            return objresult;
        }

        public CommonAdminResponse InsertParameterAnalyser(InsertParameterAnalyser insertParameterAnalyser)
        {
            CommonAdminResponse response = new CommonAdminResponse();

            CommonHelper commonUtility = new CommonHelper();
            var consumptionXML = commonUtility.ToXML(insertParameterAnalyser);

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", insertParameterAnalyser?.venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", insertParameterAnalyser?.venueBranchNo);
                    var _ConsumptionXML = new SqlParameter("ConsumptionXML", consumptionXML);
                    var _UserNo = new SqlParameter("UserNo", insertParameterAnalyser?.createdby);

                    var objresult = context.CreateParameterAnalyserDTO.FromSqlRaw(
                    "Execute dbo.Pro_InsertParameterAnalyser @VenueNo,@VenueBranchNo,@ConsumptionXML,@UserNo",
                    _VenueNo, _VenueBranchNo, _ConsumptionXML, _UserNo).ToList();
                    
                    response = objresult[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ParameterAnalyserRepositoty.InsertParameterAnalyser", ExceptionPriority.Low, ApplicationType.REPOSITORY, insertParameterAnalyser.venueNo, insertParameterAnalyser.venueBranchNo, insertParameterAnalyser.createdby);
            }
            return response;
        }
    }
}
