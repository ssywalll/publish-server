using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Context;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Banners.Commands.CreateCommand
{
    public record CreateBannerCommand : UseAprizax, IRequest<CreateBannerVm>
    {
        public string? ImagePath { get; init; }
        public string? Subject { get; set; }
        public string? Link { get; set; }
    }

    public class CreateBannerCommandHandler : IRequestHandler<CreateBannerCommand, CreateBannerVm>
    {
        private readonly IApplicationDbContext _context;

        public CreateBannerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CreateBannerVm> Handle(CreateBannerCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new NotFoundException("Request anda kosong!", HttpStatusCode.BadRequest);

            var tokenInfo = request.GetTokenInfo();

            if (tokenInfo.Is_Valid is false) return null!;

            var entity = new Banner
            {
                ImagePath = request.ImagePath,
                Subject = request.Subject,
                Link = request.Link
            };

            _context.Banners.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return new CreateBannerVm
            {
                Status = "Ok",
                Data = entity
            };
        }
    }
}