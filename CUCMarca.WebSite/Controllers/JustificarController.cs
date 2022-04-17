using CUCMarca.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using CUCMarca.WebSite.Models;

namespace CUCMarca.WebSite.Controllers
{
    public class JustificarController : Controller
    {
        // GET: Justificar
        public ActionResult Index()
        {
            string user = (string)Session["user"];
            string pass = (string)Session["pass"];
            
            if ((user == "") || (pass == "") || (user == null) || (pass == null))
            {
                return RedirectToAction("Index", "Login", new { modulo = 2 });
            }
            else
            {
                ViewBag.id = Session["user"];
                ViewBag.lista = getinconsistencias(Session["user"].ToString());
                if(ViewBag.lista == null)
                {
                    ViewBag.lista = new List<ResultadoI>();
                }
                ViewBag.url = ConfigurationManager.AppSettings["APIurl"];
                ViewBag.admin = Session["admin"];
                return View(new JustificarM());
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(JustificarM justi)
        {
            ViewBag.id = Session["user"];
            ViewBag.lista = getinconsistencias(Session["user"].ToString());

            if (justificarincon(justi))
            {
                TempData["Success"] = "Se justificó correctamente. Espere para su aprobación.";
            }

            return View(justi);
        }
        public List<ResultadoI> getinconsistencias(string id) {
            List<ResultadoI> lista = new List<ResultadoI>();

            string url = ConfigurationManager.AppSettings["APIurl"] + "/api/Inconsistencias?id=" + id;

            
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
                    lista = JsonConvert.DeserializeObject<List<ResultadoI>>(task2.Result);
                    
                }
                else
                {
                    lista = null;
                }
            }

            return lista;
        }

       public bool justificarincon(JustificarM j)
        {
            bool res = false;

            if (!j.ReponeTiempo) { j.FechaReposicion = DateTime.Now; }

            string formtdate = ConfigurationManager.AppSettings["formatdate"];
            string fechajusto = "";
            if(formtdate.ToLower() == "dmy")
            {
                fechajusto = j.FechaReposicion.Day + "-" + j.FechaReposicion.Month + "-" + j.FechaReposicion.Year;
            }

            if (formtdate.ToLower() == "mdy")
            {
                fechajusto = j.FechaReposicion.Month + "-" + j.FechaReposicion.Day + "-" + j.FechaReposicion.Year;
            }
            if (formtdate.ToLower() == "ymd")
            {
                fechajusto = j.FechaReposicion.Year+ "-" + j.FechaReposicion.Month + "-" + j.FechaReposicion.Day;
            }
            string url = ConfigurationManager.AppSettings["APIurl"] + "/api/Justificacions?Identificacion=" + Session["user"] + "&InconsistenciaID=" + j.CodigoInconsistencia;
            url = url + "&CodigoFuncionario=" + j.CodigoFuncionario + "&ReponeTiempo=" + j.ReponeTiempo + "&FechaReposicion=" + fechajusto + "&Observaciones=" + j.Observacion + "&MotivoID=" + j.idMotivo;




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
                    //Console.WriteLine("Respuesta obtenida {0}", response.StatusCode);
                    //string s = response.Content.ReadAsStringAsync();
                    var task2 = Task<string>.Run(async () =>
                    {
                        return await message.Content.ReadAsStringAsync();
                    });
                    res = true;
                    Session["token"] = task2.Result;
                    
                }
                else
                {
                    res = false;
                }
            }

            return res;
        }
    }
}