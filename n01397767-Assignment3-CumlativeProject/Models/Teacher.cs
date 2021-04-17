using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace n01397767_Assignment3_CumlativeProject.Models
{
    public class Teacher
    {
        // Helps to define the property(fields) of teacher 
        public int TeacherId;
        public string TeacherFname;
        public string TeacherLname;
        public string EmployeeNumber;
        public string TeacherHireDate;
        public double TeacherSalary;
        public List<String> TeacherCourse = new List<string>();

        public Teacher() { }
    }
    
}