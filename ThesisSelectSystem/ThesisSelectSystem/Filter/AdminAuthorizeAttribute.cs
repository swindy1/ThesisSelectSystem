using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThesisSelectSystem.Filter
{
    public class AdminAuthorizeAttribute:AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            HttpSessionStateBase session = httpContext.Session;
            if (session["role"] == "2")
            {
                return true;
            }
            else
                return false;
           
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.HttpContext.Response.Redirect("Login/ALogin");
        }
    }
}