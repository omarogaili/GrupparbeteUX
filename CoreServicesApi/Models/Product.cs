namespace Models;
public class Item
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double? Price { get; set; }
    public string? Image { get; set; }

    public int BillId { get; set; }
    public Bill? Bill { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }
}
