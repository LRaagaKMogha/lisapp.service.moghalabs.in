using Dev.IRepository.Master;
using DEV.Common;
using DEV.Model.EF;
using DEV.Model.Inventory;
using DEV.Model.Master;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Repository.Master
{
    public  class FranchisorRepository:IFranchisorRepository
    {
        private IConfiguration _config;
        public FranchisorRepository(IConfiguration config) { _config = config; }

        public List<GetFranchiseResponse> GetFranchises(int VenueNo,int VenueBranchNo)
        {
           List<GetFranchiseResponse> objresult=new List<GetFranchiseResponse>();

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo.ToString());
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", VenueBranchNo.ToString());
                    objresult = context.GetFranchises.FromSqlRaw
                        ("Execute dbo.pro_GetIsFranchise @venueNo,@VenueBranchNo", _VenueNo, _VenueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FranchisorRepository.GetFranchises", ExceptionPriority.High, ApplicationType.REPOSITORY, VenueNo, VenueBranchNo, 0);
            }
            return objresult;
        }
        public List<FranchiseRevenueSharingServiceDto> GetFranchiseRevenueSharingByService(GetFranchiseRevenueSharingByServiceRequest request)
        {
            List<FranchiseRevenueSharingServiceDto> result = new List<FranchiseRevenueSharingServiceDto>();

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var venueNo = new SqlParameter("@VenueNo", request.VenueNo);
                    var venueBranchNo = new SqlParameter("@VenueBranchNo", request.VenueBranchNo);
                    var departmentNo = new SqlParameter("@DepartmentNo", (object?)request.DepartmentNo ?? DBNull.Value);
                    var serviceType = new SqlParameter("@ServiceType", string.IsNullOrEmpty(request.ServiceType) ? DBNull.Value : (object)request.ServiceType);
                    var serviceNo = new SqlParameter("@ServiceNo", (object?)request.ServiceNo ?? DBNull.Value);
                    var franchiseNo = new SqlParameter("@FranchiseNo", request.FranchiseNo);
                    var franchisorNo = new SqlParameter("@FranchisorNo", request.FranchisorNo);

                    result = context.FranchiseRevenueSharingServiceDto
                        .FromSqlRaw("EXEC dbo.pro_GetIsFranchiseRevenueSharingbyService @VenueNo, @VenueBranchNo, @DepartmentNo, @ServiceType, @ServiceNo, @FranchiseNo, @FranchisorNo",
                                    venueNo, venueBranchNo, departmentNo, serviceType, serviceNo, franchiseNo, franchisorNo)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FranchisorRepository.GetFranchiseRevenueSharingByService", ExceptionPriority.High, ApplicationType.REPOSITORY, request.VenueNo, 0, 0);
            }

            return result;
        }
        public async Task<int> InsertFranchiseRevenueSharingAsync(FranchiseRevenueSharingInsertDTO dto)
        {
            try
            {
                CommonHelper helper = new CommonHelper();
                var revenueXml = helper.ToXML(dto.FranchiseRevenueSharingList);

                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("@VenueNo", dto.VenueNo);
                    var _UserNo = new SqlParameter("@UserNo", dto.UserNo);
                    var _FranchisorNo = new SqlParameter("@FranchisorNo", dto.FranchisorNo);
                    var _FranchiseNo = new SqlParameter("@FranchiseNo", dto.FranchiseNo);
                    var _RevenueXML = new SqlParameter("@RevenueXML", revenueXml);

                    await context.Database.ExecuteSqlRawAsync(
                        "EXEC dbo.pro_InsertUpdateFranchiseRevenueSharing @VenueNo, @UserNo, @FranchisorNo, @FranchiseNo, @RevenueXML",
                        _VenueNo, _UserNo, _FranchisorNo, _FranchiseNo, _RevenueXML
                    );

                    return 1;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "FranchiseRepository.InsertFranchiseRevenueSharingAsync",
                    ExceptionPriority.High, ApplicationType.REPOSITORY, dto.VenueNo,0, 0);
                return 0;
            }
        }

    }
}
