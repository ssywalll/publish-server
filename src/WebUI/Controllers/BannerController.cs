using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Banners.Commands.CreateCommand;
using CleanArchitecture.Application.Banners.Commands.DeleteCommand;
using CleanArchitecture.Application.Banners.Commands.UpdateCommand;
using CleanArchitecture.Application.Banners.Queries.ExportBanner;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var vm = await Mediator.Send(new GetBannerById { Id = id });

            return Ok(vm);
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteBannerCommand(id));

            return NoContent();
        }
    }
}