using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Interfaces
{
    public interface IOrderProvider
    {
        Task<(bool IsSuccess, IEnumerable<Models.Order>, string ErrorMessage)> GetOrdersAsync(int customerId);
     //   Task<(bool IsSuccess, Models.Order, string ErrorMessage)> GetOrder(int id);

    }
}
