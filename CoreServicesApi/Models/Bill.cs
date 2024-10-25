using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }
        public decimal TotalAmount { get; private set; }
        public DateTime CreatedAt { get; set; }
        public int Cargo { get; set;}
        [ForeignKey("Id")]
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}