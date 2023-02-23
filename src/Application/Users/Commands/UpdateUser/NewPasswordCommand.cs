using AutoMapper;
using MediatR;
using System.Net;
using CleanArchitecture.Application.Common.Context;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Users.Commands.UpdateUser
{
    public record NewPasswordVm
    {
        public string? Status { get; set; }
        public string? Data { get; set; }
    }
    public record NewPasswordCommand : UseAprizax, IRequest<NewPasswordVm>
    {
        public string? OldPassword { get; init; }
        public string? NewPassword { get; init; }
    }
    public class NewPasswordCommandHandler : IRequestHandler<NewPasswordCommand, NewPasswordVm>
    {
        private readonly IApplicationDbContext _context;

        public NewPasswordCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<NewPasswordVm> Handle(NewPasswordCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new NotFoundException("Request anda kosong!", HttpStatusCode.BadRequest);
            var tokenInfo = request.GetTokenInfo();
            if (tokenInfo.Is_Valid is false)
                throw new NotFoundException("Token anda tidak valid!", HttpStatusCode.BadRequest);

            var entity = _context.Users.FirstOrDefault(x => x.Id.Equals(tokenInfo.Owner_Id));

            if (entity == null)
                throw new NotFoundException("Token anda tidak valid!", HttpStatusCode.BadRequest);

            bool isAuth = BCrypt.Net.BCrypt.Verify(request.OldPassword, entity.Password);

            if (isAuth is false)
                return new NewPasswordVm
                {
                    Status = "Unauthenticated",
                    Data = null
                };

            entity.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword!);

            await _context.SaveChangesAsync(cancellationToken);

            return new NewPasswordVm
            {
                Status = "Ok",
                Data = "Password has changed!"
            };
        }

    }
}