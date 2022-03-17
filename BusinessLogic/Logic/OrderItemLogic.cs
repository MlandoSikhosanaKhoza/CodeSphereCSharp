using BusinessEntities;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
