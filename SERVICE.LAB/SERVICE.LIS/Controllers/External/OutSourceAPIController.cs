using Service.IRepository;
using Service.Common;
using Service.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Service.API.SERVICE.Controllers
{    
    [ApiController]
    public class OutSourceAPIController : ControllerBase
    {
        private readonly IOutSourceAPIRepository _IOutSourceAPIRepository;
        public OutSourceAPIController(IOutSourceAPIRepository IOutSourceAPIRepository)
        {
            _IOutSourceAPIRepository = IOutSourceAPIRepository;
        }
        [HttpPost]
        [Route("api/OutSourceAPI/GetOutSourceAPIList")]
        public List<OutSourceAPIDTOResponse> GetOutSourceAPIList(OutSourceAPIDTORequest results)
        {
            List<OutSourceAPIDTOResponse> lst = new List<OutSourceAPIDTOResponse>();
            try
            {
                lst = _IOutSourceAPIRepository.GetOutSourceAPIList(results);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OutSourceAPIController.GetOutSourceAPIList", ExceptionPriority.High, ApplicationType.APPSERVICE, results.VenueNo, results.VenueBranchNo, 0);
            }
            return lst;
        }
        [HttpPost]
        [Route("api/OutSourceAPI/AckOutSourceAPIList")]
        public int AckOutSourceAPIList(AckOutSourceAPIDTORequest results)
        {
            int OutStatus = 0;
            int ackno = 0;
            ackno = Convert.ToInt32(results.APIOutsourceSendNo);
            try
            {
                OutStatus = _IOutSourceAPIRepository.AckOutSourceAPIList(results);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "OutSourceAPIController.AckOutSourceAPIList", ExceptionPriority.High, ApplicationType.APPSERVICE, ackno, 0, 0);
            }
            return OutStatus;
        }
    }
}