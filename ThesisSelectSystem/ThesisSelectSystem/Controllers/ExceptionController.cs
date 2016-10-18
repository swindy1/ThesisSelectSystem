using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThesisSelectSystem.Controllers
{
    public class ExceptionController : Controller
    {
        //
        // GET: /Exception/
        //返回异常提醒页面
        public ActionResult Exception()
        {
            return View();
        }
	}
}