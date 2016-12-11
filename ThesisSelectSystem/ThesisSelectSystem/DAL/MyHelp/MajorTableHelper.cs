using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ThesisSelectSystem.DAL.MyHelp
{
    public class MajorTableHelper
    {
        

        public List<string> GetMajorName()
        {
            DataTable dt = SqlHelper.ExecuteDataTableProc("QueryMajorName");
            return (from DataRow row in dt.Rows select row[0].ToString()).ToList();
        }


        /// <summary>
        /// 用于检查数据库里是否存在一个为"name"的专业名
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsNoExistSameMajorName(string name)
        {
            string sqltext = @"select count(MajorName) from major where MajorName=@majorname";
            List<SqlParameter> arguments=new List<SqlParameter>();
            SqlParameter majorName=new SqlParameter("@majorname",SqlDbType.NVarChar);
            majorName.Value = name;
            arguments.Add(majorName);
            var result = (int)SqlHelper.ExecuteScalar(sqltext, arguments.ToArray());
            return result==0;

        }


        /// <summary>
        /// 添加新专业，需要传入专业名和系名两个字符串型参数
        /// </summary>
        /// <param name="majorName"></param>
        /// <param name="departmentName"></param>
        /// <returns></returns>
        public int AddMajor(string majorName, string departmentName)
        {
            int majorId = new DepartmentTableHelper().SearchDepartmentId(departmentName);
            string sqltext = @"insert into major(MajorName,DepartmentID) values(@name,@id)";
            List<SqlParameter> arguments = new List<SqlParameter>();
            SqlParameter name = new SqlParameter("@name", SqlDbType.NVarChar);
            name.Value = majorName;
            arguments.Add(name);
            SqlParameter id = new SqlParameter("@id", SqlDbType.Int);
            id.Value = majorId;
            arguments.Add(id);            
            return SqlHelper.ExecuteNonquery(sqltext, arguments.ToArray());
        }


        /// <summary>
        /// 删除专业，需要传入一个要删除专业的字符串数组，结果返回受影响的行数
        /// </summary>
        /// <param name="majorname"></param>
        /// <returns></returns>
        public int DelMajor(string[] majorname)
        {
            string sqltext = @"delete from major where MajorName=@majorName";
           
            int affectLine = 0;
            var arguments = new List<SqlParameter>();
            foreach (string t in majorname)
            {
                SqlParameter major = new SqlParameter("@majorName", SqlDbType.NVarChar);
                arguments.Clear();
                major.Value = t;
                arguments.Add(major);
                affectLine += SqlHelper.ExecuteNonquery(sqltext, arguments.ToArray());
            }
           
            return affectLine;
        }



    }
}