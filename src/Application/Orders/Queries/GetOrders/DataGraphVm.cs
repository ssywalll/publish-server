using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Orders.Queries.GetOrders
{
    public class DataGraphVm
    {
        public string Status { get; set; } = string.Empty;
        public GraphVm? Data { get; set; }
    }

    public class GraphVm
    {
        public float TotalIncome { get; set; }
        public int TotalOrdered { get; set; }
        public int? DataComparison { get; set; }
        public IList<int> DataTotalOrder {get; set;} = new List<int>();
        public IList<string?> DataOrderTime{get; set;} = new List<string?>();
        public IList<DataGraphDto2> DataGraph { get; set; } = new List<DataGraphDto2>();
    }
}