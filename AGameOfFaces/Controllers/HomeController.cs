using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AGameOfFaces.Core.Services;

namespace AGameOfFaces.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            ViewBag.Modes = GameService.Modes;
            return View();
        }
    }
}
