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
        // public IActionResult GetProducts()
        // {
        //     var umbracoContext = _umbracoContextAccessor.GetRequiredUmbracoContext();

        //     // Hämta alla produkter som är barn till 'productHandler'
        //     var productHandlers = umbracoContext.Content!.GetAtRoot()
        //         .Where(x => x.ContentType.Alias == "productHandler")
        //         .SelectMany(x => x.Children) // Hämta barnen till varje 'productHandler'
        //         .Where(x => x.ContentType.Alias == "productPage") // Filtrera på barn av typen 'productPage'
        //         .Select(x => new
        //         {
        //             Id = x.Id,
        //             Logo = x.Value<string>("logo"),
        //             HandlerName = x.Value<string>("handlerName"),
        //             Copywriter = x.Value<decimal?>("copywriter"),
        //             Description = x.Value<string>("description"),
        //             Price = x.Value<decimal>("price"),
        //             productName= x.Value<string>("productsName"),
        //             ImageUrl = x.Value<IPublishedContent>("productImage")?.Url() 
        //             // ImageContent = x.Value<IPublishedContent>("productImage") // Ingen 'var' här
        //         })
        //         .ToList();

        //     if (!productHandlers.Any())
        //     {
        //         return NotFound();
        //     }
        //     return Ok(productHandlers);
        // }
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
    }
}
