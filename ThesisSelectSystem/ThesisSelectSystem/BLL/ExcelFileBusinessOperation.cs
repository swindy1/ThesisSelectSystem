using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using ThesisSelectSystem.DAL;

namespace ThesisSelectSystem.BLL
{
    public class ExcelFileBusinessOperation
    {


        /// <summary>
        /// 根据当前时间生成一个"xxxx-xx-xx-xx-xx-xx"格式的时间字符串
        /// </summary>
        /// <returns></returns>
        public static string CreatePrefixion()
        {
            StringBuilder sbBuilder=new StringBuilder();
            sbBuilder.Append(DateTime.Now.Year).Append("-").Append(DateTime.Now.Month).Append("-")
                .Append(DateTime.Now.Day).Append("-").Append(DateTime.Now.Hour).Append("-")
                .Append(DateTime.Now.Minute).Append("-").Append(DateTime.Now.Second).Append("-");
            return sbBuilder.ToString();

        }



        /// <summary>
        /// 将excel文件导入数据库指定的表
        /// </summary>
        /// <param name="realPath">文件在服务器上的绝对路径</param>
        /// <param name="sheetName">工作表名</param>
        /// <param name="tableName">数据库表名</param>
        /// <returns>返回结果为1表示成功将Excel数据导入数据库</returns>
        public static int ImportExcelDataToSql(string realPath, string sheetName, string tableName)
        {
            int result = 1;
            DataSet myds = new DataSet();
            try
            {
                OleDbConnection olecon = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;" +
                                                             "Data Source=" + realPath + ";" +
                                                             "Extended Properties='Excel 8.0;" +
                                                             "HDR=Yes;IMEX=1'");
                olecon.Open();
                //查询excel表格数据的语句
                string excelSql = string.Format("select * from [{0}$]", sheetName);
                OleDbCommand oleDbCommand = new OleDbCommand(excelSql, olecon);
                OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
                oleDbDataAdapter.SelectCommand = oleDbCommand;
                oleDbDataAdapter.Fill(myds, sheetName);//填充数据
                using (SqlBulkCopy bcp = new SqlBulkCopy(SqlHelper.sqlConnectionString))
                {
                    bcp.BatchSize = 100;
                    bcp.DestinationTableName = tableName; //目标表的名称
                    bcp.WriteToServer(myds.Tables[0]);

                }
                olecon.Close();
                //Bug：Excel数据最后一行如果有填写数据后在删除数据会用法主键DbNull.null的异常
            }
            catch (Exception exception)
            {
                result -= 1;
                throw;
            }
            return result;
        }


        /// <summary>
        /// 读取Excel表格里的内容存放到一个集合里
        /// </summary>
        /// <param name="realPath">Excel文件所在的绝对路径</param>
        /// <param name="sheetName">Excel文件的工作表格名，一般位于表格的左下角</param>
        /// <returns>返回一个DataSet</returns>
        public static DataSet GetExcelData(string realPath, string sheetName)
        {
            DataSet myds = new DataSet();
            try
            {
                OleDbConnection olecon = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;" +
                                                             "Data Source=" + realPath + ";" +
                                                             "Extended Properties='Excel 8.0;" +
                                                             "HDR=Yes;IMEX=1'");
                olecon.Open();
                //查询excel表格数据的语句
                string excelSql = string.Format("select * from [{0}$]", sheetName);
                OleDbCommand oleDbCommand = new OleDbCommand(excelSql, olecon);
                OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
                oleDbDataAdapter.SelectCommand = oleDbCommand;
                oleDbDataAdapter.Fill(myds, sheetName);//填充数据
                olecon.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return myds;
        }


        /// <summary>
        /// 获取Excel表格里的数据，使用该方法需要先引用程序集Aspose.Cells.dll
        /// </summary>
        /// <param name="execlFileName">Excel文件所在的绝对路径</param>
        /// <param name="sheetName">Excel文件里的工作表名，创建Excel文件时如未指定则默认为Sheet1</param>
        /// <returns>不包括表头的DataTable，即返回Excel表格第二行开始的数据</returns>
        public static DataTable ConvertExcelDataToDataTable(string execlFileName, string sheetName)
        {
            DataTable datatable = null;
            try
            {
                if (File.Exists(execlFileName) == false)
                {

                    return null;
                }
                Workbook workbook = new Workbook(execlFileName);

                Worksheet worksheet = workbook.Worksheets[0];
                datatable = worksheet.Cells.ExportDataTable(1, 0, worksheet.Cells.MaxRow,
                    worksheet.Cells.MaxColumn);
            }
            catch (Exception)
            {
                throw;
            }
            return datatable;
        }


        /// <summary>
        /// 将Excel文件指定表格除列名外的数据转换为对象集合,使用该方法需要先引用程序集Aspose.Cells.dll
        /// </summary>
        /// <param name="execlFileName">Excel文件的绝对路径名</param>
        /// <param name="sheetName">Excel文件的工作表名称</param>
        /// <param name="assemblyPathAndName">目标类所对应的程序集全路径名</param>
        /// <param name="ClassName">目标对象的类名</param>
        /// <returns></returns>
        public static List<Object> ExcelToList(string execlFileName, string sheetName, string assemblyPathAndName,
            string ClassName)
        {
            List<Object> objList = new List<object>();
            Assembly asm = Assembly.LoadFrom(assemblyPathAndName);
            Type type = asm.GetType(ClassName, true);
            PropertyInfo[] properties = type.GetProperties();
            DataTable dataTable = ConvertExcelDataToDataTable(execlFileName, sheetName);
            int index = 0;
            try
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    var entity = asm.CreateInstance(type.FullName);
                    index = 0;
                    foreach (DataColumn dataColumn in dataTable.Columns)
                    {
                        if (!dataRow[dataColumn].GetType().Name.Equals("DBNull"))
                        {
                            properties[index].SetValue(entity, Convert.ChangeType(dataRow[dataColumn], properties[index].PropertyType));
                        }

                        index++;
                    }
                    objList.Add(entity);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return objList;
        }
    }
}