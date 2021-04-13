using SoLienLacDienTu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DO_AN_Thu_nghiem.Controllers
{
    public class HomeController : Controller
    {
        //DataClassesDataContext db = new DataClassesDataContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {


            return View();
        }

        public ActionResult PH()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
      
    }
}