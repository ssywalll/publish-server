namespace CleanArchitecture.Domain.Entities
{
    public class FoodDrinkOrder : BaseAuditableEntity
    {
        public int Food_Drink_Id { get; set; }
        public int Order_Id { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public FoodDrinkMenu? FoodDrinkMenus { get; set; }
        public Order? Orders { get; set; }
    }
}