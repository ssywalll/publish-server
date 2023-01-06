using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Users.Queries.GetUsers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Users.Queries.ExportUsers
{
    public record ExportUsersQuery : IRequest<UsersVm>
    {
        public int Id { get; init; }
    }

    public class ExportUsersQueryHandler : IRequestHandler<ExportUsersQuery, UsersVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ExportUsersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UsersVm> Handle(ExportUsersQuery request, CancellationToken cancellationToken)
        {
            return new UsersVm
            {
                UserDtos = await _context.Users
                    .Where(x => x.Id == request.Id)
                    .AsNoTracking()
                    .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}