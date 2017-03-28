using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodingTemple.BoardGameStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HandleError(View="Error")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            throw new Exception("This page is broken");
            return View();
        }

        [OutputCache(Duration = 3)]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}