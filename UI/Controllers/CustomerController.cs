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
        private IMapper Mapper { get; set; }
        public CustomerController(ICustomerLogic customerLogic,IItemLogic itemLogic,IMapper mapper)
        {
            _customerLogic = customerLogic;
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
                customerModel = Mapper.Map<CustomerModel>(customer);
            }
            orderModel.Customer = customerModel;
            orderModel.Items = _itemLogic.GetAllItems();
            orderModel.OrderReference = Guid.NewGuid();
            
            return View("MakeAnOrder",orderModel);
        }
        [HttpPost]
        public IActionResult PurchaseItems(OrderModel OrderModel,CustomerModel CustomerModel,int[] ItemId,int[] Quantity,decimal[] Price)
        {
            if (ModelState.IsValid)
            {

            }
            return View("MakeAnOrder",OrderModel);
        }
    }
}
