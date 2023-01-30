namespace CleanArchitecture.Domain.Entities
{
    public class User : BaseAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int Phone { get; set; }
        public Gender Gender { get; set; }
        public string Picture_Url { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}