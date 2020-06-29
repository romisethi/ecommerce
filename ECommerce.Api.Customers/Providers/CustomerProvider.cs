using AutoMapper;
using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Interfaces;
using ECommerce.Api.Customers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Providers
{
    public class CustomerProvider : ICustomerProvider
    {
        private readonly CustomersDbContext dbContext;
        private readonly ILogger<CustomerProvider> logger;
        private readonly IMapper mapper;

        public CustomerProvider(CustomersDbContext dbContext, ILogger<CustomerProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if(!dbContext.Customers.Any())
            {
                dbContext.Customers.Add(new Db.Customer { Id = 1, Name = "Rohit", Address = "Mohali" });
                dbContext.Customers.Add(new Db.Customer { Id = 2, Name = "Solemon", Address = "Pune" });
                dbContext.Customers.Add(new Db.Customer { Id = 3, Name = "Kanodia", Address = "Noida" });
                dbContext.Customers.Add(new Db.Customer { Id = 4, Name = "Parmesh", Address = "Mumbai" });
                dbContext.Customers.Add(new Db.Customer { Id = 5, Name = "Chetan", Address = "Gandi Nagar" });

                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, Models.Customer customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var customer = await dbContext.Customers.FirstOrDefaultAsync(x =>x.Id == id);
                if (customer != null)
                {
                    var result = mapper.Map<Db.Customer, Models.Customer>(customer);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (true, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Customer> customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                var customers = await dbContext.Customers.ToListAsync();
                if(customers != null && customers.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(customers);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (true, null, ex.Message);
            }
        }
    }
}
