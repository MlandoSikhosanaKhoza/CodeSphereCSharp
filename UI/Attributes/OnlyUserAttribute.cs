using BusinessEntities;
using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Attributes
{
    public class OnlyUserAttribute
    {
        public class OnlyUsersAttribute : ActionFilterAttribute
        {
            private ICustomerLogic _customerLogic { get; set; }
            public OnlyUsersAttribute(ICustomerLogic customerLogic)
            {
                _customerLogic = customerLogic;
            }
            /// <summary>
            /// Verify if its a user or admin (Obviously not the way to authorize but its quick)
            /// </summary>
            /// <param name="filterContext"></param>
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                string user = filterContext.HttpContext.Request.Cookies["user"];
                if (!string.IsNullOrEmpty(user))
                {
                    switch (user)
                    {
                        case "new-customer":
                            //user has not entered a cellphone number yet

                            break;
                        default:
                            //user has entered a cellphone number
                            Customer customer = _customerLogic.GetCustomerByMobileNumber(user);
                            filterContext.HttpContext.Items.Add("Customer", customer);
                            break;
                    }
                }
                else
                {
                    filterContext.Result = new RedirectResult("/Home/Unauthorized");
                }
            }
        }
    }
}
