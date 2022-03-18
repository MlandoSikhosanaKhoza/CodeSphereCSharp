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
        private IMapper Mapper { get; set; }
        public CustomerController(ICustomerLogic customerLogic,IMapper mapper)
        {
            _customerLogic = customerLogic;
            Mapper = mapper;
        }
        [OnlyUsers]
        public IActionResult Index()
        {
            Customer customer = _customerLogic.GetCustomerByMobileNumber(Request.Cookies["user"]);
            CustomerModel customerModel=new CustomerModel();
            if (customer != null)
            {
                customerModel = Mapper.Map<CustomerModel>(customer);
            }
            return View("MakeAnOrder",customerModel);
        }
    }
}
