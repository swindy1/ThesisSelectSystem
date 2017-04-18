using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThesisSelectSystem.BLL;
using ThesisSelectSystem.Filter;

namespace ThesisSelectSystem.Controllers
{


    [SystemAdminAuthorizeAttribute]//角色过滤
    public class SystemAdminController : Controller
    {
        //
        // GET: /SystemAdmin/
       
        public ActionResult Index()
        {
            //if (Session["role"] == null)
            //{
            //    return RedirectToAction("SALogin", "Login");
            //}
            //else
            //{
            //    //return View();
               
            //    Response.Write("123");
            //    return null;
            //}

            return View();
        }

       
        public ActionResult test()
       {
           return View();
       }


        //上传文件视图
        public ActionResult ImportExcelFile()
        {
            return View();
        }

        //处理上传文件的方法
        public ActionResult ImportExcel()
        {

           
            HttpPostedFileBase hpf = Request.Files["excelFile"];//获取上传的文件fileDemo
            string fileName = null;
            //fileName = Request.Files["fileDemo"].FileName;
            //fileName = Request.Files[0].FileName;
            fileName = hpf.FileName;
            string str = fileName.Substring(fileName.LastIndexOf("\\") + 1);
            string path = "/ExcelFiles/" + Guid.NewGuid() + str;//文件路径,调试前请先在项目根目录下上创建一个ExcelFiles文件夹
            string realPath = Server.MapPath(path);//映射虚拟路径到服务器真实路径
            hpf.SaveAs(realPath);//保存到服务器


            ImportExcel_bll importExcel_bll = new ImportExcel_bll();
            List<string> sheetNames = importExcel_bll.GetSheetName(realPath);//获取工作表名集合
            string tableName = "Stu";//指定数据库表名
            int count = 0;//记录导入成功次数；
            for (int i = 0; i < sheetNames.Count; i++)//遍历所有工作表sheet
            {
               
                    count+=importExcel_bll.ExcelToSql(realPath, sheetNames[i], tableName);
                    
                      
            }

            if (count == sheetNames.Count)
                Response.Write("<script>alert('excel文件导入成功！')</script>");
            else
                Response.Write("<script>alert('excel文件导入失败！')</script>");
            return null;
        }

      

	}
}