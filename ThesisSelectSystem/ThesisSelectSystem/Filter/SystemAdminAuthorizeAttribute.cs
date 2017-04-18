using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace ThesisSelectSystem.Filter
{

    public class SystemAdminAuthorizeAttribute:AuthorizeAttribute
    {
        
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            HttpSessionStateBase session = httpContext.Session;//获取从http传过来的session,是一个对象
            string a = session[0].ToString();//有两种获取session的方法，一种是index、一种是name
            string b = session[1].ToString();
            //System.Diagnostics.Debug.WriteLine(a); //调试时输出到控制台
            if (session["role"] =="1" )
            {

                return true;
            }
            else
                return false;
            //return base.AuthorizeCore(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //base.HandleUnauthorizedRequest(filterContext);
            filterContext.HttpContext.Response.Redirect("Login/SALogin");
        }
        
    }
}