using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using MediatR;

namespace CleanArchitecture.Application.Users.Commands.CreateUser
{
    public record CreateUserCommand : IRequest<Response>
    {
        public string Name { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        // public int Phone { get; init; }
        public Gender Gender { get; init; }
        // public string Picture_Url { get; init ;} = string.Empty;
    }
    public class CeateUserCommandHandler : IRequestHandler<CreateUserCommand, Response>
    {
        private readonly IApplicationDbContext _context;

        public CeateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                // Phone = request.Phone,
                Gender = request.Gender,
                // Picture_Url = request.Picture_Url
            };

            _context.Users.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            var response = new Response
            {
                Status = "Ok",
            };

            return response;
        } 
    }
}