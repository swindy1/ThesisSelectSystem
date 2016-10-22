using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThesisSelectSystem.DAL;
using ThesisSelectSystem.Models;

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


        /// <summary>
        /// 测试创建班级
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ActionResult CreateClasses()
        {
            
            return View();
            
        }


        [HttpPost]
        public ActionResult JsonClasses(ClassesModels classes)
        {
            classes.ClassName = "14软件3班";
            classes.MajorId = 1;
            return Json(classes);
        }

        /// <summary>
        /// 查询班级信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowClassInfo()
        {
            if (Request.IsAjaxRequest())
            {
                List<ClassesModels> ClassesInfo = new List<ClassesModels>();
                string sqlContent = "select ClassID,ClassName,MajorID,HumanNumber from classes";
                DataTable dt = SqlHelper.ExecuteDataTable(sqlContent);
                int id;
                int majorid;
                int number;
                string name;
                int col = dt.Columns.Count;

                foreach (DataRow row in dt.Rows)
                {
                    int i = 0;
                    id =(int) row[i++];
                    name = (string) row[i++];
                    majorid = (int) row[i++];
                    number = (int) row[i];
                    ClassesInfo.Add(new ClassesModels(id,name,number,majorid));
                }

                return Json(ClassesInfo, JsonRequestBehavior.AllowGet);
            }

            //
            return View();
        }


    }
}