using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using n01397767_Assignment3_CumlativeProject.Models;

namespace n01397767_Assignment3_CumlativeProject.Controllers
{
    public class ClassesController : Controller
    {
        // GET: Classes
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Helps to display the List view.That is list of all Classes.
        /// </summary>
        /// <returns>A view with list of ClassDetail objects</returns>
        public ActionResult List( )
        {
            //Connects through the webapi controller.
            ClassDataController controller = new ClassDataController();
            //Calls a method of web Api controller and stores in a list of CLass Detail object.
            IEnumerable<ClassDetail> Details = controller.ListClasses();

            //Returns a list of Details of classes.
            return View(Details);
        }
    }
}