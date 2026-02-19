using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Service.Model;
using Service.IRepository;
using Microsoft.Extensions.Logging;
using Service.Common;
using Microsoft.AspNetCore.Authorization;

namespace Service.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class DiseaseController : ControllerBase
    {
        private readonly IDiseaseRepository _diseaseRepository;
        private readonly ILogger<DiseaseController> _logger;
        public DiseaseController(IDiseaseRepository noteRepository, ILogger<DiseaseController> logger)
        {
            _diseaseRepository = noteRepository;
            _logger = logger;
        }

        [HttpPost]
        [Route("api/DiseaseCategory/GetDiseaseCategory")]
        public List<LstDiseaseCategory> GetDiseaseCategorys(ReqDiseaseCategory disCat)
        {
            List<LstDiseaseCategory> result = new List<LstDiseaseCategory>();
            try
            {
                result = _diseaseRepository.GetDiseaseCategorys(disCat);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DiseaseController.GetDiseaseCategorys" + disCat.DiseaseCategoryNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, disCat.VenueNo, disCat.VenueBranchNo, 0);
            }
            return result;
        }

        [HttpPost]
        [Route("api/DiseaseCategory/InsertDiseaseCategory")]
        public RtnDiseaseCategory InsertDiseaseCategorys(TblDiseaseCategory resq)
        {
            RtnDiseaseCategory Objresult = new RtnDiseaseCategory();
            try
            {
                Objresult = _diseaseRepository.InsertDiseaseCategorys(resq);
                string _CacheKey = CacheKeys.CommonMaster + "DISEASE" + resq.VenueNo + resq.VenueBranchNo;
                MemoryCacheRepository.RemoveItem(_CacheKey);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DiseaseController.InsertDiseaseCategorys" + resq.DiseaseCategoryNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, resq.VenueNo, resq.VenueBranchNo, 0);
            }
            return Objresult;
        }

        [HttpPost]
        [Route("api/DiseaseMaster/GetDiseaseMaster")]
        public List<LstDiseaseMaster> GetDiseaseMasters(ReqDiseaseMaster disName)
        {
            List<LstDiseaseMaster> result = new List<LstDiseaseMaster>();
            try
            {
                result = _diseaseRepository.GetDiseaseMasters(disName);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DiseaseController.GetDiseaseMasters" + disName.DiseaseMasterNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, disName.VenueNo, disName.VenueBranchNo, 0);
            }
            return result;
        }

        [HttpPost]
        [Route("api/DiseaseMaster/InsertDiseaseMaster")]
        public RtnDiseaseMaster InsertDiseaseMasters(TblDiseaseMaster ress)
        {
            RtnDiseaseMaster Objresult = new RtnDiseaseMaster();
            try
            {
                Objresult = _diseaseRepository.InsertDiseaseMasters(ress);
                string _CacheKey = CacheKeys.CommonMaster + "DISEASE" + ress.VenueNo + ress.VenueBranchNo;
                MemoryCacheRepository.RemoveItem(_CacheKey);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DiseaseController.InsertDiseaseMasters" + ress.DiseaseMasterNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, ress.VenueNo, ress.VenueBranchNo, 0);
            }
            return Objresult;
        }

        #region InsertDiseaseTemplateText
        [HttpPost]
        [Route("api/DiseaseMaster/InsertDiseaseTemplateText")]
        public int InsertDiseaseTemplateText(LstDiseaseTemplateList req)
        {
            int i = 0;
            try
            {
                i = _diseaseRepository.InsertDiseaseTemplateText(req);
                string _CommonCommonCatch = CacheKeys.CommonMaster + "DISEASETEMPLATE" + req.venueNo + req.venueBranchNo;
                MemoryCacheRepository.RemoveItem(_CommonCommonCatch);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DiseaseController.InsertDiseaseTemplateText", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return i;
        }
        #endregion
       
        #region GetDiseaseTemplateText
        [HttpPost]
        [Route("api/DiseaseMaster/GetDiseaseTemplateText")]
        public Reqresponse GetDiseaseTemplateText(LstDiseaseTemplateList req)
        {
            Reqresponse obj = new Reqresponse();
            try
            {
                obj = _diseaseRepository.GetDiseaseTemplateText(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DiseaseController.GetDiseaseTemplateText", ExceptionPriority.Low, ApplicationType.APPSERVICE, req.venueNo, req.venueBranchNo, 0);
            }
            return obj;
        }
        #endregion

        #region GetTemplateList
        [HttpGet]
        [Route("api/DiseaseMaster/GetTemplateList")]
        public List<LstDiseaseTemplateList> GetTemplateList(int VenueNo, int VenueBranchNo, int TemplateNo, int TempDiseaseNo)
        {
            List<LstDiseaseTemplateList> Objresult = new List<LstDiseaseTemplateList>();
            try
            {
                Objresult = _diseaseRepository.GetTemplateList(VenueNo, VenueBranchNo, TemplateNo,TempDiseaseNo);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DiseaseController.GetTemplateList", ExceptionPriority.Low, ApplicationType.APPSERVICE, VenueNo, VenueBranchNo, 0);
            }
            return Objresult;
        }
        #endregion

        [HttpPost]
        [Route("api/DiseaseMaster/GetDiseaseVsDrugMaster")]
        public List<DiseaseVsProductMapping> GetDiseaseVsDrugMaster(ReqDiseaseMaster disName)
        {
            List<DiseaseVsProductMapping> result = new List<DiseaseVsProductMapping>();
            try
            {
                result = _diseaseRepository.GetDiseaseVsDrugMaster(disName);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DiseaseController.GetDiseaseVsDrugMaster" + disName.DiseaseMasterNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, disName.VenueNo, disName.VenueBranchNo, 0);
            }
            return result;
        }

        [HttpPost]
        [Route("api/DiseaseMaster/GetDiseaseVsTestMaster")]
        public List<DiseaseVsTestMapping> GetDiseaseVsTestMaster(ReqDiseaseMaster disName)
        {
            List<DiseaseVsTestMapping> result = new List<DiseaseVsTestMapping>();
            try
            {
                result = _diseaseRepository.GetDiseaseVsTestMaster(disName);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DiseaseController.GetDiseaseVsTestMaster" + disName.DiseaseMasterNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, disName.VenueNo, disName.VenueBranchNo, 0);
            }
            return result;
        }

        [HttpPost]
        [Route("api/DiseaseMaster/InsertDisVsDrugMaster")]
        public RtnDisVsDrugMaster InsertDisVsDrugMaster(ReqDisVsDrugMaster res)
        {
            RtnDisVsDrugMaster Objresult = new RtnDisVsDrugMaster();
            try
            {
                Objresult = _diseaseRepository.InsertDisVsDrugMaster(res);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DiseaseController.InsertDisVsDrugMaster" + res.DiseaseMasterNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, res.VenueNo, res.VenueBranchNo, 0);
            }
            return Objresult;
        }

        [HttpPost]
        [Route("api/DiseaseMaster/InsertDisVsInvMaster")]
        public RtnDisVsInvMaster InsertDisVsInvMaster(ReqDisVsInvMaster res)
        {
            RtnDisVsInvMaster Objresult = new RtnDisVsInvMaster();
            try
            {
                Objresult = _diseaseRepository.InsertDisVsInvMaster(res);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DiseaseController.InsertDisVsDrugMaster" + res.DiseaseMasterNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, res.VenueNo, res.VenueBranchNo, 0);
            }
            return Objresult;
        }
      
        [HttpPost]
        [Route("api/Machine/GetMachineMaster")]
        public List<MachineMasterDTO> GetMachineMaster(ReqMachineMaster param)
        {
            List<MachineMasterDTO> result = new List<MachineMasterDTO>();
            try
            {
                result = _diseaseRepository.GetMachineMaster(param);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DiseaseController.GetMachineMaster" + param.machineNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, param.VenueNo, param.VenueBranchNo, 0);
            }
            return result;
        }

        [HttpPost]
        [Route("api/Machine/InsertMachineResult")]
        public ReqMachineMasterResponse InsertMachineResult(InvMachineMasterRequest res)
        {
            ReqMachineMasterResponse Objresult = new ReqMachineMasterResponse();
            try
            {
                Objresult = _diseaseRepository.InsertMachineResult(res);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "DiseaseController.InsertMachineResult" + res.machineNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, res.VenueNo, res.VenueBranchNo, 0);
            }
            return Objresult;
        }
    }
}