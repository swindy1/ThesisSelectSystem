using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThesisSelectSystem.Filter
{
    public class MyExceptionFilterAttribute:HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            
            HttpContext.Current.Response.Redirect("/Exception/Exception");
           
               
        }
    }

    public class A:ActionFilterAttribute
    {

    }
}