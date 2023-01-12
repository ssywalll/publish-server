using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Reviews.Queries.GetReviews
{
    public class ReviewsVm
    {
        public string Status { get; set; } = string.Empty;
        public IList<ReviewDto> Data { get; set; } = new List<ReviewDto>();
    }
}