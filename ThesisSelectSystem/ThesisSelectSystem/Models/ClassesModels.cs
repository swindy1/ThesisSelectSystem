using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ThesisSelectSystem.DAL;
using ThesisSelectSystem.DAL.MyHelp;

namespace ThesisSelectSystem.Models
{
    public class ClassesModels
    {
       
        public string ClassName{ get; set; }
        public int HumanNumber { get; set; }
        public string MajorName { get; set; }
       

        public ClassesModels(string name, int num, string majorname)
        {
            this.ClassName = name;
            this.HumanNumber = num;
            this.MajorName = majorname;
        }

        public ClassesModels()
        {    
        }

        public ClassesModels(string name)
        {
            this.ClassName = name;
        }
    }


    public class ClassTable 
    {
        public int ClassId { get; set; }  
        public int MajorId { get; set; }
        public int HumanNumber { get; set; }
        public string ClassName { get; set; }
        public int? GraduateYear { get; set; }

        
        public ClassTable(int id, string name, int num, int majorid, int? year)
        {
            this.ClassId = id;
            this.MajorId = majorid;
            this.GraduateYear = year;
            this.ClassName = name;
            this.HumanNumber = num;

        }

        public ClassTable()
        { }

        public ClassTable(int id, string name, int num, int? year)
        {
            this.ClassId = id;
            this.ClassName = name;
            this.HumanNumber = num;
            this.GraduateYear = year;
            
        }

        public ClassTable(int id)
        {
            this.ClassId = id;
        }


        /// <summary>
        /// 用于修改数据库里classes表里的ClassName,HumanNumber,GraduateYear属性   
        /// ClassId,ClassName,HumanNumber,GraduateYear这四个属性必须不为空     
        /// 返回结果大于0表示修改属性成功
        /// </summary>                    
        /// <returns>result</returns>         
        public int Update()
        {
            int result ;   
            string sqltext = @"update classes 
                               set HumanNumber = @number,
                               GraduateYear = @year, 
                               ClassName = @name 
                               where ClassID = @id";
            try
            {
                    #region 绑定参数到sql语句
                    List<SqlParameter> data = new List<SqlParameter>();
                    var id = new SqlParameter("@id", SqlDbType.Int);
                    id.Value = ClassId;
                    data.Add(id);

                    var name = new SqlParameter("@name", SqlDbType.NVarChar);
                    name.Value = ClassName;
                    data.Add(name);

                    var year = new SqlParameter("@year", SqlDbType.Int);
                    year.Value = this.GraduateYear;
                    data.Add(year);

                    var number = new SqlParameter("@number", SqlDbType.Int);
                    number.Value = this.HumanNumber;
                    data.Add(number);
                    #endregion
                result = SqlHelper.ExecuteNonquery(sqltext, data.ToArray());
            }
            catch (Exception)
            {
                throw;
            }
            return result;

        }


        /// <summary>
        /// 对象的id不为空时执行sql语句，删除指定id的班级，返回结果为0表示删除失败
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            string sqltext = @"delete
                               from classes
                               where ClassID = @id";
            List<SqlParameter>args = new List<SqlParameter>();
            SqlParameter id = new SqlParameter("@id",SqlDbType.Int);
            id.Value = this.ClassId;
            args.Add(id);
            return SqlHelper.ExecuteNonquery(sqltext, args.ToArray());
        }

    }
}