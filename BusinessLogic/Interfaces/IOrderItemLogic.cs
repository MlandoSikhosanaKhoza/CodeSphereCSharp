using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public interface IOrderItemLogic
    {
        List<OrderItem> GetAllOrderItems();
        void AddOrderItem(OrderItem OrderItem);
        OrderItem GetOrderItem(int OrderItemId);
        bool UpdateOrderItem(OrderItem OrderItem);
        bool DeleteOrderItem(int OrderItemId);
    }
}
