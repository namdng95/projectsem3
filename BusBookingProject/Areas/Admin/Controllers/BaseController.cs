using BusBookingProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BusBookingProject.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Kiểm tra Session trong Admin
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            //Nếu Session == null thì sẽ ko cho truy cập vào bên trong DashBoard và chuyển về trang Login
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Account", action = "Login", Area = "Admin" }));
            }
            base.OnActionExecuted(filterContext);
        }
    }
}