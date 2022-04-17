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
using System.Net.Http.Headers;

namespace CUCMarca.WebSite.Controllers
{
    public class AsistenciaController : Controller
    {
        // GET: Asistencia
        public ActionResult Index()
        {
            ViewBag.url = ConfigurationManager.AppSettings["APIurl"];
            ViewBag.Hora = DateTime.Now;
            return View(new Asistencia()
            {
                DireccionIP = "0.0.0.0",
                Latitud = 0,
                Longitud = 0
            });
        }


        Asistencia asis = new Asistencia();
        




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Asistencia asistencia)
        {
            ViewBag.Hora = DateTime.Now;
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Verifique los datos");
                return View(asistencia);
            }
            try
            {
                CUCMarca.WebSite.Controllers.LoginController login = new LoginController();
                Models.Login usuario = new Login()

                { identificacion = asistencia.CodigoFuncionario,
                    Contraseña = asistencia.Contrasena,
                    Modulo = 0
                };


               string token= login.validarUsuarioN(usuario).ToString();
               
                string url = ConfigurationManager.AppSettings["URL_MARCA"];
                Marca marca = SendMarca2(asistencia, url + "?codigo=" + asistencia.CodigoFuncionario, token);
                if (marca.CodigoRespuesta == 0)
                {
                    TempData["Success"] = "Marca realizada exitosamente a las " + marca.FechaMarca + " " + marca.Mensaje;
                    return View(new Asistencia());
                }
                else if (marca.CodigoRespuesta == -2)
                {
                    ModelState.AddModelError("", marca.Mensaje);
                    return View(asistencia);
                }
                else
                {
                    ModelState.AddModelError("", marca.Mensaje);
                    return View(asistencia);
                }
            }
            catch (Exception exc)
            {
                ModelState.AddModelError("", exc.Message);
                return View(asistencia);
            }
        }

        private Marca SendMarca(Asistencia asistencia, string url, string token)
        {
            string json = asistencia.ToJsonString();
            
            //Console.WriteLine(json);
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json;");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token );
                var task = Task.Run(async () =>
                {
                    StringContent content= new StringContent(json);
                    content.Headers.ContentType.CharSet = "";
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    return await client.PostAsync(url,content);
                }); 
                HttpResponseMessage message = task.Result;
                if (message.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new Marca()
                    {
                        CodigoRespuesta = -2,
                        Mensaje = "Usuario/Contraseña desconocidos."
                    };
                }
                else if (message.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    //Console.WriteLine("Respuesta obtenida {0}", response.StatusCode);
                    //string s = response.Content.ReadAsStringAsync();
                    var task2 = Task<string>.Run(async () =>
                    {
                        return await message.Content.ReadAsStringAsync();
                    });
                    string resultStr = task2.Result;
                    Marca result = JsonConvert.DeserializeObject<Marca>(resultStr);
                    result.CodigoRespuesta = 0;
                    return result;
                }
                else if (message.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    //Console.WriteLine("Respuesta obtenida {0}", response.StatusCode);
                    //string s = response.Content.ReadAsStringAsync();
                    var task2 = Task<string>.Run(async () =>
                    {
                        return await message.Content.ReadAsStringAsync();
                    });
                    string resultStr = task2.Result;
                    //Marca result = JsonConvert.DeserializeObject<Marca>(resultStr);
                    //result.CodigoRespuesta = 0;
                    //return result;
                    return new Marca()
                    {
                        CodigoRespuesta = -1,
                        Mensaje = "Ha ocurrido un error en la aplicación intente más tarde."
                    };
                }
                else
                {
                    return new Marca()
                    {
                        CodigoRespuesta = -1,
                        Mensaje = "Ha ocurrido un error en la aplicación intente más tarde."
                    };
                }
            }
        }

        private Marca SendMarca2(Asistencia asistencia, string url, string token)
        {
            string json = asistencia.ToJsonString();

            Console.WriteLine(json);
            using (var client = new HttpClient())
            {

                var task = Task.Run(async () =>
                {
                   
                    return await client.PostAsync(
                                url,

                    new StringContent(json, Encoding.UTF8, "application/json"));
                });
                HttpResponseMessage message = task.Result;
                if (message.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new Marca()
                    {
                        CodigoRespuesta = -2,
                        Mensaje = "Usuario/Contraseña desconocidos."
                    };
                }
                else if (message.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    //Console.WriteLine("Respuesta obtenida {0}", response.StatusCode);
                    //string s = response.Content.ReadAsStringAsync();
                    var task2 = Task<string>.Run(async () =>
                    {
                        return await message.Content.ReadAsStringAsync();
                    });
                    string resultStr = task2.Result;
                    Marca result = JsonConvert.DeserializeObject<Marca>(resultStr);
                    result.CodigoRespuesta = 0;
                    return result;
                }
                else if (message.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    //Console.WriteLine("Respuesta obtenida {0}", response.StatusCode);
                    //string s = response.Content.ReadAsStringAsync();
                    var task2 = Task<string>.Run(async () =>
                    {
                        return await message.Content.ReadAsStringAsync();
                    });
                    string resultStr = task2.Result;
                    //Marca result = JsonConvert.DeserializeObject<Marca>(resultStr);
                    //result.CodigoRespuesta = 0;
                    //return result;
                    return new Marca()
                    {
                        CodigoRespuesta = -1,
                        Mensaje = "Ha ocurrido un error en la aplicación intente más tarde."
                    };
                }
                else
                {
                    return new Marca()
                    {
                        CodigoRespuesta = -1,
                        Mensaje = "Ha ocurrido un error en la aplicación intente más tarde."
                    };
                }
            }
        }
        }
}