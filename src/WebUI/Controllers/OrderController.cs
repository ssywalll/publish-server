using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.CreateOrders.Commands.CreateOrder;
using CleanArchitecture.Application.Orders.Commands.CreateOrder;
using CleanArchitecture.Application.Orders.Commands.DeleteOrder;
using CleanArchitecture.Application.Orders.Commands.UpdateOrder;
using CleanArchitecture.Application.Orders.Queries.ExportOrder;
using CleanArchitecture.Application.Orders.Queries.ExportOrders;
using CleanArchitecture.Application.Orders.Queries.GetOrders;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.WebUI.Controllers;
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

        [HttpGet("Waiting")]
        public async Task<ActionResult<PaginatedList<OrderWaitingDto>>> GetWaiting([FromQuery] GetOrderStatusWaiting query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("OnProcces")]
        public async Task<ActionResult<PaginatedList<OrderWaitingDto>>> GetOnProcces([FromQuery] GetOrderOnProcces query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("OnDelivery")]
        public async Task<ActionResult<PaginatedList<OrderWaitingDto>>> GetOnDelivery([FromQuery] GetOrderOnDelivery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("Successful")]
        public async Task<ActionResult<PaginatedList<OrderWaitingDto>>> GetSuccessful([FromQuery] GetOrderSuccessful query)
        {
            return await Mediator.Send(query);
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
        public async Task<ActionResult<PostOrderVm>> Create(CreateOrderCommand command)
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