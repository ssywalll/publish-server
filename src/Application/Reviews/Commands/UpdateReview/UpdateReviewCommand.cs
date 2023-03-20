using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using CleanArchitecture.Application.Common.Interfaces;
using AutoMapper;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Application.Common.Context;
using System.Net;

namespace CleanArchitecture.Application.Reviews.Commands.UpdateReviews
{
    public record UpdateReviewsCommand : UseAprizax, IRequest
    {
        public int Id { get; set; }
        public Reaction Reaction { get; init; }
        public int FoodDrinkId { get; init; }
    }

    public class UpdateReviewsCommandHandler : IRequestHandler<UpdateReviewsCommand>
    {
        private readonly IApplicationDbContext _context;
        private IMapper _mapper;

        public UpdateReviewsCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateReviewsCommand request, CancellationToken cancellationToken)
        {
            var tokenInfo = request.GetTokenInfo();
            if (tokenInfo.Is_Valid is false)
                throw new NotFoundException("Token tidak ditemukan", HttpStatusCode.BadRequest);

            var entity = await _context.Reviews
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity is null)
                throw new NotFoundException("Reaction tidak ditemukan", HttpStatusCode.BadRequest);

            if (entity.User_Id.Equals(tokenInfo.Owner_Id) is false)
                throw new NotFoundException("Reaction ini bukan milik anda", HttpStatusCode.BadRequest);

            var ownerId = request.GetOwnerId();

            entity.Reaction = request.Reaction;
            entity.User_Id = (int)ownerId!;
            entity.Food_Drink_Id = request.FoodDrinkId;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}