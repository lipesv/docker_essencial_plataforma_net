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

        [HttpGet]
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
                return BadRequest($"Ocorreum erro ao executar a operação: {ex.StackTrace}");
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
                return BadRequest($"Ocorreum erro ao executar a operação: {ex.StackTrace}");
            }
        }
    }
}