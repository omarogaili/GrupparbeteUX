using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.PublishedModels;
using System.Linq;
/// <summary>
/// more categories => 5 products for each.
/// Products Name not working.   
/// </summary>
namespace Backoffice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : UmbracoController
    {
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        public ProductController(IUmbracoContextAccessor umbracoContextAccessor)
        {
            _umbracoContextAccessor = umbracoContextAccessor;
        }
        [HttpGet("products")]
        public IActionResult GetProducts()
        {
            var umbracoContext = _umbracoContextAccessor.GetRequiredUmbracoContext();
            var request = HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";
            var productHandlers = umbracoContext.Content!.GetAtRoot()
                .Where(x => x.ContentType.Alias == "productHandler")
                .SelectMany(x => x.Children)
                .Where(x => x.ContentType.Alias == "productPage")
                .Select(x => new
                {
                    Id = x.Id,
                    HandlerName = x.Value<string>("handlersName"),
                    ProductName = x.Value<string>("productsName"),
                    Category = x.Value<string>("category"),
                    Description = x.Value<string>("description"),
                    Price = x.Value<int>("price"),
                    CardDescription= x.Value<string>("cardDescription"),
                    ImageUrl = x.Value<IPublishedContent>("productImage") != null
                        ? $"{baseUrl}{x.Value<IPublishedContent>("productImage").Url()}"
                        : null
                })
                .ToList();
            if (!productHandlers.Any())
            {
                return NotFound();
            }
            return Ok(productHandlers);
        }
        [HttpGet("category")]
        public IActionResult GetCategory(string category)
        {
            var request = HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";
            var umbracoContext = _umbracoContextAccessor.GetRequiredUmbracoContext();
            var products = umbracoContext.Content!.GetAtRoot()
                .FirstOrDefault(x => x.ContentType.Alias == "productHandler")
                ?.DescendantsOrSelf()
                .Where(p => p.ContentType.Alias == "productPage" && p.Value<string>("category") == category)
                .Select(p => new
                {
                    Id = p.Id,
                    ProductName = p.Value<string>("productsName"),
                    Category = p.Value<string>("category"),
                    Description = p.Value<string>("description"),
                    CardDescription= p.Value<string>("cardDescription"),
                    ImageUrl = p.Value<IPublishedContent>("productImage") != null
                        ? $"{baseUrl}{p.Value<IPublishedContent>("productImage").Url()}"
                        : null
                })
                .ToList();
            if (products == null || !products.Any())
            {
                return NotFound($"No products found in category '{category}'.");
            }
            return Ok(products);
        }
        [HttpGet("product/{id:int}")]
        public IActionResult GetProductById(int id)
        {
            var umbracoContext = _umbracoContextAccessor.GetRequiredUmbracoContext();
            var request = HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";
            var product = umbracoContext.Content!.GetById(id);
            if (product == null || product.ContentType.Alias != "productPage")
            {
                return NotFound($"Product with ID '{id}' not found.");
            }
            var productDetails = new
            {
                Id = product.Id,
                ProductName = product.Value<string>("productsName"),
                Category = product.Value<string>("category"),
                Description = product.Value<string>("description"),
                Price = product.Value<int>("price"),
                CardDescription= product.Value<string>("cardDescription"),
                ImageUrl = product.Value<IPublishedContent>("productImage") != null
                    ? $"{baseUrl}{product.Value<IPublishedContent>("productImage").Url()}"
                    : null
            };
            return Ok(productDetails);
        }
    }
}
