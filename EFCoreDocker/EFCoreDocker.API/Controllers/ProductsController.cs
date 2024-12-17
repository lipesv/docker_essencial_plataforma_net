using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EFCoreDocker.Application.Services.Interfaces;
using EFCoreDocker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreDocker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("IEnumerable<TModel>")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _productService.GetAll());
        }

        [HttpPost]
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
                return BadRequest($"Ocorreum erro ao executar a operação: {ex.StackTrace}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var actual = await _productService.GetById(id);

                if (actual == null)
                    return NotFound();

                await _productService.Update(product);

                return Ok(product);
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Ocorreum erro ao executar a operação: {ex.StackTrace}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = await _productService.GetById(id);

                if (product == null)
                    return NotFound();

                await _productService.Delete(id);

                return Ok("Product deleted");
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Ocorreum erro ao executar a operação: {ex.StackTrace}");
            }
        }
    }
}