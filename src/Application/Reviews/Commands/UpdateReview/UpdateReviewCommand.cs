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

namespace CleanArchitecture.Application.Reviews.Commands.UpdateReviews
{
    public class UpdateReviewsCommand : IRequest
    {
        public int Id { get; init; }
        public Reaction Reaction { get; init; }
        public string Comment { get; init; } = string.Empty;
        public int User_Id { get; init; }
        public int Food_Drink_Id { get; init; }

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
            var entity = await _context.Reviews
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Reviews), request.Id);
            }

            entity.Reaction = request.Reaction;
            entity.Comment = request.Comment;
            entity.Id = request.Id;
            entity.User_Id = request.User_Id;
            entity.Food_Drink_Id = request.Food_Drink_Id;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }


    }
}