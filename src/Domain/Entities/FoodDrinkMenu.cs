namespace CleanArchitecture.Domain.Entities
{
    public class FoodDrinkMenu : BaseAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public float Price { get; set; }
        public int Min_Order { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Image_Url { get; set; } = string.Empty;
        public List<Cart>? Carts { get; set; }
    }
}