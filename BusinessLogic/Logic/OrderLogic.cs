using BusinessEntities;
using DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class OrderLogic:IOrderLogic
    {
        private GenericRepository<Order> OrderRepository { get; set; }
        private IUnitOfWork _unitOfWork;
        public OrderLogic(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
            OrderRepository = UnitOfWork.GetRepository<Order>();
        }

        public List<Order> GetAllOrders()
        {
            return (List<Order>)OrderRepository.All();
        }

        public void AddOrder(Order Order)
        {
            OrderRepository.Add(Order);
            _unitOfWork.CompleteAsync();
        }

        public Order GetOrder(int OrderId)
        {
            return OrderRepository.GetById(OrderId);
        }

        public bool UpdateOrder(Order Order)
        {
            OrderRepository.Update(Order);
            _unitOfWork.CompleteAsync();
            return true;
        }

        public bool DeleteOrder(int OrderId)
        {
            OrderRepository.Delete(OrderId);
            _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
