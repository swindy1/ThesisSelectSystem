using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThesisSelectSystem.Filter;
using ThesisSelectSystem.BLL;

namespace ThesisSelectSystem.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/


        /// <summary>
        /// 学生与教师登陆界面，Student&Teacher
        /// </summary>
        /// <returns></returns>
        
        public ActionResult STLogin()
        {
            throw new Exception("123");
            return View();
        }


        /// <summary>
        /// 管理员与审题员登陆界面，Admin&Inpector
        /// </summary>
        /// <returns></returns>
        public ActionResult AILogin()
        {
            return View();
            
           
        }


        /// <summary>
        /// 系统管理员登陆界面,SystemAdmin
        /// </summary>
        /// <returns></returns>
        public ActionResult SALogin()
        {   

            return View();
        }



        
        public ActionResult SystemAdminMakeLogin()
        {   
            //创建session对象
            Session["Account"] = null;
            Session["role"] = null;

            //接受登陆信息
            string Account = Request["Account"];
            string Password = Request["Password"];

            UserLogin_bll user = new UserLogin_bll();
            bool login = user.LoginYes(Account, Password);
            if (login)
            {
                Session["Account"] = Account;
                Session["role"] = "1";
                return Content("1");
            }
            else
            {
                return Content("0");
            }
            
        }

	}
}