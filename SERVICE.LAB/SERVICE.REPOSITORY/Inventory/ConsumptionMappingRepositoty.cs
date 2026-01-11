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
    public class ConsumptionMappingRepositoty: IConsumptionMappingRepositoty
    {
        private IConfiguration _config;
        public ConsumptionMappingRepositoty(IConfiguration config) { _config = config; }
        public List<GetConsumptionMappingResponse> GetAllConsumptionMapping(GetAllConsumptionMappingRequest request)
        {
            List<GetConsumptionMappingResponse> objresult = new List<GetConsumptionMappingResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _venueNo = new SqlParameter("VenueNo", request.venueno);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", request.venuebranchno);
                    var _consumptionNo = new SqlParameter("MasterNo", request.masterNo);
                    var _userNo = new SqlParameter("UserNo", request.userno);
                    var _pageIndex = new SqlParameter("PageIndex", request.pageIndex);
                    var _analyzerMasterNo = new SqlParameter("AnalyzerMasterNo", request.AnalyzerNo);
                    var _parameterNo = new SqlParameter("AnalyzerParamNo", request.ParameterNo);
                    var _unitNo = new SqlParameter("UnitNo",request.UnitNo);
                    var _productNo = new SqlParameter("ProductNo", request.ProductNo);

                    objresult = context.GetConsumptionMappingDTO.FromSqlRaw(
                        "Execute dbo.pro_GetAllConsumptionMapping @VenueNo,@VenueBranchNo,@MasterNo,@UserNo,@PageIndex,@AnalyzerMasterNo,@AnalyzerParamNo,@UnitNo,@ProductNo",
                     _venueNo, _venueBranchNo, _consumptionNo, _userNo, _pageIndex, _analyzerMasterNo, _parameterNo, _unitNo, _productNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ConsumptionMappingRepositoty.GetAllConsumptionMapping", ExceptionPriority.High, ApplicationType.REPOSITORY, request.venueno, (int)request.venuebranchno, (int)request.masterNo);
            }
            return objresult;
        }

        public CommonAdminResponse InsertConsumptionMapping(InsertConsumptionMapping insertConsumption)
        {
            CommonAdminResponse response = new CommonAdminResponse();

            CommonHelper commonUtility = new CommonHelper();
            var consumptionXML = commonUtility.ToXML(insertConsumption);

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {

                    var _VenueNo = new SqlParameter("VenueNo", insertConsumption?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", insertConsumption?.VenueBranchNo);
                    var _ConsumptionXML = new SqlParameter("ConsumptionXML", consumptionXML);
                    var _UserNo = new SqlParameter("UserNo", insertConsumption?.Createdby);

                    var objresult = context.CreateConsumptionMappingDTO.FromSqlRaw(
                        "Execute dbo.Pro_InsertConsumptionMappingMaster @VenueNo,@VenueBranchNo,@ConsumptionXML,@UserNo",
                     _VenueNo, _VenueBranchNo, _ConsumptionXML, _UserNo).ToList();
                    response = objresult[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ConsumptionMappingRepositoty.InsertConsumptionMapping", ExceptionPriority.Low, ApplicationType.REPOSITORY, insertConsumption.VenueNo, insertConsumption.VenueBranchNo, insertConsumption.Createdby);
            }
            return response;
        }      
    }
}
