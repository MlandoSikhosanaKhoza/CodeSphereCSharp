using BusinessEntities;
using BusinessEntities.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BusinessLogic
{
    public class OrderItemLogic:IOrderItemLogic
    {
        private GenericRepository<OrderItem> OrderItemRepository { get; set; }
        private IUnitOfWork _unitOfWork;
        public OrderItemLogic(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
            OrderItemRepository = UnitOfWork.GetRepository<OrderItem>();
        }

        public List<OrderItem> GetAllOrderItems()
        {
            return (List<OrderItem>)OrderItemRepository.All();
        }

        public List<OrderItemView> GetOrderViewItems(int OrderId)
        {
            return OrderItemRepository.GetAll().Where(oi=>oi.OrderId==OrderId).Select(oi=>new OrderItemView { 
                OrderItemId=oi.OrderItemId,
                Description=oi.Item.Description,
                Quantity=oi.Quantity,
                Price=oi.Price 
            }).ToList();
        }

        public void AddOrderItem(OrderItem OrderItem)
        {
            OrderItemRepository.Add(OrderItem);
            _unitOfWork.CompleteAsync();
        }

        public OrderItem GetOrderItem(int OrderItemId)
        {
            return OrderItemRepository.GetById(OrderItemId);
        }

        public bool UpdateOrderItem(OrderItem OrderItem)
        {
            OrderItemRepository.Update(OrderItem);
            _unitOfWork.CompleteAsync();
            return true;
        }

        public bool DeleteOrderItem(int OrderItemId)
        {
            OrderItemRepository.Delete(OrderItemId);
            _unitOfWork.CompleteAsync();
            return true;
        }

        public List<OrderItem> AddOrderItems(int OrderId,int[] ItemId, int[] Quantity, decimal[] Price)
        {
            List<OrderItem> orderItems = new List<OrderItem>();
            for (int i = 0; i < ItemId.Length; i++)
            {
                orderItems.Add(OrderItemRepository.Add(new OrderItem { OrderId = OrderId, ItemId = ItemId[i], Quantity = Quantity[i], Price = Price[i] }));
            }
            return orderItems;
        }
    }
}
