using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThesisSelectSystem.DAL;
using ThesisSelectSystem.DAL.MyHelp;
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
        /// 测试创建班级,接收get请求
        /// </summary>
        /// <param name=""></param>
        /// <returns>返回一个添加班级的表单</returns>
        public ActionResult CreateClasses()
        {
            
            return View();
            
        }


        /// <summary>
        /// 接收post请求，对表单提交过来的添加班级信息进行验证，并保存到数据库
        /// </summary>
        /// <param name="classes"></param>
        /// <returns>操作成功后返回到班级信息页面</returns>
        [HttpPost]
        public ActionResult JsonClasses(ClassesModels classes)
        {
            var res = new ClassesTableHelper().CheckClassNameLegal(classes);

            #region 通过验证的数据插入数据库
            if (res==0)
            {
                List<SqlParameter> insertParameters = new List<SqlParameter>();

                var cName= new SqlParameter("@ClassName",SqlDbType.NVarChar);
                cName.Value = classes.ClassName.Trim();
                insertParameters.Add(cName);

                var number = new SqlParameter("@Number", SqlDbType.Int);
                number.Value = classes.HumanNumber;
                insertParameters.Add(number);

                var majorid= new SqlParameter("@MajorID",SqlDbType.Int);
                majorid.Value = classes.MajorId;
                insertParameters.Add(majorid);

                int isSuccess = SqlHelper.ExecuteNonqueryProc("CreateClasses", insertParameters.ToArray());
                if (isSuccess>0)
                {
                    classes.ClassName = "Success";
                    return Json(classes);
                }
                else
                {
                    classes.ClassName = "Fail";
                    return Json(classes);
                }

            }
            else
            {
                
                return Json("该班级已经存在");
            }
            #endregion
        }


        /// <summary>
        /// 查询班级信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowClassInfo()
        {
            if (Request.IsAjaxRequest())
            {
                #region 初始化sql语句及用于临时接收实体属性的变量
                List<ClassesModels> ClassesInfo = new List<ClassesModels>();
                string sqlContent = "select ClassID,ClassName,MajorID,HumanNumber, GraduateYear from classes";
                DataTable dt = SqlHelper.ExecuteDataTable(sqlContent);
                int id;
                int majorid;
                int number;
                int? year;
                string name;
                int col = dt.Columns.Count;
                #endregion
                #region 把查询结果添加到集合里
                foreach (DataRow row in dt.Rows)
                {
                    int i = 0;
                    id =(int) row[i++];
                    name = (string) row[i++];
                    majorid = (int) row[i++];
                    number = (int) row[i++];
                    year = (int?) row[i];
                    ClassesInfo.Add(new ClassesModels(id,name,number,majorid,year));
                }
                #endregion
                return Json(ClassesInfo, JsonRequestBehavior.AllowGet);
            }
            
            return View();
        }


    }
}