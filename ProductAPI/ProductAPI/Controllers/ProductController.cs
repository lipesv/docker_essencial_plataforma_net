using Microsoft.AspNetCore.Mvc;
using Product.Application.Services.Interfaces;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService)); ;
        }

        [HttpGet]
        public async Task<ActionResult<Domain.Entities.Product>> GetProducts()
        {
            var products = await _productService.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        public async Task<ActionResult<Domain.Entities.Product>> GetProductById(string id)
        {
            var product = await _productService.GetProductAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [Route("[action]/{category}", Name = "GetProductByCategory")]
        [HttpGet]
        public async Task<ActionResult<Domain.Entities.Product>> GetProductByCategory(string category)
        {
            var products = await _productService.GetByCategoryAsync(category);
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<Domain.Entities.Product>> CreateProduct([FromBody] Domain.Entities.Product product)
        {
            await _productService.Create(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] Domain.Entities.Product product)
        {
            return Ok(await _productService.Update(product));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            return Ok(await _productService.Delete(id));
        }
    }
}