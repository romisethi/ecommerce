using Ecommerce.Api.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Products.Interfaces
{
    public interface IProductProvider
    {
        Task<(bool IsSuccess, IEnumerable<Product> products, string ErrorMessage)> GetProductsAsync();
        Task<(bool IsSuccess, Product product, string ErrorMessage)> GetProductAsync(int id);

    }
}
