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

        //GET : Teacher/DeleteConfirm/{id}
        /// <summary>
        /// This method helps to display a confirmation page before deleting.
        /// </summary>
        /// <param name="id">The id of the teacher.</param>
        /// <returns>A view with a particular teacher information.</returns>
        public ActionResult DeleteConfirm(int id)
        {
            //Creates a instance of Controller.
            TeacherDataController controller = new TeacherDataController();

            // Calls the find teacher method of the conrtoller.
            Teacher newTeacher = controller.FindTeacher(id);

            //Returns the evalute result.
            return View(newTeacher);
        }
        /// <summary>
        /// This methods helps to delete the teacher.
        /// </summary>
        /// <param name="id">Specifies which teacher to be deleted</param>
        /// <returns>If deletes succesfully, then redirets to the list page.</returns>
        //Post : /Teacher/Delete/{id}
        public ActionResult Delete(int id)
        {
            //Creates a instance of Controller.
            TeacherDataController controller = new TeacherDataController();

            // Calls the delete teacher method of the conrtoller.
            controller.deleteTeacher(id);

            //Redirects to the list page.
            return RedirectToAction("List");
        }

        //Get :/Teacher/new
        /// <summary>
        /// This is use to diplay the form for adding a teacher.
        /// </summary>
        /// <returns>Just a view.</returns>
        public ActionResult New ()
        {
            return View();
        }

        //POST :/Teacher/Create
        /// <summary>
        /// This action is called when we submit the form for adding a new teacher.
        /// </summary>
        /// <param name="TeacherFname">Teacher First Name</param>
        /// <param name="TeacherLname">Teacher Last Name</param>
        /// <param name="EmployeeNumber">Employee Number</param>
        /// <param name="TeacherHireDate">The Hiring date</param>
        /// <param name="TeacherSalary">Salary of the teacher</param>
        /// <returns></returns>
        public ActionResult Create(string TeacherFname, string TeacherLname,string EmployeeNumber, string TeacherHireDate, Double TeacherSalary)
        {
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(TeacherHireDate);
            Debug.WriteLine(TeacherSalary);
            //Creates a model for teacher object.
            Teacher NewTeacher = new Teacher();
            //This is a server side validation As this would be the first site 
            // where this information will used by the server.
            if (TeacherFname == "" || TeacherLname == "" || EmployeeNumber == "" || TeacherHireDate == "")
            {
                return RedirectToAction("InvalidData");
            }
            else
            {
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.TeacherHireDate = TeacherHireDate;
                NewTeacher.TeacherSalary = TeacherSalary;
                NewTeacher.TeacherSalary = TeacherSalary;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                // Calling Add teacher controller.
                TeacherDataController controller = new TeacherDataController();
                controller.AddTeacher(NewTeacher);

                return RedirectToAction("List");

            }
        }
        /// <summary>
        /// When the server validation occurs during submit the form this page is rendered.
        /// </summary>
        /// <returns>A view </returns>
        //GET : /Teacher/Invalid
        public ActionResult InvalidData()
        {
            return View();
        }
    }
    

}