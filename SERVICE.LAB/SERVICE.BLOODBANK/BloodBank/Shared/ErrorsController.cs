using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Shared
{
    [Route("/error")]
    public class ErrorsController : ApiController
    {
        [HttpGet]
        public IActionResult Error()
        {
            return Problem();
        }
    }
}