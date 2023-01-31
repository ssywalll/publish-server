using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Tags.Commands.CreateTag
{
    public record CreateTagCommand : IRequest<Tag>
    {

        public string Name { get; init; } = string.Empty;
        public int Food_Drink_Id { get; init; }
    }

    public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, Tag>
    {
        private readonly IApplicationDbContext _context;

        public CreateTagCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Tag> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var entity = new Tag
            {
                Name = request.Name,
                Food_Drink_Id = request.Food_Drink_Id
            };

            _context.Tags.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity;

        }
    }
}