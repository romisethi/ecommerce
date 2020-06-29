using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService orderService;
        private readonly IProductsService productsService;
        private readonly ICustomerService customerService;

        public SearchService(IOrderService orderService, IProductsService productsService, ICustomerService customerService)
        {
            this.orderService = orderService;
            this.productsService = productsService;
            this.customerService = customerService;
        }
        public async Task<(bool IsSuccess, dynamic searchResult)> SearchAsync(int customerId)
        {
            var orderResult = await orderService.GetOrdersAsync(customerId);
            var productResult = await productsService.GetProductAsync();

            var customerResult = await customerService.GetCustomerAsync(customerId);


            if(orderResult.isSuccess)
            {
                foreach (var order in orderResult.orders)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productResult.IsSuccess ? productResult.products.FirstOrDefault(p => p.Id == item.ProductId)?.Name 
                            : "Product Information not available" ;
                    }
                }

                var result = new
                {
                    Customer = customerResult.IsSuccess ? 
                               customerResult.customer :
                               new { Name = "Customer service is down" },
                    Orders = orderResult.orders
                };

                return (true, result);
            }
            return (false, null);
        }
    }
}
