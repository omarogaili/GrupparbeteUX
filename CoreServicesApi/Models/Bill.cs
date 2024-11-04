using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Cargo { get; set; }
        [ForeignKey ("UserId")]
        public int? UserId { get; set;}
        public User? User { get; set; }
        public List<Product>? Items { get; set; } = new List<Product>();
    }

}
