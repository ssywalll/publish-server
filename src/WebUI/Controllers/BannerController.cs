using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Banners.Commands.CreateCommand;
using CleanArchitecture.Application.Banners.Commands.UpdateCommand;
using CleanArchitecture.Application.Banners.Queries.GetBanner;
using CleanArchitecture.WebUI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [ApiController]
    public class BannerController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<BannerVm>> Get()
        {
            return await Mediator.Send(new GetBannerQuery());
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<CreateBannerVm>> Create(CreateBannerCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateBannerCommand command)
        {
            await Mediator.Send(command);
            return Ok();    
        }
    }
}