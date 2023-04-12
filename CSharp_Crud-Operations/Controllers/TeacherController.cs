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
            Teacher teach = controller.FindTeacher(id);
            
            return View(teach);
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
        public ActionResult Create(string TeacherFname, string TeacherLname, /*DateTime TeacherHireDate, */string TeacherEmployeeNumber, Decimal TeacherSalary)
        {
            //Identify this method is running
            //Identify the inputs provided from the form

            Debug.WriteLine("I have accessed the create method");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            //Debug.WriteLine(TeacherHireDate);
            Debug.WriteLine(TeacherEmployeeNumber);
            Debug.WriteLine(TeacherSalary);


            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            //NewTeacher.TeacherHireDate = TeacherHireDate;
            NewTeacher.TeacherEmployeeNumber = TeacherEmployeeNumber;
            NewTeacher.TeacherSalary = TeacherSalary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");

        }
    }
}