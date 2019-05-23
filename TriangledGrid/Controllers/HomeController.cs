using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TriangledGrid.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            var allVertics = ValuesController.StaticGrid.GetVerticesForTriangle("d3");
            var id = ValuesController.StaticGrid.GetTriangleIdFromRowColumn(4, 3);
            return View();
        }
    }
}
