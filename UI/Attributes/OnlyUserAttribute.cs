using BusinessEntities;
using BusinessLogic;
using Microsoft.AspNetCore.Components;
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
            /// <summary>
            /// Verify if its a user or admin (Obviously not the way to authorize but its quick)
            /// </summary>
            /// <param name="filterContext"></param>
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                string user = filterContext.HttpContext.Request.Cookies["user"];
                if (!string.IsNullOrEmpty(user))
                {
                }
                else
                {
                    filterContext.Result = new RedirectResult("/Home/Unauthorized");
                }
            }
        }
    }
}
