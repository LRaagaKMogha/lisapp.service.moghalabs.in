using System;
using System.Collections.Generic;
using Service.IRepository;
using Service.Model;
using Service.Model.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Service.Common;
using Microsoft.Data.SqlClient;

namespace Service.Repository
{
    public class ServiceOrderRepository : IServiceOrderRepository
    {
        private IConfiguration _config;
        public ServiceOrderRepository(IConfiguration config) { _config = config; }

        public List<GetserviceDetails> GetserviceOrderMaster(ServiceOrderMasterRequest serviceOrderMasterRequest)
        {
            List<GetserviceDetails> Objresult = new List<GetserviceDetails>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _MainDeptNo = new SqlParameter("MainDeptNo", serviceOrderMasterRequest?.MainDeptNo);
                    var _DeptNo = new SqlParameter("DeptNo", serviceOrderMasterRequest?.DeptNo);
                    var _ServiceType = new SqlParameter("ServiceType", serviceOrderMasterRequest?.ServiceType);
                    var _ServiceNo = new SqlParameter("ServiceNo", serviceOrderMasterRequest?.ServiceNo);
                    var _VenueNo = new SqlParameter("VenueNo", serviceOrderMasterRequest?.VenueNo);

                    Objresult = context.GetserviceOrder.FromSqlRaw(
                    "Execute dbo.pro_GetserviceOrder @MainDeptNo,@DeptNo,@ServiceType,@ServiceNo,@VenueNo",
                    _MainDeptNo, _DeptNo, _ServiceType, _ServiceNo, _VenueNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ServiceOrderRepository.GetserviceOrderMaster", ExceptionPriority.Low, ApplicationType.REPOSITORY, serviceOrderMasterRequest?.VenueNo, serviceOrderMasterRequest?.ServiceNo, 0);
            }
            return Objresult;
        }

        public ServiceOrderMasterResponse InsertServiceOrderMaster(TblServiceOrder resultItem)
        {            
            ServiceOrderMasterResponse result = new ServiceOrderMasterResponse();
            CommonHelper commonUtility = new CommonHelper();    

            try
            {
                string testServiceXML = "";
                if (resultItem?.testServiceXML?.Count > 0)
                {
                    testServiceXML = commonUtility.ToXML(resultItem?.testServiceXML);
                }

                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {           
                    var _testServiceXML = new SqlParameter("testServiceXML", testServiceXML);
                    var _userNo = new SqlParameter("userno", resultItem?.userNo);
                    var _venueNo = new SqlParameter("venueNo", resultItem?.venueNo);
                    var _venueBranchNo = new SqlParameter("venueBranchNo", resultItem?.venueBranchNo);

                    var obj = context.InsertServiceOrder.FromSqlRaw(
                    "Execute dbo.pro_InsertServiceOrder @testServiceXML,@userNo,@venueNo,@venueBranchNo",
                    _testServiceXML, _userNo, _venueNo, _venueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            { 
                MyDevException.Error(ex, "ServiceOrderRepository.InsertServiceOrderMaster", ExceptionPriority.Low, ApplicationType.REPOSITORY, resultItem?.venueNo, resultItem?.venueBranchNo, 0);
            }
            return result;
        }
    }
}