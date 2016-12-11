using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ThesisSelectSystem.Models;

namespace ThesisSelectSystem.DAL.MyHelp
{
    public  class ClassesTableHelper
    {
        /// <summary>
        /// 用于验证班级名在数据库里是否存在，存在返回1，否则返回0
        /// </summary>
        /// <param name="classes"></param>
        /// <returns>res</returns>
        public int CheckClassNameLegal(ClassesModels classes)
        {
            #region 验证需要创建的班级是否已经存在

            int res;
            try
            {
                List<SqlParameter> args = new List<SqlParameter>();
                var name = new SqlParameter("@ClassName", SqlDbType.NVarChar);
                name.Value = classes.ClassName;
                args.Add(name);
                res = (int)SqlHelper.ExecuteScalarProc("IsExistClassName", args.ToArray());
            }
            catch (Exception)
            {
                throw;
            }

            #endregion

            return res;
        }


        public int FindMajorId(string MajorName)
        {
            int majorId = 0;
            try
            {
                List<SqlParameter> args = new List<SqlParameter>();
                var name = new SqlParameter("@MajorName", SqlDbType.NVarChar);
                name.Value = MajorName;
                args.Add(name);
                majorId = (int)SqlHelper.ExecuteScalarProc("FindMajorId", args.ToArray());
            }
            catch (Exception)
            {
                
                throw;
            }
            
            return majorId;
        }


        public List<string> ListClassName()
        {
            const string sqltext = @"select ClassName from classes where GraduateYear=@Year";
            var parameters=new List<SqlParameter>();
            var year= new SqlParameter("@Year",SqlDbType.Int);
            year.Value = 2018;
            parameters.Add(year);
            var dt = SqlHelper.ExecuteDataTable(sqltext, parameters.ToArray());
            return (from DataRow dr in dt.Rows select dr[0].ToString()).ToList();

        } 


    }
}