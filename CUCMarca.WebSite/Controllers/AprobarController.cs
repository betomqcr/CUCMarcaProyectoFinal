using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CUCMarca.WebSite.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CUCMarca.DataAccess;

namespace CUCMarca.WebSite.Controllers
{
    public class AprobarController : Controller
    {
        // GET: Aprobar
        public ActionResult Index()
        {
            string user = (string)Session["user"];
            string pass = (string)Session["pass"];

            if ((user == "") || (pass == "") || (user == null) || (pass == null))
            {
                return RedirectToAction("Index", "Login", new { modulo = 3 });
            }
            else
            {

                ViewBag.id = Session["user"];
                List<AprobarM> lista = getJustificaciones(user);
                if(lista == null)
                {
                    ViewBag.lista = new List<AprobarM>(); 
                }
                else
                {
                    ViewBag.lista = lista;
                }
                ViewBag.url = ConfigurationManager.AppSettings["APIurl"];
                ViewBag.admin = Session["admin"];
                return View(new AprobarM());
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(AprobarM aprobarM)
        {
            return View();
        }

        public List<AprobarM> getJustificaciones(string id) {

            List<AprobarM> lista = new List<AprobarM>();

            string url = ConfigurationManager.AppSettings["APIurl"] + "/api/Justificacions?id=" + id;


            using (var client = new HttpClient())
            {
                var task = Task.Run(async () =>
                {
                    return await client.GetAsync(url);
                });
                HttpResponseMessage message = task.Result;
                if (message.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    lista = null;
                }
                else if (message.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //Console.WriteLine("Respuesta obtenida {0}", response.StatusCode);
                    //string s = response.Content.ReadAsStringAsync();
                    var task2 = Task<string>.Run(async () =>
                    {
                        return await message.Content.ReadAsStringAsync();
                    });
                    lista = JsonConvert.DeserializeObject<List<AprobarM>>(task2.Result);

                }
                else
                {
                    lista = null;
                }
            }
            return lista;
        }


    }
}