using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using n01397767_Assignment3_CumlativeProject.Models;

namespace n01397767_Assignment3_CumlativeProject.Controllers
{
    public class StudentDataController : ApiController
    {
        // Reference to School Database inorder to establish the connection.
        private SchoolDbContext School = new SchoolDbContext();

        /// <summary>
        /// This methods list all the students from the DB and if a search key is given then list all names with that value.
        /// </summary>
        /// <example>GET api/StudentData/ListStudents</example>
        /// <example>GET api/StudentData/ListStudents/linda</example>
        /// <param name="searchKey">It represents Student first name or last Name or both. As it is not a complusory arugment in the route we initalised it as null.</param>
        /// <returns>A list of Student Object.</returns>
        [HttpGet]
        [Route("api/StudentData/ListStudents/{searchKey?}")]
        public IEnumerable<Student> ListStudents(string searchKey)
        {
            //Instance of a connection using MySQL object.
            MySqlConnection Connection = School.AccessDatabase();

            //Establishes connection between web server and the database
            Connection.Open();

            //This helps to create a new command of SQL.
            MySqlCommand cmd = Connection.CreateCommand();


            //SQL QUERY which finds list of student using where clause.(using searchkey)
            cmd.CommandText = "Select * from students where lower(studentfname) like lower(@key) or lower(studentlname) like lower(@key) or lower(concat(studentfname, ' ', studentlname)) like lower(@key)";
           
            // Replacing the @key value in the above query with actually value.
            cmd.Parameters.AddWithValue("@key", "%" + searchKey + "%");

            //When Query runs using executeReader the result is stored in Resultset
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Empty list of Student Names to store all the data from the DB.
            List<Student> StudentNames = new List<Student> { };

            //Iterating through each row of the result computed in result set.
            while (ResultSet.Read())
            {
                //We can access each colunmname giving it as a index of the resut set.
                // We need to typeCast each value into suitable Data type.
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentfName = (string)ResultSet["studentfname"];
                string studentlName = (string)ResultSet["studentlname"];
                string studentNumber = (string)ResultSet["studentnumber"];
                DateTime studentEnrolDate = (DateTime)ResultSet["enroldate"];
                string date = studentEnrolDate.ToLongDateString();

                // Creating a NewStudent object of type Student to store the information of one Student at a time.
                Student NewStudent = new Student();

                //Add the inoformation to the NewStudent

                NewStudent.StudentId = StudentId;
                NewStudent.StudentFname = StudentfName;
                NewStudent.StudentLname = studentlName;
                NewStudent.StudentNumber = studentNumber;
                NewStudent.StudentEnrolDate = date;

                // Then add this to the StudentsNames final List. 
                StudentNames.Add(NewStudent);
            }
            //Important to close the connection between webserver and Database.
            Connection.Close();

            //Return the final list of Student names
            return StudentNames;
        }

        ///<summary>
        /// This methods helps to find the student detail with the help of primary key that is id.
        /// </summary>
        /// <param name="id">Student's Id the student Table of School Database.</param>
        /// <returns>A student Object with all necessary information</returns>
        [HttpGet]
        [Route("api/StudentData/FindStudent/{id}")]
        public Student FindStudent(int id)
        {
            //Instance of a connection using MySQL object.
            MySqlConnection Connection = School.AccessDatabase();

            //Establishes connection between web server and the database
            Connection.Open();

            //This helps to create a new command of SQL.
            MySqlCommand cmd = Connection.CreateCommand();


            //SQL QUERY which finds list of student using where clause.(using searchkey)
            cmd.CommandText = "Select * from students where studentid ="+id;

            //When Query runs using executeReader the result is stored in Resultset
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Creating a NewStudent object of type Student to store the information 
            Student NewStudent = new Student();

            //Iterating through each row of the result computed in result set here it will one row.
            while (ResultSet.Read())
            {
                //We can access each colunmname giving it as a index of the resut set.
                // We need to typeCast each value into suitable Data type.
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentfName = (string)ResultSet["studentfname"];
                string studentlName = (string)ResultSet["studentlname"];
                string studentNumber = (string)ResultSet["studentnumber"];
                DateTime studentEnrolDate = (DateTime)ResultSet["enroldate"];
                string date = studentEnrolDate.ToLongDateString();

                

                //Add the inoformation to the NewStudent

                NewStudent.StudentId = StudentId;
                NewStudent.StudentFname = StudentfName;
                NewStudent.StudentLname = studentlName;
                NewStudent.StudentNumber = studentNumber;
                NewStudent.StudentEnrolDate = date;
            }
            //Important to close the connection between webserver and Database.
            Connection.Close();

            //Return the final list of Student names
            return NewStudent;
        }


    }
}
