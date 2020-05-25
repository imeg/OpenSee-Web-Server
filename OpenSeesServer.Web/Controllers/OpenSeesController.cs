using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenSeesServer.Core.Services.OpenSeesServices;

namespace OpenSeesServer.Web.Controllers
{
    public class OpenSeesController : OpsBaseController
    {
        private readonly IOpenSeesService openseesService;

        public OpenSeesController(IOpenSeesService openseesService)
        {
            this.openseesService = openseesService;
        }

        [HttpGet("test/{connectionId}")]
        public async Task<IActionResult> Test(string connectionId)
        {
            return Ok(await openseesService.Execute(connectionId));
        }
    }
}