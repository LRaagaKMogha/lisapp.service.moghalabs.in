using Dev.IRepository;
using DEV.Common;
using DEV.Model;
using DEV.Model.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dev.Repository
{
    public class MultiPriceListRepository: IMultiPriceListRepository
    {
        private IConfiguration _config;
        public MultiPriceListRepository(IConfiguration config) { _config = config; }

        /// <summary>
        /// Get MultiPriceList Details
        /// </summary>
        /// <returns></returns>
        public List<GetmultiPriceListResponse> GetMultiPriceListDetails(GetmultiPriceListRequest getRequest)
        {
            List<GetmultiPriceListResponse> objresult = new List<GetmultiPriceListResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", getRequest.venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", getRequest.venueBranchNo);                    
                    var _DepartmentNo = new SqlParameter("DepartmentNo", getRequest.departmentNo);
                    var _RateListNo = new SqlParameter("rateListNo", getRequest.rateListNo);
                    var _serviceNo = new SqlParameter("serviceNo", getRequest.serviceNo);
                    

                    objresult = context.GetMultiPriceListDTO.FromSqlRaw(
                        "Execute dbo.pro_GetMultiPriceListDetails @VenueNo,@VenueBranchNo,@departmentNo,@rateListNo,@serviceNo",
                     _VenueNo, _VenueBranchNo, _DepartmentNo, _RateListNo, _serviceNo).ToList();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetMultiPriceListDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, getRequest.venueNo, getRequest.venueBranchNo, getRequest.userNo);
            }
            return objresult;
        }

        /// <summary>
        /// Insert ClientMaster Details
        /// </summary>
        /// <param name="ClientMasteritem"></param>
        /// <returns></returns>
        public InsertMultiPriceListResponse InsertMultiPriceListDetails(InsertMultiPriceListRequest multiPriceListRequest)
        {
            InsertMultiPriceListResponse result = new InsertMultiPriceListResponse();
            CommonHelper commonUtility = new CommonHelper();

            try
            {

                string rateListXml = commonUtility.ToXML(multiPriceListRequest);

                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _rateListXml = new SqlParameter("rateListXml", rateListXml);
                    var _VenueNo = new SqlParameter("venueNo", multiPriceListRequest.VenueNo);
                    var _VenueBranchNo = new SqlParameter("venueBranchNo", multiPriceListRequest.VenueBranchNo);                  
                    var _CreatedBy = new SqlParameter("CreatedBy", multiPriceListRequest.CreatedBy);

                    var dbResponse = context.InsertMultiPriceListDTO.FromSqlRaw(
                        "Execute dbo.Pro_CreateMultipriceList @VenueNo,@VenueBranchNo,@rateListXml,@CreatedBy",
                     _VenueNo, _VenueBranchNo, _rateListXml, _CreatedBy).ToList();
                    result = dbResponse[0];

                }
            }
            catch (Exception ex)
            {
               // MyDevException.Error(ex, "InsertMultiPriceListDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, tariffMasteritem.VenueNo, tariffMasteritem.VenueBranchNo, 0);
            }
            return result;
        }
        

    }
}
