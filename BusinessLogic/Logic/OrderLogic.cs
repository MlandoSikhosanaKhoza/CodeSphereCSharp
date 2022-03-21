using BusinessEntities;
using BusinessEntities.Models;
using DAL;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace BusinessLogic
{
    public class OrderLogic : IOrderLogic
    {
        private GenericRepository<Order> OrderRepository { get; set; }
        private GenericRepository<Customer> CustomerRepository { get; set; }
        private GenericRepository<Employee> EmployeeRepository { get; set; }
        private IUnitOfWork _unitOfWork;
        public OrderLogic(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
            OrderRepository = UnitOfWork.GetRepository<Order>();
            CustomerRepository = UnitOfWork.GetRepository<Customer>();
            EmployeeRepository = UnitOfWork.GetRepository<Employee>();
        }

        public List<Order> GetAllOrders()
        {
            return (List<Order>)OrderRepository.All();
        }

        public List<Order> GetOngoingOrdersForCustomer(int CustomerId)
        {
            return OrderRepository.GetAll().Where(o => o.CustomerId == CustomerId && o.DateFulfilled == null && o.EmployeeId == null).Select(x => new Order
            {
                OrderId = x.OrderId,
                CustomerId = x.CustomerId,
                EmployeeId = x.EmployeeId,
                Employee = x.Employee,
                DateFulfilled = x.DateFulfilled,
                DateCreated = x.DateCreated,
                OrderReference = x.OrderReference,
                VAT = x.VAT,
                Subtotal = x.Subtotal,
                GrandTotal = x.GrandTotal
            }).ToList();
        }

        public List<Order> GetCompleteOrdersForCustomer(int CustomerId)
        {
            return OrderRepository.GetAll().Where(o => o.CustomerId == CustomerId && o.DateFulfilled != null && o.EmployeeId != null).Select(x=>new Order{ 
                OrderId=x.OrderId,
                CustomerId=x.CustomerId,
                EmployeeId=x.EmployeeId,
                Employee=x.Employee,
                DateFulfilled=x.DateFulfilled,
                DateCreated=x.DateCreated,
                OrderReference=x.OrderReference,
                VAT=x.VAT,
                Subtotal=x.Subtotal,
                GrandTotal=x.GrandTotal
            }).ToList();
        }

        public List<Order> GetOngoingOrders()
        {
            return OrderRepository.GetAll().Where(o => o.DateFulfilled == null && o.EmployeeId == null).Select(x => new Order
            {
                OrderId = x.OrderId,
                CustomerId = x.CustomerId,
                Customer=x.Customer,
                EmployeeId = x.EmployeeId,
                Employee = x.Employee,
                DateFulfilled = x.DateFulfilled,
                DateCreated = x.DateCreated,
                OrderReference = x.OrderReference,
                VAT = x.VAT,
                Subtotal = x.Subtotal,
                GrandTotal = x.GrandTotal
            }).ToList();
        }

        public List<Order> GetCompleteOrders()
        {
            return OrderRepository.GetAll().Where(o => o.DateFulfilled != null && o.EmployeeId != null).Select(x => new Order
            {
                OrderId = x.OrderId,
                CustomerId = x.CustomerId,
                Customer = x.Customer,
                EmployeeId = x.EmployeeId,
                Employee = x.Employee,
                DateFulfilled = x.DateFulfilled,
                DateCreated = x.DateCreated,
                OrderReference = x.OrderReference,
                VAT = x.VAT,
                Subtotal = x.Subtotal,
                GrandTotal = x.GrandTotal
            }).ToList();
        }

        public List<CustomerOrderView> GetNumberOfCustomerOrders()
        {
            List<CustomerOrderView> customerOrderViews = new List<CustomerOrderView>();
            var customerOrderQuery = from c in CustomerRepository.GetAll()
                                     join o in (OrderRepository.GetAll().GroupBy(or => or.CustomerId).Select(n=>new { n.Key,Count=n.Count() }))
                                     on c.CustomerId equals o.Key
                                     select new CustomerOrderView
                                     {
                                         CustomerId=c.CustomerId,
                                         Name=c.Name,
                                         Surname=c.Surname,
                                         Mobile=c.Mobile,
                                         NumOfOrders=o.Count
                                     };
            customerOrderViews = customerOrderQuery.ToList();
            return customerOrderViews;
        }

        public List<CustomerOrderPriceView> GetTotalSpentOfCustomerOrders()
        {
            //var totalSpentQuery = OrderRepository.GetAll().GroupBy(o => o.CustomerId).Select(
            //    o => new { CustomerId = o.Key, Price = o.Sum(o => o.GrandTotal) });
            var customerOrderQuery = from c in CustomerRepository.GetAll()
                                     join o in (OrderRepository.GetAll().GroupBy(o => o.CustomerId).Select(
                o => new { CustomerId = o.Key, Price = o.Sum(o => o.GrandTotal) }))
                                     on c.CustomerId equals o.CustomerId
                                     select new CustomerOrderPriceView
                                     {
                                         CustomerId = c.CustomerId,
                                         Name = c.Name,
                                         Surname = c.Surname,
                                         Mobile = c.Mobile,
                                         Price = o.Price
                                     };
            return customerOrderQuery.ToList();
        }

        public List<CustomerOrderPriceView> GetAverageSpentOfCustomerOrders()
        {
            //var totalSpentQuery = OrderRepository.GetAll().GroupBy(o => o.CustomerId).Select(
            //    o => new { CustomerId = o.Key, Price = o.Sum(o => o.GrandTotal) });
            var customerOrderQuery = from c in CustomerRepository.GetAll()
                                     join o in (OrderRepository.GetAll().GroupBy(o => o.CustomerId).Select(
                o => new { CustomerId = o.Key, Price = o.Average(o => o.GrandTotal) }))
                                     on c.CustomerId equals o.CustomerId
                                     select new CustomerOrderPriceView
                                     {
                                         CustomerId = c.CustomerId,
                                         Name = c.Name,
                                         Surname = c.Surname,
                                         Mobile = c.Mobile,
                                         Price = o.Price
                                     };
            return customerOrderQuery.ToList();
        }

        public Order AddOrder(Order Order)
        {
            Order OrderAdded = OrderRepository.Add(Order);
            _unitOfWork.CompleteAsync();
            return OrderAdded;
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
