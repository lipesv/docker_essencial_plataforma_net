using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Services.Interfaces;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            // TODO: Your code here
            var products = await _productService.GetAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetById(id);

            if (product == null)
                return NotFound();
            else
                return Ok(product);
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _productService.Create(product);

                return Ok(product);
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Falha ao executar operação de inclusão: {ex.StackTrace}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _productService.Update(product);

                if (result == null)
                    return NotFound();

                return Ok(product);

            }
            catch (System.Exception ex)
            {
                return BadRequest($"Falha ao executar operação de atualização: {ex.StackTrace}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _productService.Delete(id);

                if (result)
                    return Ok("Product deleted");
                else
                    return NotFound();
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Falha ao executar operação de exclusão: {ex.StackTrace}");
            }
        }
    }
}