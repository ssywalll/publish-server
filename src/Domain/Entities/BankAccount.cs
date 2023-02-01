namespace CleanArchitecture.Domain.Entities
{
    public class BankAccount : BaseAuditableEntity
    {
        public string Bank_Number { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Bank_Name { get; set; } = string.Empty;
        public int User_Id { get; set; }
        public User? Users { get; set; }
        public List<Order>? Orders { get; set; }
    }
}