using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Model;

namespace ThesisSelectSystem.DAL.MyHelp
{
    public class TeacherInfoQuery
    {
        public Dictionary<string, string> QueryTeacherNameAndAccount()
        {
            Dictionary<string,string>teacherInfo=new Dictionary<string, string>();
            var records = SqlHelper.ExecuteDataTableProc("QueryTeacherName");
            foreach (DataRow row in records.Rows)
            {
                string[] record=new string[2];
                int index = 0;
                foreach (DataColumn column in records.Columns)
                {
                    record[index++] = row[column].ToString();
                }
                teacherInfo[record[0]] = record[1];
            }
            return teacherInfo;
        }

        public TeacherInfoDetail[] QueryTeacherInfoDetails(SqlParameter[] parameters)
        {
            List<TeacherInfoDetail> teacherInfoDetails=new List<TeacherInfoDetail>();
            string[] record=new string[3];
            
            var records = SqlHelper.ExecuteDataTableProc("QueryTeacherBaseInfo", parameters);
            foreach (DataRow row in records.Rows)
            {
                int index = 0;
                foreach (DataColumn column in records.Columns)
                {
                    record[index++] = row[column].ToString();
                }
                TeacherInfoDetail teacherInfo=new TeacherInfoDetail();
                teacherInfo.id = record[0];
                teacherInfo.name = record[1];
                teacherInfo.departmentName = record[2];
                teacherInfoDetails.Add(teacherInfo);

            }
            return teacherInfoDetails.ToArray();
        }

    }
}