using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Reviews.Commands.CreateReview;
using CleanArchitecture.Application.Reviews.Commands.DeleteReview;
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
    }
}