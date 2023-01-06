using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Users.Commands.CreateUser;
using CleanArchitecture.Application.Users.Commands.DeleteUser;
using CleanArchitecture.Application.Users.Commands.UpdateUser;
using CleanArchitecture.Application.Users.Queries.ExportUsers;
using CleanArchitecture.Application.Users.Queries.GetUsers;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebUI.Controllers
{
    [ApiController]
    public class UsersController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<UsersVm>> Get()
        {
            return await Mediator.Send( new GetUserQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var vm = await Mediator.Send(new ExportUsersQuery {Id = id});

            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Response>> Create(CreateUserCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateUserCommand command)
        {
            if(id != command.Id)
            {
                return BadRequest();
            }

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