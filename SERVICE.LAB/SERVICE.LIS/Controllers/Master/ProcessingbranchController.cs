using System;
using System.Collections.Generic;
using Dev.IRepository;
using DEV.Common;
using DEV.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;

namespace DEV.API.SERVICE.Controllers
{

    [ApiController]
    public class ProcessingbranchController : ControllerBase
    {
        private readonly IProcessingbranchRepository _ProcessingbranchRepository;
        public ProcessingbranchController(IProcessingbranchRepository noteRepository)
        {
            _ProcessingbranchRepository = noteRepository;
        }

        [HttpPost]
        [Route("api/ProcessingBranch/GetProcessingbranch")]
        public List<responsebranch> GetProcessingbranch(reqbranch req)
        {
            List<responsebranch> lst = new List<responsebranch>();
            try
            {
                lst = _ProcessingbranchRepository.GetProcessingbranch(req);
            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProcessingbranchController.GetProcessingbranch" + req.processingBranchMapNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, req.venueNo, req.processingBranchMapNo, 0);
            }
            return lst;
        }
        [HttpPost]
        [Route("api/ProcessingBranch/InsertProcessingbranch")]
        public Storeprocessingbranch InsertProcessingbranch(insertbranch obj1)

        {
            Storeprocessingbranch objresult = new Storeprocessingbranch();
            try
            {
                objresult = _ProcessingbranchRepository.InsertProcessingbranch(obj1);

            }
            catch (Exception ex)
            {
                MyDevException.Error(ex, "ProcessingbranchController.InsertProcessingbranch" + obj1.processingBranchMapNo.ToString(), ExceptionPriority.Low, ApplicationType.REPOSITORY, obj1.venueNo, obj1.userNo, obj1.processingBranchMapNo);
            }
            return objresult;
        }

    }
}