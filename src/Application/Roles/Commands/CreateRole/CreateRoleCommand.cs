using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Roles.Commands.CreateRole
{
    public record CreateRoleCommand : IRequest<Role>
    {
        public int User_Id { get; init; }
    }

    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Role>
    {
        private readonly IApplicationDbContext _context;

        public CreateRoleCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Role> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var entity = new Role()
            {
                User_Id = request.User_Id
            };

            _context.Roles.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}