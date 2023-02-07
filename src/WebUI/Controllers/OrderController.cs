using CleanArchitecture.Application.CreateOrders.Commands.CreateOrder;
using CleanArchitecture.Application.Orders.Commands.DeleteOrder;
using CleanArchitecture.Application.Orders.Commands.UpdateOrder;
using CleanArchitecture.Application.Orders.Queries.ExportOrder;
using CleanArchitecture.Application.Orders.Queries.ExportOrders;
using CleanArchitecture.Application.Orders.Queries.GetOrders;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.WebUI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace WebUI.Controllers
{

    [ApiController]
    public class OrdersController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<OrdersVm>> Handle()
        {
            return await Mediator.Send(new GetOrdersQuery());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var vm = await Mediator.Send(new ExportOrdersQuery { Id = id });
            return Ok(vm);
        }

        [HttpGet("Token")]
        public async Task<ActionResult<OrderDto>> GetToken([FromHeader(Name = "Authorization")] GetOrdersByToken query)
        {
            return await Mediator.Send(query);
        } 

        [HttpPost]
        public async Task<ActionResult<Order>> Create(CreateOrderCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteOrderCommand(id));

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateOrderCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            await Mediator.Send(command);
            return Ok();
        }
    }
}