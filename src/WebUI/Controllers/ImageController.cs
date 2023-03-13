using CleanArchitecture.Application.Images;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [ApiController]
    public class ImageController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] ReadImageQueries query)
        {
            var fileContent = await Mediator.Send(query);
            return fileContent;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create([FromForm] UploadFileCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}