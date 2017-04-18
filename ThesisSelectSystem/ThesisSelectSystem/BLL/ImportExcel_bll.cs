using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using ThesisSelectSystem.DAL;

namespace ThesisSelectSystem.BLL
{
    public class ImportExcel_bll
    {
        
       /// <summary>
       ///  传递参数给SqlHelper.ImportExcelDataToSql
       /// </summary>
        /// <param name="realPath">文件在服务器上的路径</param>
        /// <param name="sheetName">工作表名</param>
        /// <param name="tableName">数据库表名</param>
       /// <returns></returns>
        public int ExcelToSql(string realPath, string sheetName,string tableName)
        {
            
            return SqlHelper.ImportExcelDataToSql(realPath, sheetName, tableName);
        }


        /// <summary>
        /// 获取工作表sheet的名称,将其保存为一个List
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<string> GetSheetName(string realPath)
        {
            List<string> SheetName = new List<string>();

            OleDbConnection olecon = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + realPath + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'");//连接字符串
            olecon.Open();
            DataTable DTable = olecon.GetSchema("Tables");
            DataTableReader DTReader = new DataTableReader(DTable);
            while (DTReader.Read())
            {
                string sName = DTReader["Table_Name"].ToString().Replace('$', ' ').Trim();//得到当前sheetName
                if (!SheetName.Contains(sName))//集合中不包含此条工作表名
                    SheetName.Add(sName);//添加到集合

            }

            DTable = null;//清空
            DTReader = null;//清空
            olecon.Close();
            return SheetName;

        }

    }


    
}