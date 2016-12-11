using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThesisSelectSystem.Filter.HurricanFilter
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,AllowMultiple = true,Inherited = true)]
    public class SystemAdminActionFilterAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["Account"] ==null)
            {
                filterContext.Result = new RedirectResult("/Login/SALogin");
            }
            base.OnActionExecuting(filterContext);

        }
    }
}