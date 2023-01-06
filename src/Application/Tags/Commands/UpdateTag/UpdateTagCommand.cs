using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;

namespace CleanArchitecture.Application.Tags.Commands.UpdateTag
{
    public record UpdateTagCommand : IRequest
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public int Food_Drink_Id { get; init; }
    } 

    public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateTagCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<Unit> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Tags
                .FindAsync(new object[] {request.Id}, cancellationToken);

            if(entity == null)
            {
                throw new NotFoundException(nameof(Tags), request.Id);
            }

            entity.Name = request.Name;
            entity.Food_Drink_Id = request.Food_Drink_Id;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}