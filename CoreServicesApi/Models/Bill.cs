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

    [ForeignKey("UserId")]
    public int? UserId { get; set; }
    public User? User { get; set; }
    
    //if the user doesn't  have an account
    public string? UserName { get; set; }
    public string? UserEmail { get; set; }
    public string? Address { get; set; }
    public string? productName { get; set; }

    public List<Product>? Items { get; set; } = new List<Product>();
}


}
