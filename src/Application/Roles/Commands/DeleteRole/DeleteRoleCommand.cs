using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Roles.Commands.DeleteRole
{
    public record DeleteRoleCommand(int Id) : IRequest; 

    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteRoleCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Roles
                .Where(x => x.User_Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if( entity == null )
            {
                throw new NotFoundException(nameof(Role), request.Id);
            }

            _context.Roles.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}