using CUCMarca.WebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Configuration;

namespace CUCMarca.WebSite.Controllers
{
    public class ExepcionController : Controller
    {
        // GET: Exepcion
        public ActionResult Index()
        {
            string user = (string)Session["user"];
            string pass = (string)Session["pass"];

            if ((user == "") || (pass == "") || (user == null) || (pass == null))
            {
                return RedirectToAction("Index", "Login", new { modulo = 4 });
            }
            else
            {
                ViewBag.url = ConfigurationManager.AppSettings["APIurl"];
                ViewBag.user = Session["user"];
                ViewBag.admin = Session["admin"];
                return View(new Exepcion());
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Exepcion ex)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Verifique los datos");
                return View();
            }

            string user = (string)Session["user"];


            ViewBag.url = ConfigurationManager.AppSettings["APIurl"];
            ViewBag.user = Session["user"];
            ViewBag.admin = Session["admin"];

            if (sendExcepcion(user, ex))
            {

                TempData["Success"] = "Excepción creada correctamente";
                return View(new Exepcion());
            }
            else
            {


                ModelState.AddModelError("", "Excepción no creada");
                return View(ex);
            }


        }


        public bool sendExcepcion(string user, Exepcion ex)
        {
            string fecharepo = "";
            if (ex.TipoRepone.ToLower() != "true")
            {
                fecharepo = getFechaformat(DateTime.Now);
            }
            else
            {
                fecharepo = getFechaformat(ex.FechaReposicion);
            }

            string url = ConfigurationManager.AppSettings["APIurl"] + "/api/Excepcions?id=" + user + "&FechaExcepcion=" + getFechaformat(ex.FechaExcepcion) +
                "&ReponeTiempo=" + ex.TipoRepone + "&FechaReposicion=" + fecharepo + "&Observaciones=" + ex.Observaciones + "&Motivo=" + ex.MotivoID;

            bool res = false;

            using (var client = new HttpClient())
            {
                var task = Task.Run(async () =>
                {
                    return await client.PostAsync(url,
                        new StringContent("", Encoding.UTF8, "application/json"));
                });
                HttpResponseMessage message = task.Result;
                if (message.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    res = false;
                }
                else if (message.StatusCode == System.Net.HttpStatusCode.Created)
                {
                  
                    var task2 = Task<string>.Run(async () =>
                    {
                        return await message.Content.ReadAsStringAsync();
                    });

                    res = true;
                }
                else
                {
                    res = false;
                }
            }

            return res;
        }

        public string getFechaformat(DateTime j)
        {
            string fechajusto = "";

            string formtdate = ConfigurationManager.AppSettings["formatdate"];
            if (formtdate.ToLower() == "dmy")
            {
                fechajusto = j.Day + "-" + j.Month + "-" + j.Year;
            }

            if (formtdate.ToLower() == "mdy")
            {
                fechajusto = j.Month + "-" + j.Day + "-" + j.Year;
            }
            if (formtdate.ToLower() == "ymd")
            {
                fechajusto = j.Year + "-" + j.Month + "-" + j.Day;
            }

            return fechajusto;
        }



    }
}