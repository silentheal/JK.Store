 using CodingTemple.BoardGameStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodingTemple.BoardGameStore.Controllers
{
    public class StudentController : Controller
    {
        private static List<StudentModel> students = new List<StudentModel>();

        // GET: Student
        public ActionResult Index(string id, string format = "html", bool? edited = false)
        {
            ViewBag.Edited = edited;
            if(students.Count == 0)
            {
                students.Add(new StudentModel { ID = 1, FirstName = "Ralph", LastName = "Comb", FavoriteFood = "Coffee" });
                students.Add(new StudentModel { ID = 2, FirstName = "Jinseong", LastName = "Kim", FavoriteFood = "Pasta" });
                students.Add(new StudentModel { ID = 3, FirstName = "Sam", LastName = "Fessler", FavoriteFood = "Shrimp" });
                students.Add(new StudentModel { ID = 4, FirstName = "Erica", LastName = "Wasilenko", FavoriteFood = "Hummus" });
                students.Add(new StudentModel { ID = 5, FirstName = "Will", LastName = "Mabry", FavoriteFood = "Ice Cream" });
                students.Add(new StudentModel { ID = 6, FirstName = "Joe", LastName = "Johnson", FavoriteFood = "Nachoes" });
            }

            if(format == "html")
            {
                return View(students);
            }
            if(format == "text")
            {
                return Content(string.Join(",", students.Select(x => x.FirstName)));
            }
            if(format == "json") 
            {
                return Json(students, JsonRequestBehavior.AllowGet);
            }
            if(format == "file")
            {
                return File("/content/04_Controller.pdf", "application/pdf", "controllers.pdf");
            }
            if(format == "notFound")
            {
                return HttpNotFound("Sorry");
            }
            return RedirectToAction("index", "Home", new { howIGetHere = "You were redirected" });

            //return Redirect("/");
            //return View(students);
            //return Content(string.Join(",", students.Select(x => x.FirstName)));
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            return View(students.Find(x => x.ID == id));
        }

        [HttpPost]
        public ActionResult Edit(StudentModel model)
        {
            var student = students.Find(x => x.ID == model.ID);

            student.FirstName = model.FirstName;
            student.LastName = model.LastName;
            student.FavoriteFood = model.FavoriteFood;

            return RedirectToAction("Index", new { edited = true });
        }

        //[HttpPost]
        //public ActionResult Edit(FormCollection form)
        //{
        //    var student = students.Find(x => x.ID.ToString() == form["ID"]);

        //    student.FirstName = form["FirstName"];
        //    student.LastName = form["LastName"];
        //    student.FavoriteFood = form["FavoriteFood"];

        //    return RedirectToAction("Index", new { edited = true });
        //}

        [ActionName("Coffee")]
        public ActionResult GetMoreCoffee()
        {
            return Json("I'm going to get more coffee", JsonRequestBehavior.AllowGet);
        }
    }
}