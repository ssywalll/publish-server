using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using MediatR;

namespace CleanArchitecture.Application.Users.Commands.CreateUser
{
    public record CreateUserCommand : IRequest<User>
    {
        public string Name { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public string Role { get; init; } = string.Empty;
        public Gender Gender { get; init; }
    }
    public class CeateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IApplicationDbContext _context;

        public CeateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password =  BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = request.Role,
                Gender = request.Gender,
            };


            _context.Users.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        } 
    }
}