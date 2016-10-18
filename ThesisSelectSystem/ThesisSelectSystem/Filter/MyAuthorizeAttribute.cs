using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThesisSelectSystem.Filter
{
    public class MyAuthorizeAttribute:AuthorizeAttribute
    {
        //授权验证的逻辑处理，返回true的则是通过授权，返回了false则不是
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return base.AuthorizeCore(httpContext);
        }


        //处理授权失败
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}