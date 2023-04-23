using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSharp_Crud_Operations.Models;
using System.Diagnostics;

namespace CSharp_Crud_Operations.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        //GET: /Teacher/List
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);

            return View(Teachers);
        }

        //GET: /Teacher/Show/{id}

        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);
            
            return View(SelectedTeacher);
        }
        //GET: /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher teach = controller.FindTeacher(id);

            return View(teach);
        }

        //POST: /Teacher/Delete/{id}
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        //GET: /Teacher/New
        public ActionResult New()
        {
            return View();
        }

        //POST: /Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, DateTime TeacherHireDate, string TeacherEmployeeNumber, Decimal TeacherSalary)
        {
            //Identify this method is running
            //Identify the inputs provided from the form

            Debug.WriteLine("I have accessed the create method");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(TeacherHireDate);
            Debug.WriteLine(TeacherEmployeeNumber);
            Debug.WriteLine(TeacherSalary);


            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.TeacherHireDate = TeacherHireDate;
            NewTeacher.TeacherEmployeeNumber = TeacherEmployeeNumber;
            NewTeacher.TeacherSalary = TeacherSalary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");

        }

        /// <summary>
        /// Routes to a dynamically generated "Teacher Update" Page. Gathers information from the school database.
        /// </summary>
        /// <param name="id">Id of the Teacher</param>
        /// <returns>A dynamic "Update Teacher" webpage which will display the info of Teacher and ask user to upadte info </returns>
        /// <example>GET : /Teacher/Update/</example>
        /// GET: /Teacher/Update{id}
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }

        /// <summary>
        /// Receives a post request about an existing teacher in the system with new values
        /// </summary>
        /// <param name="id">Id of the Teacher to update</param>
        /// <param name="TeacherFname">The updated first name of the Teacher</param>
        /// <param name="TeacherLname">The updated last name of the Teacher</param>
        /// <param name="TeacherEmployeeNumber">The updated employee number of the Teacher.</param>
        /// <param name="TeacherSalary">The updated salary of the Teacher.</param>
        /// <returns>A dynamic webpage which provides the current information of the Teacher.</returns>
        /// <example>
        /// POST : /Teacher/Update/10
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"TeacherFname":"Shubham",
        ///	"TeacherLname":"Mishra",
        ///	"TeacherEmployeeNumber":"T123",
        ///	"TeacherSalary":"50.00"
        /// }
        /// </example>

        [HttpPost]
        public ActionResult Update(int id, string TeacherFname, string TeacherLname,DateTime TeacherHireDate, string TeacherEmployeeNumber, Decimal TeacherSalary)
        {
            Teacher TeacherInfo = new Teacher();
            TeacherInfo.TeacherFname = TeacherFname;
            TeacherInfo.TeacherLname = TeacherLname;
            TeacherInfo.TeacherHireDate = TeacherHireDate;
            TeacherInfo.TeacherEmployeeNumber = TeacherEmployeeNumber;
            TeacherInfo.TeacherSalary = TeacherSalary;

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, TeacherInfo);

            return RedirectToAction("Show/" + id);
        }


    
    }
}