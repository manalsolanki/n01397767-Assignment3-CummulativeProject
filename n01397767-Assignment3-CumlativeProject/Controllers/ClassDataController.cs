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
    public class ClassDataController : ApiController
    {
        // Reference to School Database inorder to establish the connection.
        private SchoolDbContext School = new SchoolDbContext();

        /// <summary>
        /// This methods list all the details of classes from the DB.
        /// </summary>
        /// <example>GET api/ClassData/ListClasses</example>
        /// <returns>A list of Class Object.</returns>
        [HttpGet]
        [Route("api/ClassData/ListClasses")]
        public IEnumerable<ClassDetail> ListClasses( )
        {
            //Instance of a connection using MySQL object.
            MySqlConnection Connection = School.AccessDatabase();

            //Establishes connection between web server and the database
            Connection.Open();

            //This helps to create a new command of SQL.
            MySqlCommand cmd = Connection.CreateCommand();


            //SQL QUERY which finds list of classes along with teacher associated with that.
            cmd.CommandText = "SELECT classid,classcode,classname,teacherfname,teacherlname,startdate,finishdate FROM classes c Join teachers t on c.teacherid= t.teacherid";
           

            //When Query runs using executeReader the result is stored in Resultset
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Empty list of Class Detail to store all the data from the DB.
            List<ClassDetail> ClassesDetails = new List<ClassDetail> { } ;
  
            //Iterating through each row of the result computed in result set.
            while (ResultSet.Read())
            {
                //We can access each colunmname giving it as a index of the resut set.
                // We need to typeCast each value into suitable Data type.
                int ClassId = (int)ResultSet["classid"];
                string TeacherfName = (string)ResultSet["teacherfname"];
                string TeacherlName = (string)ResultSet["teacherlname"];
                string classcode = (string)ResultSet["classcode"];
                string classname = (string)ResultSet["classname"];
                DateTime startDate = (DateTime)ResultSet["startdate"];
                DateTime finishdate = (DateTime)ResultSet["finishdate"];
                string start = startDate.ToLongDateString();
                string end = finishdate.ToLongDateString();
     

                // Creating a NewClassDetails object of type ClassDetail to store the information of one Class at a time.
                ClassDetail NewClassDetails = new ClassDetail();

                //Add the inoformation to the NewClassDetails
                NewClassDetails.classId = ClassId;
                NewClassDetails.teacherFName = TeacherfName;
                NewClassDetails.teacherlName = TeacherlName;
                NewClassDetails.className = classname;
                NewClassDetails.classCode = classcode;
                NewClassDetails.startDate = start;
                NewClassDetails.FinishDate = end;

                // Then add this to the ClassDetails final List. 
                ClassesDetails.Add(NewClassDetails);
            }
            //Important to close the connection between webserver and Database.
            Connection.Close();

            //Return the final list of ClassDetials names
            return ClassesDetails;
        }
    }
}
