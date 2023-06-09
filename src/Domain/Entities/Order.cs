namespace CleanArchitecture.Domain.Entities
{
    public class Order : BaseAuditableEntity
    {
        public DateTime Order_Time { get; set; } = DateTime.Now;
        public DateTime Meal_Date { get; set; }
        public Status Status { get; set; }
        public int BankAccount_Id { get; set; }
        public string Payment_Url { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Bank_Number { get; set; } = string.Empty;
        public int User_Id { get; set; }
        public User? users { get; set; }
        public BankAccount? BankAccounts { get; set; }
        public List<FoodDrinkOrder>? FoodDrinkOrders { get; set; }
    }
}