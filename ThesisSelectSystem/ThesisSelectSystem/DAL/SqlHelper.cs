using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.OleDb;

namespace ThesisSelectSystem.DAL
{
    public static class SqlHelper
    {
        public static string sqlConnectionString = SqlConnectionString.sqlConnectionString;


        /// <summary>
        /// 执行sql语句，返回受影响的行数
        /// </summary>
        /// <param name="sqlStr">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public static int ExecuteNonquery(string sqlStr,params SqlParameter[] parameters)
        {
            using(SqlConnection coon = new SqlConnection(sqlConnectionString))
            {   
                coon.Open();
                using(SqlCommand cmd = new SqlCommand(sqlStr,coon))
                {
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }

            }
        }


        /// <summary>
        /// 执行sql语句，返回查询的第一行第一列
        /// </summary>
        /// <param name="sqlStr">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public static object ExecuteScalar(string sqlStr,params SqlParameter[] parameters)
        {
            using(SqlConnection coon = new SqlConnection(sqlConnectionString))
            {
                coon.Open();
                using(SqlCommand cmd = new SqlCommand(sqlStr,coon))
                {
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            }
        }


        /// <summary>
        /// 执行sql语句，返回一个数据表
        /// </summary>
        /// <param name="sqlStr">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string sqlStr,params SqlParameter[] parameters)
        {
            using(SqlConnection coon = new SqlConnection(sqlConnectionString))
            {
                coon.Open();
                using(SqlCommand cmd = new SqlCommand(sqlStr,coon))
                {
                    cmd.Parameters.AddRange(parameters);
                    using(SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }


        /// <summary>
        /// 执行存储过程，返回受影响的行数
        /// </summary>
        /// <param name="Proc">存储过程</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public static int ExecuteNonqueryProc(string Proc, params SqlParameter[] parameters)
        {
            using(SqlConnection coon = new SqlConnection(sqlConnectionString))
            {
                coon.Open();

                using(SqlCommand cmd = new SqlCommand(Proc,coon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }


        /// <summary>
        /// 执行存储过程，返回查询的第一行第一列
        /// </summary>
        /// <param name="Proc">存储过程</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public static object ExecuteScalarProc(string Proc, params SqlParameter[] parameters)
        {
            using (SqlConnection coon = new SqlConnection(sqlConnectionString))
            {
                coon.Open();
                using (SqlCommand cmd = new SqlCommand(Proc, coon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            }
        }


        /// <summary>
        /// 执行存储过程，返回一个数据表
        /// </summary>
        /// <param name="Proc"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTableProc(string Proc, params SqlParameter[] parameters)
        {
            using (SqlConnection coon = new SqlConnection(sqlConnectionString))
            {
                coon.Open();
                using (SqlCommand cmd = new SqlCommand(Proc, coon))
                {
                    cmd.Parameters.AddRange(parameters);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }


        /// <summary>
        /// 将excel文件导入数据库指定的表
        /// </summary>
        /// <param name="realPath">文件在服务器上的路径</param>
        /// <param name="sheetName">工作表名</param>
        /// <param name="tableName">数据库表名</param>
        /// <returns></returns>
        public static int ImportExcelDataToSql(string realPath, string sheetName,string tableName)
        {
            DataSet myds = new DataSet();
            try
            {
                OleDbConnection olecon = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + realPath + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'");
                string excelSql = "";//用来记录excel查询语句
                OleDbDataAdapter oledbda = null;//
                excelSql = string.Format("select * from [{0}$]", sheetName);
                oledbda = new OleDbDataAdapter(excelSql, olecon);
                oledbda.Fill(myds, sheetName);//填充数据

                using (SqlBulkCopy bcp = new SqlBulkCopy(sqlConnectionString))
                {
                    bcp.BatchSize = 100;
                    bcp.DestinationTableName = tableName;//目标表的名称
                    bcp.WriteToServer(myds.Tables[0]);

                }

               return 1;
            }
            
            catch
            {
                return 0;
            }
             

        }




    }
}