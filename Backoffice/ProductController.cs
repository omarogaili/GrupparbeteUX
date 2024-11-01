using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.PublishedModels;
using System.Linq;

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
                    Logo = x.Value<string>("logo"),
                    HandlerName = x.Value<string>("handlerName"),
                    Copywriter = x.Value<string>("copywriter"),
                    ProductName = x.Value<string>("productName"),
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
    }
}
