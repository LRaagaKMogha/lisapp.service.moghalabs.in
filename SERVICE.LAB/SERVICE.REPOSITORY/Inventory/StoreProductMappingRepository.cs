using Dev.IRepository.Inventory;
using DEV.Common;
using Service.Model.EF;
using Service.Model.Inventory;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Repository.Inventory
{
    public class StoreProductMappingRepository : IStoreProductMappingRepository
    {
        private readonly IConfiguration _config;

        public StoreProductMappingRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<object> GetStoreProductMappingAsync(StoreProductMappingRequestDTO request)
        {
            object result = null;

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("@VenueNo", request.VenueNo);
                    var _ProductNo = new SqlParameter("@ProductNo", request.ProductNo);
                    var _StoreNo = new SqlParameter("@StoreNo", request.StoreNo);
                    var _CommonCodeMapping = new SqlParameter("@CommonCodeMapping", request.CommonCodeMapping);
                    var _IsActiveorNot = new SqlParameter("@IsActiveorNot", request.IsActiveorNot ?? (object)DBNull.Value);
                    var _PageIndex = new SqlParameter("@PageIndex", request.PageIndex);

                    switch (request.CommonCodeMapping)
                    {
                        case "StoreToProductMappingAdd":
                            result = await context.StoreToProductMappingAdd
                                .FromSqlRaw("EXEC dbo.pro_IV_GetStoreProductMapping @VenueNo, @ProductNo, @StoreNo, @CommonCodeMapping, @IsActiveorNot, @PageIndex",
                                    _VenueNo, _ProductNo, _StoreNo, _CommonCodeMapping, _IsActiveorNot, _PageIndex)
                                .ToListAsync();
                            break;

                        case "StoreToProductMapping":
                            result = await context.StoreToProductMapping
                                .FromSqlRaw("EXEC dbo.pro_IV_GetStoreProductMapping @VenueNo, @ProductNo, @StoreNo, @CommonCodeMapping, @IsActiveorNot, @PageIndex",
                                    _VenueNo, _ProductNo, _StoreNo, _CommonCodeMapping, _IsActiveorNot, _PageIndex)
                                .ToListAsync();
                            break;

                        default:
                            throw new Exception("Invalid CommonCodeMapping value.");
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StoreProductMappingRepository.GetStoreProductMappingAsync",
                    ExceptionPriority.Low, ApplicationType.REPOSITORY, request.VenueNo, 0, 0);
            }

            return result;
        }

        public async Task<int> InsertStoreProductMappingAsync(StoreProductMappingInsertDTO dto)
        {
            try
            {
                CommonHelper helper = new CommonHelper();
                var storeXml = helper.ToXML(dto.StoreProductList);

                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("@VenuNo", dto.VenueNo);
                    var _UserNo = new SqlParameter("@UserNo", dto.UserNo);
                    var _StoreVsProductXML = new SqlParameter("@StoreVsProductXML", storeXml);

                    await context.Database.ExecuteSqlRawAsync(
                        "EXEC dbo.pro_IV_InsertStoreProductMapping @VenuNo, @UserNo, @StoreVsProductXML",
                        _VenueNo, _UserNo, _StoreVsProductXML
                    );
                    return 1;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StoreProductMappingRepository.InsertStoreProductMappingAsync",
                    ExceptionPriority.High, ApplicationType.REPOSITORY, dto.VenueNo, dto.UserNo, 0);
                return 0;
            }
        }
    }
}
