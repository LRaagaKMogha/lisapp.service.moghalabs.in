using Dev.IRepository;
using Dev.Repository;
using DEV.Common;
using DEV.Model;
using DEV.Model.Integration;
using Didstopia.PDFSharp.Drawing.BarCodes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.Server;
using Microsoft.Extensions.Logging;
using Microsoft.Office.Interop.Word;
using RtfPipe;
using Serilog;
using SixLabors.ImageSharp;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DEV.API.SERVICE.Controllers
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
            ackno = results.APIOutsourceSendNo != null ? Convert.ToInt32(results.APIOutsourceSendNo) : 0;
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