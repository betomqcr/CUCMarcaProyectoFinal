using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CUCMarca.WebSite.Controllers
{
    public class ReporteDistanciaController : Controller
    {
        // GET: ReporteDistancia
        public ActionResult Index()
        {
            ViewBag.url = ConfigurationManager.AppSettings["APIurl"];
            ViewBag.lat = ConfigurationManager.AppSettings["latitud"];
            ViewBag.lon = ConfigurationManager.AppSettings["longitud"];
            ViewBag.key = ConfigurationManager.AppSettings["googlekey"];
            return View();
        }
    }
}