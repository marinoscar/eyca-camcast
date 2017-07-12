using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eyca.web.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
            ViewBag.HomeUrl = "/Home/Index";
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Thanks(int src)
        {
            var url = "/Home/Index";
            if (src == 1) url = "/Contact/Home";
            if (src == 2) url = "/Invoice/Home";
            ViewBag.HomeUrl = url;
            return View();
        }
    }
}