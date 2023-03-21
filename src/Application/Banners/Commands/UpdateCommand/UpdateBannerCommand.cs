using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Context;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;

namespace CleanArchitecture.Application.Banners.Commands.UpdateCommand
{
    public record UpdateBannerCommand : UseAprizax, IRequest
    {
        public int Id { get; init; }
        public string? ImagePath { get; init; }
        public string? Subject { get; set; }
        public string? Link { get; set; }
    }

    public class UpdateBannerCommandHandler : IRequestHandler<UpdateBannerCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateBannerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateBannerCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Banners
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Carts), request.Id);
            }

            entity.Id = request.Id;
            entity.ImagePath = request.ImagePath;
            entity.Subject = request.Subject;
            entity.Link = request.Link;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}