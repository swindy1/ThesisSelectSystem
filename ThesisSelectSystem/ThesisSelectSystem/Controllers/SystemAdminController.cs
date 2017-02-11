using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using ThesisSelectSystem.BLL;
using ThesisSelectSystem.DAL;
using ThesisSelectSystem.DAL.MyHelp;
using System.Text;

namespace ThesisSelectSystem.Controllers
{
    public class SystemAdminController : Controller
    {
        private string filename;
        private static string xmlPath = AppDomain.CurrentDomain.BaseDirectory + "TableMappingObj.xml";
        private static string asmPathAndName = AppDomain.CurrentDomain.BaseDirectory + @"\bin\Model.dll";


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

            SqlParameter parameter=new SqlParameter("@condition",SqlDbType.NVarChar);
            parameter.Value = "管理员";

            Dictionary<string, string> teachersName =
                new TeacherInfoQuery().QueryTeacherNameAndAccount();
            ViewBag.teacherName = teachersName.Values.ToArray();
            ViewBag.teacherID = teachersName.Keys.ToArray();
            ViewBag.teacherCount = teachersName.Values.ToArray().Length;

            TeacherInfoDetail[] teacherInfoDetails =
                new TeacherInfoQuery().QueryTeacherInfoDetails(new SqlParameter[] {parameter});
            string[] adminID = teacherInfoDetails.Select(entity => (entity.id)).ToArray();
            string[] adminName = teacherInfoDetails.Select(entity => (entity.name)).ToArray();
            string[] departmentName = teacherInfoDetails.Select(entity => (entity.departmentName)).ToArray();
            ViewBag.adminIds = adminID;
            ViewBag.adminNames = adminName;
            ViewBag.departmentNames = departmentName;
            ViewBag.adminCount = adminID.Length;
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

        /// <summary>
        ///  测试导入教师信息页面
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadTeacherTable()
        {

            return View();
        }

        
        /// <summary>
        /// 接收客户端上传的Excel文件并把Excel表格里的数据导入数据库相应的表
        /// </summary>
        /// <returns></returns>
        public ActionResult ReceiveFile()
        {
            HttpPostedFileBase file = Request.Files["files"];
            filename = Path.GetFileName(file.FileName);
            string fileExtensionName = Path.GetExtension(file.FileName);
            string serverFileName = ExcelFileBusinessOperation.CreatePrefixion()+ filename;
            string virtualPath = "/ExcelFiles/" + serverFileName;
            
            string savePath = Server.MapPath(virtualPath);
            if (fileExtensionName == ".xls" || fileExtensionName == ".xlsx" || fileExtensionName == ".xlsm")
            {
                try
                {
                    file.SaveAs(savePath);
                    DbOperation.SetXmlPath(xmlPath);
                    bool result;
                    var students = ExcelFileBusinessOperation.ExcelToList(savePath, "Sheet1", asmPathAndName, "Model.Student");
                    foreach (var student in students)
                    {
                        if (student is Model.Student)
                        {
                            DbOperation.Save(student, "student", out result);
                            User_dal.CreateNewUser(((Student) student).sno);
                        }
                    }
                }
                catch (Exception)
                {
                    
                    throw;
                } 
                return Json(new {tip = "导入学生信息成功"});
            }
            else
            {
                string errorMessage = "请不要上传excel以外的文件";
                return Json(new {tip = errorMessage});
            }

        }


        /// <summary>
        /// 导入教师信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ImportTeacherInfo()
        {
            HttpPostedFileBase file = Request.Files["teacherInfo"];
            filename = Path.GetFileName(file.FileName);
            string fileExtensionName = Path.GetExtension(file.FileName);
            string serverFileName = ExcelFileBusinessOperation.CreatePrefixion() + filename;
            string virtualPath = "/ExcelFiles/" + serverFileName; 
            string savePath = Server.MapPath(virtualPath);
            if (fileExtensionName == ".xls" || fileExtensionName == ".xlsx" || fileExtensionName == ".xlsm")
            {
                try
                {
                    file.SaveAs(savePath); 
                    List<Object> teachers = ExcelFileBusinessOperation.ExcelToList(savePath, "teacher", asmPathAndName, "Model.Teacher");
                    DbOperation.SetXmlPath(xmlPath);
                    foreach (var teacher in teachers)
                    {
                        bool result;
                        if (teacher is Teacher)
                        {
                            DbOperation.Save(teacher, "teacher", out result);
                            User_dal.CreateNewUser(((Teacher) teacher).id ,"老师");


                        }
                    }
                }
                catch (Exception)
                {
                    return Json(new { tip = "导入教师信息出错" });
                    throw;
                }
                return Json(new { tip = "成功导入教师信息" });
            }
            else
            {
                string errorMessage = "请不要上传excel以外的文件";
                return Json(new { tip = errorMessage });
            }
           
        }



        public JsonResult AddAdmin()
        {
            var teacherId = Request["id"];
            bool result;
            UserInfo user=new UserInfo();
            user.account = teacherId;
            user.roles = "管理员";
            DbOperation.SetXmlPath(xmlPath);
            result = DbOperation.Update(user,"userinfo");
            if (result)
            {
                return Json(new { tip = "成功添加管理员" });
            }
            else
            {
                return Json(new { tip = "添加管理员失败" });
            }
        }

        public ActionResult DeleteAdmin()
        {
            var adminId = Request["id"];
            
            UserInfo user=new UserInfo();
            user.account = adminId;
            user.roles = "教师";
            DbOperation.SetXmlPath(xmlPath);
            bool result = DbOperation.Update(user,"userinfo");
            if (result)
            {
                return Json(new { tip = "成功删除管理员" });
            }
            else
            {
                return Json(new { tip = "操作失败！" });
            }
            
        }
    }
}