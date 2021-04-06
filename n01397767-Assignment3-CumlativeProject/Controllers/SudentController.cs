using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using n01397767_Assignment3_CumlativeProject.Models;

namespace n01397767_Assignment3_CumlativeProject.Controllers
{
    public class SudentController : Controller
    {
        // GET: Sudent
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string SearchKey = null)
        {
            //Connects through the webapi controller.
            StudentDataController controller = new StudentDataController();
            //Calls a method of web Api controller and stores in a list of Teacher object.
            IEnumerable<Student> Students = controller.ListStudents("");

            //Returns a list of teachers.
            return View(Students);
        }

   


    }
}