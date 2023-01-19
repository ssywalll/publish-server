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

       [HttpPost]
       public async Task<ActionResult<int>> Create(CreateCartCommand command)
       {
        return await Mediator.Send(command);
       }

       [HttpDelete("{id}")]
       public async Task<ActionResult> Delete(int id)
       {
            await Mediator.Send(new DeleteCartCommand(id));

            return NoContent();
       }
    }
}