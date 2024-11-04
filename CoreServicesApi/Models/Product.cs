using System.ComponentModel.DataAnnotations.Schema;

namespace Models;
public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double? Price { get; set; }
    public string? Image { get; set; }
    [ForeignKey ("BillId")]
    public int? BillId { get; set; }
    public Bill? Bill { get; set; }
}
