using AutoMapper;
using ECommerce.Api.Orders.Db;
using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.NewFolder
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderProvider orderProvider;

        public OrderController(IOrderProvider orderProvider)
        {
            this.orderProvider = orderProvider;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrdersAsync(int customerId)
        {
            var result = await orderProvider.GetOrdersAsync(customerId);
            if(result.IsSuccess)
            {
                return Ok(result.Item2);
            }
            return NoContent();
        }
    }
}
