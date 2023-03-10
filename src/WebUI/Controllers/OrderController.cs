using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.CreateOrders.Commands.CreateOrder;
using CleanArchitecture.Application.Orders.Commands.CreateOrder;
using CleanArchitecture.Application.Orders.Commands.DeleteOrder;
using CleanArchitecture.Application.Orders.Commands.UpdateOrder;
using CleanArchitecture.Application.Orders.Queries.ExportOrder;
using CleanArchitecture.Application.Orders.Queries.ExportOrders;
using CleanArchitecture.Application.Orders.Queries.GetOrders;
using CleanArchitecture.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;


namespace WebUI.Controllers
{

    [ApiController]
    public class OrdersController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<OrderDto>> Get(int id)
        {
            var vm = await Mediator.Send(new GetOrdersQuery { Id = id });
            return Ok(vm);
        }
        [HttpGet("User")]
        public async Task<ActionResult<OrderWithTokenVm>> GetUserOrder([FromQuery] GetOrderWithToken query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("Switch")]
        public async Task<ActionResult<PaginatedList<OrderWaitingDto>>> Get([FromQuery] GetOrderWithPagination query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("Graph")]
        public async Task<ActionResult<PaginatedList<ItemGraphDto>>> GetGraph([FromQuery] GetOrderGraph query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("DataGraph")]
        public async Task<ActionResult<DataGraphVm>> GetDataGraph()
        {
            return await Mediator.Send(new GetDataGraph());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
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
        public async Task<ActionResult<PostOrderVm>> Create([FromForm] CreateOrderCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteOrderCommand(id));
            return NoContent();
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Update(UpdateOrderCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
    }
}