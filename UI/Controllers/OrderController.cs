using AutoMapper;
using BusinessEntities;
using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Models;

namespace UI.Controllers
{
    public class OrderController : Controller
    {
        private ICustomerLogic _customerLogic { get; set; }
        private IItemLogic _itemLogic { get; set; }
        private IOrderLogic _orderLogic { get; set; }
        private IOrderItemLogic _orderItemLogic { get; set; }
        private IEmployeeLogic _employeeLogic { get; set; }
        private IMapper Mapper { get; set; }

        public OrderController(ICustomerLogic customerLogic, IItemLogic itemLogic, IEmployeeLogic employeeLogic, IOrderLogic orderLogic, IOrderItemLogic orderItemLogic, IMapper mapper)
        {
            _customerLogic = customerLogic;
            _orderLogic = orderLogic;
            _orderItemLogic = orderItemLogic;
            _employeeLogic = employeeLogic;
            _itemLogic = itemLogic;
            Mapper = mapper;
        }
        public IActionResult Index()
        {
            ViewBag.OngoingOrders = _orderLogic.GetOngoingOrders();
            ViewBag.CompleteOrders = _orderLogic.GetCompleteOrders();
            return View("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Order order = _orderLogic.GetOrder((int)id);
            Customer customer = _customerLogic.GetCustomer(order.CustomerId);
            ViewBag.NameSurname = customer.Name + " " + customer.Surname;
            List<OrderItemsViewModel> orderItems = _orderItemLogic.GetOrderViewItems(order.OrderId).Select(oiv => new OrderItemsViewModel { Description = oiv.Description, Quantity = oiv.Quantity, Price = oiv.Price }).ToList();
            OrderModel orderModel = Mapper.Map<OrderModel>(order);
            orderModel.OrderItemsView = orderItems;
            ViewBag.EmployeeId = new SelectList(_employeeLogic.GetAllEmployees().Select(n=>new { Id=n.EmployeeId, FullName=n.Name+" "+n.Surname }), "Id", "FullName", order.EmployeeId);
            return View("Fulfill",orderModel);
        }
        [HttpPost]
        public IActionResult FulfillOrder(int OrderId,int EmployeeId)
        {
            Order order = _orderLogic.GetOrder(OrderId);
            order.EmployeeId = EmployeeId;
            order.DateFulfilled = DateTime.Now;
            _orderLogic.UpdateOrder(order);
            return RedirectToAction("Index");
        }
    }
}
