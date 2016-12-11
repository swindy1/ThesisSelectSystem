using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThesisSelectSystem.DAL;
using ThesisSelectSystem.DAL.MyHelp;

namespace ThesisSelectSystem.Controllers
{
    public class SystemAdminController : Controller
    {
        //
        // GET: /SystemAdmin/
        public ActionResult Index()
        {
            string account = (string)Session["Account"];
            ViewBag.data = account;
            return View();
        }

        /// <summary>
        /// 初始化系统管理员界面信息
        /// </summary>
        /// <returns></returns>
        public ActionResult SystemAdministrator()
        {
            List<string> majorNames = new MajorTableHelper().GetMajorName();
            ViewBag.Data = majorNames;

            List<string> classnames=new ClassesTableHelper().ListClassName();
            ViewBag.ClassName = classnames;

            List<string> departList = new DepartmentTableHelper().ListDepartmentName();
            ViewBag.departments = departList;

            return View();
        }



        /// <summary>
        /// 管理员添加专业
        /// </summary>
        /// <param name="major"></param>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddMajor(string major,string department)
        {
            var  helper=new MajorTableHelper();
            if (helper.IsNoExistSameMajorName(major))
            {
                helper.AddMajor(major, department);
                return Json(new {name=major,depart=department});
            }
            else
            {
                return Json(new { message = "添加新专业失败" });
            }
            
        }


        /// <summary>
        /// 删除专业
        /// </summary>
        /// <returns></returns>
        public ActionResult DelMajor()
        {
            var message = Request["test"];
            string[] majornames = message.Split(',');
            var  helper=new MajorTableHelper();
            int affectLine= helper.DelMajor(majornames);
            if (affectLine>0)
            {
                return Json(new { message = "success" });
            }
            else
            {
                return Json(new { message = "fail" });
            }
            
        }



        /// <summary>
        /// 添加一个新系/二级学院
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddDepartment(string text)
        {
            string message=new DepartmentTableHelper().AddDepartment(text);
            return Json(new {result = message});
        }


        public ActionResult UploadTeacherTable()
        {

            return View();
        }

        public ActionResult ReceiveFile()
        {
            HttpPostedFileBase file = Request.Files["files"];
            string filename = Path.GetFileName(file.FileName);
            string fileExtensionName = Path.GetExtension(file.FileName);
            if (fileExtensionName == ".xls" || fileExtensionName == ".xlsx" || fileExtensionName == ".xlsm")
            {
                try
                {
                    string path = "/ExcelFiles/" + Guid.NewGuid() + filename;
                    string savePath = Server.MapPath(path);
                    file.SaveAs(savePath);
                }
                catch (Exception)
                {
                    Console.WriteLine("出现异常");
                    throw;
                }
                
                return Json(new {tip = "上传文件成功"});
            }
            else
            {
                string errorMessage = "请不要上传excel以外的文件";
                return Json(new {tip = errorMessage});
            }

        }


    }
}