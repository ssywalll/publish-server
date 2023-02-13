using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Users.Commands.CreateUser;
using CleanArchitecture.Application.Users.Commands.DeleteUser;
using CleanArchitecture.Application.Users.Commands.Login;
using CleanArchitecture.Application.Users.Commands.UpdateUser;
using CleanArchitecture.Application.Users.Commands.ValidateToken;
using CleanArchitecture.Application.Users.Queries.ExportUsers;
using CleanArchitecture.Application.Users.Queries.GetUsers;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.WebUI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [ApiController]
    public class UsersController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<UserDto>>> Get([FromQuery] GetUserQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var vm = await Mediator.Send(new ExportUsersQuery { Id = id });

            return Ok(vm);
        }

        [HttpPost("Authenticate")]
        public async Task<ActionResult<LoginVm>> Authenticate(LoginCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegisterVm>> Create(CreateUserCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("ValidateToken")]
        public async Task<ActionResult<ValidateVm>> Validate([FromHeader(Name = "Authorization")]ValidateToken query)
        {
            return await Mediator.Send(query);
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateUserCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteUserCommand(id));

            return NoContent();
        }
    }
}