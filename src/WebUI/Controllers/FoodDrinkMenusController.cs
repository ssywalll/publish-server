using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CleanArchitecture.WebUI.Controllers;
using CleanArchitecture.Application.FoodDrinkMenus.Queries.GetFoodDrinkMenus;
using CleanArchitecture.Application.FoodDrinkMenus.Commands.CreateFoodDrinkMenu;
using CleanArchitecture.Application.FoodDrinkMenus.Commands.DeleteFoodDrinkMenuCommand;
using CleanArchitecture.Application.FoodDrinkMenus.Commands.UpdateFoodDrinkMenu;
using CleanArchitecture.Application.FoodDrinkMenus.Queries.ExportFoodDrinkMenus;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Application.Common.Models;

namespace WebUI.Controllers
{
    [ApiController]
    public class FoodDrinkMenusController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<FoodDrinkMenuDto>>> Get([FromQuery] GetFoodDrinkMenuWithPagination query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("CurrentTotal")]
        public async Task<ActionResult<GetFoodDrinkQuantityVm>> Get([FromQuery] GetFoodDrinkQuantity query)
        {
            return await Mediator.Send(query);
        }


        [HttpGet("{id}/detail")]
        public async Task<IActionResult> Get(int id)
        {
            var vm = await Mediator.Send(new ExportFoodDrinkMenusQuery { Id = id });

            return Ok(vm);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("Post")]
        public async Task<ActionResult<FoodDrinkMenu>> Create([FromForm] CreateFoodDrinkMenuCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}/edit")]
        public async Task<ActionResult<FoodDrinkMenuDto>> Update(int id, [FromForm] UpdateFoodDrinkMenuCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return Ok();
        }

        [HttpDelete("{id}delete")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteFoodDrinkMenuCommand(id));

            return Ok();
        }
    }
}