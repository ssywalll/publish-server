using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Carts.Commands.CreateCart;
using CleanArchitecture.Application.Carts.Commands.DeleteCart;
using CleanArchitecture.Application.Carts.Queries.GetCarts;
using CleanArchitecture.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using CleanArchitecture.Application.Carts.Queries.ExportCarts;
using CleanArchitecture.Application.Carts.Commands.UpdateCart;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;

namespace WebUI.Controllers
{
    [Authorize]
    [ApiController]
    public class CartsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<CartsVm>> Get()
        {
            return await Mediator.Send(new GetCartsQuery());
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int id)
        {
            var vm = await Mediator.Send(new ExportCartsQuery { Id = id });
            return Ok(vm);
        }

        [HttpGet("Token")]
        public async Task<ActionResult<PaginatedList<CartDto>>> GetToken([FromQuery] GetCartByToken query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("CurrentQuantity")]
        public async Task<ActionResult<CurrentQuantityCartVm>> Get([FromQuery] CurrentQuantityCart query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<CreateCartVm>> Create(CreateCartCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("Remove/Single")]
        public async Task<ActionResult> RemoveSingle(RemoveCartSingle command)
        {
            await Mediator.Send(command);

            return Ok();
        }
        [HttpDelete("Remove/Multiple")]
        public async Task<ActionResult> RemoveMultiple(RemoveCartMultiple command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateCartsCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            await Mediator.Send(command);
            return Ok();
        }

        [HttpPut("CheckboxTrigger")]
        public async Task<ActionResult> CheckboxTrigger(CheckboxTriggerCart command)
        {
            await Mediator.Send(command);
            return Ok();
        }
        [HttpPut("CheckboxTrigger/All")]
        public async Task<ActionResult> CheckboxTriggerAll(CheckboxAllCart command)
        {
            await Mediator.Send(command);
            return Ok();
        }
        [HttpPut("OneQuantity")]
        public async Task<ActionResult> OneQuantity(OneQuantityCartDto command)
        {
            await Mediator.Send(command);
            return Ok();
        }

    }
}