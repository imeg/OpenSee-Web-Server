using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenSeesServer.Core.Models.RequestModels;
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

        [HttpGet("initialize/{connId}")]
        public async Task<IActionResult> Initialize(string connId)
        {
            await openseesService.Initialize(connId);
            return Ok();
        }

        [HttpPost("execute")]
        public async Task<IActionResult> Execute([FromBody] ExecutionCommandRequest model)
        {
            return Ok(await openseesService.Execute(model));
        }
    }
}