// using Microsoft.EntityFrameworkCore;
// using Models;

// namespace Services;
// public class ProductService : IProductService
// {
//     private readonly AppDbContext? _context;
//     private readonly string? _configuration;
//     public ProductService(AppDbContext? context, IConfiguration configuration)
//     {
//         _context = context;
//         _configuration = configuration.GetConnectionString("DefaultConnection");
//     }
//     public async Task<Product> AddNewProduct(Product product)
//     {
//         _context!.Products.Add(product);
//         await _context.SaveChangesAsync();
//         return product;
//     }
//     public async Task<List<Product>> GetAllProducts(Product product)
//     {
//         return await _context!.Products.ToListAsync();
//     }
//     public async Task<Product> GetProductByName(string productName)
//     {
//         return await _context!.Products.FirstOrDefaultAsync(x => x.Name == productName);
//     }
// }