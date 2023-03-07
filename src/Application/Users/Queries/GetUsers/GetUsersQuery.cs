using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Users.Queries.GetUsers
{
    public record GetUserQuery : IRequest<PaginatedList<UserDto>>
    {
        public string? Keyword { get; set; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetUsersQueryHandler : IRequestHandler<GetUserQuery, PaginatedList<UserDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            IQueryable<User> users = _context.Users;
            if (String.IsNullOrWhiteSpace(request.Keyword) is false)
                users = users.Where(
                    x => (x.Name.ToLower() + x.Email.ToLower()).Contains(request.Keyword.ToLower())
                );

            return await users
                    .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                    .OrderBy(x => x.Name)
                    .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}