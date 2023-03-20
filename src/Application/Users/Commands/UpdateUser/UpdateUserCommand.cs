using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Context;
using CleanArchitecture.Application.Users.Queries.GetUsers;
using CleanArchitecture.Domain.Entities;
using AutoMapper;
using MediatR;
using System.Net;

namespace CleanArchitecture.Application.Users.Commands.UpdateUser
{
    public record UpdateUserCommand : UseAprizax, IRequest<UserDto>
    {
        public IFormFile? Avatar { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Phone { get; init; } = string.Empty;
    }
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new NotFoundException("Request anda kosong!", HttpStatusCode.BadRequest);
            var tokenInfo = request.GetTokenInfo();
            if (tokenInfo.Is_Valid is false)
                throw new NotFoundException("Token anda tidak valid!", HttpStatusCode.BadRequest);

            var entity = await _context.Users
                .FindAsync(new object[] { tokenInfo.Owner_Id! }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(User), tokenInfo.Owner_Id!);
            }

            entity.Name = request.Name;
            entity.Email = request.Email;
            entity.Phone = request.Phone;

            if (request.Avatar is null)
            {
                await _context.SaveChangesAsync(cancellationToken);

                return await _context.Users
                    .Where(x => x.Id.Equals(tokenInfo.Owner_Id))
                    .AsNoTracking()
                    .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                    .SingleAsync(cancellationToken);
            }

            var dateName = DateTime.Now.ToString("yyyy-MM-dd");
            var fileExtension = Path.GetExtension(request.Avatar!.FileName);
            var fileName = $"{entity.Id}-avatar-{dateName}{fileExtension}";
            var myPath = Path.Combine("user", fileName);

            if (File.Exists(entity.Picture_Url.GetFullPath()))
            {
                File.Delete(entity.Picture_Url.GetFullPath());
            }

            using (var stream = File.Create(myPath.GetFullPath()))
            {
                await request.Avatar.CopyToAsync(stream, cancellationToken);
            }

            entity.Picture_Url = myPath;

            await _context.SaveChangesAsync(cancellationToken);

            return await _context.Users
                .Where(x => x.Id.Equals(tokenInfo.Owner_Id))
                .AsNoTracking()
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .SingleAsync(cancellationToken);
        }
    }
}