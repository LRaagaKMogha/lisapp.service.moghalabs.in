using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Service.Model;
using Service.Common;
using Microsoft.AspNetCore.Authorization;
using Service.IRepository;

namespace Service.API.SERVICE.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class PackController : ControllerBase
    {
        private readonly IPackRepository _packRepository;
        public PackController(IPackRepository noteRepository )
        {
            _packRepository = noteRepository;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/Pack/Getpackmaster")]
        public IEnumerable<TblPack> Getpackmaster(PackMasterRequest packRequest)
        {
             List<TblPack> result = new List<TblPack>();
            try
            {               
                result= _packRepository.Getpackmaster(packRequest);             
            }
            catch(Exception ex)
            {
                MyDevException.Error(ex, "PackController.Getpackmaster" + packRequest.packNo.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, packRequest.venueNo, packRequest.venueBranchno, 0);
            }
            return result;
        }

        [CustomAuthorize("INVMASTERS")]
        [HttpPost]
        [Route("api/Pack/Insertpackmaster")]
        public PackMasterResponse Insertpackmaster(TblPack tblPack)
        {
            PackMasterResponse Objresult = new PackMasterResponse();
            try
            {
                Objresult = _packRepository.Insertpackmaster(tblPack);
                string _CacheKey = CacheKeys.CommonMaster + "PACK" + tblPack.venueNo + tblPack.venueBranchno;
                MemoryCacheRepository.RemoveItem(_CacheKey);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "PackController.Insertpackmaster" + tblPack.packNo.ToString(), ExceptionPriority.Low, ApplicationType.APPSERVICE, tblPack.venueNo, tblPack.venueBranchno, tblPack.userNo);
            }
            return Objresult;
        }
    }
}