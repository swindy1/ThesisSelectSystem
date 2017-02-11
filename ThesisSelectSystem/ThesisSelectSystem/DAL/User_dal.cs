using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Web;
using Model;
using ThesisSelectSystem.Models;

namespace ThesisSelectSystem.DAL
{
    public class User_dal
    {
        /// <summary>
        /// 检测用户登录时帐号和密码
        /// </summary>
        /// <param name="Account">帐号</param>
        /// <returns></returns>
        public UserModel GetPswAndRoleAndSaltModel(string Account)
        {
            string sqlStr = "select Passwords,Salt,Roles from userinfo where accounts=@Account";
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
                model.passwords = row["Passwords"].ToString();
                model.role = row["Roles"].ToString();
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


        /// <summary>
        /// 用于导入学生、老师数据时初始化学生、老师账户信息
        /// </summary>
        /// <param name="account">帐号</param>
        /// <param name="role">角色，有老师和学生两种</param>
        /// <returns>导入的结果</returns>
        public static bool CreateNewUser(string account, string role="学生")
        {
            bool result = false;
            string salt = Guid.NewGuid().ToString();
            UserInfo user=new UserInfo();
            var temp = Encoding.UTF8.GetBytes(account + salt);
            user.account = account;
            user.salt = salt;
            user.roles = role; 
            user.passwords = Convert.ToBase64String(new System.Security.Cryptography.SHA256Managed().ComputeHash(temp));
            DbOperation.Save(user, "userinfo", out result);
            return result;
        }

        
    }
}