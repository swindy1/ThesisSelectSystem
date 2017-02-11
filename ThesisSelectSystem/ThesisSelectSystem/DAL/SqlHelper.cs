using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.IO;
using Aspose.Cells;
using System.Reflection;

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


    }
}