using AutoMapper;
using BusinessEntities;
using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ICustomerLogic _customerLogic { get; set; }
        public IMapper Mapper { get; set; }

        public HomeController(ICustomerLogic customerLogic, ILogger<HomeController> logger, IMapper mapper)
        {
            _logger = logger;
            Mapper = mapper;
            _customerLogic = customerLogic;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult LoginAsUser()
        {
            string user = Request.Cookies["user"];
            if (string.IsNullOrEmpty(user))
            {
                Response.Cookies.Append("user", "new-customer");
            }
            return RedirectToAction("Index", "Customer");
        }

        [HttpPost]
        public IActionResult Login(CustomerLoginModel CustomerLoginModel)
        {
            Customer customer = _customerLogic.GetCustomerByMobileNumber(CustomerLoginModel.Mobile);
            Response.Cookies.Append("user", customer.Mobile);
            return RedirectToAction("LoginAsUser");
        }

        [HttpPost]
        public IActionResult SignUp(CustomerModel CustomerModel)
        {
            Customer customer = _customerLogic.AddCustomer(Mapper.Map<Customer>(CustomerModel));
            Response.Cookies.Append("user", customer.Mobile);
            return RedirectToAction("LoginAsUser");
        }

        public IActionResult LoginAsAdmin()
        {
            string user = Request.Cookies["admin"];
            if (string.IsNullOrEmpty(user))
            {
                Response.Cookies.Append("admin", "temporary");
            }
            return RedirectToAction("Index", "Item");
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Unauthorized()
        {
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            ViewBag.Status = (int)HttpStatusCode.Unauthorized;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
