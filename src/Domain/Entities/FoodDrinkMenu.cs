namespace CleanArchitecture.Domain.Entities
{
    public class FoodDrinkMenu : BaseAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public float Price { get; set; }
        public int Min_Order { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Image_Url { get; set; } = string.Empty;
        public int Order_Id { get; set; }
        public List<Cart>? Carts { get; set; }
        public List<Review>? Reviews { get; set; }
        public List<Tag>? Tags { get; set; }
        public List<FoodDrinkOrder>? FoodDrinkOrders { get; set; }
        public Order? Orders { get; set; }
    }
}