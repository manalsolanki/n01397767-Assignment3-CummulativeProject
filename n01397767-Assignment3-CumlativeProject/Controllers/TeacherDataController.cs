using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using n01397767_Assignment3_CumlativeProject.Models;
using System.Diagnostics;
using System.Web.Http.Cors;

namespace n01397767_Assignment3_CumlativeProject.Controllers
{
    // This Controller mainly will iterate through the Teachers table in School Database.
    public class TeacherDataController : ApiController
    {
        // Reference to School Database inorder to establish the connection.
        private SchoolDbContext School = new SchoolDbContext();

        /// <summary>
        /// This methods list all the teachers from the DB and if a search key is given then list all names with that value.
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <example>GET api/TeacherData/ListTeachers/linda</example>
        /// <param name="searchKey">It represents Teacher first name or last Name or both. As it is not a complusory arugment in the route we initalised it as null.</param>
        /// <returns>A list of Teacher Object.</returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{searchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string searchKey=null)
        {
            //Instance of a connection using MySQL object.
            MySqlConnection Connection = School.AccessDatabase();

            //Establishes connection between web server and the database
            Connection.Open();

            //This helps to create a new command of SQL.
            MySqlCommand cmd = Connection.CreateCommand();


            //SQL QUERY which finds list of teacher using where clause.(using searchkey)
            cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";

            // Replacing the @key value in the above query with actually value.
            cmd.Parameters.AddWithValue("@key", "%" + searchKey + "%");

            //When Query runs using executeReader the result is stored in Resultset
            MySqlDataReader ResultSet = cmd.ExecuteReader();
            
            //Empty list of Teacher Names to store all the data from the DB.
            List<Teacher> TeachersNames = new List<Teacher> { };

            //Iterating through each row of the result computed in result set.
            while (ResultSet.Read())
            {
                //We can access each colunmname giving it as a index of the resut set.
                // We need to typeCast each value into suitable Data type.
                int TeacherId = (int)ResultSet["teacherid"]; 
                string TeacherfName = (string)ResultSet["teacherfname"];
                string TeacherlName = (string)ResultSet["teacherlname"];
                DateTime Hiredate = (DateTime)ResultSet["hiredate"];
                string TeacherHireDate = Hiredate.ToLongDateString();
                double TeacherSalary = Convert.ToDouble(ResultSet["salary"]);

                // Creating a NewTeacher object of type Teacher to store the information of one teacher at a time.
                Teacher NewTeacher = new Teacher();

                //Add the inoformation to the NewTeacher

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherfName;
                NewTeacher.TeacherLname = TeacherlName;
                NewTeacher.TeacherHireDate = TeacherHireDate;
                NewTeacher.TeacherSalary = TeacherSalary;

                // Then add this to the TeachersNames final List. 
                TeachersNames.Add(NewTeacher);
            }
            //Important to close the connection between webserver and Database.
            Connection.Close();

            //Return the final list of Teacher names
            return TeachersNames;
        }


        // This method uses class table also from the School Database
        // as it shows which teacher is assigned which subject.
        /// <summary>
        /// This methods helps to find the teacher detail with the help of primary key that is id.
        /// </summary>
        /// <param name="id">Teacher's Id the Teacher Table of School Database.</param>
        /// <returns>A teacher Object with all necessary information</returns>
        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            // Creating a object of Teacher to store the result.
            Teacher NewTeacher = new Teacher();

            //Instance of a connection using MySQL object.
            MySqlConnection Connection = School.AccessDatabase();

            //Establishes connection between web server and the database
            Connection.Open();

            //This helps to create a new command of SQL.
            MySqlCommand cmd = Connection.CreateCommand();
            
            // SQL Query to find teacher by Id.
            cmd.CommandText = "SELECT * FROM teachers where teacherid = " + id;

            //When Query runs using executeReader the result is stored in Resultset
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // This iterated through the result set but here as it is will contian only 1 row it would excute 1 time.
            while (ResultSet.Read())
            {
                //We can access each colunmname giving it as a index of the resut set.
                // We need to typeCast each value into suitable Data type.
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherfName = (string)ResultSet["teacherfname"];
                string TeacherlName = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];
                DateTime TeacherHireDate =(DateTime) ResultSet["hiredate"];
                double TeacherSalary = Convert.ToDouble(ResultSet["salary"]);
                string date = TeacherHireDate.ToLongDateString();
                // Assigning this value to the final object.
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherfName;
                NewTeacher.TeacherLname = TeacherlName;
                NewTeacher.TeacherHireDate = date;
                NewTeacher.TeacherSalary = TeacherSalary;
                NewTeacher.EmployeeNumber = EmployeeNumber;
            }

            // As we want to execute another query , we need to close the first one.
            ResultSet.Close();

            // Now for getting the list of classes with which each teacher is associated
            // We will run another query on Class Database where teacher id is given so it
            // will us to get all the course name.
            cmd.CommandText = "SELECT classname from classes where teacherid =" + id;

            // Again executing the query and storing in the resultset.
            ResultSet = cmd.ExecuteReader();

            //Iterating through the ResultSet to read each Value.
            while (ResultSet.Read())
            {
                // To store a course Name
                string TeacherCourse = (string)ResultSet["classname"];

                //Appending to the New Teacher object .
                NewTeacher.TeacherCourse.Add(TeacherCourse);
            }
            //As more than 1 query is being excuted , closing is necessary (This is what i understood )
            //If wasnt doing then was getting an error.
            ResultSet.Close();

            //Important to close the connection between webserver and Database.
            Connection.Close();

            // Returns an object of Teacher with all the information.
            return NewTeacher;
        }

        /// <summary>
        /// This method helps to delete the teacher by id. 
        /// </summary>
        /// <param name="id">This is the primary key</param>
        /// <return>It doesnot return anything as the main motive is to delete.</return>
        [HttpPost]
        public void deleteTeacher(int id)
        {
            //Instance of a connection using MySQL object.
            MySqlConnection Connection = School.AccessDatabase();

            //Establishes connection between web server and the database
            Connection.Open();

            //This helps to create a new command of SQL.
            MySqlCommand cmd = Connection.CreateCommand();

            //SQL QUERY to delete by Id
            // I have updated the Database so in the Classes where teacherid is foreign key
            // have updated the restriction to ON DELETE CASCADE so whenever a teacher is deleted automatically classes are 
            // are also remove and same for studentvsclass table.
            //By this we can maintain data-integrity.
            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            //Important to close the connection between webserver and Database.
            Connection.Close();
        }
        /// <summary>
        /// This Class helps to add a new teacher to the school database .
        /// </summary>
        /// <param name="newTeacher">Its a teacher model with all the necssary information.</param>
        /// <returns>As there is nothing to return, it is void</returns>
        [HttpPost]
        public void AddTeacher([FromBody]Teacher newTeacher)
        {
            //Instance of a connection using MySQL object.
            MySqlConnection Connection = School.AccessDatabase();

            //Establishes connection between web server and the database
            Connection.Open();

            //This helps to create a new command of SQL.
            MySqlCommand cmd = Connection.CreateCommand();

            //SQL QUERY to Insert 
            cmd.CommandText = "INSERT INTO teachers " +
                "(teacherfname, teacherlname, employeenumber, hiredate, salary) " +
                "VALUES (@fname, @lname, @employeenumber,@date,@salary);";
            cmd.Parameters.AddWithValue("@fname", newTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@lname", newTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@employeenumber", newTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@date",Convert.ToDateTime(newTeacher.TeacherHireDate));
            cmd.Parameters.AddWithValue("@salary", newTeacher.TeacherSalary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            //Important to close the connection between webserver and Database.
            Connection.Close();
        }
        [HttpPost]
        public void UpdateTeacher (int id , [FromBody]Teacher teacherInfo)
        {
            //Instance of a connection using MySQL object.
            MySqlConnection Connection = School.AccessDatabase();

            //Establishes connection between web server and the database
            Connection.Open();

            //This helps to create a new command of SQL.
            MySqlCommand cmd = Connection.CreateCommand();

            //SQL QUERY to Insert 
            cmd.CommandText = "UPDATE teachers set " +
                "teacherfname = @fname , teacherlname = @lname, employeenumber = @employeenumber, salary = @salary " +
                "where teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@fname", teacherInfo.TeacherFname);
            cmd.Parameters.AddWithValue("@lname", teacherInfo.TeacherLname);
            cmd.Parameters.AddWithValue("@employeenumber", teacherInfo.EmployeeNumber);
            cmd.Parameters.AddWithValue("@salary", teacherInfo.TeacherSalary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            //Important to close the connection between webserver and Database.
            Connection.Close();

        }
    }
}
