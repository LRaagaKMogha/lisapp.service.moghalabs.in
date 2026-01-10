using Dev.IRepository.Inventory;
using DEV.Common;
using DEV.Model.EF;
using DEV.Model.Inventory;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dev.Repository.Inventory
{
    public class StoreMasterRepository : IStoreMasterRepository
    {
        private IConfiguration _config;
        public StoreMasterRepository(IConfiguration config) { _config = config; }

        //Get StoreMaster
        public List<StoreMasterResponseDTO> GetStoreMasterDetails(StoreMasterRequestDTO storeMasterRequest)
        {
            List<StoreMasterResponseDTO> objResult = new List<StoreMasterResponseDTO>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _StoreID = new SqlParameter("StoreID", storeMasterRequest.StoreID);
                    var _VenueNo = new SqlParameter("VenueNo", storeMasterRequest.VenueNo);
                    var _Venuebranchno = new SqlParameter("Venuebranchno", storeMasterRequest.Venuebranchno);
                    var _PageIndex = new SqlParameter("PageIndex", storeMasterRequest.PageIndex);

                    objResult = context.GetStoreMasterDetails.FromSqlRaw(
                    "Execute dbo.pro_GetStoremaster @StoreID,@VenueNo,@VenueBranchNo,@PageIndex", _StoreID,_VenueNo, _Venuebranchno, _PageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StoreMasterRepository.GetStoreMasterDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, storeMasterRequest.VenueNo, 0, 0);
            }
            return objResult;
        }

        public List<StoreDetails> GetAllStoreByBranch(int VenueNo, int VenueBranchNo)
        {
            List<StoreDetails> objResult = new List<StoreDetails>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", VenueNo);
                    var _Venuebranchno = new SqlParameter("Venuebranchno", VenueBranchNo);

                    objResult = context.GetAllStoreByBranch.FromSqlRaw(
                    "Execute dbo.pro_GetStoreDetailsbyBranchNo @VenueNo,@VenueBranchNo", _VenueNo, _Venuebranchno).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StoreMasterRepository.GetAllStoreByBranch", ExceptionPriority.Low, ApplicationType.REPOSITORY, VenueNo, 0, 0);
            }
            return objResult;
        }

        public StoreMasterInsertResponseDTO InsertStoreMaster(StoreMasterInsertDTO req)
        {
            StoreMasterInsertResponseDTO result = new StoreMasterInsertResponseDTO();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _StoreID = new SqlParameter("StoreID", req?.StoreID);
                    var _StoreCode = new SqlParameter("StoreCode", req?.StoreCode);
                    var _StoreName = new SqlParameter("StoreName", req?.StoreName);
                    var _StoreAddress = new SqlParameter("StoreAddress", req?.StoreAddress);
                    var _ContactPerson = new SqlParameter("ContactPerson", req?.ContactPerson);
                    var _MobileNo = new SqlParameter("MobileNo", req?.MobileNo);
                    var _IsCenterStore = new SqlParameter("IsCenterStore", req?.IsCenterStore);
                    var _VenueNo = new SqlParameter("VenueNo", req?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req?.VenueBranchNo);
                    var _CreatedBy = new SqlParameter("CreatedBy", req?.CreatedBy);
                    var _ModifiedBy = new SqlParameter("ModifiedBy", (object?)req?.ModifiedBy ?? DBNull.Value);
                    var _Status = new SqlParameter("Status", req?.Status);
                    
                    var obj = context.InsertStoreMaster.FromSqlRaw(
                    "Execute dbo.pro_InsertStoremaster @StoreID, @StoreCode, @StoreName, @StoreAddress, @ContactPerson,@MobileNo,@IsCenterStore,@VenueNo, @VenueBranchNo,@CreatedBy,@ModifiedBy,@Status",
                    _StoreID, _StoreCode, _StoreName, _StoreAddress, _ContactPerson, _MobileNo, _IsCenterStore, _VenueNo, _VenueBranchNo, _CreatedBy, _ModifiedBy,_Status).ToList();

                    result.StoreID = obj[0].StoreID;
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "StoreMasterRepository.InsertStoreMasterDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, 0);
            }
            return result;
        }
    }
}

