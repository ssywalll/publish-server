namespace CleanArchitecture.Application.Orders.Queries.GetOrders
{
    public class OrdersVm
    {
        public string Status { get; set; } = string.Empty;
        public IList<OrderDto> Data { get; set; } = new List<OrderDto>();
    }
}