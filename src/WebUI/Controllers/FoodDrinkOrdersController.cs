using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.FoodDrinkOrders.Commands.CreateFoodDrinkOrder;
using CleanArchitecture.Application.FoodDrinkOrders.Commands.DeleteFoodDrinkOrder;
using CleanArchitecture.Application.FoodDrinkOrders.Commands.UpdateFoodDrinkOrder;
using CleanArchitecture.Application.FoodDrinkOrders.Queries.ExportFoodDrinkOrders;
using CleanArchitecture.Application.FoodDrinkOrders.Queries.GetFoodDrinkOrders;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [ApiController]
    public class FoodDrinkOrdersController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<FoodDrinkOrdersVm>> Get()
        {
            return await Mediator.Send(new GetFoodDrinkOrdersQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var vm = await Mediator.Send(new ExportFoodDrinkOrdersQuery { Id = id });
            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<FoodDrinkOrder>> Create(CreateFoodDrinkOrderCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateFoodDrinkOrderCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteFoodDrinkOrderCommand(id));

            return NoContent();
        }
    }
}