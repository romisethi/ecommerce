using Ecommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Products.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductProvider productProvider;

        public ProductsController(IProductProvider productProvider)
        {
            this.productProvider = productProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var result = await productProvider.GetProductsAsync();
            if(result.IsSuccess)
            {
                return Ok(result.products);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var result = await productProvider.GetProductAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.product);
            }
            return NotFound();
        }
    }
}
