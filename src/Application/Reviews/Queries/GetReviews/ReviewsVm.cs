using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Reviews.Queries.GetReviews
{
    public class ReviewsVm
    {
        public IList<ReviewDto> ReviewDtos { get; set; } = new List<ReviewDto>();
    }
}