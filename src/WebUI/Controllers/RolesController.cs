using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Roles.Commands.CreateRole;
using CleanArchitecture.Application.Roles.Commands.DeleteRole;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [ApiController]
    public class RolesController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Role>> Create(CreateRoleCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteRoleCommand(id));

            return NoContent();
        }
    }
}