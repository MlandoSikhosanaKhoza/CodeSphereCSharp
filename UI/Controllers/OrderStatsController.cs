using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Controllers
{
    public class OrderStatsController : Controller
    {
        public IOrderLogic _orderLogic { get; set; }
        public OrderStatsController(IOrderLogic orderLogic)
        {
            _orderLogic = orderLogic;
        }
        public IActionResult Index()
        {
            ViewBag.CustomerNumOrdersList = _orderLogic.GetNumberOfCustomerOrders();
            ViewBag.CustomerTotalSpentList = _orderLogic.GetTotalSpentOfCustomerOrders();
            ViewBag.CustomerAverageSpentList = _orderLogic.GetAverageSpentOfCustomerOrders();
            return View();
        }
    }
}
