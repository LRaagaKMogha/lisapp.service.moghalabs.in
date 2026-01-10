using DEV.Common;
using DEV.Model.Common;
using DEV.Model.Inventory;
using DEV.Model.Master;
using DEV.Model.Sample;
using DEV.Model.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections;
using static Dapper.SqlMapper;

namespace DEV.Model.EF
{
    public partial class MasterContext : DbContext
    {
        public string _connectionstring = string.Empty;
        public MasterContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public MasterContext(DbContextOptions<MasterContext> options)
            : base(options)
        {
        }
        public virtual DbSet<CommonMasterDto> CommonMasterDTO { get; set; }
        public virtual DbSet<ConfigurationDto> ConfigurationDTO { get; set; }
        public virtual DbSet<TblUser> TblUser { get; set; }
        public virtual DbSet<TblDepartment> TblDepartment { get; set; }
        public virtual DbSet<TblMethod> TblMethod { get; set; }
        public virtual DbSet<TblPhysician> TblPhysician { get; set; }
        public virtual DbSet<TblUnits> TblUnits { get; set; }
        public virtual DbSet<TblOrganism> TblOrganism { get; set; }
        public virtual DbSet<lstotdrugmap> GetOrgTypeAntiMapList { get; set; }
        public virtual DbSet<TblTemplate> GetTemplateList { get; set; }
        public virtual DbSet<CustomerResponse> GetClientMasterDTO { get; set; }
        public virtual DbSet<InsertCustomerResponse> InsertClientMaster { get; set; }
        public virtual DbSet<InsertCustomersubuserResponse> InsertClientsubuserMaster { get; set; }
        public virtual DbSet<CustomerReturnResponse> InsertSubClientMapping { get; set; }
        //Start of Tariff
        public virtual DbSet<GetTariffMasterResponse> GetTariffMasterDTO { get; set; }
        public virtual DbSet<InsertTariffMasterResponse> InsertTariffMasterDTO { get; set; }
        public virtual DbSet<GetServices> GetServiceDetailsDTO { get; set; }
        public virtual DbSet<GetTariffMasterListResponse> GetTariffMasterListDTO { get; set; }
        public virtual DbSet<TariffMastServicesResponse> TariffMasterServiceListDTO { get; set; }
        public virtual DbSet<TariffMasterInsertResponse> TariffMasterInsertDTO { get; set; }
        public virtual DbSet<GetClientTariffMasterListResponse> GetClientTariffMasterListDTO { get; set; }
        public virtual DbSet<CTMInsertResponse> ClientTariffMasterInsertDTO { get; set; }
        public virtual DbSet<ClientTariffServicesResponse> ClientTariffServiceListDTO { get; set; }
        // End of Tariff
        public virtual DbSet<UserDetailsDTO> UserDetailsEF { get; set; }
        public virtual DbSet<UserResponseDTO> UserResponseEF { get; set; }
        public virtual DbSet<UserMenuMappingDTO> UserMenuMappingEF { get; set; }
        public virtual DbSet<UserMenuTaskDTO> UserMenuTaskEF { get; set; }
        public virtual DbSet<UserResponseDTO> UserInsertMenuEF { get; set; }
        public virtual DbSet<UserNavDTO> UserNavEF { get; set; }
        public virtual DbSet<UserSessionResponseDTO> usersesssionUpdate { get; set; }
        public virtual DbSet<UserMenuNosResponseDTO> GetUserMenuNosEF { get; set; }

        //Start Test Master
        public virtual DbSet<objTestList> GetTestList { get; set; }
        public virtual DbSet<objtestdbl> GetEditTest { get; set; }
        public virtual DbSet<rtntest> InsertTest { get; set; }
        public virtual DbSet<rtntemplateNo> InsertTemplateText { get; set; }
        //public virtual DbSet<returntemplateNo> GetTemplateMasterDetails { get; set; }
        public virtual DbSet<rtntest> UpdateSequence { get; set; }
        //End Test Master

        //Start Group/Package Master
        public virtual DbSet<lstgrppkg> GetGroupPackageList { get; set; }
        public virtual DbSet<objgrppkgdbl> GetEditGroupPackage { get; set; }
        public virtual DbSet<rtntest> InsertGroupPackage { get; set; }
        public virtual DbSet<lstgrppkgservice> GetSearchService { get; set; }
        //End Group/Package Master

        //Sub test master
        public virtual DbSet<lststestdbl> GetSubTestList { get; set; }
        public virtual DbSet<objsubtestdbl> GetEditSubTest { get; set; }
        public virtual DbSet<rtntest> InsertSubTest { get; set; }
        //End
        public virtual DbSet<lstorgAntiRange> GetOrgAntibioticRange { get; set; }
        public virtual DbSet<rtntest> SaveOrganismAntibioticRange { get; set; }
        public virtual DbSet<CustomerMappingDTO> GetCustomerMapping { get; set; }
        public virtual DbSet<CustomerMappingDTO> GetCLinic { get; set; }
        public virtual DbSet<ClientSubClientMappingDTO> GetAllClients { get; set; }
        public virtual DbSet<ClientSubUserResponse> GetClientSubUserResponse { get; set; }

        // public virtual DbSet<CommonResponse> PhysicianMerging { get; set; }
        public virtual DbSet<ClientRestrictionDayResponse> ClientRestriction { get; set; }
        public virtual DbSet<CommonResponse> PhysicianMerging { get; set; }

        //Inventory modal
        public virtual DbSet<TblProductCategory> GetProductCategory { get; set; }
        public virtual DbSet<ProductcategoryResponse> InsertproductCategory { get; set; }
        public virtual DbSet<GetProductMainbyDeptRes> GetProductSubyMaindept { get; set; }
        public virtual DbSet<lstdrugresponse> GetProdVsDrug { get; set; }
        public virtual DbSet<savedruglstresponse> InsertProdVsDrugs { get; set; }
        public virtual DbSet<GetProductMasterResponse> GetProductMasterDTO { get; set; }
        public virtual DbSet<GetSupplierMappingDTO> GetSupplierMappingDTO { get; set; }
        public virtual DbSet<GetDepartmentMappingDTO> GetDepartmentMappingDTO { get; set; }
        public virtual DbSet<CommonAdminResponse> CreateProductMasterDTO { get; set; }
        public virtual DbSet<tbl_IV_ProductMaster> tbl_IV_ProductMaster { get; set; }
        public virtual DbSet<UpdateSupplierMaster> Tbl_IV_Suppliers { get; set; }
        public virtual DbSet<GetManufacturerMasterResponse> GetManufacturersDetail { get; set; }
        public virtual DbSet<postManufacturerMasterDTO> InsertManufacturerDetails { get; set; }
        public virtual DbSet<GetPurchaseOrderResponse> GetPurchaseOrderDTO { get; set; }
        public virtual DbSet<GetSupplierServiceDTO> GetSupplierServiceDTO { get; set; }
        public virtual DbSet<GetPOBySupplierResponse> GetPOBySupplierDetailsDTO { get; set; }
        public virtual DbSet<GetStockProductListResponse> GetProductListByDeapartmentDTO { get; set; }
        public virtual DbSet<GetStockReportResponse> GetStockReport { get; set; }
        public virtual DbSet<GetProductsByPOResponse> GetProductByPODTO { get; set; }
        public virtual DbSet<GetProductsByPOResponse> GetGRNDetailsByGRNDTO { get; set; }
        public virtual DbSet<GetProductsByGRNResponse> GetProductByGRNDTO { get; set; }
        public virtual DbSet<GetProductbatchdetailsbyPO> GetProGRNBatchDetails { get; set; }
        public virtual DbSet<CommonAdminResponse> CreatePurchaseOrderDTO { get; set; }
        public virtual DbSet<CommonAdminResponse> CreateGRNMasterDTO { get; set; }
        public virtual DbSet<GetPurchaseDetailsDTO> GetPurchaseDetailsDTO { get; set; }
        public virtual DbSet<CommonAdminResponse> CreateStockUploadDTO { get; set; }
        public virtual DbSet<FetchProductListResponse> FetchProductListDTO { get; set; }
        public virtual DbSet<GetSupplierMasterResponse> GetSupplierDetails { get; set; }
        //public virtual DbSet<GetManufacturerMasterResponse> GetManufacturersDetail { get; set; }
        public virtual DbSet<SupplierResponse> CreateSupplierMasterDTO { get; set; }
        public virtual DbSet<CommonAdminResponse> CreateManufacturerMasterDTO { get; set; }
        public virtual DbSet<POProductDetailsDTO> GetPOProductDetailsDTO { get; set; }
        public virtual DbSet<GetTaxDatilsResponse> GetPOTaxDetailsDTO { get; set; }
        public virtual DbSet<otherChargeModal> GetPOOCDetailsDTO { get; set; }
        public virtual DbSet<otherChargeModal> GetGRNOCDetailsDTO { get; set; }
        public virtual DbSet<Termsconditionlist> GetPOTermsDetailsDTO { get; set; }
        public virtual DbSet<GetAllGRNResponse> GetAllGRNDetailsDTO { get; set; }
        public virtual DbSet<GetAllGRNReturnResponse> GetAllGRNReturnDetailsDTO { get; set; }
        public virtual DbSet<GetGRNBySupplierResponse> GetGRNBySupplierDetailsDTO { get; set; }
        public virtual DbSet<CommonAdminResponse> CreateGRNReturnDTO { get; set; }
        public virtual DbSet<GetProductsByGRNNo> GetGRNReturnProductDTO { get; set; }
        public virtual DbSet<GetConsumptionMappingResponse> GetConsumptionMappingDTO { get; set; }
        public virtual DbSet<CommonAdminResponse> CreateConsumptionMappingDTO { get; set; }
        public virtual DbSet<ReagentOpeningStockResponse> GetReagentOpeningStockDTO { get; set; }
        public virtual DbSet<CommonAdminResponse> CreateReagentOpeningStockDTO { get; set; }
        public virtual DbSet<GetStockCorrectionResponse> GetStockCorrectionDTO { get; set; }
        public virtual DbSet<GetStockAdjustmentResponse> GetStockAdjustmentDTO { get; set; }
        public virtual DbSet<GetProductStockResponse> GetProductStockDTO { get; set; }
        public virtual DbSet<LstStockAdjustProductDetailsResponse> GetStockAdjustmentProductDTO { get; set; }
        public virtual DbSet<GetStoreStockProductListResponse> GetStockStockProductListDTO { get; set; }
        public virtual DbSet<CommonAdminResponse> CreateStockCorrectionDTO { get; set; }
        public virtual DbSet<CommonAdminResponse> CreateStockAdjustmentDTO { get; set; }
        public virtual DbSet<CommonAdminResponse> CreateStockConsumptionDTO { get; set; }
        public virtual DbSet<GetParameterAnalyserResponse> GetParameterAnalyserDTO { get; set; }
        public virtual DbSet<CommonAdminResponse> CreateParameterAnalyserDTO { get; set; }
        public virtual DbSet<GetAllConsumptionListResponse> GetAllConsumptionListDTO { get; set; }
        public virtual DbSet<ConsumptionDetailsInListResponse> GetStockConsumptionDetailsDTO { get; set; }
        public virtual DbSet<CommonAdminResponse> UpdateGRNInvoiceDTO { get; set; }

        //Multi price list
        public virtual DbSet<GetmultiPriceListResponse> GetMultiPriceListDTO { get; set; }
        public virtual DbSet<InsertMultiPriceListResponse> InsertMultiPriceListDTO { get; set; }
        public virtual DbSet<rtnUnit> InsertUnit { get; set; }
        public virtual DbSet<lstunits> GetUnitList { get; set; }
        public virtual DbSet<IndentDetailsSaveResponse> CreateIndentProductDTO { get; set; }
        public virtual DbSet<IndentDetailsResponse> GetIndentDetails { get; set; }
        public virtual DbSet<IndentProductDetailsNewResponse> GetIndentProductDetailsls { get; set; }
        public virtual DbSet<GetIssueProductResponse> GetIssueProduct { get; set; }
       public virtual DbSet<GetIssueProductByIssueNoResponse> GetIssueProductByIssueNo { get; set; }
        public virtual DbSet<SaveIssueProductResponse> InsertIssueProduct { get; set; }
        public virtual DbSet<SaveFormulaResponse> InsertTestFormula { get; set; }
        public virtual DbSet<GetFormulaResponse> GetTestFormula { get; set; }
        public virtual DbSet<orggetresponse> GetOrgmaster { get; set; }
        public virtual DbSet<orgGrpresponse> GetOrgGrpmaster { get; set; }
        public virtual DbSet<orginsertresponse> InsertOrgmaster { get; set; }
        public virtual DbSet<orginsertGrpresponse> InsertOrgGrpmaster { get; set; }
        public virtual DbSet<orgtyperesponse> GetOrgtypemaster { get; set; }
        public virtual DbSet<orgtypeinsertresponse> InsertOrgtypemaster { get; set; }
        public virtual DbSet<CheckMasterNameExistsResponse> CheckfunMasterExists { get; set; }
        public virtual DbSet<CheckMasterNameExistsResponse> Checkfun2MasterExists { get; set; }
        public virtual DbSet<TblProductType> Getproducttype { get; set; }
        public virtual DbSet<ProductTypeMasterResponse> Insertproducttype { get; set; }
        public virtual DbSet<antiresponse> Getantibiotic { get; set; }
        public virtual DbSet<antinsertresponse> Insertantimaster { get; set; }
        public virtual DbSet<orgAntiresponse> Getantirog { get; set; }
        public virtual DbSet<organtinsertresponse> Insertantiorg { get; set; }
        public virtual DbSet<TblHSN> GetHSNMasters { get; set; }
        public virtual DbSet<HSNMasterResponse> InsertHSNmaster { get; set; }
        public virtual DbSet<TblHSNRange> GetHSNRangeMaster { get; set; }
        public virtual DbSet<HSNInsertResponse> InsertHSNRangeMaster { get; set; }
        //pharmacy master

        public virtual DbSet<TblGeneric> GetGeneric { get; set; }
        public virtual DbSet<TblMedtype> GetMedicinetype { get; set; }
        public virtual DbSet<TblMedstr> GetMedstr { get; set; }
        public virtual DbSet<GenericMasterResponse> InsertGeneric { get; set; }
        public virtual DbSet<MedtypeMasterResponse> InsertMedtype { get; set; }
        public virtual DbSet<MedstrMasterResponse> InsertMedstr { get; set; }

        public virtual DbSet<responsegetvendor> GetVendorMaster { get; set; }
        public virtual DbSet<StoreVendorMaster> InsertVendorMaster { get; set; }
        public virtual DbSet<TblCountry> GetCountrymaster { get; set; }
        public virtual DbSet<CountryMasteResponse> InsertCountrymaster { get; set; }
        public virtual DbSet<EditSupplier> GetEditSuppiler { get; set; }
        public virtual DbSet<CommonResponse> PhysicianHaveVisits { get; set; }
        public virtual DbSet<lstState> GetStatemaster { get; set; }
        public virtual DbSet<StateResponse> InsertStatemaster { get; set; }
        public virtual DbSet<RoleResponseDTO> RoleInsertMenuEF { get; set; }
        public virtual DbSet<UserMenuMappingDTO> RoleMenuMappingEF { get; set; }
        public virtual DbSet<CityLst> GetCitymaster { get; set; }
        public virtual DbSet<CityResponse> InsertCitymaster { get; set; }
        public virtual DbSet<PlaceLst> GetPlacemaster { get; set; }
        public virtual DbSet<PlaceResponse> InsertPlacemaster { get; set; }
        public virtual DbSet<NationalityLst> GetNationalityMaster { get; set; }
        public virtual DbSet<NationalityResponse> InsertNationalitymaster { get; set; }
        public virtual DbSet<PhysicianNo> SavePhysicianDetaile { get; set; }
        public virtual DbSet<GetServiceDetails> GetServiceOrder { get; set; }
        public virtual DbSet<ServiceOrderMasterResponse> InsertServiceOrder { get; set; }
        public virtual DbSet<getcontactlst> GetVendorvsContactmaster { get; set; }
        public virtual DbSet<StorecontactMaster> InsertVendorContactmaster { get; set; }
        public virtual DbSet<getservicelst> GetVendorvsservices { get; set; }
        public virtual DbSet<storeservice> InsertVendorService { get; set; }
        public virtual DbSet<Fetchlookalike> lookalike { get; set; }
        public virtual DbSet<Fetchsoundalike> Soundalike { get; set; }
        public virtual DbSet<responsebranch> GetProcessingbranch { get; set; }
        public virtual DbSet<Storeprocessingbranch> InsertProcessingbranch { get; set; }
        public virtual DbSet<GetDeptIssueProductResponse> GetDeptIssueProductlst { get; set; }
        public virtual DbSet<GetTariffupdateResponse> GetTariffupdateList { get; set; }
        public virtual DbSet<AnaParamDtoResponse> InsertAnalyzerParameter { get; set; }
        public virtual DbSet<AnaParamGetDto> GetAnalyzerParameter { get; set; }
        public virtual DbSet<FetchAnaParamDto> FetchAnalyzerParameter { get; set; }
        public virtual DbSet<TblMethod> GetMethods { get; set; }
        public virtual DbSet<MethodResponse> InsertMethodDetails { get; set; }
        public virtual DbSet<ConfigurationDto> GetSingleConfiguration { get; set; }
        public virtual DbSet<returnquotationlst> Getquotation { get; set; }
        public virtual DbSet<storequotationlst> Insertquotation { get; set; }
        public virtual DbSet<DocVsSerResponse> Getdoctorlst { get; set; }
        public virtual DbSet<DocVsSerGetRes> GetdocVsSerlst { get; set; }
        public virtual DbSet<DocVsSerInsRes> InsertdocVsSer { get; set; }
        public virtual DbSet<GetTblqcmaster> Getqcmaster { get; set; }
        public virtual DbSet<QcMasterResponse> Insertqcmaster { get; set; }
        public virtual DbSet<Tblqcmaster> updateqcmaster { get; set; }
        public virtual DbSet<Qclotresponse> Qclotlist { get; set; }
        public virtual DbSet<Qclevelresponse> Qclevellist { get; set; }
        public virtual DbSet<Qclowhighresponse> Qclowhighlist { get; set; }
        public virtual DbSet<DocVsSerAppRes> GetdocVsSerApproval { get; set; }
        public virtual DbSet<DocVsSerAppdetailsRes> GetdocVsSerAppDetails { get; set; }
        public virtual DbSet<DocVsSerProfInsRes> InsertdocVsSerProf { get; set; }
        public virtual DbSet<CommonResponse> DocumentUploadDetails { get; set; }
        public virtual DbSet<PhysicianDocUploadRes> GetPhysicianDocumentDetails { get; set; }
        public virtual DbSet<PhysicianDocUploadRes> GetClientDocumentDetails { get; set; }
        public virtual DbSet<GetTblqcresult> Getqcresult { get; set; }
        public virtual DbSet<QcresultResponse> Insertqcresult { get; set; }
        public virtual DbSet<Tblqcresult> editqcresult { get; set; }
        public virtual DbSet<TbltestMap> GetAnalVsParamVsTest { get; set; }
        public virtual DbSet<analVsparamVstestMap> InsertAnalVsParamVsTest { get; set; }
        public virtual DbSet<subresponse> GetSubTest { get; set; }
        public virtual DbSet<SubProductRes> SubProduct { get; set; }
        public virtual DbSet<CommentGetRes> Getcomment { get; set; }
        public virtual DbSet<CommentInsRes> Insertcomment { get; set; }
        public virtual DbSet<CommentSubCatyInsResponse> InsertSubCatyComment { get; set; }
        public virtual DbSet<FetchCommentSubCategoryResponse> GetSubCatyComment { get; set; }
        public virtual DbSet<ActionMenuNoResponseDTO> GetActionMenuEF { get; set; }
        public virtual DbSet<ActionMenuCodeResponseDTO> GetActionMenuCodeEF { get; set; }
        public virtual DbSet<UserMenuCodeDTO> GetUserMenuCodeEF { get; set; }
        public virtual DbSet<UserRoleNameDTO> UserRoleNameDTOs { get; set; }
        public virtual DbSet<CommericalGetRes> Getcompany { get; set; }
        public virtual DbSet<CommericalInsRes> Insertcompany { get; set; }
        public virtual DbSet<GetNationRaceRes> GetNationRace { get; set; }
        public virtual DbSet<InsNationRaceRes> InsNationRace { get; set; }
        public virtual DbSet<CheckTestcodeExistsRes> GetCheckTestcodeExists { get; set; }
        public virtual DbSet<GSTGetRes> GetGST { get; set; }
        public virtual DbSet<GSTInsRes> InsertGST { get; set; }
        public virtual DbSet<restestapprove> GetTestApprove { get; set; }
        public virtual DbSet<restestappHistory> GetApproveHistory { get; set; }
        public virtual DbSet<GetTATRes> GetTATMaster { get; set; }
        public virtual DbSet<InsTATRes> InsertTATMaster { get; set; }
        public virtual DbSet<GetloincRes> GetLoincMaster { get; set; }
        public virtual DbSet<InsloincRes> InsertLoincMaster { get; set; }
        public virtual DbSet<CommonTokenResponse> CreateSlidePrintingDTO { get; set; }
        public virtual DbSet<AppSettingResponse> GetSingleAppSettings { get; set; }
        public virtual DbSet<GetContractRes> GetContractMaster { get; set; }
        public virtual DbSet<InsertContractRes> InserContractMaster { get; set; }
        public virtual DbSet<TariffMastServicesResponse> ContractMasterServiceListDTO { get; set; }
        public virtual DbSet<GetContractVsClientRes> GetContractVsClient { get; set; }
        public virtual DbSet<GetSnomedRes> GetSnomedMaster { get; set; }
        public virtual DbSet<InsSnomedRes> InsertSnomedMaster { get; set; }
        public virtual DbSet<IntegrationPackageRes> GetIntegrationPackageRes { get; set; }
        public virtual DbSet<IntegrationPackageResult> InsertIntegrationPackageResult { get; set; }
        public virtual DbSet<InsTariffRes> InsertClienttTariffMap { get; set; }
        public virtual DbSet<GetTariffRes> GetClienttTariffMap { get; set; }
        public virtual DbSet<TariffMastServicesResponse> GetRefSplRateServiceList { get; set; }
        public virtual DbSet<GetReflstRes> GetReflst { get; set; }
        public virtual DbSet<InsertContractRes> InsertReferrerlst { get; set; }
        public virtual DbSet<InsertBaseRateRes> InsertBaseRatelst { get; set; }
        public virtual DbSet<Tariffdeptdisreq> GetTariffDeptDiscount { get; set; }
        public virtual DbSet<ContractVsSubCustomerMap> GetSubContractVsClient { get; set; }
        public virtual DbSet<GetRoleRes> GetRoleMaster { get; set; }
        public virtual DbSet<InsertRoleRes> InsertRoleMaster { get; set; }
        public virtual DbSet<PriceHistoryServiceResponse> GetPriceHistory { get; set; }
        public virtual DbSet<BaseRateResponse> GetBasePrice { get; set; }        
        public virtual DbSet<TemplateCommentRes> TemplateInsertcomment { get; set; }
        public virtual DbSet<objgrppkgdbl> GetPackageInstrauction { get; set; }
        public virtual DbSet<PrintPackageDetails> GetPrintPakg { get; set; }
        public virtual DbSet<GetStatinMasterDetailsRes> GetStainMasterDetails { get; set; }
        public virtual DbSet<StainMasterInsertRes> insertStainMaster { get; set; }
        public virtual DbSet<RefTypeCommonMasterDto> RefTypeListDTO { get; set; }
        public virtual DbSet<GetUserDepartmentDTO> GetUserDepartment { get; set; }
        public virtual DbSet<templateresponse> InsertDiseaseTemplateText { get; set; }
        public virtual DbSet<TreatmentPlanMasterResponse> InsertTreatmentplan { get; set; }
        public virtual DbSet<reqTreatmentMaster> GetTreatmentMaster { get; set; }
        public virtual DbSet<TreatmentPlanProMaster> GetTreatmentMasterDetailsPRO { get; set; }
        public virtual DbSet<TreatmentPlanPrmMaster> GetTreatmentMasterDetailsPRM { get; set; }
        public virtual DbSet<OPDMachineRes> GetOPDMachineRes { get; set; }
        public virtual DbSet<OPDPhysicianRes> GetPhysicianOPDDetails { get; set; }
        public virtual DbSet<CommonResponse> OPDPatientDetails { get; set; }
        //
        public virtual DbSet<GetAssetManagementResponse> GetInstrumentDetail { get; set; }
        public virtual DbSet<postAssetManagementDTO> InsertInstrumentDetails { get; set; }
        //
        public virtual DbSet<GetStockAlertResponse> GetStockAlertsDetails { get; set; }

        public virtual DbSet<StoreMasterResponseDTO> GetStoreMasterDetails { get; set; }

        public virtual DbSet<StoreDetails> GetAllStoreByBranch { get; set; }
        public virtual DbSet<StoreMasterInsertResponseDTO> InsertStoreMaster { get; set; }
        public virtual DbSet<CommonAdminResponse> InsertConsumptionMapping { get; set; }
        public virtual DbSet<GetDiscountDetails> GetDiscountMasterData { get; set; }
        public virtual DbSet<DiscountMasterReponse> InsertDiscountMasterData { get; set; }
        public virtual DbSet<lstCollectDTS> GetCollectionDetails { get; set; }
        public virtual DbSet<resCollectDTS> UpdateCollectionDetails { get; set; }
        public virtual DbSet<PhysicianOrClientCodeResponse> GetLastPhysicianCode { get; set; }
        public virtual DbSet<consultantdetails> GetConsultant { get; set; }
        public virtual DbSet<saveConsultantlst> SaveConsultant { get; set; }
        public virtual DbSet<ProductUnitDTO> ProductUnitResponse { get; set; }
         public virtual DbSet<BOMMappingDTO> GetBOMMapping { get; set; }
        public virtual DbSet<BOMMappingResponse> InsertBOMMapping { get; set; }
        public virtual DbSet<SupplierToProductMappingAddDTO> SupplierToProductMappingAdd { get; set; }
        public virtual DbSet<ProductToSupplierMappingAddDTO> ProductToSupplierMappingAdd { get; set; }
        public virtual DbSet<SupplierToProductMappingDTO> SupplierToProductMapping { get; set; }
        public virtual DbSet<ProductToSupplierMappingDTO> ProductToSupplierMapping { get; set; }
        public virtual DbSet<VenueVsMenuResponseDTO> GetVenueVsMenu { get; set; }
        public virtual DbSet<ScrollTextMasterResponse> GetScrollTextMasterResponseDTO { get; set; }
        public virtual DbSet<SaveScrollTextMasterResponse> SaveScrollTextMasterDTO { get; set; }
        public virtual DbSet<CommonConfigurationResponseDTO> GetCommonConfiguration { get; set; }
        public virtual DbSet<GetEditTemplateTestMasterRawDto> GetEditTemplateTestMasterRaw { get; set; }
        public virtual DbSet<GetTestTemplateMasterRes> GetTestTemplateMasterList { get; set; }
        public virtual DbSet<TemplatePathRes> TemplatePathInsertResponse { get; set; }
        public virtual DbSet<InsertTestTemplateMasterRes> InsertTestTemplateMaster { get; set; }
        public virtual DbSet<GetTestTemplateTextMasterRes> GetTextTemplateTextMaster { get; set; }
        public virtual DbSet<GetTemplateApprovalRes> GetTemplateApprovalList { get; set; }
        public virtual DbSet<StoreToProductMappingAddDTO> StoreToProductMappingAdd { get; set; }
        public virtual DbSet<StoreToProductMappingDTO> StoreToProductMapping { get; set; }
        public virtual DbSet<BankMasterResponse> InsertBankMaster { get; set; }
        public virtual DbSet<BankMasterResponse> GetBankMaster { get; set; }
        public virtual DbSet<BankBranchResponse> InsertBankBranch { get; set; }
        public virtual DbSet<BankBranchResponse> GetBankBranch { get; set; }
        public virtual DbSet<GetFranchiseResponse> GetFranchises { get; set; }
        public virtual DbSet<FranchiseRevenueSharingServiceDto> FranchiseRevenueSharingServiceDto { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(EncryptionHelper.Decrypt(_connectionstring));
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<CommonMasterDto>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_CommonDetails");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });

            modelBuilder.Entity<ConfigurationDto>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetConfiguration");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.UserNo)
                    .HasName("PK__tbl_User__1788955F1E231A32");

                entity.ToTable("tbl_User");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LoginName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PinCode)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblDepartment>(entity =>
            {
                entity.HasKey(e => e.DepartmentNo)
                    .HasName("PK__tbl_Depa__B207A396BDC21C2A");

                entity.ToTable("tbl_Department");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DepartmentCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentDisplayText)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IsSample)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<TblMethod>(entity =>
            {
                entity.HasKey(e => e.MethodNo)
                    .HasName("PK__tbl_Meth__FC66EF31EA242143");

                entity.ToTable("tbl_Method");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.MethodDisplayText).HasMaxLength(100);

                entity.Property(e => e.MethodName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<TblPhysician>(entity =>
            {
                entity.HasKey(e => e.PhysicianNo)
                    .HasName("PhysicianNo");

                entity.ToTable("tbl_Physician");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<TblUnits>(entity =>
            {
                entity.HasKey(e => e.UnitsNo)
                    .HasName("PK__tbl_Unit__1A2949B8D01AE70E");

                entity.ToTable("tbl_Units");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UnitsCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UnitsName)
                    .IsRequired()
                    .HasMaxLength(50);
            });
            modelBuilder.Entity<GetIssueProductByIssueNoResponse>().HasNoKey();
            modelBuilder.Entity<TblOrganism>(entity =>
            {
                entity.HasKey(e => e.OrganismNo)
                    .HasName("PK__tbl_Orga__3C7281A731E611FF");

                entity.ToTable("tbl_Organism");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .HasMaxLength(1500)
                    .IsUnicode(false);

                entity.Property(e => e.OrganismCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.OrganismMccode)
                    .HasColumnName("OrganismMCCode")
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasComputedColumnSql("('ORG'+CONVERT([varchar](10),[OrganismNo]))");

                entity.Property(e => e.OrganismName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<lstotdrugmap>(entity =>
            {
                entity.HasKey(e => e.organismantibioticmapno);
                entity.ToTable("pro_GetOrgTypeAntiMap");
                entity.Property(e => e.organismantibioticmapno).HasColumnName("organismantibioticmapno");
            });

            modelBuilder.Entity<TblTemplate>(entity =>
            {
                entity.HasKey(e => e.templateNo);
                entity.ToTable("pro_GetTemplateMaster");
                entity.Property(e => e.templateNo).HasColumnName("templateNo");
            });

            modelBuilder.Entity<CustomerResponse>(entity =>
            {
                entity.HasKey(e => e.CustomerNo);
                entity.ToTable("Pro_GetCustomer");
                entity.Property(e => e.CustomerNo).HasColumnName("CustomerNo");
            });
            modelBuilder.Entity<InsertCustomerResponse>(entity =>
            {
                entity.HasKey(e => e.userName);
                entity.ToTable("Pro_InsertClientMaster");
                entity.Property(e => e.userName).HasColumnName("userName");
            });
            modelBuilder.Entity<InsertCustomersubuserResponse>(entity =>
            {
                entity.HasKey(e => e.status);
                entity.ToTable("Pro_InsertClientsubuserMaster");
                entity.Property(e => e.status).HasColumnName("status");
            });
            modelBuilder.Entity<GetTariffMasterResponse>(entity =>
            {
                entity.HasKey(e => e.Sno);
                entity.ToTable("Pro_GetTariff");
                entity.Property(e => e.Sno).HasColumnName("Sno");
            });

            modelBuilder.Entity<GetTariffMasterListResponse>(entity =>
            {
                entity.HasKey(e => e.Sno);
                entity.ToTable("Pro_GetTariffList");
                entity.Property(e => e.Sno).HasColumnName("Sno");
            });

            modelBuilder.Entity<GetClientTariffMasterListResponse>(entity =>
            {
                entity.HasKey(e => e.Sno);
                entity.ToTable("Pro_GetClientTariffList");
                entity.Property(e => e.Sno).HasColumnName("Sno");
            });

            modelBuilder.Entity<InsertTariffMasterResponse>(entity =>
            {
                entity.HasKey(e => e.resultStatus);
                entity.ToTable("Pro_CreateTariff");
                entity.Property(e => e.resultStatus).HasColumnName("resultStatus");
            });

            modelBuilder.Entity<TariffMasterInsertResponse>(entity =>
            {
                entity.HasKey(e => e.resultStatus);
                entity.ToTable("Pro_CreateTariffMaster");
                entity.Property(e => e.resultStatus).HasColumnName("resultStatus");
            });

            modelBuilder.Entity<CTMInsertResponse>(entity =>
            {
                entity.HasKey(e => e.resultStatus);
                entity.ToTable("Pro_CreateClientTariff");
                entity.Property(e => e.resultStatus).HasColumnName("resultStatus");
            });

            modelBuilder.Entity<GetServices>(entity =>
            {
                entity.HasKey(e => e.sNo);
                entity.ToTable("pro_TariffSearchService");
                entity.Property(e => e.sNo).HasColumnName("sNo");
            });

            modelBuilder.Entity<TariffMastServicesResponse>(entity =>
            {
                entity.HasKey(e => e.sNo);
                entity.ToTable("pro_TariffMasterSearchService");
                entity.Property(e => e.sNo).HasColumnName("sNo");
            });

            modelBuilder.Entity<ClientTariffServicesResponse>(entity =>
            {
                entity.HasKey(e => e.sNo);
                entity.ToTable("pro_ClientTariffSearchService");
                entity.Property(e => e.sNo).HasColumnName("sNo");
            });

            modelBuilder.Entity<UserDetailsDTO>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("Pro_UserMasterDetails");
                entity.Property(e => e.Row_Num).HasColumnName("Row_Num");

            });

            modelBuilder.Entity<UserResponseDTO>(entity =>
            {
                entity.HasKey(e => e.UserNo);
                entity.ToTable("Pro_InsertUserMaster");
                entity.Property(e => e.UserNo).HasColumnName("UserNo");
            });

            modelBuilder.Entity<UserMenuMappingDTO>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("Pro_GetUserMenuMapping");
                entity.Property(e => e.Row_Num).HasColumnName("Row_Num");

            });

            modelBuilder.Entity<UserMenuTaskDTO>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("Pro_GetUserPageTask");
                entity.Property(e => e.Row_Num).HasColumnName("Row_Num");

            });

            modelBuilder.Entity<UserResponseDTO>(entity =>
            {
                entity.HasKey(e => e.UserNo);
                entity.ToTable("pro_InsertMenuMapping");
                entity.Property(e => e.UserNo).HasColumnName("UserNo");
            });

            modelBuilder.Entity<UserNavDTO>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("Pro_GetUserMenu");
                entity.Property(e => e.Row_Num).HasColumnName("Row_Num");
            });

            modelBuilder.Entity<objTestList>(entity =>
            {
                entity.HasKey(e => e.testNo);
                entity.ToTable("pro_GetTestMaster");
                entity.Property(e => e.testNo).HasColumnName("testNo");
            });

            modelBuilder.Entity<objtestdbl>(entity =>
            {
                entity.HasKey(e => e.testNo);
                entity.ToTable("pro_GetSingleTestMaster");
                entity.Property(e => e.testNo).HasColumnName("testNo");
            });

            modelBuilder.Entity<rtntest>(entity =>
            {
                entity.HasKey(e => e.testNo);
                entity.ToTable("pro_InsertTest");
                entity.Property(e => e.testNo).HasColumnName("testNo");
            });

            modelBuilder.Entity<rtntemplateNo>(entity =>
            {
                entity.HasKey(e => e.templateNo);
                entity.ToTable("pro_InsertTemplateText");
                entity.Property(e => e.templateNo).HasColumnName("templateNo");
            });

            modelBuilder.Entity<rtntest>(entity =>
            {
                entity.HasKey(e => e.testNo);
                entity.ToTable("pro_UpdateSequence");
                entity.Property(e => e.testNo).HasColumnName("testNo");
            });

            modelBuilder.Entity<lstgrppkg>(entity =>
            {
                entity.HasKey(e => e.serviceNo);
                entity.ToTable("pro_GetGroupPackageMaster");
                entity.Property(e => e.serviceNo).HasColumnName("serviceNo");
                entity.Property(x => x.FromDate).IsRequired(false);
                entity.Property(x => x.ToDate).IsRequired(false);
            });

            modelBuilder.Entity<objgrppkgdbl>(entity =>
            {
                entity.HasKey(e => e.serviceNo);
                entity.ToTable("pro_GetSingleGroupPackageMaster");
                entity.Property(e => e.serviceNo).HasColumnName("serviceNo");
            });

            modelBuilder.Entity<rtntest>(entity =>
            {
                entity.HasKey(e => e.testNo);
                entity.ToTable("pro_InsertGroup");
                entity.Property(e => e.testNo).HasColumnName("testNo");
            });

            modelBuilder.Entity<lstgrppkgservice>(entity =>
            {
                entity.HasKey(e => e.rowNo);
                entity.ToTable("pro_SearchTestGroupPackage");
                entity.Property(e => e.rowNo).HasColumnName("rowNo");
            });

            modelBuilder.Entity<lststestdbl>(entity =>
            {
                entity.HasKey(e => e.rowNo);
                entity.ToTable("pro_GetSubTestList");
                entity.Property(e => e.rowNo).HasColumnName("rowNo");
            });

            modelBuilder.Entity<CustomerMappingDTO>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("Pro_GetCustomerMapping");
                entity.Property(e => e.Row_Num).HasColumnName("Row_Num");
            });
            modelBuilder.Entity<ClientSubClientMappingDTO>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("Pro_GetAllClientBySubCLinic");
            });

            modelBuilder.Entity<ClientSubUserResponse>(entity =>
            {
                entity.HasKey(e => e.CustomerSubUserNo);
                entity.ToTable("pro_GetClientSubUser");
                entity.Property(e => e.CustomerSubUserNo).HasColumnName("CustomerSubUserNo");
            });

            modelBuilder.Entity<objsubtestdbl>(entity =>
            {
                entity.HasKey(e => e.subTestNo);
                entity.ToTable("pro_GetSingleSubTestMaster");
                entity.Property(e => e.subTestNo).HasColumnName("subTestNo");
            });

            modelBuilder.Entity<CustomerReturnResponse>(entity =>
            {
                entity.HasKey(e => e.CustomerNo);
                entity.ToTable("Pro_InsertSubClientMapping");
                entity.Property(e => e.CustomerNo).HasColumnName("CustomerNo");
            });

            modelBuilder.Entity<rtntest>(entity =>
            {
                entity.HasKey(e => e.testNo);
                entity.ToTable("pro_InsertSubTest");
                entity.Property(e => e.testNo).HasColumnName("testNo");
            });

            modelBuilder.Entity<rtntest>(entity =>
            {
                entity.HasKey(e => e.testNo);
                entity.ToTable("pro_InsertOrganismAntibioticRange");
                entity.Property(e => e.testNo).HasColumnName("testNo");
            });

            modelBuilder.Entity<lstorgAntiRange>(entity =>
            {
                entity.HasKey(e => e.antibioticno);
                entity.ToTable("pro_GetOrgAntibioticRangeMaster");
                entity.Property(e => e.antibioticno).HasColumnName("antibioticno");
            });

            modelBuilder.Entity<ClientRestrictionDayResponse>(entity =>
            {
                entity.HasKey(e => e.AvailVisitNo);
                entity.ToTable("pro_GetClientRestrictionDayIsValid");
                entity.Property(e => e.AvailVisitNo).HasColumnName("AvailVisitNo");
            });

            modelBuilder.Entity<CommonResponse>(entity =>
            {
                entity.HasKey(e => e.status);
                entity.ToTable("Pro_PhysicianMerging");
                entity.Property(e => e.status).HasColumnName("status");
            });

            modelBuilder.Entity<GetProductMasterResponse>(entity =>
            {
                entity.HasKey(e => e.ProductMasterNo);
                entity.ToTable("Pro_GetProductMaster");
                entity.Property(e => e.ProductMasterNo).HasColumnName("ProductMasterNo");
            });

            modelBuilder.Entity<GetSupplierMappingDTO>(entity =>
            {
                entity.HasKey(e => e.RowNum);
                entity.ToTable("Pro_GetSupplierMapping");
                entity.Property(e => e.RowNum).HasColumnName("RowNum");
            });

            modelBuilder.Entity<SupplierResponse>(entity =>
            {
                entity.HasKey(e => e.status);
                entity.ToTable("Pro_InsertProductMaster");
                entity.Property(e => e.status).HasColumnName("status");
            });

            modelBuilder.Entity<SupplierResponse>(entity =>
            {
                entity.HasKey(e => e.status);
                entity.ToTable("Pro_Iv_InsertSupplierMaster");
                entity.Property(e => e.status).HasColumnName("status");
            });

            modelBuilder.Entity<tbl_IV_ProductMaster>(entity =>
            {
                entity.HasKey(e => e.ProductMasterNo)
                    .HasName("ProductMasterNo");

                entity.ToTable("tbl_IV_ProductMaster");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<UpdateSupplierMaster>(entity =>
            {
                entity.HasKey(e => e.supplierMasterNo)
                    .HasName("supplierMasterNo");
                entity.ToTable("tbl_IV_SupplierMaster");
                entity.Property(e => e.supplierMasterNo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<postManufacturerMasterDTO>(entity =>
            {
                entity.HasKey(e => e.manufacturerNo)
                    .HasName("manufacturerNo");
                entity.ToTable("Pro_Iv_InsertManufacturerMaster");
                entity.Property(e => e.manufacturerNo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<GetmultiPriceListResponse>(entity =>
            {
                entity.HasKey(e => e.sNo)
                    .HasName("sNo");
                entity.ToTable("pro_GetMultiPriceListDetails");
                entity.Property(e => e.sNo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<InsertMultiPriceListResponse>(entity =>
            {
                entity.HasKey(e => e.resultStatus)
                    .HasName("resultStatus");
                entity.ToTable("Pro_CreateMultipriceList");
                entity.Property(e => e.resultStatus)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<GetPurchaseOrderResponse>(entity =>
            {
                entity.HasKey(e => e.Sno)
                    .HasName("SupplierMasterNo");
                entity.ToTable("Pro_GetPurchaseOrder");
                entity.Property(e => e.Sno)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });
            modelBuilder.Entity<GetSupplierServiceDTO>(entity =>
            {
                entity.HasKey(e => e.RowNo)
                    .HasName("RowNo");
                entity.ToTable("pro_GetSupplierServiceDetails");
                entity.Property(e => e.RowNo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<CommonResponse>(entity =>
            {
                entity.HasKey(e => e.status)
                    .HasName("status");
                entity.ToTable("Pro_InsertPurchaseOrder");
                entity.Property(e => e.status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<GetPurchaseDetailsDTO>(entity =>
            {
                entity.HasKey(e => e.RowNo)
                    .HasName("RowNo");
                entity.ToTable("pro_GetPurchaseDetailsById");
                entity.Property(e => e.RowNo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<rtnUnit>(entity =>
            {
                entity.HasKey(e => e.unitNo);
                entity.ToTable("pro_InsertUnit");
                entity.Property(e => e.unitNo).HasColumnName("unitNo");
            });

            modelBuilder.Entity<lstunits>(entity =>
            {
                entity.HasKey(e => e.unitsno);
                entity.ToTable("pro_GetUnitMaster");
                entity.Property(e => e.unitsno).HasColumnName("unitsno");
            });

            modelBuilder.Entity<GetDepartmentMappingDTO>(entity =>
            {
                entity.HasKey(e => e.RowNum);
                entity.ToTable("Pro_GetDepartmentMapping");
                entity.Property(e => e.RowNum).HasColumnName("RowNum");
            });

            modelBuilder.Entity<GetPOBySupplierResponse>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetPOBySupplier");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });

            modelBuilder.Entity<GetProductsByPOResponse>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_IV_GetProductsByPO");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });

            modelBuilder.Entity<GetProductsByPOResponse>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetGRNByGRNNo");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });

            modelBuilder.Entity<CommonResponse>(entity =>
            {
                entity.HasKey(e => e.status)
                    .HasName("status");
                entity.ToTable("Pro_InsertGRNMaster");
                entity.Property(e => e.status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<IndentDetailsSaveResponse>(entity =>
            {
                entity.HasKey(e => e.indentno);
                entity.ToTable("pro_InsertIndent");
                entity.Property(e => e.indentno).HasColumnName("indentno");
            });

            modelBuilder.Entity<GetProductbatchdetailsbyPO>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<IndentDetailsResponse>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetIndentDetails");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });

            modelBuilder.Entity<IndentProductDetailsNewResponse>(entity =>
            {
                entity.HasKey(e => e.rowNo);
                entity.ToTable("pro_GetIndentProductDetails");
                entity.Property(e => e.rowNo).HasColumnName("rowNo");
            });

            modelBuilder.Entity<GetStockProductListResponse>(entity =>
            {
                entity.HasKey(e => e.rowNo);
                entity.ToTable("pro_GetStockUploadProducts");
                entity.Property(e => e.rowNo).HasColumnName("rowNo");
            });

            modelBuilder.Entity<CommonResponse>(entity =>
            {
                entity.HasKey(e => e.status);
                entity.ToTable("Pro_InsertProductStock");
                entity.Property(e => e.status).HasColumnName("status");
            });

            modelBuilder.Entity<GetIssueProductResponse>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetIssueProductlst");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });

            modelBuilder.Entity<SaveIssueProductResponse>(entity =>
            {
                entity.HasKey(e => e.IssueNo);
                entity.ToTable("pro_InsertIssueProductlst");
                entity.Property(e => e.IssueNo).HasColumnName("IssueNo");
            });

            modelBuilder.Entity<FetchProductListResponse>(entity =>
            {
                entity.HasKey(e => e.ProductMasterNo);
                entity.ToTable("Pro_Iv_FetchProductList");
                entity.Property(e => e.ProductMasterNo).HasColumnName("ProductMasterNo");
            });

            modelBuilder.Entity<GetSupplierMasterResponse>(entity =>
            {
                entity.HasKey(e => e.supplierMasterNo);
                entity.ToTable("Pro_Iv_FetchSuppliersDetails");
                entity.Property(e => e.supplierMasterNo).HasColumnName("SupplierMasterNo");
            });

            modelBuilder.Entity<GetManufacturerMasterResponse>(entity =>
            {
                entity.HasKey(e => e.manufacturerNo);
                entity.ToTable("pro_Iv_FetchManufacturersDetail");
                entity.Property(e => e.manufacturerNo).HasColumnName("manufacturerNo");
            });

            modelBuilder.Entity<SaveFormulaResponse>(entity =>
            {
                entity.HasKey(e => e.formulaNo);
                entity.ToTable("pro_InsertFormulaMaster");
                entity.Property(e => e.formulaNo).HasColumnName("formulaNo");
            });

            modelBuilder.Entity<GetFormulaResponse>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.ToTable("pro_GetFormulaDetails");
                entity.Property(e => e.ID).HasColumnName("id");
            });

            modelBuilder.Entity<orggetresponse>(entity =>
            {
                entity.HasKey(e => e.organismno);
                entity.ToTable("pro_GetOrganismmaster");
                entity.Property(e => e.organismno).HasColumnName("organismNo");
            });
            modelBuilder.Entity<orgGrpresponse>(entity =>
            {
                entity.HasKey(e => e.organismgrpno);
                entity.ToTable("pro_GetOrganismGroupMaster");
                entity.Property(e => e.organismgrpno).HasColumnName("organismgrpno");
            });

            modelBuilder.Entity<orginsertresponse>(entity =>
            {
                entity.HasKey(e => e.organismno);
                entity.ToTable("pro_InsertOrganismmaster");
                entity.Property(e => e.organismno).HasColumnName("organismNo");
            });

            modelBuilder.Entity<orginsertGrpresponse>(entity =>
            {
                entity.HasKey(e => e.organismGrpno);
                entity.ToTable("pro_InsertOrganismGrpmaster");
                entity.Property(e => e.organismGrpno).HasColumnName("organismGrpno");
            });



            modelBuilder.Entity<orgtyperesponse>(entity =>
            {
                entity.HasKey(e => e.organismtypeno);
                entity.ToTable("pro_GetOrganismTypemaster");
                entity.Property(e => e.organismtypeno).HasColumnName("organismtypeno");
            });

            modelBuilder.Entity<orgtypeinsertresponse>(entity =>
            {
                entity.HasKey(e => e.organismtypeno);
                entity.ToTable("pro_InsertOrganismTypemaster");
                entity.Property(e => e.organismtypeno).HasColumnName("organismtypeno");
            });

            modelBuilder.Entity<CheckMasterNameExistsResponse>(entity =>
            {
                entity.HasKey(e => e.avail);
                entity.ToTable("pro_CheckNameExistsAntiandorg");
                entity.Property(e => e.avail).HasColumnName("avail");
            });

            modelBuilder.Entity<CheckMasterNameExistsResponse>(entity =>
            {
                entity.HasKey(e => e.avail);
                entity.ToTable("pro_CheckNamefourexists");
                entity.Property(e => e.avail).HasColumnName("avail");
            });

            modelBuilder.Entity<TblProductType>(entity =>
            {
                entity.HasKey(e => e.productTypeno);
                entity.ToTable("pro_GetproductType");
                entity.Property(e => e.productTypeno).HasColumnName("productTypeno");
            });

            modelBuilder.Entity<ProductTypeMasterResponse>(entity =>
            {
                entity.HasKey(e => e.productTypeno);
                entity.ToTable("pro_InsertProductType");
                entity.Property(e => e.productTypeno).HasColumnName("productTypeno");
            });

            modelBuilder.Entity<POProductDetailsDTO>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetPOProductDetailsById");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });

            modelBuilder.Entity<GetTaxDatilsResponse>(entity =>
            {
                entity.HasKey(e => e.rowNo);
                entity.ToTable("pro_GetPOTaxDetailsById");
                entity.Property(e => e.rowNo).HasColumnName("RowNo");
            });

            modelBuilder.Entity<otherChargeModal>(entity =>
            {
                entity.HasKey(e => e.rowNo);
                entity.ToTable("pro_GetPOOCDetailsById");
                entity.Property(e => e.rowNo).HasColumnName("RowNo");
            });

            modelBuilder.Entity<Termsconditionlist>(entity =>
            {
                entity.HasKey(e => e.rowNo);
                entity.ToTable("pro_GetPOTermsDetailsById");
                entity.Property(e => e.rowNo).HasColumnName("RowNo");
            });

            modelBuilder.Entity<GetAllGRNResponse>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetAllGRN");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });

            modelBuilder.Entity<otherChargeModal>(entity =>
            {
                entity.HasKey(e => e.rowNo);
                entity.ToTable("pro_GetGRNOCDetailsById");
                entity.Property(e => e.rowNo).HasColumnName("RowNo");
            });

            modelBuilder.Entity<GetAllGRNReturnResponse>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetAllGRNReturn");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });

            modelBuilder.Entity<antiresponse>(entity =>
            {
                entity.HasKey(e => e.antibioticno);
                entity.ToTable("pro_GetAntibioticmaster");
                entity.Property(e => e.antibioticno).HasColumnName("antibioticno");
            });

            modelBuilder.Entity<antinsertresponse>(entity =>
            {
                entity.HasKey(e => e.antibioticno);
                entity.ToTable("pro_InsertAntibioticsmaster");
                entity.Property(e => e.antibioticno).HasColumnName("antibioticno");
            });

            modelBuilder.Entity<orgAntiresponse>(entity =>
            {
                entity.HasKey(e => e.organismAntibioticMapNo);
                entity.ToTable("pro_GetOrganismtypeandantibioticmaster");
                entity.Property(e => e.organismAntibioticMapNo).HasColumnName("organismAntibioticMapNo");
            });

            modelBuilder.Entity<organtinsertresponse>(entity =>
            {
                entity.HasKey(e => e.organismAntibioticMapNo);
                entity.ToTable("pro_InsertOrganismtypeandantibioticmaster");
                entity.Property(e => e.organismAntibioticMapNo).HasColumnName("organismAntibioticMapNo");
            });

            modelBuilder.Entity<TblHSN>(entity =>
            {
                entity.HasKey(e => e.HSNNo);
                entity.ToTable("pro_GetHSNMaster");
                entity.Property(e => e.HSNNo).HasColumnName("HSNNo");
            });

            modelBuilder.Entity<HSNMasterResponse>(entity =>
            {
                entity.HasKey(e => e.HSNNo);
                entity.ToTable("pro_InsertHSNmaster");
                entity.Property(e => e.HSNNo).HasColumnName("HSNNo");
            });

            modelBuilder.Entity<TblHSNRange>(entity =>
            {
                entity.HasKey(e => e.HSNRangeNo);
                entity.ToTable("pro_GetHSNRangeWiseTax");
                entity.Property(e => e.HSNRangeNo).HasColumnName("HSNRangeNo");
            });

            modelBuilder.Entity<HSNInsertResponse>(entity =>
            {
                entity.HasKey(e => e.HSNRangeNo);
                entity.ToTable("pro_InsertHSNRangeWiseTax");
                entity.Property(e => e.HSNRangeNo).HasColumnName("HSNRangeNo");
            });

            modelBuilder.Entity<TblGeneric>(entity =>
            {
                entity.HasKey(e => e.genericNo);
                entity.ToTable("pro_GetGenericMaster");
                entity.Property(e => e.genericNo).HasColumnName("genericNo");
            });

            modelBuilder.Entity<GenericMasterResponse>(entity =>
            {
                entity.HasKey(e => e.genericNo);
                entity.ToTable("pro_InsertGenericMaster");
                entity.Property(e => e.genericNo).HasColumnName("genericNo");
            });

            modelBuilder.Entity<TblMedtype>(entity =>
            {
                entity.HasKey(e => e.medicineTypeNo);
                entity.ToTable("pro_GetMedicineType");
                entity.Property(e => e.medicineTypeNo).HasColumnName("medicineTypeNo");
            });

            modelBuilder.Entity<MedtypeMasterResponse>(entity =>
            {
                entity.HasKey(e => e.medicineTypeNo);
                entity.ToTable("pro_InsertMedicineType");
                entity.Property(e => e.medicineTypeNo).HasColumnName("medicineTypeNo");
            });

            modelBuilder.Entity<responsegetvendor>(entity =>
            {
                entity.HasKey(e => e.vendorno);
                entity.ToTable("pro_GetVendormaster");
                entity.Property(e => e.vendorno).HasColumnName("vendorno");
            });

            modelBuilder.Entity<StoreVendorMaster>(entity =>
            {
                entity.HasKey(e => e.vendorno);
                entity.ToTable("pro_InsertVendormaster");
                entity.Property(e => e.vendorno).HasColumnName("vendorno");
            });

            modelBuilder.Entity<TblMedstr>(entity =>
            {
                entity.HasKey(e => e.strengthNo);
                entity.ToTable("pro_GetMedicineStrength");
                entity.Property(e => e.strengthNo).HasColumnName("strengthNo");
            });

            modelBuilder.Entity<MedstrMasterResponse>(entity =>
            {
                entity.HasKey(e => e.strengthNo);
                entity.ToTable("pro_InsertMedicineStrength");
                entity.Property(e => e.strengthNo).HasColumnName("strengthNo");
            });

            modelBuilder.Entity<TblCountry>(entity =>
            {
                entity.HasKey(e => e.countryNo);
                entity.ToTable("pro_GetCountrymaster");
                entity.Property(e => e.countryNo).HasColumnName("countryNo");
            });

            modelBuilder.Entity<CountryMasteResponse>(entity =>
            {
                entity.HasKey(e => e.countryNo);
                entity.ToTable("pro_InsertCountrymaster");
                entity.Property(e => e.countryNo).HasColumnName("countryNo");
            });

            modelBuilder.Entity<EditSupplier>(entity =>
            {
                entity.HasKey(e => e.supplierMasterNo);
                entity.ToTable("pro_GetSuppilerMaster");
                entity.Property(e => e.supplierMasterNo).HasColumnName("supplierMasterNo");
            });

            modelBuilder.Entity<GetProductsByGRNResponse>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_IV_GetProductsByGRN");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });

            modelBuilder.Entity<GetGRNBySupplierResponse>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetGRNBySupplier");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });

            modelBuilder.Entity<CommonResponse>(entity =>
            {
                entity.HasKey(e => e.status);
                entity.ToTable("Pro_InsertProductStock");
                entity.Property(e => e.status).HasColumnName("status");
            });

            modelBuilder.Entity<GetProductsByGRNNo>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_IV_GetGRNReturnProduct");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });

            modelBuilder.Entity<CommonResponse>(entity =>
            {
                entity.HasKey(e => e.status);
                entity.ToTable("Pro_PhysicianHaveVisits");
                entity.Property(e => e.status).HasColumnName("status");
            });

            modelBuilder.Entity<GetConsumptionMappingResponse>(entity =>
            {
                entity.HasKey(e => e.Sno);
                entity.ToTable("Pro_GetAllConsumptionMapping");
                entity.Property(e => e.Sno).HasColumnName("Sno");
            });

            modelBuilder.Entity<CommonAdminResponse>(entity =>
            {
                entity.HasKey(e => e.status);
                entity.ToTable("Pro_InsertConsumptionMapping");
                entity.Property(e => e.status).HasColumnName("status");
            });

            modelBuilder.Entity<ReagentOpeningStockResponse>(entity =>
            {
                entity.HasKey(e => e.Sno);
                entity.ToTable("Pro_GetAllReagentOpeningStock");
                entity.Property(e => e.Sno).HasColumnName("Sno");
            });

            modelBuilder.Entity<CommonResponse>(entity =>
            {
                entity.HasKey(e => e.status);
                entity.ToTable("Pro_InsertReagentOpeningStock");
                entity.Property(e => e.status).HasColumnName("status");
            });

            modelBuilder.Entity<GetStockCorrectionResponse>(entity =>
            {
                entity.HasKey(e => e.Sno);
                entity.ToTable("Pro_GetAllStockCorrection");
                entity.Property(e => e.Sno).HasColumnName("Sno");
            });

            modelBuilder.Entity<CommonResponse>(entity =>
            {
                entity.HasKey(e => e.status);
                entity.ToTable("Pro_InsertStockCorrection");
                entity.Property(e => e.status).HasColumnName("status");
            });

            modelBuilder.Entity<GetParameterAnalyserResponse>(entity =>
            {
                entity.HasKey(e => e.Sno);
                entity.ToTable("Pro_GetAllParaeterAnalyser");
                entity.Property(e => e.Sno).HasColumnName("Sno");
            });

            modelBuilder.Entity<CommonResponse>(entity =>
            {
                entity.HasKey(e => e.status);
                entity.ToTable("Pro_InsertParaeterAnalyser");
                entity.Property(e => e.status).HasColumnName("status");
            });

            modelBuilder.Entity<lstState>(entity =>
            {
                entity.HasKey(e => e.stateNo);
                entity.ToTable("pro_GetStatemaster");
                entity.Property(e => e.stateNo).HasColumnName("stateNo");
            });

            modelBuilder.Entity<StateResponse>(entity =>
            {
                entity.HasKey(e => e.stateNo);
                entity.ToTable("pro_InsertStatemaster");
                entity.Property(e => e.stateNo).HasColumnName("stateNo");
            });

            modelBuilder.Entity<RoleResponseDTO>(entity =>
            {
                entity.HasKey(e => e.RoleId);
                entity.ToTable("pro_InsertRoleMenuMapping");
                entity.Property(e => e.RoleId).HasColumnName("RoleId");
            });

            modelBuilder.Entity<UserMenuMappingDTO>(entity =>
            {
                entity.HasKey(e => e.Row_Num);
                entity.ToTable("Pro_GetRoleMenuMapping");
                entity.Property(e => e.Row_Num).HasColumnName("Row_Num");

            });

            modelBuilder.Entity<CityLst>(entity =>
            {
                entity.HasKey(e => e.cityNo);
                entity.ToTable("pro_GetCitymaster");
                entity.Property(e => e.cityNo).HasColumnName("cityNo");
            });

            modelBuilder.Entity<CityResponse>(entity =>
            {
                entity.HasKey(e => e.cityNo);
                entity.ToTable("pro_InsertCitymaster");
                entity.Property(e => e.cityNo).HasColumnName("cityNo");
            });

            modelBuilder.Entity<PlaceLst>(entity =>
            {
                entity.HasKey(e => e.placeMasterNo);
                entity.ToTable("pro_GetPlacemaster");
                entity.Property(e => e.placeMasterNo).HasColumnName("placeMasterNo");
            });

            modelBuilder.Entity<PlaceResponse>(entity =>
            {
                entity.HasKey(e => e.placeMasterNo);
                entity.ToTable("pro_InsertPlacemaster");
                entity.Property(e => e.placeMasterNo).HasColumnName("placeMasterNo");
            });

            modelBuilder.Entity<NationalityLst>(entity =>
            {
                entity.HasKey(e => e.nationalityMasterNo);
                entity.ToTable("pro_GetNationalityMaster");
                entity.Property(e => e.nationalityMasterNo).HasColumnName("nationalityMasterNo");
            });

            modelBuilder.Entity<NationalityResponse>(entity =>
            {
                entity.HasKey(e => e.nationalityMasterNo);
                entity.ToTable("pro_InsertNationalitymaster");
                entity.Property(e => e.nationalityMasterNo).HasColumnName("nationalityMasterNo");
            });

            modelBuilder.Entity<PhysicianNo>(entity =>
            {
                entity.HasKey(e => e.physicianNo);
                entity.ToTable("Pro_InsertPhysicianMaster");
                entity.Property(e => e.physicianNo).HasColumnName("physicianNo");
            });

            modelBuilder.Entity<getcontactlst>(entity =>
            {
                entity.HasKey(e => e.vendorContactNo);
                entity.ToTable("pro_GetVendorvsContactmaster");
                entity.Property(e => e.vendorContactNo).HasColumnName("VendorContactNo");
            });

            modelBuilder.Entity<StorecontactMaster>(entity =>
            {
                entity.HasKey(e => e.VendorContactNo);
                entity.ToTable("pro_InsertVendorVsContact");
                entity.Property(e => e.VendorContactNo).HasColumnName("VendorContactNo");
            });

            modelBuilder.Entity<getservicelst>(entity =>
            {
                entity.HasKey(e => e.vendorServiceNo);
                entity.ToTable("pro_GetVendorVsServices");
                entity.Property(e => e.vendorServiceNo).HasColumnName("VendorServiceNo");
            });

            modelBuilder.Entity<storeservice>(entity =>
            {
                entity.HasKey(e => e.VendorServiceNo);
                entity.ToTable("pro_InsertVendorVsServices");
                entity.Property(e => e.VendorServiceNo).HasColumnName("VendorServiceNo");
            });

            modelBuilder.Entity<Fetchlookalike>(entity =>
            {
                entity.HasKey(e => e.lookalikemasterno);
                entity.ToTable("pro_GetLookalike");
                entity.Property(e => e.lookalikemasterno).HasColumnName("lookalikemasterno");
            });
            modelBuilder.Entity<Fetchsoundalike>(entity =>
            {
                entity.HasKey(e => e.soundalikemasterno);
                entity.ToTable("pro_GetSoundalike");
                entity.Property(e => e.soundalikemasterno).HasColumnName("soundalikemasterno");
            });

            modelBuilder.Entity<responsebranch>(entity =>
            {
                entity.HasKey(e => e.processingBranchMapNo);
                entity.ToTable("pro_GetProcessingBranch");
                entity.Property(e => e.processingBranchMapNo).HasColumnName("processingBranchMapNo");
            }
          );
            modelBuilder.Entity<Storeprocessingbranch>(entity =>
            {
                entity.HasKey(e => e.processingBranchMapNo);
                entity.ToTable("pro_InsertProcessingBranch");
                entity.Property(e => e.processingBranchMapNo).HasColumnName("processingBranchMapNo");
            }
          );
            modelBuilder.Entity<GetDeptIssueProductResponse>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetDeptIssueProductlst");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });

            modelBuilder.Entity<GetTariffupdateResponse>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("pro_Gettariff_walkin_branchwise");
                entity.Property(e => e.Id).HasColumnName("Id");
            });

            modelBuilder.Entity<AnaParamDtoResponse>(entity =>
            {
                entity.HasKey(e => e.AnalyzerMasterNo);
                entity.ToTable("pro_InsertAnalyzerVsParameters");
                entity.Property(e => e.AnalyzerMasterNo).HasColumnName("AnalyzerMasterNo");
            });
            modelBuilder.Entity<AnaParamGetDto>(entity =>
            {
                entity.HasKey(e => e.AnalyzerParamNo);
                entity.ToTable("pro_GetAnalyzerVsParameters");
                entity.Property(e => e.AnalyzerParamNo).HasColumnName("AnalyzerParamNo");

            });
            modelBuilder.Entity<FetchAnaParamDto>(entity =>
            {
                entity.HasKey(e => e.AnalyzerParamNo);
                entity.ToTable("pro_FetchAnalyzerVsParametersMapping");
                entity.Property(e => e.AnalyzerParamNo).HasColumnName("AnalyzerParamNo");
            });
            modelBuilder.Entity<TblMethod>(entity =>
            {
                entity.HasKey(e => e.MethodNo);
                entity.ToTable("pro_GetMethod");
                entity.Property(e => e.MethodNo).HasColumnName("MethodNo");
            });

            modelBuilder.Entity<MethodResponse>(entity =>
            {
                entity.HasKey(e => e.MethodNo);
                entity.ToTable("pro_InsertMethod");
                entity.Property(e => e.MethodNo).HasColumnName("MethodNo");
            });

            modelBuilder.Entity<GetServiceDetails>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetServiceOrder");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });

            modelBuilder.Entity<ServiceOrderMasterResponse>(entity =>
            {
                entity.HasKey(e => e.Status);
                entity.ToTable("pro_InsertServiceOrder");
                entity.Property(e => e.Status).HasColumnName("Status");
            });

            modelBuilder.Entity<ConfigurationDto>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetSingleConfiguration");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });
            modelBuilder.Entity<returnquotationlst>(entity =>
            {
                entity.HasKey(e => e.quotationMasterNo);
                entity.ToTable("pro_GetServiceQuotation");
                entity.Property(e => e.quotationMasterNo).HasColumnName("QuotationMasterNo");
            });
            modelBuilder.Entity<storequotationlst>(entity =>
            {
                entity.HasKey(e => e.quotationMasterNo);
                entity.ToTable("pro_InsertServiceQuotation");
                entity.Property(e => e.quotationMasterNo).HasColumnName("QuotationMasterNo");
            });
            modelBuilder.Entity<DocVsSerResponse>(entity =>
            {
                entity.HasKey(e => e.DoctorNo);
                entity.ToTable("pro_GetDoctorDetails");
                entity.Property(e => e.DoctorNo).HasColumnName("DoctorNo");
            });
            modelBuilder.Entity<DocVsSerGetRes>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetDocVsSerDetails");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });
            modelBuilder.Entity<DocVsSerInsRes>(entity =>
            {
                entity.HasKey(e => e.DoctorServiceNo);
                entity.ToTable("pro_InsertDocVsSerDetails");
                entity.Property(e => e.DoctorServiceNo).HasColumnName("DoctorServiceNo");
            });
            modelBuilder.Entity<GetTblqcmaster>(entity =>
            {
                entity.HasKey(e => e.rowNo);
                entity.ToTable("pro_GetQCMaster");
                entity.Property(e => e.rowNo).HasColumnName("rowNo");
            });
            modelBuilder.Entity<Tblqcmaster>(entity =>
            {
                entity.HasKey(e => e.qcmasterNo);
                entity.ToTable("pro_GeteditQCMaster");
                entity.Property(e => e.qcmasterNo).HasColumnName("qcmasterNo");
            });
            modelBuilder.Entity<Qclotresponse>(entity =>
            {
                entity.HasKey(e => e.rowNo);
                entity.ToTable("pro_GetQClot");
                entity.Property(e => e.rowNo).HasColumnName("rowNo");
            });
            modelBuilder.Entity<Qclevelresponse>(entity =>
            {
                entity.HasKey(e => e.rowNo);
                entity.ToTable("pro_GetQClevel");
                entity.Property(e => e.rowNo).HasColumnName("rowNo");
            });
            modelBuilder.Entity<Qclowhighresponse>(entity =>
            {
                entity.HasKey(e => e.rowNo);
                entity.ToTable("pro_GetQClowhighvalue");
                entity.Property(e => e.rowNo).HasColumnName("rowNo");
            });
            modelBuilder.Entity<QcMasterResponse>(entity =>
            {
                entity.HasKey(e => e.status);
                entity.ToTable("pro_InsertQcmaster");
                entity.Property(e => e.status).HasColumnName("status");
            });
            modelBuilder.Entity<DocVsSerAppRes>(entity =>
            {
                entity.HasKey(e => e.OrderListNo);
                entity.ToTable("pro_getInternalDoctorVsAppraisalVsTransaction");
                entity.Property(e => e.OrderListNo).HasColumnName("OrderListNo");
            });
            modelBuilder.Entity<DocVsSerAppdetailsRes>(entity =>
            {
                entity.HasKey(e => e.TranNo);
                entity.ToTable("pro_GetDocVsSerVsProfCharge");
                entity.Property(e => e.TranNo).HasColumnName("TranNo");
            });
            modelBuilder.Entity<DocVsSerProfInsRes>(entity =>
            {
                entity.HasKey(e => e.DoctorProfMastNo);
                entity.ToTable("pro_InsertDocVsSerProfDetails");
                entity.Property(e => e.DoctorProfMastNo).HasColumnName("DoctorProfMastNo");
            });

            modelBuilder.Entity<GetTblqcresult>(entity =>
            {
                entity.HasKey(e => e.rowNo);
                entity.ToTable("pro_GetQCResultEntry");
                entity.Property(e => e.rowNo).HasColumnName("rowNo");
            });
            modelBuilder.Entity<QcresultResponse>(entity =>
            {
                entity.HasKey(e => e.status);
                entity.ToTable("pro_InsertQcresultentry");
                entity.Property(e => e.status).HasColumnName("status");
            });
            modelBuilder.Entity<Tblqcresult>(entity =>
            {
                entity.HasKey(e => e.qcresultNo);
                entity.ToTable("pro_GeteditQcresultentry");
                entity.Property(e => e.qcresultNo).HasColumnName("qcresultNo");
            });
            modelBuilder.Entity<CommonResponse>(entity =>
            {
                entity.HasKey(e => e.status);
                entity.ToTable("Pro_InsertPhysicianDocument");
                entity.Property(e => e.status).HasColumnName("status");
            });
            modelBuilder.Entity<PhysicianDocUploadRes>(entity =>
            {
                entity.HasKey(e => e.documentType);
                entity.ToTable("pro_GetEntityDocument");
                entity.Property(e => e.documentType).HasColumnName("documentType");
            });
            modelBuilder.Entity<PhysicianDocUploadRes>(entity =>
            {
                entity.HasKey(e => e.documentType);
                entity.ToTable("pro_GetEntityDocument");
                entity.Property(e => e.documentType).HasColumnName("documentType");
            });
            modelBuilder.Entity<CommonResponse>(entity =>
            {
                entity.HasKey(e => e.status);
                entity.ToTable("Pro_InsertClientDocument");
                entity.Property(e => e.status).HasColumnName("status");
            });
            modelBuilder.Entity<TbltestMap>(entity =>
            {
                entity.HasKey(e => e.analyzerparamTestNo);
                entity.ToTable("pro_GetAnalVsParamVsTest");
                entity.Property(e => e.analyzerparamTestNo).HasColumnName("analyzerparamTestNo");
            });
            modelBuilder.Entity<analVsparamVstestMap>(entity =>
            {
                entity.HasKey(e => e.analyzerparamTestNo);
                entity.ToTable("pro_InsertAnalVsParamVsTest");
                entity.Property(e => e.analyzerparamTestNo).HasColumnName("analyzerparamTestNo");
            });
            modelBuilder.Entity<subresponse>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetSubTest");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });
            modelBuilder.Entity<SubProductRes>(entity =>
            {
                entity.HasKey(e => e.ProductMasterNo);
                entity.ToTable("pro_GetSubProduct");
                entity.Property(e => e.ProductMasterNo).HasColumnName("ProductMasterNo");
            });
            modelBuilder.Entity<ActionMenuNoResponseDTO>(entity =>
            {
                entity.HasKey(e => e.Result);
                entity.ToTable("pro_CheckUserMenuAvailable");
                entity.Property(e => e.Result).HasColumnName("Result");
            });
            modelBuilder.Entity<CommentGetRes>(entity =>
            {
                entity.HasKey(e => e.CommentsMastNo);
                entity.ToTable("pro_GetCommentMaster");
                entity.Property(e => e.CommentsMastNo).HasColumnName("CommentsMastNo");
            });
            modelBuilder.Entity<CommentInsRes>(entity =>
            {
                entity.HasKey(e => e.CommentsMastNo);
                entity.ToTable("pro_InsertCommentMaster");
                entity.Property(e => e.CommentsMastNo).HasColumnName("CommentsMastNo");
            });
            modelBuilder.Entity<UserSessionResponseDTO>(entity =>
            {
                entity.HasKey(e => e.status);
                entity.ToTable("Pro_UpdateSessionStatus");
                entity.Property(e => e.status).HasColumnName("status");
            });
            modelBuilder.Entity<ActionMenuCodeResponseDTO>(entity =>
            {
                entity.HasKey(e => e.Result);
                entity.ToTable("pro_CheckUserMenuCode");
                entity.Property(e => e.Result).HasColumnName("Result");
            });
            modelBuilder.Entity<UserMenuCodeDTO>(entity =>
            {
                entity.HasKey(e => e.MenuCode);
                entity.ToTable("pro_GetUserMenuCode");
                entity.Property(e => e.MenuCode).HasColumnName("MenuCode");
            });
            modelBuilder.Entity<UserMenuNosResponseDTO>(entity =>
            {
                entity.HasKey(e => e.menuNos);
                entity.ToTable("pro_GetUserMenuNos");
                entity.Property(e => e.menuNos).HasColumnName("menuNos");
            });
            modelBuilder.Entity<UserRoleNameDTO>(entity =>
            {
                entity.HasKey(e => e.RoleName);
                entity.ToTable("pro_GetUserRoleNames");
                entity.Property(e => e.RoleName).HasColumnName("RoleName");
            });
            modelBuilder.Entity<CommericalGetRes>(entity =>
            {
                entity.HasKey(e => e.CompanyNo);
                entity.ToTable("pro_GetCompanyDetails");
                entity.Property(e => e.CompanyNo).HasColumnName("CompanyNo");
            });
            modelBuilder.Entity<CommericalInsRes>(entity =>
            {
                entity.HasKey(e => e.CompanyNo);
                entity.ToTable("pro_InsertCompanyDetails");
                entity.Property(e => e.CompanyNo).HasColumnName("CompanyNo");
            });
            modelBuilder.Entity<GetNationRaceRes>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("pro_GetNationalityRace");
                entity.Property(e => e.Id).HasColumnName("Id");
            });
            modelBuilder.Entity<InsNationRaceRes>(entity =>
            {
                entity.HasKey(e => e.CommonNo);
                entity.ToTable("pro_InsertNationalityRace");
                entity.Property(e => e.CommonNo).HasColumnName("CommonNo");
            });
            modelBuilder.Entity<CheckTestcodeExistsRes>(entity =>
            {
                entity.HasKey(e => e.outNo);
                entity.ToTable("pro_GetAlreadyExisitingTestCode");
                entity.Property(e => e.outNo).HasColumnName("outNo");
            });
            modelBuilder.Entity<GSTGetRes>(entity =>
            {
                entity.HasKey(e => e.TaxMastNo);
                entity.ToTable("Pro_GetGSTTaxmaster");
                entity.Property(e => e.TaxMastNo).HasColumnName("TaxMastNo");
            });
            modelBuilder.Entity<GSTInsRes>(entity =>
            {
                entity.HasKey(e => e.TaxMastNo);
                entity.ToTable("Pro_InsertGSTTaxMaster");
                entity.Property(e => e.TaxMastNo).HasColumnName("TaxMastNo");
            });
            modelBuilder.Entity<restestapprove>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetTestApproveDetails");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });
            modelBuilder.Entity<restestappHistory>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetApproveHistory");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });
            modelBuilder.Entity<GetTATRes>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetTATMaster");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });
            modelBuilder.Entity<InsTATRes>(entity =>
            {
                entity.HasKey(e => e.Status);
                entity.ToTable("pro_InsertTATMaster");
                entity.Property(e => e.Status).HasColumnName("Status");
            });
            modelBuilder.Entity<GetloincRes>(entity =>
            {
                entity.HasKey(e => e.LoincNo);
                entity.ToTable("pro_GetLoincMaster");
                entity.Property(e => e.LoincNo).HasColumnName("LoincNo");
            });
            modelBuilder.Entity<InsloincRes>(entity =>
            {
                entity.HasKey(e => e.LoincNo);
                entity.ToTable("pro_InsertLoincMaster");
                entity.Property(e => e.LoincNo).HasColumnName("LoincNo");
            });
            modelBuilder.Entity<CommonTokenResponse>(entity =>
            {
                entity.HasKey(e => e.responseValue);
                entity.ToTable("Pro_InsertSlidePrinting");
                entity.Property(e => e.responseValue).HasColumnName("responseValue");
            });
            modelBuilder.Entity<AppSettingResponse>(entity =>
            {
                entity.HasKey(e => e.ConfigNo);
                entity.ToTable("pro_GetSingleAppSetting");
                entity.Property(e => e.ConfigNo).HasColumnName("ConfigNo");
            });
            modelBuilder.Entity<GetContractRes>(entity =>
            {
                entity.HasKey(e => e.Sno);
                entity.ToTable("Pro_GetContractMaster");
                entity.Property(e => e.Sno).HasColumnName("Sno");
            });
            modelBuilder.Entity<InsertContractRes>(entity =>
            {
                entity.HasKey(e => e.resultStatus);
                entity.ToTable("Pro_InsertContractMaster");
                entity.Property(e => e.resultStatus).HasColumnName("resultStatus");
            });
            modelBuilder.Entity<InsertBaseRateRes>(entity =>
            {
                entity.HasKey(e => e.result);
                entity.ToTable("Pro_InsertBaseRate");
                entity.Property(e => e.result).HasColumnName("result");
            });
            modelBuilder.Entity<TariffMastServicesResponse>(entity =>
            {
                entity.HasKey(e => e.sNo);
                entity.ToTable("pro_ContractMasterSearchService");
                entity.Property(e => e.sNo).HasColumnName("sNo");
            });
            modelBuilder.Entity<GetContractVsClientRes>(entity =>
            {
                entity.HasKey(e => e.Sno);
                entity.ToTable("pro_GetContractVSClient");
                entity.Property(e => e.Sno).HasColumnName("Sno");
            });
            modelBuilder.Entity<GetSnomedRes>(entity =>
            {
                entity.HasKey(e => e.SnomedNo);
                entity.ToTable("pro_GetSnomedMaster");
                entity.Property(e => e.SnomedNo).HasColumnName("SnomedNo");
            });
            modelBuilder.Entity<InsSnomedRes>(entity =>
            {
                entity.HasKey(e => e.SnomedNo);
                entity.ToTable("pro_InsertSnomedMaster");
                entity.Property(e => e.SnomedNo).HasColumnName("SnomedNo");
            });
            modelBuilder.Entity<InsTariffRes>(entity =>
            {
                entity.HasKey(e => e.ClientTariffMapNo);
                entity.ToTable("pro_InsertClientTariffMapping");
                entity.Property(e => e.ClientTariffMapNo).HasColumnName("ClientTariffMapNo");
            });
            modelBuilder.Entity<GetTariffRes>(entity =>
            {
                entity.HasKey(e => e.ClientTariffMapNo);
                entity.ToTable("pro_GetClientTariffMapping");
                entity.Property(e => e.ClientTariffMapNo).HasColumnName("ClientTariffMapNo");
            });
            modelBuilder.Entity<TariffMastServicesResponse>(entity =>
            {
                entity.HasKey(e => e.sNo);
                entity.ToTable("pro_RefSplPriceSearchService");
                entity.Property(e => e.sNo).HasColumnName("sNo");
            });
            modelBuilder.Entity<GetReflstRes>(entity =>
            {
                entity.HasKey(e => e.Sno);
                entity.ToTable("Pro_GetRefSplPrice");
                entity.Property(e => e.Sno).HasColumnName("Sno");
            });
            modelBuilder.Entity<InsertContractRes>(entity =>
            {
                entity.HasKey(e => e.resultStatus);
                entity.ToTable("Pro_InsertRefSplPrice");
                entity.Property(e => e.resultStatus).HasColumnName("resultStatus");
            });
            modelBuilder.Entity<Tariffdeptdisreq>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetTariffDeptDiscount");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });
            modelBuilder.Entity<ContractVsSubCustomerMap>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetSubContractVsClient");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });
            modelBuilder.Entity<GetRoleRes>(entity =>
            {
                entity.HasKey(e => e.RoleId);
                entity.ToTable("pro_GetRoleMaster");
                entity.Property(e => e.RoleId).HasColumnName("RoleId");
            });
            modelBuilder.Entity<InsertRoleRes>(entity =>
            {
                entity.HasKey(e => e.RoleId);
                entity.ToTable("pro_InsertRoleMaster");
                entity.Property(e => e.RoleId).HasColumnName("RoleId");
            });
            modelBuilder.Entity<IntegrationPackageRes>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("pro_GetIntegrationMapping");
                entity.Property(e => e.Id).HasColumnName("Id");
            });

            modelBuilder.Entity<IntegrationPackageResult>(entity =>
            {
                entity.HasKey(e => e.Result);
                entity.ToTable("pro_InsertIntegrationMapping");
                entity.Property(e => e.Result).HasColumnName("Result");
            });

            modelBuilder.Entity<PriceHistoryServiceResponse>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("Pro_ServiceRate_History");
                entity.Property(e => e.EntityName).HasColumnName("EntityName");
            });
            modelBuilder.Entity<BaseRateResponse>(entity =>
            {
                entity.HasKey(e => e.BaseRateNo);
                entity.ToTable("Pro_GetBaseRate");
                entity.Property(e => e.BaseRateNo).HasColumnName("BaseRateNo");
            });

            modelBuilder.Entity<TemplateCommentRes>(entity =>
            {
                entity.HasKey(e => e.CH_QcNo);
                entity.ToTable("pro_CRUDTemplateCommentbyPatientVisit");
                entity.Property(e => e.CH_QcNo).HasColumnName("CH_QcNo");
            });

            modelBuilder.Entity<CommentSubCatyInsResponse>(entity =>
            {
                entity.HasKey(e => e.SubCatyNo);
                entity.ToTable("pro_InsertCommentSubCategory");
                entity.Property(e => e.SubCatyNo).HasColumnName("SubCatyNo");
            });

            modelBuilder.Entity<FetchCommentSubCategoryResponse>(entity =>
            {
                entity.HasKey(e => e.SubCatyNo);
                entity.ToTable("pro_GetCommentSubCategory");
                entity.Property(e => e.SubCatyNo).HasColumnName("SubCatyNo");
            });

            modelBuilder.Entity<PrintPackageDetails>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("Pro_GetPackageDetailsByNo");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });

            modelBuilder.Entity<GetStatinMasterDetailsRes>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.ToTable("pro_GetStainDetails");
                entity.Property(e => e.id).HasColumnName("id");
            });
            modelBuilder.Entity<StainMasterInsertRes>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("pro_InsertStainDetails");
                entity.Property(e => e.status).HasColumnName("statusRes");
            });
            modelBuilder.Entity<RefTypeCommonMasterDto>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("pro_RefTypeSettings");
                entity.Property(e => e.CommonCode).HasColumnName("CommonCode");
            });
            modelBuilder.Entity<GetUserDepartmentDTO>(entity =>
            {
                entity.HasKey(e => e.rowNo);
                entity.ToTable("pro_GetUserDepartment");
                entity.Property(e => e.rowNo).HasColumnName("rowNo");
            });
            modelBuilder.Entity<lstdrugresponse>(entity =>
            {
                entity.HasKey(e => e.drugPresTempNo);
                entity.ToTable("pro_GetProductVsDrugs");
                entity.Property(e => e.drugPresTempNo).HasColumnName("drugPresTempNo");
            });
            modelBuilder.Entity<savedruglstresponse>(entity =>
            {
                entity.HasKey(e => e.DrugPresTempNo);
                entity.ToTable("pro_InsertProductVsDrugs");
                entity.Property(e => e.DrugPresTempNo).HasColumnName("DrugPresTempNo");
            });
            modelBuilder.Entity<GetProductMainbyDeptRes>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetProductSubandMaindept");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });
            modelBuilder.Entity<ProductcategoryResponse>(entity =>
            {
                entity.HasKey(e => e.categoryNo);
                entity.ToTable("pro_InsertProductCategory");
                entity.Property(e => e.categoryNo).HasColumnName("categoryNo");
            });
            modelBuilder.Entity<TblProductCategory>(entity =>
            {
                entity.HasKey(e => e.categoryNo);
                entity.ToTable("pro_GetProductCategory");
                entity.Property(e => e.categoryNo).HasColumnName("categoryNo");
            });
            modelBuilder.Entity<TreatmentPlanMasterResponse>(entity =>
            {
                entity.HasKey(e => e.treatmentNo);
                entity.ToTable("Pro_InsertTreatmentPlan");
                entity.Property(e => e.treatmentNo).HasColumnName("TreatmentNo");
            });
            modelBuilder.Entity<reqTreatmentMaster>(entity =>
            {
                entity.HasKey(e => e.treatmentNo);
                entity.ToTable("pro_GetTreatmentMaster");
                entity.Property(e => e.treatmentNo).HasColumnName("treatmentNo");
            });
            modelBuilder.Entity<TreatmentPlanProMaster>(entity =>
            {
                entity.HasKey(e => e.treatmentPlanProceduresNo);
                entity.ToTable("pro_GetTreatmentMasterDetailsByPRO");
                entity.Property(e => e.treatmentPlanProceduresNo).HasColumnName("treatmentPlanProceduresNo");
            });
            modelBuilder.Entity<TreatmentPlanPrmMaster>(entity =>
            {
                entity.HasKey(e => e.treatmentPlanPharmacyNo);
                entity.ToTable("pro_GetTreatmentMasterDetailsByPRM");
                entity.Property(e => e.treatmentPlanPharmacyNo).HasColumnName("treatmentPlanPharmacyNo");
            });
            modelBuilder.Entity<templateresponse>(entity =>
            {
                entity.HasKey(e => e.templateNo);
                entity.ToTable("pro_InsertDiseaseTemplateText");
                entity.Property(e => e.templateNo).HasColumnName("templateNo");
            });
            modelBuilder.Entity<OPDMachineRes>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetMachineTimelist");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });
            modelBuilder.Entity<OPDPhysicianRes>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetPhysicianOPDlist");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });
            modelBuilder.Entity<CommonResponse>(entity =>
            {
                entity.HasKey(e => e.status);
                entity.ToTable("Pro_InsertOPDPhysicianMaster");
                entity.Property(e => e.status).HasColumnName("status");
            });
            modelBuilder.Entity<postAssetManagementDTO>(entity =>
            {
                entity.HasKey(e => e.InstrumentsNo)
                    .HasName("InstrumentsNo");
                entity.ToTable("Pro_InsertInventoryInstrument");
                entity.Property(e => e.InstrumentsNo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<GetAssetManagementResponse>(entity =>
            {
                entity.HasKey(e => e.InstrumentsNo);
                entity.ToTable("Pro_GetInventoryInstrument");
                entity.Property(e => e.InstrumentsNo).HasColumnName("InstrumentsNo");
            });

            modelBuilder.Entity<GetStockAlertResponse>(entity =>
            {
                // This entity doesn't have a primary key
                entity.HasNoKey();
                entity.ToView("Pro_Getstockalert");

                // Map properties to their respective column names (if needed)
                entity.Property(e => e.ProductMasterNo).HasColumnName("ProductMasterNo");
                entity.Property(e => e.ProductMasterName).HasColumnName("ProductMasterName");
                entity.Property(e => e.ManufacturerNo).HasColumnName("ManufacturerNo");
                entity.Property(e => e.ManufacturerName).HasColumnName("ManufacturerName");
                entity.Property(e => e.MinQty).HasColumnName("MinQty");
                entity.Property(e => e.MaxQty).HasColumnName("MaxQty");
                entity.Property(e => e.Opening_TestCnt).HasColumnName("Opening_TestCnt");
                entity.Property(e => e.Closing_TestCnt).HasColumnName("Closing_TestCnt");
                entity.Property(e => e.Adjust_TestCnt).HasColumnName("Adjust_TestCnt");
                entity.Property(e => e.Alert).HasColumnName("Alert");
                entity.Property(e => e.PageIndex).HasColumnName("PageIndex");
                entity.Property(e => e.TotalRecords).HasColumnName("TotalRecords");
                entity.Property(e => e.AgingStatus).HasColumnName("AgingStatus");
                entity.Property(e => e.Aging).HasColumnName("Aging");

            });

            modelBuilder.Entity<StoreMasterResponseDTO>(entity =>
            {
                entity.HasKey(e => e.StoreID);
                entity.ToTable("pro_GetStoremaster");
                entity.Property(e => e.StoreID).HasColumnName("StoreID");
            });
            modelBuilder.Entity<StoreDetails>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("pro_GetStoreDetailsbyBranchNo");
            });
            modelBuilder.Entity<StoreMasterInsertResponseDTO>(entity =>
            {
                entity.HasKey(e => e.StoreID);
                entity.ToTable("pro_InsertStoremaster");
                entity.Property(e => e.StoreID).HasColumnName("StoreID");
            });
            modelBuilder.Entity<resCollectDTS>(entity =>
            {
                entity.HasKey(e => e.CollectionNo);
                entity.ToTable("pro_UpdateCollectionDetails");
                entity.Property(e => e.CollectionNo).HasColumnName("CollectionNo");
            });
            modelBuilder.Entity<GetStockReportResponse>(entity =>
            {
                entity.HasKey(e => e.rowNo);
                entity.ToTable("pro_GetStockReport");
                entity.Property(e => e.rowNo).HasColumnName("rowNo");
            });
            modelBuilder.Entity<consultantdetails>(entity =>
            {
                entity.HasKey(e => e.consultantNo);
                entity.ToTable("pro_GetConsultant");
                entity.Property(e => e.consultantNo).HasColumnName("consultantNo");
            });
            modelBuilder.Entity<saveConsultantlst>(entity =>
            {
                entity.HasKey(e => e.consultantNo);
                entity.ToTable("pro_SaveConsultant");
                entity.Property(e => e.consultantNo).HasColumnName("consultantNo");
            });
            modelBuilder.Entity<GetDiscountDetails>(entity =>
            {
                entity.HasKey(e => e.discountNo);
                entity.ToTable("pro_GetDiscountmaster");
                entity.Property(e => e.discountNo).HasColumnName("discountNo");
            });
            modelBuilder.Entity<lstCollectDTS>(entity =>
            {
                entity.HasKey(e => e.CollectionNo);
                entity.ToTable("pro_GetCollectionDetails");
                entity.Property(e => e.CollectionNo).HasColumnName("CollectionNo");
            });

            modelBuilder.Entity<DiscountMasterReponse>(entity =>
            {
                entity.HasKey(e => e.discountNo);
                entity.ToTable("pro_InsertDiscountmaster");
                entity.Property(e => e.discountNo).HasColumnName("discountNo");
            });
            modelBuilder.Entity<PhysicianOrClientCodeResponse>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("pro_GetPhysicianOrClinetCode");
            });
            modelBuilder.Entity<ProductUnitDTO>(entity =>
            {
                entity.HasKey(e => e.ProductMasterNo);
                entity.ToTable("Pro_GetProductDetails");
                entity.Property(e => e.ProductMasterNo).HasColumnName("ProductMasterNo");
            });
           
            modelBuilder.Entity<BOMMappingDTO>().HasNoKey().ToView(null);
            modelBuilder.Entity<BOMMappingResponse>(entity =>
            {
                entity.HasKey(e => e.result);
                entity.ToTable("Upsert_Product_TestMapping");
                entity.Property(e => e.result).HasColumnName("result");
            });

            modelBuilder.Entity<SupplierToProductMappingAddDTO>().HasNoKey().ToView(null);
            modelBuilder.Entity<ProductToSupplierMappingAddDTO>().HasNoKey().ToView(null);
            modelBuilder.Entity<SupplierToProductMappingDTO>().HasNoKey().ToView(null);
            modelBuilder.Entity<ProductToSupplierMappingDTO>().HasNoKey().ToView(null);

            modelBuilder.Entity<GetProductStockResponse>(entity =>
            { 
                entity.HasKey(e => e.BatchNo);
                entity.ToTable("pro_GetProductStock");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });
            modelBuilder.Entity<CommonAdminResponse>(entity =>
            {
                entity.HasKey(e => e.status);
                entity.ToTable("Pro_InsertStockAdjustment");
                entity.Property(e => e.status).HasColumnName("status");
            });

            modelBuilder.Entity<VenueVsMenuResponseDTO>().HasNoKey().ToView(null);

            modelBuilder.Entity<GetStockAdjustmentResponse>(entity =>
            {
                entity.HasKey(e => e.rowNo);
                entity.ToTable("pro_GetAllStockAdjustment");
                entity.Property(e => e.rowNo).HasColumnName("rowNo");
            });
            modelBuilder.Entity<LstStockAdjustProductDetailsResponse>(entity =>
            {
                entity.HasKey(e => e.ProductNo);
                entity.ToTable("pro_GetStockAdjustmentProductDetails");
                entity.Property(e => e.ProductNo);
            });
            modelBuilder.Entity<GetStoreStockProductListResponse>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_GetStoreStockProductDetails");
                entity.Property(e => e.ProductNo);
            });
            modelBuilder.Entity<ScrollTextMasterResponse>().HasNoKey().ToView(null);
            modelBuilder.Entity<SaveScrollTextMasterResponse>().HasNoKey().ToView(null);
            modelBuilder.Entity<CommonConfigurationResponseDTO>().HasNoKey().ToView(null);

            modelBuilder.Entity<GetAllConsumptionListResponse>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_IV_GetAllConsumptionList");
                entity.Property(e => e.ConsumptionNo);
            });
            modelBuilder.Entity<CommonAdminResponse>(entity =>
            {
                entity.HasKey(e => e.status);
                entity.ToTable("Pro_IV_InsertStockConsumption");
                entity.Property(e => e.status).HasColumnName("status");
            });

            modelBuilder.Entity<GetTestTemplateTextMasterRes>(entity =>
            {
                entity.HasKey(e => e.templateText);
                entity.ToTable("pro_GetTemplateMasterDetails");
                entity.Property(e => e.templateText).HasColumnName("templateText");
            });
            modelBuilder.Entity<GetTestTemplateMasterRes>().HasNoKey().ToView(null);
            modelBuilder.Entity<GetEditTemplateTestMasterRawDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<TemplatePathRes>().HasNoKey().ToView(null);
            modelBuilder.Entity<GetTemplateApprovalRes>().HasNoKey().ToView(null);

            modelBuilder.Entity<ConsumptionDetailsInListResponse>(entity =>
            {
                entity.HasKey(e => e.RowNo);
                entity.ToTable("pro_IV_GetStoreConsumptionProductDetails");
                entity.Property(e => e.RowNo).HasColumnName("RowNo");
            });
            modelBuilder.Entity<InsertTestTemplateMasterRes>(entity =>
            {
                entity.HasKey(e => e.templateNo);
                entity.ToTable("pro_InsertTestTemplateText");
                entity.Property(e => e.templateNo).HasColumnName("templateNo");
            });
            modelBuilder.Entity<StoreToProductMappingAddDTO>().HasNoKey().ToView(null);
            modelBuilder.Entity<StoreToProductMappingDTO>().HasNoKey().ToView(null);

            modelBuilder.Entity<CommonResponse>(entity =>
            {
                entity.HasKey(e => e.status)
                    .HasName("status");
                entity.ToTable("Pro_IV_UpdateGRNInvoiceDetails");
                entity.Property(e => e.status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<BankMasterResponse>(entity =>
            {
                entity.HasKey(e => e.BankID);
                entity.ToTable("Pro_InsertBank_Master");
                entity.Property(e => e.BankID).HasColumnName("BankID");
            });

            modelBuilder.Entity<BankMasterResponse>(entity =>
            {
                entity.HasKey(e => e.BankID);
                entity.ToTable("Pro_GetBank_Master");
                entity.Property(e => e.BankID).HasColumnName("BankID");
            });

            modelBuilder.Entity<BankBranchResponse>(entity =>
            {
                entity.HasKey(e => e.BranchID);
                entity.ToTable("Pro_InsertBank_Branch");
                entity.Property(e => e.BranchID).HasColumnName("BranchID");
            });

            modelBuilder.Entity<BankBranchResponse>(entity =>
            {
                entity.HasKey(e => e.BranchID);
                entity.ToTable("Pro_GetBank_Branch");
                entity.Property(e => e.BranchID).HasColumnName("BranchID");
            });
            modelBuilder.Entity<GetFranchiseResponse>(entity =>
            {
                entity.HasKey(e => e.FranchiseNo);
                entity.ToTable("pro_GetIsFranchise");
                entity.Property(e => e.FranchiseNo).HasColumnName("FranchiseNo");
            });
            modelBuilder.Entity<FranchiseRevenueSharingServiceDto>(entity =>
            {
                entity.HasKey(e => e.Sno);
                entity.ToTable("pro_GetIsFranchiseRevenueSharingbyService");
                entity.Property(e => e.Sno).HasColumnName("Sno");
            });
        }
    }
}
