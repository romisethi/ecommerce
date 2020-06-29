using AutoMapper;
using ECommerce.Api.Orders.Db;
using ECommerce.Api.Orders.Interfaces;
using ECommerce.Api.Orders.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Providers
{
    public class OrderProvider : IOrderProvider
    {
        private readonly OrderDbContext dbContext;
        private readonly ILogger<OrderProvider> logger;
        private readonly IMapper mapper;

        public OrderProvider(OrderDbContext dbContext, ILogger<OrderProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }
        public async Task<(bool IsSuccess, IEnumerable<Models.Order>, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var order = await dbContext.Orders.Where(x => x.CustomerId == customerId).ToListAsync();
                if (order != null)
                {
                    var result = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(order);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }

        }

        private void SeedData()
        {
            if (!dbContext.Orders.Any() || dbContext.Orders.Any(x =>x.Items == null))
            {

                var order1 = new Db.Order()
                {
                    Id = 101,
                    CustomerId = 1,
                    Items = new List<Db.OrderItem>()
                    {
                        new Db.OrderItem(){ Id = 1001, OrderId = 101, ProductId = 1, Quantity = 2, UnitPrice = 35.45 },
                        new Db.OrderItem() { Id = 1002, OrderId = 101, ProductId = 4, Quantity = 2, UnitPrice = 35.45 }
                    },
                    OrderDate = DateTime.Now,
                    Total = 70.9
                };

                var order2 = new Db.Order()
                {
                    Id = 102,
                    CustomerId = 2,
                    Items = new List<Db.OrderItem>()
                    {
                        new Db.OrderItem(){   Id = 1003, OrderId = 102, ProductId = 2, Quantity = 1, UnitPrice = 56  },
                        new Db.OrderItem() {  Id = 1004, OrderId = 102, ProductId = 3, Quantity = 3, UnitPrice = 452 }
                    },
                    OrderDate = DateTime.Now,
                    Total = 508
                };

                var order3 = new Db.Order()
                {
                    Id = 103,
                    CustomerId = 1,
                    Items = new List<Db.OrderItem>()
                    {
                        new Db.OrderItem(){  Id = 1005, OrderId = 103, ProductId = 1, Quantity = 2, UnitPrice = 35.45 },
                        new Db.OrderItem() { Id = 1006, OrderId = 103, ProductId = 4, Quantity = 2, UnitPrice = 123  }
                    },
                    OrderDate = DateTime.Now,
                    Total = 158.45
                };

                dbContext.Orders.Add(order1);
                dbContext.Orders.Add(order2);
                dbContext.Orders.Add(order3);
                dbContext.SaveChanges();
                //var Items1 = new List<Db.OrderItem> {
                //        new Db.OrderItem { Id = 1001, OrderId = 101, ProductId = 1, Quantity = 2, UnitPrice = 35.45 }
                //       ,new Db.OrderItem { Id = 1002, OrderId = 101, ProductId = 4, Quantity = 2, UnitPrice = 35.45 } };

                //dbContext.OrderItems.AddRange(Items1);

                //var Items2 = new List<Db.OrderItem> {
                //        new Db.OrderItem { Id = 1003, OrderId = 102, ProductId = 2, Quantity = 1, UnitPrice = 56 }
                //       ,new Db.OrderItem { Id = 1004, OrderId = 102, ProductId = 3, Quantity = 3, UnitPrice = 452 } };

                //dbContext.OrderItems.AddRange(Items2);

                //var Items3 = new List<Db.OrderItem> {
                //        new Db.OrderItem { Id = 1005, OrderId = 103, ProductId = 1, Quantity = 2, UnitPrice = 35.45 }
                //       ,new Db.OrderItem { Id = 1006, OrderId = 103, ProductId = 4, Quantity = 2, UnitPrice = 123 } };

                //dbContext.OrderItems.AddRange(Items3);

                //dbContext.Orders.Add(new Db.Order
                //{
                //    Id = 101,
                //    CustomerId = 1,
                //    Items = Items1,
                //    OrderDate = DateTime.Now,
                //    Total = 70.9
                //}); ;


                //dbContext.Orders.Add(new Db.Order
                //{
                //    Id = 102,
                //    CustomerId = 1,
                //    Items = Items2,
                //    OrderDate = DateTime.Now,
                //    Total = 508
                //});

                //dbContext.Orders.Add(new Db.Order
                //{
                //    Id = 103,
                //    CustomerId = 3,
                //    Items = Items3,
                //    OrderDate = DateTime.Now,
                //    Total = 158.45
                //});
            }
        }

        //public async Task<(bool IsSuccess, IEnumerable<Models.Order>, string ErrorMessage)> GetOrders()
        //{
        //    try
        //    {
        //        var orders = await dbContext.Orders.ToListAsync();
        //        if (orders != null && orders.Any())
        //        {
        //            var result = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
        //            return (true, result, null);
        //        }
        //        return (false, null, "Not Found");
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.ToString());
        //        return (false, null, ex.Message);
        //    }
        //}
    }
}
