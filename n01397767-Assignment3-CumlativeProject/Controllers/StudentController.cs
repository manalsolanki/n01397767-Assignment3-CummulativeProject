using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using n01397767_Assignment3_CumlativeProject.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace n01397767_Assignment3_CumlativeProject.Controllers
{
    //An intermediate between view and Web api controller.

    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Helps to display the List view.That is list of all Student.
        /// </summary>
        /// <param name="SearchKey">To search by Name.</param>
        /// <returns>A view with list of Student objects</returns>
        public ActionResult List( string searchKey=null)
        {
            //Connects through the webapi controller.
            StudentDataController controller = new StudentDataController();
            //Calls a method of web Api controller and stores in a list of Student object.
            IEnumerable<Student> Students = controller.ListStudents(searchKey);

            //Returns a list of students.
            return View(Students);
        }


        // GET : Teacher/Show/{id}
        /// <summary>
        /// This methods help to display a teacher property or details of a teacher.
        /// </summary>
        /// <param name="id">Helps to find by id</param>
        /// <returns>A view with information of a particular teacher(id)</returns>
        public ActionResult Show(int id)
        {
            //Connects through the webapi controller.
            StudentDataController controller = new StudentDataController();
            //Calls a method of web Api controller and stores in a particular information of Student object.
            Student NewStudent = controller.FindStudent(id);
            Debug.WriteLine("The student detail is");
            Debug.WriteLine(NewStudent);

            //Returns the evalute result.
            return View(NewStudent);
        }
    }
}