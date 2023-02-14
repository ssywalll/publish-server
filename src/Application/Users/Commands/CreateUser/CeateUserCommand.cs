using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Users.Queries.GetUsers;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Application.Users.Commands.CreateUser
{
    public record CreateUserCommand : IRequest<RegisterVm>
    {
        public string Name { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public string Role { get; init; } = "user";
        public Gender Gender { get; init; }
    }
    public class CeateUserCommandHandler : IRequestHandler<CreateUserCommand, RegisterVm>
    {
        private readonly IApplicationDbContext _context;

        public CeateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RegisterVm> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValidEmail(_context) is false)
                return new RegisterVm
                {
                    Status = "Email Is Not Valid",
                    Data = null
                };
            var entity = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = request.Role,
                Gender = request.Gender,
            };
            _context.Users.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return new RegisterVm
            {
                Status = "Ok",
                Data = new UserDto
                {
                    Name = entity.Name,
                    Email = entity.Email,
                    Gender = entity.Gender
                }
            };
        }
    }
}