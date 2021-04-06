using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using n01397767_Assignment3_CumlativeProject.Models;
using System.Diagnostics;

namespace n01397767_Assignment3_CumlativeProject.Controllers
{
    public class TeacherDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{searchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string searchKey=null)
        {
            //Create an instance of a connection
            MySqlConnection Connection = School.AccessDatabase();

            //Open the connection between the web server and database
            Connection.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Connection.CreateCommand();


            //SQL QUERY
            cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + searchKey + "%");
            //Gather Result Set of Query into a variable specific result set of the query we provied
            MySqlDataReader ResultSet = cmd.ExecuteReader();
            
            //Create an empty list of Author Names
            List<Teacher> TeachersNames = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherfName = (string)ResultSet["teacherfname"];
                string TeacherlName = (string)ResultSet["teacherlname"];
                string TeacherHireDate = ResultSet["hiredate"].ToString();
                double TeacherSalary = Convert.ToDouble(ResultSet["salary"]);
                Teacher NewTeacher = new Teacher();
                //Add the Teacher Name to the List

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherfName;
                NewTeacher.TeacherLname = TeacherlName;
                NewTeacher.TeacherHireDate = TeacherHireDate;
                NewTeacher.TeacherSalary = TeacherSalary;
                TeachersNames.Add(NewTeacher);
            }
            /* ResultSet.Close();
           foreach(Teacher teacherForCourse in TeachersNames)
            {
                cmd.CommandText = "SELECT classname from classes where teacherid =" + ss.TeacherId;
                ResultSet = cmd.ExecuteReader();
                while (ResultSet.Read())
                {
                    string TeacherCourse = (string)ResultSet["classname"];
                    teacherForCourse.TeacherCourse.Add(TeacherCourse);
                }
                ResultSet.Close();
            }
            */
            //Close the connection between the MySQL Database and the WebServer
            Connection.Close();

            //Return the final list of author names
            return TeachersNames;
        }

        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();
            MySqlConnection Connection = School.AccessDatabase();
            Connection.Open();
            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM teachers where teacherid = " + id;
            MySqlDataReader ResultSet = cmd.ExecuteReader();
            while (ResultSet.Read())
            {
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherfName = (string)ResultSet["teacherfname"];
                string TeacherlName = (string)ResultSet["teacherlname"];
                string TeacherHireDate = ResultSet["hiredate"].ToString();
                double TeacherSalary = Convert.ToDouble(ResultSet["salary"]);
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherfName;
                NewTeacher.TeacherLname = TeacherlName;
                NewTeacher.TeacherHireDate = TeacherHireDate;
                NewTeacher.TeacherSalary = TeacherSalary;
            }
            ResultSet.Close();
            cmd.CommandText = "SELECT classname from classes where teacherid =" + id;
            ResultSet = cmd.ExecuteReader();
            while (ResultSet.Read())
            {
                string TeacherCourse = (string)ResultSet["classname"];
                NewTeacher.TeacherCourse.Add(TeacherCourse);
            }
            ResultSet.Close();
            Connection.Close();
            return NewTeacher;
        }
    }
}
