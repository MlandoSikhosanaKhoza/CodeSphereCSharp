using AutoMapper;
using BusinessEntities;
using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Models;
using static UI.Attributes.OnlyUserAttribute;

namespace UI.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerLogic _customerLogic { get; set; }
        private IItemLogic _itemLogic { get; set; }
        private IOrderLogic _orderLogic { get; set; }
        private IOrderItemLogic _orderItemLogic { get; set; }
        private IMapper Mapper { get; set; }
        public CustomerController(ICustomerLogic customerLogic,IItemLogic itemLogic, IOrderLogic orderLogic, IOrderItemLogic orderItemLogic, IMapper mapper)
        {
            _customerLogic = customerLogic;
            _orderLogic = orderLogic;
            _orderItemLogic = orderItemLogic;
            _itemLogic = itemLogic;
            Mapper = mapper;
        }
        [OnlyUsers]
        public IActionResult Index()
        {
            Customer customer = _customerLogic.GetCustomerByMobileNumber(Request.Cookies["user"]);
            CustomerModel customerModel=new CustomerModel();
            OrderModel orderModel = new OrderModel();
            if (customer != null)
            {
                ViewBag.NameSurname = customer.Name + " " + customer.Surname;
                customerModel = Mapper.Map<CustomerModel>(customer);
                orderModel.CustomerId = customer.CustomerId;
            }
            orderModel.Customer = customerModel;
            orderModel.Items = _itemLogic.GetAllItems();
            orderModel.OrderReference = Guid.NewGuid();
            orderModel.DateCreated = DateTime.Now;
            
            return View("MakeAnOrder",orderModel);
        }

        public IActionResult CheckIfMobileExist(string Mobile,int CustomerId)
        {
            if (CustomerId != 0)
            {
                return Json(true);
            }
            Customer customer = _customerLogic.GetCustomerByMobileNumber(Mobile);
            return Json(customer==null);
        }

        public IActionResult CheckIfMobileDoesntExist(string Mobile)
        {
            Customer customer = _customerLogic.GetCustomerByMobileNumber(Mobile);
            return Json(customer != null);
        }

        [HttpPost]
        public IActionResult PurchaseItems(OrderModel OrderModel,CustomerModel CustomerModel,int[] ItemId,int[] Quantity,decimal[] Price)
        {
            if (ModelState.IsValid)
            {
                Customer customer = _customerLogic.ConfigureCustomer(Mapper.Map<Customer>(CustomerModel));
                ViewBag.NameSurname = customer.Name + " " + customer.Surname;
                OrderModel.CustomerId = customer.CustomerId;
                Order order = _orderLogic.AddOrder(Mapper.Map<Order>(OrderModel));
                OrderModel.Customer = CustomerModel;
                _orderItemLogic.AddOrderItems(order.OrderId, ItemId, Quantity, Price);
                OrderModel.OrderItemsView = _orderItemLogic.GetOrderViewItems(order.OrderId).Select(oiv => new OrderItemsViewModel { Description = oiv.Description, Quantity = oiv.Quantity, Price = oiv.Price }).ToList();
                Response.Cookies.Append("user", customer.Mobile);
                return View("Reciept", OrderModel);
            }
            return View("MakeAnOrder",OrderModel);
        }

        public IActionResult MyOrders()
        {
            Customer customer = _customerLogic.GetCustomerByMobileNumber(Request.Cookies["user"]);
            CustomerModel customerModel = new CustomerModel();
            CustomerHistoryModel customerHistoryModel = new CustomerHistoryModel();
            if (customer != null)
            {
                ViewBag.NameSurname = customer.Name + " " + customer.Surname;
                customerModel = Mapper.Map<CustomerModel>(customer);
                customerHistoryModel = new CustomerHistoryModel {CustomerId=customer.CustomerId, Name=customerModel.Name,Surname=customerModel.Surname,Mobile=customerModel.Mobile };
                customerHistoryModel.OngoingOrders = _orderLogic.GetOngoingOrdersForCustomer(customer.CustomerId);
                customerHistoryModel.CompleteOrders = _orderLogic.GetCompleteOrdersForCustomer(customer.CustomerId);
            }
            else
            {
                customerHistoryModel.OngoingOrders = new List<Order>();
                customerHistoryModel.CompleteOrders = new List<Order>();
            }
            return View("MyOrders",customerHistoryModel);
        }
        [HttpPost]
        public IActionResult MyOrders(CustomerModel CustomerModel)
        {
            Customer customer = _customerLogic.GetCustomerByMobileNumber(CustomerModel.Mobile);
            CustomerHistoryModel customerHistoryModel = new CustomerHistoryModel();
            if (customer != null)
            {
                CustomerModel = Mapper.Map<CustomerModel>(customer);
                customerHistoryModel = new CustomerHistoryModel { Name = CustomerModel.Name, Surname = CustomerModel.Surname, Mobile = CustomerModel.Mobile };
                customerHistoryModel.OngoingOrders = _orderLogic.GetOngoingOrdersForCustomer(customer.CustomerId);
                customerHistoryModel.CompleteOrders = _orderLogic.GetCompleteOrdersForCustomer(customer.CustomerId);
            }
            else
            {
                customerHistoryModel.OngoingOrders = new List<Order>();
                customerHistoryModel.CompleteOrders = new List<Order>();
            }
            return View("MyOrders", customerHistoryModel);
        }

        public IActionResult ShowReceipt(int id)
        {
            Order order = _orderLogic.GetOrder(id);
            Customer customer = _customerLogic.GetCustomer(order.CustomerId);
            ViewBag.NameSurname = customer.Name + " " + customer.Surname;
            List<OrderItemsViewModel> orderItems= _orderItemLogic.GetOrderViewItems(order.OrderId).Select(oiv => new OrderItemsViewModel { Description = oiv.Description, Quantity = oiv.Quantity, Price = oiv.Price }).ToList();
            OrderModel orderModel = Mapper.Map<OrderModel>(order);
            orderModel.OrderItemsView = orderItems;
            return View("Reciept",orderModel);
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("user");
            return RedirectToAction("Index", "Home");
        }
    }
}
