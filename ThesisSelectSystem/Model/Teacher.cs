using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Teacher
    {
        public string id { get; set; }
        public string name { get; set; }
        public string longTel { get; set; }
        public string shortTel { get; set; }
        public string email { get; set; }
        public string rank { get; set; }
        public int guideStudentNumber { get; set; }
        public string educationRoom { get; set; }
        public int departmentId { get; set; }
        public string account { get; set; }
        public string specialIdentity { get; set; }
        public int maxMakeTopicNumber { get; set; }
        public string topicGroup { get; set; }

        public Teacher()
        {
            
            
        }
    }


    public class TeacherInfoDetail : Teacher
    {
        public string departmentName { get; set; }

    }
}
