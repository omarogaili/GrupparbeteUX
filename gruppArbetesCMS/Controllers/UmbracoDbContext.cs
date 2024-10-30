using Microsoft.EntityFrameworkCore;

public class UmbracoDbContext : DbContext
{
    public UmbracoDbContext(DbContextOptions<UmbracoDbContext> options)
        : base(options)
    {
    }
    public DbSet<Item> Items { get; set; }
}
