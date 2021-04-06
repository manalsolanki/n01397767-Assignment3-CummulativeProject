using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
namespace n01397767_Assignment3_CumlativeProject.Models
{
    // This class stores the information about School Database.
    public class SchoolDbContext
    {
        //These properties needs to be private and read only so we declare as private.It
        // contains the detail of DB.
        private static string User { get { return "root"; } }
        private static string Password { get { return "root"; } }
        private static string Database { get { return "school"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }

      // To achieve the connection between the Database , this string is key to that.It contains all the necessary information.
        protected static string ConnectionString
        {
            get
            {
                //As 0000-00-00 date returns null and error may occue so by declaring true we are preventing that error.

                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password
                    + "; convert zero datetime = True";

            }
        }
  
        /// <summary>
        /// It returns a connection of School Database and this method can be used globally.
        /// It is just simple instantisation of the classes to create an object.
        /// </summary>
        /// <example>
        /// private SchoolDbContext School = new SchoolDbContext();
        /// MySqlConnection Connection = School.AccessDatabase();
        /// </example>
        /// <returns>MySqlConnection Object</returns>
        public MySqlConnection AccessDatabase()
        {
           // This object refers connection with Database on localhost port 3306.
            return new MySqlConnection(ConnectionString);
        }
    }
}