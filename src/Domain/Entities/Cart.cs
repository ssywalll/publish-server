namespace CleanArchitecture.Domain.Entities
{
    public class Cart : BaseAuditableEntity
    {
        public int User_Id { get; set; }
        public int Food_Drink_Id { get; set; }
        public int Quantity { get; set; }
        public FoodDrinkMenu? FoodDrinkMenu { get; set; }
    }
}