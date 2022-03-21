using BusinessEntities;
using BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public interface IOrderLogic
    {
        List<Order> GetAllOrders();
        List<Order> GetOngoingOrdersForCustomer(int CustomerId);
        List<Order> GetCompleteOrdersForCustomer(int CustomerId);
        List<Order> GetOngoingOrders();
        List<Order> GetCompleteOrders();
        List<CustomerOrderView> GetNumberOfCustomerOrders();
        List<CustomerOrderPriceView> GetTotalSpentOfCustomerOrders();
        List<CustomerOrderPriceView> GetAverageSpentOfCustomerOrders();
        Order AddOrder(Order Order);
        Order GetOrder(int OrderId);
        bool UpdateOrder(Order Order);
        bool DeleteOrder(int OrderId);
    }
}
