using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerProvider customerProvider;

        public CustomerController(ICustomerProvider customerProvider)
        {
            this.customerProvider = customerProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var response = await customerProvider.GetCustomersAsync();
            if (response.IsSuccess)
            {
                return Ok(response.customers);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var response = await customerProvider.GetCustomerAsync(id);
            if (response.IsSuccess)
            {
                return Ok(response.customer);
            }
            return NotFound();
        }
    }
}
