using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Orders.Queries.ExportOrder
{
    public class GetByTokenVm
    {
        public string Status { get; set; } = string.Empty;
        public GetTotalOrder? Data { get; set; } 
    }

    public class GetTotalOrder
    {
        public int TotalOrdered { get; set; }
        public float TotalPriceOrdered { get; set; }
        public IList<GetByTokenDto> ListData { get; set; } = new List<GetByTokenDto>();
    }
}