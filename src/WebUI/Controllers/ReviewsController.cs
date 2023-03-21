using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Models;
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
    [ApiController]
    public class ReviewsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ReviewsVm>> Handle()
        {
            return await Mediator.Send(new GetReviewsQuery());
        }

        [HttpGet("foodDrinkId")]
        public async Task<ActionResult<PaginatedList<ReviewDto>>> Get([FromQuery] ExportReviewsQuery command)
        {
            var vm = await Mediator.Send(command);
            return Ok(vm);
        }

        [HttpGet("Token")]
        public async Task<ActionResult<ReviewDto>> GetToken([FromHeader(Name = "Authorization")] GetReviewsByToken query)
        {
            return await Mediator.Send(query);
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

        [HttpPut]
        public async Task<ActionResult> Update(UpdateReviewsCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }

    }
}