using Dev.IRepository;
using DEV.Common;
using Service.Model;
using Service.Model.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Dev.Repository
{
    public class TariffMasterRepository : ITariffMasterRepository
    {
        private IConfiguration _config;
        public TariffMasterRepository(IConfiguration config) { _config = config; }

        /// <summary>
        /// Get TariffMaster Details
        /// </summary>
        /// <returns></returns>
        public List<GetTariffMasterResponse> GetTariffMasterDetails(GetTariffMasterRequest getRequest)
        {
            List<GetTariffMasterResponse> objresult = new List<GetTariffMasterResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", getRequest?.venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", getRequest?.venueBranchNo);
                    var _PageIndex = new SqlParameter("PageIndex", getRequest?.pageIndex);
                    var _RateListNo = new SqlParameter("rateListNo", getRequest?.rateListNo);
                    var _Type = new SqlParameter("type", getRequest?.type);
                    var _ClientNo = new SqlParameter("ClientNo", getRequest?.filterClientNo);
                    var _IsFranchisee = new SqlParameter("IsFranchisee", getRequest?.IsFranchisee);
                    var _DoctorNo = new SqlParameter("DoctorNo", getRequest?.filterDoctorNo);

                    objresult = context.GetTariffMasterDTO.FromSqlRaw(
                        "Execute dbo.Pro_GetTariff @VenueNo,@VenueBranchNo,@rateListNo,@PageIndex,@type,@ClientNo,@IsFranchisee,@DoctorNo",
                     _VenueNo, _VenueBranchNo, _RateListNo, _PageIndex, _Type, _ClientNo, _IsFranchisee, _DoctorNo).ToList();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMasterRepository.GetTariffMasterDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, getRequest?.venueNo, getRequest?.venueBranchNo, getRequest?.userNo);
            }
            return objresult;
        }

        /// <summary>
        /// Insert ClientMaster Details
        /// </summary>
        /// <param name="ClientMasteritem"></param>
        /// <returns></returns>

        public InsertTariffMasterResponse InsertTariffMasterDetails(InsertTariffMasterRequest tariffMasteritem)
        {
            InsertTariffMasterResponse result = new InsertTariffMasterResponse();
            CommonHelper commonUtility = new CommonHelper();

            try
            {
                string rateListXml = commonUtility.ToXML(tariffMasteritem?.serviceDetails);

                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _rateListXml = new SqlParameter("rateListXml", rateListXml);
                    var _VenueNo = new SqlParameter("venueNo", tariffMasteritem?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("venueBranchNo", tariffMasteritem?.VenueBranchNo);
                    var _RateListNo = new SqlParameter("RateListNo", tariffMasteritem?.RateListNo);
                    var _RateListName = new SqlParameter("RateListName", tariffMasteritem?.RateListName);
                    var _EffectiveFrom = new SqlParameter("EffectiveFrom", tariffMasteritem?.EffectiveFrom?.ValidateEmpty());
                    var _EffectiveTo = new SqlParameter("EffectiveTo", tariffMasteritem?.EffectiveTo?.ValidateEmpty());
                    var _SequenceNo = new SqlParameter("SequenceNo", tariffMasteritem?.SequenceNo);
                    var _Status = new SqlParameter("Status", tariffMasteritem?.Status);
                    var _CreatedBy = new SqlParameter("CreatedBy", tariffMasteritem?.CreatedBy);
                    var _mappingType = new SqlParameter("mappingType", tariffMasteritem?.mappingType);
                    var _OldRateListNo = new SqlParameter("OldRateListNo", tariffMasteritem?.OldRateListNo);
                    var _customerNo = new SqlParameter("customerNo", tariffMasteritem?.ClientNo);

                    var dbResponse = context.InsertTariffMasterDTO.FromSqlRaw(
                    "Execute dbo.Pro_CreateTariff @VenueNo,@VenueBranchNo,@rateListXml,@RateListNo,@RateListName,@EffectiveFrom," +
                    "@EffectiveTo,@SequenceNo,@Status,@CreatedBy,@mappingType,@OldRateListNo,@customerNo",
                    _VenueNo, _VenueBranchNo, _rateListXml, _RateListNo, _RateListName, _EffectiveFrom, _EffectiveTo,
                    _SequenceNo, _Status, _CreatedBy, _mappingType, _OldRateListNo, _customerNo).ToList();
                    
                    result = dbResponse[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMasterRepository.InsertTariffMasterDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, tariffMasteritem?.VenueNo, tariffMasteritem?.VenueBranchNo, 0);
            }
            return result;
        }
        /// <summary>
        /// Get TariffMaster Details
        /// </summary>
        /// <returns></returns>

        public List<GetServices> GetTariffService(GetTariffMasterRequest getRequest)
        {
            List<GetServices> objresult = new List<GetServices>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", getRequest?.venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", getRequest?.venueBranchNo);
                    var _DeptNo = new SqlParameter("departmentNo", getRequest?.departmentNo);
                    var _RateListNo = new SqlParameter("rateListNo", getRequest?.rateListNo);
                    var _Type = new SqlParameter("type", getRequest?.type);
                    var _clientNo = new SqlParameter("clientNo", getRequest?.clientNo);
                    var _PhysicianNo = new SqlParameter("physicianNo", getRequest?.physicianNo);

                    objresult = context.GetServiceDetailsDTO.FromSqlRaw(
                        "Execute dbo.pro_TariffSearchService @VenueNo,@VenueBranchNo,@departmentNo,@rateListNo,@type,@clientNo,@physicianNo",
                     _VenueNo, _VenueBranchNo, _DeptNo, _RateListNo, _Type, _clientNo, _PhysicianNo).ToList();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMasterRepository.GetTariffService", ExceptionPriority.High, ApplicationType.REPOSITORY, getRequest?.venueNo, getRequest?.venueBranchNo, getRequest?.userNo);
            }
            return objresult;
        }

        public List<GetTariffMasterListResponse> GetTariffMasterList(GetTariffMasterListRequest getRequest)
        {
            List<GetTariffMasterListResponse> objresult = new List<GetTariffMasterListResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", getRequest?.venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", getRequest?.venueBranchNo);
                    var _RateListNo = new SqlParameter("rateListNo", getRequest?.rateListNo);
                    var _PageIndex = new SqlParameter("PageIndex", getRequest?.pageIndex);
                    var _IsApproval = new SqlParameter("IsApproval", getRequest?.IsApproval);
                    var _CommercialType = new SqlParameter("CommercialType", getRequest?.CommercialType);

                    objresult = context.GetTariffMasterListDTO.FromSqlRaw(
                        "Execute dbo.Pro_GetTariffList @VenueNo,@VenueBranchNo,@rateListNo,@PageIndex,@IsApproval,@CommercialType",
                     _VenueNo, _VenueBranchNo, _RateListNo, _PageIndex, _IsApproval, _CommercialType).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMasterRepository.GetTariffMasterDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, getRequest?.venueNo, getRequest?.venueBranchNo, getRequest?.userNo);
            }
            return objresult;
        }

        /// <summary>
        /// Get Tariff Master Service List
        /// </summary>
        /// <returns></returns>
        public List<TariffMastServicesResponse> GetTariffMasterServiceList(GetTariffMasterListRequest getRequest)
        {
            List<TariffMastServicesResponse> objresult = new List<TariffMastServicesResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", getRequest?.venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", getRequest?.venueBranchNo);
                    var _DeptNo = new SqlParameter("departmentNo", getRequest?.departmentNo);
                    var _RateListNo = new SqlParameter("rateListNo", getRequest?.rateListNo);
                    var _IsApproval = new SqlParameter("IsApproval", getRequest?.IsApproval);
                    var _IsRateShow = new SqlParameter("IsRateShow", getRequest?.israteshow);

                    objresult = context.TariffMasterServiceListDTO.FromSqlRaw(
                        "Execute dbo.pro_TariffMasterSearchService @VenueNo,@VenueBranchNo,@departmentNo,@rateListNo,@IsApproval,@IsRateShow",
                     _VenueNo, _VenueBranchNo, _DeptNo, _RateListNo, _IsApproval, _IsRateShow).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMasterRepository.GetTariffMasterServiceList", ExceptionPriority.High, ApplicationType.REPOSITORY, getRequest?.venueNo, getRequest?.venueBranchNo, getRequest?.userNo);
            }
            return objresult;
        }

        public TariffMasterInsertResponse InsertTariffMaster(InsertTariffMasterRequest tariffMasteritem)
        {
            TariffMasterInsertResponse result = new TariffMasterInsertResponse();
            CommonHelper commonUtility = new CommonHelper();

            try
            {
                string rateListXml = commonUtility.ToXML(tariffMasteritem?.serviceDetails);
                string deptDetailsXML = commonUtility.ToXML(tariffMasteritem?.deptDetails);

                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _rateListXml = new SqlParameter("rateListXml", rateListXml);
                    var _VenueNo = new SqlParameter("venueNo", tariffMasteritem?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("venueBranchNo", tariffMasteritem?.VenueBranchNo);
                    var _RateListNo = new SqlParameter("RateListNo", tariffMasteritem?.RateListNo);
                    var _RateListName = new SqlParameter("RateListName", tariffMasteritem?.RateListName);
                    var _EffectiveFrom = new SqlParameter("EffectiveFrom", tariffMasteritem?.EffectiveFrom.ValidateEmpty());
                    var _EffectiveTo = new SqlParameter("EffectiveTo", tariffMasteritem?.EffectiveTo.ValidateEmpty());
                    var _SequenceNo = new SqlParameter("SequenceNo", tariffMasteritem?.SequenceNo);
                    var _Status = new SqlParameter("Status", tariffMasteritem?.Status);
                    var _CreatedBy = new SqlParameter("CreatedBy", tariffMasteritem?.CreatedBy);
                    var _MappingType = new SqlParameter("mappingType", tariffMasteritem?.mappingType);
                    var _OldRateListNo = new SqlParameter("OldRateListNo", tariffMasteritem?.OldRateListNo);
                    var _CustomerNo = new SqlParameter("customerNo", tariffMasteritem?.ClientNo);
                    var _BaseRateListNo = new SqlParameter("baseRateListNo", tariffMasteritem?.BaseRateListNo);
                    var _IsBasePriceChanged = new SqlParameter("isBasePriceChanged", tariffMasteritem?.IsBasePriceChanged);
                    var _IsApproval = new SqlParameter("IsApproval", tariffMasteritem?.IsApproval);
                    var _IsReject = new SqlParameter("IsReject", tariffMasteritem?.IsReject);
                    var _RejectReason = new SqlParameter("RejectReason", tariffMasteritem?.RejectReason);
                    var _OldRateListAppNo = new SqlParameter("OldRateListAppNo", tariffMasteritem?.OldRateListAppNo);
                    var _AppRateListAppNo = new SqlParameter("AppRateListAppNo", tariffMasteritem?.AppRateListAppNo);
                    var _deptDetailsXML = new SqlParameter("deptDetailsXML", deptDetailsXML);

                    var dbResponse = context.TariffMasterInsertDTO.FromSqlRaw(
                        "Execute dbo.Pro_CreateTariffMaster " +
                        "@VenueNo, @VenueBranchNo, @rateListXml, @RateListNo, @RateListName, @EffectiveFrom," +
                        "@EffectiveTo, @SequenceNo, @Status, @CreatedBy,@mappingType, @OldRateListNo,@customerNo,@baseRateListNo,@isBasePriceChanged" +
                        ",@IsApproval,@IsReject,@RejectReason,@OldRateListAppNo,@AppRateListAppNo,@deptDetailsXML",
                     _VenueNo, _VenueBranchNo, _rateListXml, _RateListNo, _RateListName, _EffectiveFrom, _EffectiveTo,
                     _SequenceNo, _Status, _CreatedBy, _MappingType, _OldRateListNo, _CustomerNo, _BaseRateListNo, _IsBasePriceChanged,
                     _IsApproval, _IsReject, _RejectReason, _OldRateListAppNo, _AppRateListAppNo, _deptDetailsXML).ToList();
                    result = dbResponse[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMasterRepository.InsertTariffMaster", ExceptionPriority.High, ApplicationType.REPOSITORY, tariffMasteritem?.VenueNo, tariffMasteritem?.VenueBranchNo, 0);
            }
            return result;
        }

        public List<GetClientTariffMasterListResponse> GetClientTariffMasterList(GetClientTariffMasterRequest getRequest)
        {
            List<GetClientTariffMasterListResponse> objresult = new List<GetClientTariffMasterListResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", getRequest?.venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", getRequest?.venueBranchNo);
                    var _PageIndex = new SqlParameter("PageIndex", getRequest?.pageIndex);
                    var _RateListNo = new SqlParameter("rateListNo", getRequest?.rateListNo);
                    var _Type = new SqlParameter("type", getRequest?.type);
                    var _ClientNo = new SqlParameter("ClientNo", getRequest?.filterClientNo);
                    var _DoctorNo = new SqlParameter("DoctorNo", getRequest?.filterDoctorNo);

                    objresult = context.GetClientTariffMasterListDTO.FromSqlRaw(
                        "Execute dbo.Pro_GetClientTariffList @VenueNo,@VenueBranchNo,@rateListNo,@PageIndex,@type,@ClientNo,@DoctorNo",
                     _VenueNo, _VenueBranchNo, _RateListNo, _PageIndex, _Type, _ClientNo, _DoctorNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMasterRepository.GetTariffMasterDetails", ExceptionPriority.High, ApplicationType.REPOSITORY, getRequest?.venueNo, getRequest?.venueBranchNo, getRequest?.userNo);
            }
            return objresult;
        }

        public CTMInsertResponse InsertClientTariffMaster(InsertCTMRequest tariffMasteritem)
        {
            CTMInsertResponse result = new CTMInsertResponse();
            CommonHelper commonUtility = new CommonHelper();

            try
            {
                string rateListXml = commonUtility.ToXML(tariffMasteritem?.serviceDetails);

                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _rateListXml = new SqlParameter("rateListXml", rateListXml);
                    var _venueNo = new SqlParameter("VenueNo", tariffMasteritem?.VenueNo);
                    var _venueBranchNo = new SqlParameter("VenueBranchNo", tariffMasteritem?.VenueBranchNo);
                    var _rateListNo = new SqlParameter("RateListNo", tariffMasteritem?.RateListNo);
                    var _rateListName = new SqlParameter("RateListName", tariffMasteritem?.RateListName);
                    var _effectiveFrom = new SqlParameter("EffectiveFrom", tariffMasteritem?.EffectiveFrom.ValidateEmpty());
                    var _effectiveTo = new SqlParameter("EffectiveTo", tariffMasteritem?.EffectiveTo.ValidateEmpty());
                    var _sequenceNo = new SqlParameter("SequenceNo", tariffMasteritem?.SequenceNo);
                    var _status = new SqlParameter("Status", tariffMasteritem?.Status);
                    var _createdBy = new SqlParameter("CreatedBy", tariffMasteritem?.CreatedBy);
                    var _mappingType = new SqlParameter("mappingType", tariffMasteritem?.mappingType);
                    var _oldRateListNo = new SqlParameter("OldRateListNo", tariffMasteritem?.OldRateListNo);
                    var _customerNo = new SqlParameter("CustomerNo", tariffMasteritem?.ClientNo);
                    var _physicianNo = new SqlParameter("PhysicianNo", tariffMasteritem?.PhysicianNo);

                    var dbResponse = context.ClientTariffMasterInsertDTO.FromSqlRaw(
                        "Execute dbo.Pro_CreateClientTariff " +
                        "@VenueNo, @VenueBranchNo, @rateListXml, @RateListNo, @RateListName, @EffectiveFrom," +
                        "@EffectiveTo, @SequenceNo, @Status, @CreatedBy, @mappingType, @OldRateListNo, @CustomerNo, @PhysicianNo",
                     _venueNo, _venueBranchNo, _rateListXml, _rateListNo, _rateListName, _effectiveFrom, _effectiveTo,
                     _sequenceNo, _status, _createdBy, _mappingType, _oldRateListNo, _customerNo, _physicianNo).ToList();

                    result = dbResponse[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMasterRepository.InsertClientTariffMaster", ExceptionPriority.High, ApplicationType.REPOSITORY, tariffMasteritem?.VenueNo, tariffMasteritem?.VenueBranchNo, 0);
            }
            return result;
        }

        public List<ClientTariffServicesResponse> GetClientTariffServiceList(GetClientTariffMasterRequest getRequest)
        {
            List<ClientTariffServicesResponse> objresult = new List<ClientTariffServicesResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", getRequest?.venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", getRequest?.venueBranchNo);
                    var _DeptNo = new SqlParameter("departmentNo", getRequest?.departmentNo);
                    var _RateListNo = new SqlParameter("rateListNo", getRequest?.rateListNo);
                    var _Type = new SqlParameter("type", getRequest?.type);
                    var _clientNo = new SqlParameter("clientNo", getRequest?.clientNo);
                    var _PhysicianNo = new SqlParameter("physicianNo", getRequest?.physicianNo);

                    objresult = context.ClientTariffServiceListDTO.FromSqlRaw(
                        "Execute dbo.pro_ClientTariffSearchService @VenueNo,@VenueBranchNo,@departmentNo,@rateListNo,@type,@clientNo,@physicianNo",
                     _VenueNo, _VenueBranchNo, _DeptNo, _RateListNo, _Type, _clientNo, _PhysicianNo).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMasterRepository.GetClientTariffServiceList", ExceptionPriority.High, ApplicationType.REPOSITORY, getRequest?.venueNo, getRequest?.venueBranchNo, getRequest?.userNo);
            }
            return objresult;
        }
        public GetTariffupdateResponse GetTariffupdateList(GetTariffupdateRequest getRequest)
        {
            GetTariffupdateResponse obj = new GetTariffupdateResponse();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", getRequest?.venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", getRequest?.venueBranchNo);
                    var _ServiceType = new SqlParameter("ServiceType", getRequest?.ServiceType);
                    var _ServiceNo = new SqlParameter("ServiceNo", getRequest?.ServiceNo);
                    var _DeptNo = new SqlParameter("DeptNo", getRequest?.DeptNo);

                    obj = context.GetTariffupdateList.FromSqlRaw(
                        "Execute dbo.pro_Gettariff_walkin_branchwise @VenueNo,@VenueBranchNo,@ServiceType,@ServiceNo,@DeptNo",
                     _VenueNo, _VenueBranchNo, _ServiceType, _ServiceNo, _DeptNo).SingleOrDefault();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMasterRepository.GetTariffupdateList", ExceptionPriority.High, ApplicationType.REPOSITORY, getRequest?.venueNo, getRequest?.venueBranchNo, 0);
            }
            return obj;
        }

        public List<GetContractRes> GetContractMaster(GetContractReq req)
        {
            List<GetContractRes> objresult = new List<GetContractRes>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", req?.VenueNo);
                    var _ContractNo = new SqlParameter("ContractNo", req?.ContractNo);
                    var _PageIndex = new SqlParameter("PageIndex", req?.pageIndex);

                    objresult = context.GetContractMaster.FromSqlRaw(
                        "Execute dbo.Pro_GetContractMaster @VenueNo,@ContractNo,@pageIndex",
                     _VenueNo, _ContractNo, _PageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMasterRepository.GetContractMaster", ExceptionPriority.High, ApplicationType.REPOSITORY, req.VenueNo, 0);
            }
            return objresult;
        }

        public InsertContractRes InserContractMaster(InsertContractReq req)
        {
            InsertContractRes result = new InsertContractRes();
            CommonHelper commonUtility = new CommonHelper();

            try
            {
                string ContractListXml = commonUtility.ToXML(req?.serviceDetails);
                string ContractVsClient = commonUtility.ToXML(req?.ContractVsClient);

                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _ContractListXml = new SqlParameter("ContractListXml", ContractListXml);
                    var _VenueNo = new SqlParameter("venueNo", req?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("venueBranchNo", req?.VenueBranchNo);
                    var _ContractNo = new SqlParameter("ContractNo", req?.ContractNo);
                    var _Code = new SqlParameter("Code", req?.Code);
                    var _Description = new SqlParameter("Description", req?.Description);
                    var _ValidFrom = new SqlParameter("ValidFrom", req?.ValidFrom);
                    var _ValidTo = new SqlParameter("ValidTo", req?.ValidTo);
                    var _Status = new SqlParameter("Status", req?.Status);
                    var _UserNo = new SqlParameter("UserNo", req?.UserNo);
                    var _ContractVsClient = new SqlParameter("ContractVsClient", ContractVsClient);
                    var _IsApproval = new SqlParameter("IsApproval", req?.IsApproval);
                    var _IsReject = new SqlParameter("IsReject", req?.IsReject);
                    var _RejectReason = new SqlParameter("RejectReason", req?.RejectReason);
                    var _OldContractNo = new SqlParameter("OldContractNo", req?.OldContractNo);

                    var dbResponse = context.InserContractMaster.FromSqlRaw(
                        "Execute dbo.Pro_InsertContractMaster " +
                        "@VenueNo, @ContractListXml, @ContractNo, @Description, @Code, @ValidFrom," +
                        "@ValidTo, @Status, @UserNo, @VenueBranchNo,@ContractVsClient" +
                        ",@IsApproval,@IsReject,@RejectReason,@OldContractNo",
                     _VenueNo, _ContractListXml, _ContractNo, _Description, _Code, _ValidFrom, _ValidTo,
                     _Status, _UserNo, _VenueBranchNo, _ContractVsClient, _IsApproval
                     , _IsReject, _RejectReason, _OldContractNo).ToList();
                    result = dbResponse[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMasterRepository.InsertTariffMaster", ExceptionPriority.High, ApplicationType.REPOSITORY, req?.VenueNo, req?.VenueBranchNo, req.UserNo);
            }
            return result;
        }
        public List<TariffMastServicesResponse> GetContractMasterServiceList(GetContractMasterListRequest getRequest)
        {
            List<TariffMastServicesResponse> objresult = new List<TariffMastServicesResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", getRequest?.venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", getRequest?.venueBranchNo);
                    var _DeptNo = new SqlParameter("departmentNo", getRequest?.departmentNo);
                    var _ContractNo = new SqlParameter("ContractNo", getRequest?.ContractNo);
                    var _Servicetype = new SqlParameter("Servicetype", getRequest?.ServiceType);
                    var _ServiceNo = new SqlParameter("ServiceNo", getRequest?.ServiceNo);
                    var _IsApproval = new SqlParameter("IsApproval", getRequest?.IsApproval);

                    objresult = context.ContractMasterServiceListDTO.FromSqlRaw(
                        "Execute dbo.pro_ContractMasterSearchService @VenueNo,@VenueBranchNo,@departmentNo,@ContractNo,@ServiceType,@ServiceNo,@IsApproval",
                     _VenueNo, _VenueBranchNo, _DeptNo, _ContractNo, _Servicetype, _ServiceNo, _IsApproval).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMasterRepository.GetContractMasterServiceList", ExceptionPriority.High, ApplicationType.REPOSITORY, getRequest?.venueNo, getRequest?.venueBranchNo, getRequest?.userNo);
            }
            return objresult;
        }
        public List<ContractVsCustomerMap> GetContractVsClient(GetContractVsClientReq getRequest)
        {
            List<ContractVsCustomerMap> objresult = new List<ContractVsCustomerMap>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _ContractNo = new SqlParameter("ContractNo", getRequest?.ContractNo);
                    var _VenueNo = new SqlParameter("VenueNo", getRequest?.VenueNo);
                    var _IsApproval = new SqlParameter("IsApproval", getRequest?.IsApproval);

                    var clientlst = context.GetContractVsClient.FromSqlRaw("Execute dbo.pro_GetContractVSClient @ContractNo,@VenueNo,@IsApproval", _ContractNo, _VenueNo, _IsApproval).ToList();


                    int oldClientNo = 0;
                    int newClientNo = 0;

                    objresult = new List<ContractVsCustomerMap>();
                    foreach (var obj in clientlst)
                    {
                        ContractVsCustomerMap ClientItem = new ContractVsCustomerMap();
                        List<ContractVsSubCustomerMap> lstSubCustomerMap = new List<ContractVsSubCustomerMap>();
                        newClientNo = obj.ClientNo;
                        var subclientlst = clientlst.Where(x => x.MapCustomerNo == newClientNo).Select(x => new { x.MapCustomerNo, x.SubCustomerNo, x.SubCustomerName, x.SubClientStatus, x.ContractClientNo }).ToList();
                        if (newClientNo != oldClientNo)
                        {
                            ClientItem.ClientNo = obj.ClientNo;
                            ClientItem.ClientName = obj.ClientName;
                            ClientItem.ContractMasterNo = obj.ContractMasterNo;
                            ClientItem.ContractClientNo = obj.ContractClientNo;
                            var Clientstatus = clientlst.Where(x => x.ClientNo == obj.ClientNo && x.ClientStatus == true).ToList();
                            if (Clientstatus.Count > 0)
                                ClientItem.Status = true;
                            oldClientNo = obj.ClientNo;

                            lstSubCustomerMap = new List<ContractVsSubCustomerMap>();
                            foreach (var Sitem in subclientlst)
                            {
                                ContractVsSubCustomerMap SubCustomerMap = new ContractVsSubCustomerMap()
                                {
                                    MapCustomerNo = Sitem.MapCustomerNo,
                                    SubCustomerName = Sitem.SubCustomerName,
                                    Status = Sitem.SubClientStatus,
                                    SubCustomerNo = Sitem.SubCustomerNo
                                };
                                lstSubCustomerMap.Add(SubCustomerMap);
                                ClientItem.SubCustomerMap = lstSubCustomerMap;

                            }
                            objresult.Add(ClientItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMasterRepository.GetContractVsClient", ExceptionPriority.High, ApplicationType.REPOSITORY, getRequest?.VenueNo, getRequest?.ContractNo, 0);
            }
            return objresult;
        }
        public InsTariffRes InsertClienttTariffMap(InsTariffReq req)
        {
            InsTariffRes result = new InsTariffRes();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _ClientTariffMapNo = new SqlParameter("ClientTariffMapNo", req?.ClientTariffMapNo);
                    var _RefTypeNo = new SqlParameter("RefTypeNo", req?.RefTypeNo);
                    var _ReferrerNo = new SqlParameter("ReferrerNo", req?.ReferrerNo);
                    var _RateListNo = new SqlParameter("RateListNo", req?.RateListNo);
                    var _EffectiveFrom = new SqlParameter("EffectiveFrom", req?.EffectiveFrom);
                    var _EffectiveTo = new SqlParameter("EffectiveTo", req?.EffectiveTo);
                    var _Status = new SqlParameter("Status", req?.Status);
                    var _VenueNo = new SqlParameter("VenueNo", req?.VenueNo);
                    var _UserNo = new SqlParameter("UserNo", req?.UserNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req?.VenueBranchNo);

                    var dbResponse = context.InsertClienttTariffMap.FromSqlRaw(
                        "Execute dbo.pro_InsertClientTariffMapping " +
                        "@ClientTariffMapNo, @RefTypeNo, @ReferrerNo, @RateListNo, @EffectiveFrom, @EffectiveTo," +
                        "@Status, @VenueNo, @UserNo, @VenueBranchNo",
                     _ClientTariffMapNo, _RefTypeNo, _ReferrerNo, _RateListNo, _EffectiveFrom, _EffectiveTo, _Status,
                     _VenueNo, _UserNo, _VenueBranchNo).ToList();
                    result = dbResponse[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMasterRepository.InsertReferrerTariffMap", ExceptionPriority.High, ApplicationType.REPOSITORY, req?.VenueNo, req?.VenueBranchNo, req.UserNo);
            }
            return result;
        }
        public List<GetTariffRes> GetClienttTariffMap(GetTariffReq req)
        {
            List<GetTariffRes> objresult = new List<GetTariffRes>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _ClientTariffMapNo = new SqlParameter("ClientTariffMapNo", req?.ClientTariffMapNo);
                    var _RefTypeNo = new SqlParameter("RefTypeNo", req?.RefTypeNo);
                    var _ReferrerNo = new SqlParameter("ReferrerNo", req?.ReferrerNo);
                    var _RateListNo = new SqlParameter("RateListNo", req?.RateListNo);
                    var _VenueNo = new SqlParameter("VenueNo", req?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req?.VenueBranchNo);
                    var _pageIndex = new SqlParameter("pageIndex", req?.pageIndex);

                    objresult = context.GetClienttTariffMap.FromSqlRaw(
                    "Execute dbo.pro_GetClientTariffMapping @ClientTariffMapNo, @RefTypeNo, @ReferrerNo, @RateListNo, @VenueNo, @VenueBranchNo, @pageIndex",
                    _ClientTariffMapNo, _RefTypeNo, _ReferrerNo, _RateListNo, _VenueNo, _VenueBranchNo, _pageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMasterRepository.GetClienttTariffMap", ExceptionPriority.High, ApplicationType.REPOSITORY, req?.VenueNo, 0, 0);
            }
            return objresult;
        }
        public List<TariffMastServicesResponse> GetRefSplRateServiceList(GetContractMasterListRequest getRequest)
        {
            List<TariffMastServicesResponse> objresult = new List<TariffMastServicesResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", getRequest?.venueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", getRequest?.venueBranchNo);
                    var _DeptNo = new SqlParameter("departmentNo", getRequest?.departmentNo);
                    var _ContractNo = new SqlParameter("ContractNo", getRequest?.ContractNo);
                    var _Servicetype = new SqlParameter("Servicetype", getRequest?.ServiceType);
                    var _ServiceNo = new SqlParameter("ServiceNo", getRequest?.ServiceNo);
                    var _IsRateShow = new SqlParameter("IsRateShow", getRequest?.ismodified);

                    objresult = context.GetRefSplRateServiceList.FromSqlRaw(
                        "Execute dbo.pro_RefSplPriceSearchService @VenueNo,@VenueBranchNo,@departmentNo,@ContractNo,@ServiceType,@ServiceNo,@IsRateShow",
                     _VenueNo, _VenueBranchNo, _DeptNo, _ContractNo, _Servicetype, _ServiceNo, _IsRateShow).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMasterRepository.GetRefSplRateServiceList", ExceptionPriority.High, ApplicationType.REPOSITORY, getRequest?.venueNo, getRequest?.venueBranchNo, getRequest?.userNo);
            }
            return objresult;
        }
        public List<GetReflstRes> GetReflst(GetContractReq req)
        {
            List<GetReflstRes> objresult = new List<GetReflstRes>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", req?.VenueNo);
                    var _ContractNo = new SqlParameter("ContractNo", req?.ContractNo);
                    var _PageIndex = new SqlParameter("PageIndex", req?.pageIndex);

                    objresult = context.GetReflst.FromSqlRaw(
                        "Execute dbo.Pro_GetRefSplPrice @VenueNo,@ContractNo,@pageIndex",
                     _VenueNo, _ContractNo, _PageIndex).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMasterRepository.GetReflst", ExceptionPriority.High, ApplicationType.REPOSITORY, req.VenueNo, 0);
            }
            return objresult;
        }
        public InsertContractRes InsertReferrerlst(InsertReflstReq req)
        {
            InsertContractRes result = new InsertContractRes();
            CommonHelper commonUtility = new CommonHelper();

            try
            {
                string SplPriceListXml = commonUtility.ToXML(req?.serviceDetails);

                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _RefSplNo = new SqlParameter("RefSplNo", req?.RefSplNo);
                    var _SplPriceListXml = new SqlParameter("SplPriceListXml", SplPriceListXml);
                    var _RefTypeNo = new SqlParameter("RefTypeNo", req?.RefTypeNo);
                    var _ReferrerNo = new SqlParameter("ReferrerNo", req?.ReferrerNo);
                    var _VenueNo = new SqlParameter("VenueNo", req?.VenueNo);
                    var _Status = new SqlParameter("Status", req?.Status);
                    var _UserNo = new SqlParameter("UserNo", req?.UserNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req?.VenueBranchNo);

                    var dbResponse = context.InsertReferrerlst.FromSqlRaw(
                        "Execute dbo.Pro_InsertRefSplPrice " +
                        "@RefSplNo, @SplPriceListXml, @RefTypeNo, @ReferrerNo, @VenueNo, @Status," +
                        "@UserNo, @VenueBranchNo",
                     _RefSplNo, _SplPriceListXml, _RefTypeNo, _ReferrerNo, _VenueNo, _Status, _UserNo, _VenueBranchNo).ToList();
                    result = dbResponse[0];
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMasterRepository.InsertReferrerlst", ExceptionPriority.High, ApplicationType.REPOSITORY, req?.VenueNo, req?.VenueBranchNo, req.UserNo);
            }
            return result;
        }
        public List<Tariffdeptdisreq> GetTariffDeptDiscount(Tariffdeptdis req)
        {
            List<Tariffdeptdisreq> objresult = new List<Tariffdeptdisreq>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _RateListNo = new SqlParameter("RateListNo", req?.RateListNo);
                    var _VenueNo = new SqlParameter("VenueNo", req?.VenueNo);
                    var _IsApproval = new SqlParameter("IsApproval", req?.IsApproval);

                    objresult = context.GetTariffDeptDiscount.FromSqlRaw(
                        "Execute dbo.pro_GetTariffDeptDiscount @RateListNo,@VenueNo,@IsApproval",
                     _RateListNo, _VenueNo, _IsApproval).ToList();
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMasterRepository.GetTariffDeptDiscount", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0);
            }
            return objresult;
        }
        public RateHistoryServiceResponse GetPriceHistory(RateHistoryServiceRequest req)
        {
            RateHistoryServiceResponse objresult = new RateHistoryServiceResponse();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", req?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req?.VenueBranchNo);
                    var _ContractNo = new SqlParameter("ContractNo", req?.ContractNo);
                    var _PageCode = new SqlParameter("PageCode", req?.PageCode);
                    var _RateListNo = new SqlParameter("RateListNo", req?.RateListNo);
                    var _RefTypeNo = new SqlParameter("RefTypeNo", req?.RefTypeNo);
                    var _ReferrerNo = new SqlParameter("ReferrerNo", req?.ReferrerNo);
                    var _ServiceType = new SqlParameter("ServiceType", req?.ServiceType);
                    var _ServiceNo = new SqlParameter("ServiceNo", req?.ServiceNo);

                    var objRes = context.GetPriceHistory.FromSqlRaw(
                        "Execute dbo.Pro_ServiceRate_History " +
                        "@VenueNo, @VenueBranchNo, @PageCode, @RateListNo, @ContractNo, @RefTypeNo, @ReferrerNo, @ServiceType, @ServiceNo",
                        _VenueNo, _VenueBranchNo, _PageCode, _RateListNo, _ContractNo, _RefTypeNo, _ReferrerNo, _ServiceType, _ServiceNo).AsEnumerable().FirstOrDefault();

                    objresult.RowNo = objRes.RowNo;
                    objresult.EntityName = objRes.EntityName;
                    objresult.ServiceType = objRes.ServiceType;
                    objresult.ServiceName = objRes.ServiceName;
                    objresult.RateHistory = JsonConvert.DeserializeObject<List<PriceHistoryService>>(objRes.RateHistory);
                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMasterRepository.GetReflst", ExceptionPriority.High, ApplicationType.REPOSITORY, (short)req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return objresult;
        }
        public List<BaseRateResponse> GetBasePrice(RateHistoryServiceRequest req)
        {
            List<BaseRateResponse> objresult = new List<BaseRateResponse>();
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    var _VenueNo = new SqlParameter("VenueNo", req?.VenueNo);
                    var _VenueBranchNo = new SqlParameter("VenueBranchNo", req?.VenueBranchNo);
                    var _ServiceType = new SqlParameter("ServiceType", req?.ServiceType);
                    var _ServiceNo = new SqlParameter("ServiceNo", req?.ServiceNo);

                    objresult = context.GetBasePrice.FromSqlRaw(
                        "Execute dbo.Pro_GetBaseRate " +
                        "@VenueNo, @VenueBranchNo, @ServiceType, @ServiceNo", _VenueNo, _VenueBranchNo, _ServiceType, _ServiceNo).ToList();

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMasterRepository.GetBasePrice", ExceptionPriority.High, ApplicationType.REPOSITORY, (short)req.VenueNo, req.VenueBranchNo, req.UserNo);
            }
            return objresult;
        }
        public int InsertBaseRate(List<BaseRateResponse> req)
        {
            CommonHelper commonUtility = new CommonHelper();
            int objresult = 0;
            try
            {
                using (var context = new MasterContext(_config.GetConnectionString(ConfigKeys.DefaultConnection)))
                {
                    string BasePriceListXml = commonUtility.ToXML(req);
                    var _BasePriceListXml = new SqlParameter("BasePriceListXml", BasePriceListXml);

                    var result = context.InsertBaseRatelst.FromSqlRaw(
                         "Execute dbo.Pro_InsertBaseRate " +
                         "@BasePriceListXml", _BasePriceListXml).AsEnumerable().FirstOrDefault();

                    objresult = result.result;

                }
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TariffMasterRepository.InsertBaseRate", ExceptionPriority.High, ApplicationType.REPOSITORY, 0, 0, 0);
            }
            return objresult;
        }
    }
}
