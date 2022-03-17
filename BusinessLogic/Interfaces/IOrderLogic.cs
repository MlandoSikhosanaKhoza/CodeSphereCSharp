using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public interface IOrderLogic
    {
        List<Order> GetAllOrders();
        void AddOrder(Order Order);
        Order GetOrder(int OrderId);
        bool UpdateOrder(Order Order);
        bool DeleteOrder(int OrderId);
    }
}
