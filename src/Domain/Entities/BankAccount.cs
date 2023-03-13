namespace CleanArchitecture.Domain.Entities
{
    public class BankAccount : BaseAuditableEntity
    {
        public string BankNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
        public int UserId { get; set; }
        public bool IsChoosen { get; set; } = true;
        public User? Users { get; set; }
        public List<Order>? Orders { get; set; }
    }
}