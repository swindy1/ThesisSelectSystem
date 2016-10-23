using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThesisSelectSystem.Models
{
    public class ClassesModels
    {
        public int ClassId { get; set; }
        public string ClassName{ get; set; }
        public int MajorId { get; set; }
        public int HumanNumber { get; set; }
        
        public int? GraduateYear { get; set; }

        public ClassesModels(int id, string name, int num, int majorid,int? year)
        {
            this.ClassName = name;
            this.ClassId = id;
            this.MajorId = majorid;
            this.HumanNumber = num;
            this.GraduateYear = year;
        }

        public ClassesModels()
        {
            this.MajorId = 1;
        }

        public class ClassTable:ClassesModels
        {
            public string MonitorId { get; set; }

            public ClassTable(int id, string name, int num, int majorid, int? year):base(id,name,num,majorid,year)
            {
                MonitorId = "xxxxxxx";
            }

            public ClassTable() { }
            
        }


    }
}