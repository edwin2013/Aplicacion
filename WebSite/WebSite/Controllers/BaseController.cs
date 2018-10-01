using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebSite.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool noExisteSesion = Session["usuario"] == null;
            if (noExisteSesion)
            {
                filterContext.Result = new RedirectResult(Url.Action("Login", "Login"));
            }
        }
    }
}