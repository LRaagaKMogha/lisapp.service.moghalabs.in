using Dev.IRepository.Inventory;
using Dev.IRepository.Master;
using Dev.Repository.Inventory;
using DEV.Common;
using Service.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DEV.API.SERVICE.Controllers.Master
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class DiscountController : ControllerBase
    {

        private readonly IConfiguration _config;
        private readonly IDiscountRepository _discountRepository;
        public DiscountController(IDiscountRepository DiscountRepository, IConfiguration config)
        {
            _discountRepository = DiscountRepository;
            _config = config;
        }

        [HttpPost]
        [Route("api/Discount/GetDiscountMaster")]
        public List<GetDiscountDetails> GetDiscountMasters(DiscountMasterRequest discountItem)
        {
            List<GetDiscountDetails> objresult = new List<GetDiscountDetails>();
            try
            {

                objresult = _discountRepository.GetDiscountMasters(discountItem);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "GetDiscountMasters", ExceptionPriority.Low, ApplicationType.APPSERVICE, discountItem.venueNo, discountItem.venueBranchno, 0);
            }
            return objresult;
        }

        [HttpPost]
        [Route("api/Discount/InsertDiscountMaster")]
        public DiscountMasterReponse InsertDiscountMasters(DiscountInsertData disResponse)
        {
            DiscountMasterReponse result = new DiscountMasterReponse();
            try
            {
                result = _discountRepository.InsertDiscountMasters(disResponse);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "InsertDiscountMasters", ExceptionPriority.Medium, ApplicationType.APPSERVICE, disResponse.discountNo, disResponse.venueBranchNo, 0);
            }
            return result;
        }
    }
}
