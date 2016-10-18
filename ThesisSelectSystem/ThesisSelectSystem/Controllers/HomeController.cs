using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThesisSelectSystem.Controllers
{    

    //用于测试的控制器
    public class HomeController : Controller
    {
       
        /// <summary>
        /// 返回hashpassword与salt
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //return View();
            string A = Guid.NewGuid().ToString();
            string psw = "1234";
            byte[] B = System.Text.Encoding.UTF8.GetBytes(psw + A);
            byte[] hashBytes = new System.Security.Cryptography.SHA256Managed().ComputeHash(B);
            string hashPassword = Convert.ToBase64String(hashBytes);

            Response.Write(hashPassword + "&nbsp");
            Response.Write(A);
            return null;
        }

	}
}