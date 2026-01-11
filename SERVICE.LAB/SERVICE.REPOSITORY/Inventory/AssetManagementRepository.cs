using Dev.IRepository.Inventory;
using DEV.Common;
using Service.Model;
using Service.Model.EF;
using Service.Model.Inventory;
using Service.Model.Inventory.Master;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Dev.Repository
{
    public class AssetManagementRepository : IAssetManagementRepository
    {
        private IConfiguration _config;
        public AssetManagementRepository(IConfiguration config) { _config = config; }
        public int InsertInstrumentDetails(postAssetManagementDTO objManuDTO)
        {
            Int32 result = 0;
            CommonAdminResponse response = new CommonAdminResponse();

            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _InstrumentsNo = new SqlParameter("instrumentsNo", objManuDTO.InstrumentsNo);
                    var _InstrumentsName = new SqlParameter("InstrumentsName", objManuDTO.InstrumentsName);
                    var _venueNo = new SqlParameter("venueNo", objManuDTO.venueNo);
                    var _venueBranchno = new SqlParameter("venueBranchno", objManuDTO.venueBranchno);
                    var _userNo = new SqlParameter("userNo", objManuDTO.userNo);
                    var _branchNo = new SqlParameter("branchNo", objManuDTO.branchNo);
                    var _DepartmentNo = new SqlParameter("DepartmentNo", objManuDTO.DepartmentNo);
                    var _InstallationDate = new SqlParameter("installationDate", objManuDTO.InstallationDate);
                    var _ModificationDate = new SqlParameter("modificationDate", objManuDTO.ModificationDate);
                    var _Remark = new SqlParameter("remark", objManuDTO.Remark);
                    //var _DocumentPath = new SqlParameter("documentPath", objManuDTO.DocumentPath);
                    var _manufacturerName = new SqlParameter("ManufacturerName", objManuDTO.manufacturerName);
                    var _ContactPersonName = new SqlParameter("ContactPersonName", objManuDTO.ContactPersonName);
                    var _MobileNo = new SqlParameter("MobileNo", objManuDTO.MobileNo);
                    var _CallCenterContactNumber = new SqlParameter("CallCenterContactNumber", objManuDTO.CallCenterContactNumber);
                    var _Email = new SqlParameter("Email", objManuDTO.Email);
                    var _RequestRaised = new SqlParameter("RequestRaised", objManuDTO.RequestRaised);
                    var _ResolvedDate = new SqlParameter("ResolvedDate", objManuDTO.ResolvedDate);
                    var _RequestRemarks = new SqlParameter("RequestRemarks", objManuDTO.RequestRemarks);
                    var _Assetno = new SqlParameter("AssetNo", objManuDTO.AssetNo);
                    var _machineSerialNo = new SqlParameter("machineSerialNo", objManuDTO.MachineSerialNo);
                    var _pageIndex = new SqlParameter("pageIndex", objManuDTO.pageIndex);
                    var _Status = new SqlParameter("Status", objManuDTO.Status);

                    var objResult = context.CreateManufacturerMasterDTO.FromSqlRaw(
                    "Execute dbo.Pro_InsertInventoryInstrument @InstrumentsNo,@InstrumentsName,@venueNo,@venueBranchno,@userNo,@branchNo,@DepartmentNo,@InstallationDate,@ModificationDate, @Remark," +
                    "@ManufacturerName,@ContactPersonName,@MobileNo,@CallCenterContactNumber,@Email,@RequestRaised,@ResolvedDate,@RequestRemarks,@AssetNo,@MachineSerialNo,@pageIndex,@Status",
                    _InstrumentsNo,_InstrumentsName,_venueNo, _venueBranchno, _userNo, _branchNo, _DepartmentNo , _InstallationDate, _ModificationDate, _Remark,// _DocumentPath,
                    _manufacturerName, _ContactPersonName,_MobileNo,_CallCenterContactNumber,_Email,_RequestRaised,_ResolvedDate,_RequestRemarks, _Assetno, _machineSerialNo, _pageIndex,_Status).ToList();

                    response = objResult[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsertInstrumentDetails", ExceptionPriority.Low, ApplicationType.REPOSITORY, objManuDTO.venueNo, 0, 0);
            }
            return result;
        }
        public List<GetAssetManagementResponse> GetInstrumentDetail(AssetManagementRequest masterRequest)
        {
            List<GetAssetManagementResponse> objResult = new List<GetAssetManagementResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _InstrumentsNo = new SqlParameter("InstrumentsNo", masterRequest.InstrumentsNo);
                    var _VenueNo = new SqlParameter("VenueNo", masterRequest.venueNo);                    
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", masterRequest.venueBranchNo);
                    var _branchNo = new SqlParameter("BranchNo", masterRequest.branchNo);
                    var _DepartmentNo = new SqlParameter("DepartmentNo", masterRequest.DepartmentNo);
                    var _status = new SqlParameter("Status", masterRequest.status);
                    var _pageIndex = new SqlParameter("pageIndex", masterRequest.pageIndex);

                    objResult = context.GetInstrumentDetail.FromSqlRaw(
                    "Execute dbo.Pro_GetInventoryInstrument @InstrumentsNo, @VenueNo, @VenueBranchNo, @branchNo, @DepartmentNo, @Status,  @pageIndex",
                    _InstrumentsNo, _VenueNo, _VenueBranchNo, _branchNo, _DepartmentNo, _status, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetInstrumentDetail", ExceptionPriority.Low, ApplicationType.REPOSITORY, masterRequest.venueNo, 0, 0);
            }
            return objResult;
        }
    }
}