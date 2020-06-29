﻿using ECommerce.Api.Customers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Interfaces
{
    public interface ICustomerProvider
    {
        Task<(bool IsSuccess, IEnumerable<Customer> customers, string ErrorMessage)> GetCustomersAsync();
        Task<(bool IsSuccess, Customer customer, string ErrorMessage)> GetCustomerAsync(int id);
    }
}
