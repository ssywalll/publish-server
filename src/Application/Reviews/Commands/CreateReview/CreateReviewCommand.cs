using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Reviews.Commands.CreateReview
{
    public record CreateReviewCommand : IRequest<Review>
    {
        public Reaction Reaction { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int User_Id { get; set; }
        public int Food_Drink_Id { get; set; }
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
            var entity = new Review
            {
                Reaction = request.Reaction,
                Comment = request.Comment,
                User_Id = request.User_Id,
                Food_Drink_Id = request.Food_Drink_Id
            };

            _context.Reviews.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}