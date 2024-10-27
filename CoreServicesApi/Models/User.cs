using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int BillId { get; set; }
        public List<Bill>? Bills { get; set; }
        public int itemId { get; set; }
        public List<Product>? Items { get; set; }
    }
}