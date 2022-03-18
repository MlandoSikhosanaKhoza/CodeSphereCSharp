using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Attributes
{
    public class OnlyAdminAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Verify if its a user or admin (Obviously not the way to authorize but its quick)
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string admin = filterContext.HttpContext.Request.Cookies["admin"];
            if (!string.IsNullOrEmpty(admin))
            {
                
            }
            else
            {
                filterContext.Result = new RedirectResult("/Home/Unauthorized");
            }
        }
    }
}
