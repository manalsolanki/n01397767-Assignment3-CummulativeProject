using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using n01397767_Assignment3_CumlativeProject.Models;
using System.Diagnostics;

namespace n01397767_Assignment3_CumlativeProject.Controllers
{
    //An intermediate between view and Web api controller.
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }
        //GET : Teacher/List
        
        /// <summary>
        /// Helps to display the List view.That is list of all teachers.
        /// </summary>
        /// <param name="SearchKey">To search by Name.</param>
        /// <returns>A view with list of teacher objects</returns>
        public ActionResult List(string SearchKey = null)
        {
            //Connects through the webapi controller.
            TeacherDataController controller = new TeacherDataController();

            //Calls a method of web Api controller and stores in a list of Teacher object.
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);

            //Returns a list of teachers.
            return View(Teachers);
        }

        // GET : Teacher/Show/{id}
        /// <summary>
        /// This methods help to display a teacher property or details of a teacher.
        /// </summary>
        /// <param name="id">Helps to find by id</param>
        /// <returns>A view with information of a particular teacher(id)</returns>
        public ActionResult Show(int id)
        {
            //Creates a instance of Controller.
            TeacherDataController controller = new TeacherDataController();

            // Calls the find teacher method of the conrtoller.
            Teacher newTeacher = controller.FindTeacher(id);

            //Returns the evalute result.
            return View(newTeacher);
        }

        //GET : 
        public ActionResult DeleteConfirm(int id)
        {
            //Creates a instance of Controller.
            TeacherDataController controller = new TeacherDataController();

            // Calls the find teacher method of the conrtoller.
            Teacher newTeacher = controller.FindTeacher(id);

            //Returns the evalute result.
            return View(newTeacher);
        }

        //Post : /Teacher/Delete/{id}
        public ActionResult Delete(int id)
        {
            //Creates a instance of Controller.
            TeacherDataController controller = new TeacherDataController();

            // Calls the delete teacher method of the conrtoller.
            controller.deleteTeacher(id);
            return RedirectToAction("List");
        }

        //Get :/Teacher/new

        public ActionResult New ()
        {
            return View();
        }

        //POST :/Teacher/Create

        public ActionResult Create(string TeacherFname, string TeacherLname,string EmployeeNumber, string TeacherHireDate, Double TeacherSalary)
        {
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(TeacherHireDate);
            Debug.WriteLine(TeacherSalary);
            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.TeacherHireDate = TeacherHireDate;
            NewTeacher.TeacherSalary = TeacherSalary;
            NewTeacher.TeacherSalary = TeacherSalary;
            NewTeacher.EmployeeNumber = EmployeeNumber;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);
            return RedirectToAction("List");
        }
    }
}