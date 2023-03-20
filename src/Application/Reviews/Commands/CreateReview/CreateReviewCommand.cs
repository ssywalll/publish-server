using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Context;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Reviews.Commands.CreateReview
{
    public record CreateReviewCommand : UseAprizax, IRequest<Review>
    {
        public Reaction? Reaction { get; set; } = null;
        public int FoodDrinkId { get; set; }
    }

    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Review>
    {
        private readonly IApplicationDbContext _context;

        public CreateReviewCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Review> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                return null!;

            var tokenInfo = request.GetTokenInfo();

            if (tokenInfo.Is_Valid is false) return null!;

            var entity = new Review
            {
                Reaction = request.Reaction,
                User_Id = tokenInfo.Owner_Id ?? 0,
                Food_Drink_Id = request.FoodDrinkId
            };

            _context.Reviews.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}