using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Tags.Commands.CreateTag;
using CleanArchitecture.Application.Tags.Commands.DeleteTag;
using CleanArchitecture.Application.Tags.Commands.UpdateTag;
using CleanArchitecture.Application.Tags.Queries.ExportTags;
using CleanArchitecture.Application.Tags.Queries.GetTags;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [ApiController]
    public class TagsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<TagsVm>> Get()
        {
            return await Mediator.Send(new GetTagsQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var vm = await Mediator.Send(new ExportTagsQuery { Id = id });
            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Tag>> Create(CreateTagCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateTagCommand command)
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
            await Mediator.Send(new DeleteTagCommand(id));

            return NoContent();
        }
    }
}