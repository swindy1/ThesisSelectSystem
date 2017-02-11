using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ThesisSelectSystem.DAL.MyHelp
{
    public class DepartmentTableHelper
    {
        /// <summary>
        /// 查找所有二级学院名称
        /// </summary>
        /// <returns></returns>
        public List<string> ListDepartmentName()
        {
            const string sqltext = @"select DepartmentName from department";
            var dt = SqlHelper.ExecuteDataTable(sqltext);
            return (from DataRow dr in dt.Rows select dr[0].ToString()).ToList();
        }



        /// <summary>
        /// 查找系/学院名为参数"department"的id
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public int SearchDepartmentId(string department)
        {
            string sqltext = @"select DepartmentId from department where DepartmentName=@departname";
            List<SqlParameter> arguments = new List<SqlParameter>();
            SqlParameter departName = new SqlParameter("@departname", SqlDbType.NVarChar);
            departName.Value = department;
            arguments.Add(departName);
            int result= -1;
            try
            {
                result = (int)SqlHelper.ExecuteScalar(sqltext, arguments.ToArray());
            }
            catch (Exception)
            {
                
                throw;
            }
            return result;

        }

        /// <summary>
        /// 查询指定名字的系
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string SearchDepartmentName(string name)
        {
            string queryResult;
            const string sqltext = @"select DepartmentName from department where DepartmentName= @departmentName";
            SqlParameter[] parameter=new SqlParameter[1];
            parameter[0]=new SqlParameter("@departmentName",SqlDbType.NVarChar);
            parameter[0].Value = name;
            queryResult=(string)SqlHelper.ExecuteScalar(sqltext, parameter);
            return queryResult;
        }


        /// <summary>
        /// 添加一个新系
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string AddDepartment(string text)
        {
            string result;
            if (SearchDepartmentName(text)==null)
            {
                string sqltext = @"insert into department(DepartmentName) values(@name)";
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@name", SqlDbType.NVarChar);
                parameter[0].Value = text;
                int affectline = SqlHelper.ExecuteNonquery(sqltext, parameter);
                if (affectline>0)
                {
                    result = "成功添加";
                }
                else
                {
                    result = "添加失败";
                }
            }
            else
            {
                result = "不可重复添加";
            }
            return result;
        }


       
    }
}