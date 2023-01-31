using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Reviews.Commands.CreateReview;
using CleanArchitecture.Application.Reviews.Commands.DeleteReview;
using CleanArchitecture.Application.Reviews.Commands.UpdateReviews;
using CleanArchitecture.Application.Reviews.Queries.ExportReviews;
using CleanArchitecture.Application.Reviews.Queries.GetReviews;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.WebUI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace WebUI.Controllers
{
    [Authorize]
    [ApiController]
    public class ReviewsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ReviewsVm>> Handle()
        {
            return await Mediator.Send(new GetReviewsQuery());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var vm = await Mediator.Send(new ExportReviewsQuery { Id = id });
            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Review>> Create(CreateReviewCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteReviewCommand(id));

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateReviewsCommand command)
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