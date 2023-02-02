using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.Reviews.Queries.GetReviews
{
    public class ReviewDto : IMapFrom<Review>
    {
        public int Id { get; set; }
        public Reaction Reaction { get; set; }
        public string Comment { get; set; } = string.Empty;
        public string? User_Name { get; set; }
        public int Food_Drink_Id { get; set; } 
    }
}