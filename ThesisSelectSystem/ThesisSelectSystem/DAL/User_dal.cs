using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ThesisSelectSystem.Models;

namespace ThesisSelectSystem.DAL
{
    public class User_dal
    {
        public UserModel GetPswAndRoleAndSaltModel(string Account)
        {
            string sqlStr = "select passwords,Salt,role from login where accounts=@Account";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Account",Account)
            };
            DataTable Tb = new DataTable();
            Tb = SqlHelper.ExecuteDataTable(sqlStr, parameters);

            //表中只有一条数据
            if(Tb.Rows.Count == 1)
            {
                DataRow row = Tb.Rows[0];//获取第一行的数据
                UserModel model = new UserModel();
                model.passwords = row["passwords"].ToString();
                model.role = row["role"].ToString();
                model.Salt = row["Salt"].ToString();
                return model;
            }
            //超过一条数据
            else if (Tb.Rows.Count > 1)
            {
                throw new Exception("login 表中存在两条以上账号相同的数据");
            }
            //查询结果为空
            else
            {
                return null;
            }
            
        }

        
    }
}