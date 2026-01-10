using Dev.IRepository.Inventory;
using DEV.Common;
using DEV.Model.EF;
using DEV.Model.Inventory;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Repository.Inventory
{
    public class CommonProductSupplierMappingRepository : ICommonProductSupplierMappingRepository
    {
        private readonly IConfiguration _config;

        public CommonProductSupplierMappingRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<object> GetProductSupplierMappingAsync(ProductSupplierMappingRequestDTO request)
        {
            object result = null;

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("@VenueNo", request.VenueNo);
                    var _ProductNo = new SqlParameter("@ProductNo", request.ProductNo);
                    var _SupplierNo = new SqlParameter("@SupplierNo", request.SupplierNo);
                    var _CommonCodeMapping = new SqlParameter("@CommonCodeMapping", request.CommonCodeMapping);
                    var _IsActiveorNot = new SqlParameter("@IsActiveorNot", request.IsActiveorNot ?? (object)DBNull.Value);
                    var _PageIndex = new SqlParameter("@PageIndex", request.PageIndex);

                    switch (request.CommonCodeMapping)
                    {
                        case "SupplierToProductMappingAdd":
                            result = await context.SupplierToProductMappingAdd
                                .FromSqlRaw("EXEC dbo.pro_IV_GetProductSupplierMapping_Common @VenueNo, @ProductNo, @SupplierNo, @CommonCodeMapping, @IsActiveorNot, @PageIndex",
                                    _VenueNo, _ProductNo, _SupplierNo, _CommonCodeMapping, _IsActiveorNot, _PageIndex)
                                .ToListAsync();
                            break;

                        case "ProductToSupplierMappingAdd":
                            result = await context.ProductToSupplierMappingAdd
                                .FromSqlRaw("EXEC dbo.pro_IV_GetProductSupplierMapping_Common @VenueNo, @ProductNo, @SupplierNo, @CommonCodeMapping, @IsActiveorNot, @PageIndex",
                                    _VenueNo, _ProductNo, _SupplierNo, _CommonCodeMapping, _IsActiveorNot, _PageIndex)
                                .ToListAsync();
                            break;

                        case "SupplierToProductMapping":
                            result = await context.SupplierToProductMapping
                                .FromSqlRaw("EXEC dbo.pro_IV_GetProductSupplierMapping_Common @VenueNo, @ProductNo, @SupplierNo, @CommonCodeMapping, @IsActiveorNot, @PageIndex",
                                    _VenueNo, _ProductNo, _SupplierNo, _CommonCodeMapping, _IsActiveorNot, _PageIndex)
                                .ToListAsync();
                            break;

                        case "ProductToSupplierMapping":
                            result = await context.ProductToSupplierMapping
                                .FromSqlRaw("EXEC dbo.pro_IV_GetProductSupplierMapping_Common @VenueNo, @ProductNo, @SupplierNo, @CommonCodeMapping, @IsActiveorNot, @PageIndex",
                                    _VenueNo, _ProductNo, _SupplierNo, _CommonCodeMapping, _IsActiveorNot, _PageIndex)
                                .ToListAsync();
                            break;

                        default:
                            throw new Exception("Invalid CommonCodeMapping value.");
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "CommonProductSupplierMappingRepository.GetProducSystem.InvalidCastException: 'Unable to cast object of type 'System.Int16' to type 'System.Int32'.'tSupplierMappingAsync", ExceptionPriority.Low, ApplicationType.REPOSITORY, request.VenueNo, 0, 0);
            }

            return result;
        }
        public async Task<int> InsertProductSupplierMappingAsync(ProductSupplierMappingInsertDTO dto)
        {
            try
            {
                CommonHelper helper = new CommonHelper();
                var supplierXml = helper.ToXML(dto.ProductSupplierList);

                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("@VenuNo", dto.VenueNo);
                    var _UserNo = new SqlParameter("@UserNo", dto.UserNo);
                    var _ProductVsSupplierXML = new SqlParameter("@ProductVsSupplierXML", supplierXml);

                    await context.Database.ExecuteSqlRawAsync(
                        "EXEC dbo.pro_IV_InsertProductSupplierMapping_Common @VenuNo, @UserNo, @ProductVsSupplierXML",
                        _VenueNo, _UserNo, _ProductVsSupplierXML
                    );
                    return 1;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsertProductSupplierMappingAsync",
                    ExceptionPriority.High, ApplicationType.REPOSITORY, dto.VenueNo, dto.UserNo, 0);
                return 0;
            }
        }

    }
}
