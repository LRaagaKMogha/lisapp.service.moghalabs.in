using Dev.IRepository;
using DEV.Common;
using DEV.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Serilog;
using Microsoft.AspNetCore.Authorization;

namespace DEV.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class TitleController : ControllerBase
    {
        private readonly ITitleRepository _TitleRepository;

        public TitleController(ITitleRepository TitleRepository)
        {
            _TitleRepository = TitleRepository;

        }
        [HttpPost]
        [Route("api/Title/GettitleDetails")]
        public IEnumerable<TblTitle> GettitleDetails(TitlemasterRequest gettitle)
       {
            List<TblTitle> titleresult = new List<TblTitle>();
            try
            {
               
                titleresult = _TitleRepository.GettitleDetails(gettitle);
                
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TitleController.GettitleDetails", ExceptionPriority.Low, ApplicationType.APPSERVICE, gettitle.venueNo, 0, 0);
            }
            return titleresult;
        }


        [HttpPost]
        [Route("api/Title/InsertTitlemaster")]
        public ActionResult<Titlemasterresponse> InsertTitlemaster(TblName tblTitle)
        {
            Titlemasterresponse objresult = new Titlemasterresponse();
            try
            {
                var _errormsg = MasterValidation.InsertTitlemaster(tblTitle);
                if (!_errormsg.status)
                {
                    objresult = _TitleRepository.InsertTitlemaster(tblTitle);
                    string _CacheKey = CacheKeys.CommonMaster + "COMMON" + tblTitle.venueNo + tblTitle.venueBranchno;
                    MemoryCacheRepository.RemoveItem(_CacheKey);
                }
                else
                    return BadRequest(_errormsg);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "TitleController.InsertTitlemaster", ExceptionPriority.Low, ApplicationType.APPSERVICE, tblTitle.venueNo, tblTitle.venueBranchno, 0);
            }
            return Ok(objresult);
        }
    }
}