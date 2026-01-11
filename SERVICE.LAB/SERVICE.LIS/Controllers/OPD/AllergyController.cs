using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Model;
using Dev.IRepository;
using Microsoft.Extensions.Logging;
using DEV.Common;
using Microsoft.AspNetCore.Authorization;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class AllergyController : ControllerBase
    {
        private readonly IAllergyRepository _allergyRepository;
        private readonly ILogger<AllergyController> _logger;
        public AllergyController(IAllergyRepository allergyRepository, ILogger<AllergyController> logger)
        {
            _allergyRepository = allergyRepository;
            _logger = logger;
        }

        [HttpPost]
        [Route("api/AllergyType/GetAllergyType")]
        public List<lstAllergyType> GetAllergyType(reqAllergyType allType)
        {
            List<lstAllergyType> result = new List<lstAllergyType>();
            try
            {
                result = _allergyRepository.GetAllergyTypes(allType);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AllergyController.GetAllergyType" + allType.AllergyTypeNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, allType.VenueNo, 0, 0);
            }
            return result;
        }

        [HttpPost]
        [Route("api/AllergyType/InsertAllergyType")]
        public AllergyTypeResponse InsertAllergyType(TblAllergyType req)
        {
            AllergyTypeResponse objresult = new AllergyTypeResponse();
            try
            {
                objresult = _allergyRepository.InsertAllergyTypes(req);
                string _CacheKey = CacheKeys.CommonMaster + "ALLERGY" + req.VenueNo + req.VenueBranchNo;
                MemoryCacheRepository.RemoveItem(_CacheKey);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AllergyController.InsertAllergyType" + req.AllergyTypeNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.VenueNo, req.VenueBranchNo, 0);
            }
            return objresult;
        }

        [HttpPost]
        [Route("api/AllergyMaster/GetAllergyMaster")]
        public List<lstAllergyMaster> GetAllergyMaster(reqAllergyMaster allyName)
        {
            List<lstAllergyMaster> result = new List<lstAllergyMaster>();
            try
            {
                result = _allergyRepository.GetAllergyMasters(allyName);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AllergyController.GetAllergyMaster" + allyName.AllergyMasterNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, allyName.VenueNo, 0, 0);
            }
            return result;
        }

        [HttpPost]
        [Route("api/AllergyMaster/InsertAllergyMaster")]
        public rtnAllergyMaster InsertAllergyMaster(TblAllergyMaster res)
        {
            rtnAllergyMaster objresult = new rtnAllergyMaster();
            try
            {
                objresult = _allergyRepository.InsertAllergyMasters(res);
                string _CacheKey = CacheKeys.CommonMaster + "ALLERGY" + res.VenueNo + res.VenueBranchNo;
                MemoryCacheRepository.RemoveItem(_CacheKey);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AllergyController.InsertAllergyMaster" + res.AllergyMasterNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, res.VenueNo, res.VenueBranchNo, 0);
            }
            return objresult;
        }

        [HttpPost]
        [Route("api/OPDReasonMaster/GetOPDReasonMaster")]
        public List<lstOPDReasonMaster> GetOPDReasonMaster(reqOPDReasonMaster resMas)
        {
            List<lstOPDReasonMaster> result = new List<lstOPDReasonMaster>();
            try
            {
                result = _allergyRepository.GetOPDReasonMaster(resMas);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AllergyController.GetOPDReasonMaster" + resMas.OPDReasonMastNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, resMas.VenueNo, resMas.VenueBranchNo, 0);
            }
            return result;
        }

        [HttpPost]
        [Route("api/OPDReasonMaster/InsertOPDReasonMaster")]
        public rtnOPDReasonMaster InsertOPDReasonMaster(TblOPDReasonMaster reasonMas)
        {
            rtnOPDReasonMaster objresult = new rtnOPDReasonMaster();
            try
            {
                objresult = _allergyRepository.InsertOPDReasonMaster(reasonMas);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AllergyController.InsertOPDReasonMaster" + reasonMas.OPDReasonMastNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, reasonMas.VenueNo, reasonMas.VenueBranchNo, 0);
            }
            return objresult;
        }

        [HttpPost]
        [Route("api/AllergyReaction/InsertAllergyReaction")]
        public rtnAllergyReaction InsertAllergyMaster(TblAllergyReaction res)
        {
            rtnAllergyReaction objresult = new rtnAllergyReaction();
            try
            {
                objresult = _allergyRepository.InsertAllergyReaction(res);
                string _CacheKey = CacheKeys.CommonMaster + "ALLERGYREACTION" + res.VenueNo ;
                MemoryCacheRepository.RemoveItem(_CacheKey);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "AllergyController.InsertAllergyMaster" + res.AllergyReactionNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, res.VenueNo, 0, 0);
            }
            return objresult;
        }


        [HttpPost]
        [Route("api/AllergyReaction/GetAllergyReactionl")]
        public List<rtnAllergyReactionres> GetAllergyReactionl(rtnAllergyReactionreq masterRequest)
        {

            List<rtnAllergyReactionres> objResult = new List<rtnAllergyReactionres>();
            try
            {
                objResult = _allergyRepository.GetAllergyReactionl(masterRequest);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetAllergyMaster", ExceptionPriority.Low, ApplicationType.APPSERVICE, masterRequest.VenueNo, 0, 0);
            }
            return objResult;
        }
    }
}