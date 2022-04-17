
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CUCMarca;
using System.Configuration;
using CUCMarca.WebSite.Models;

using Newtonsoft.Json;

using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Login = CUCMarca.WebSite.Models.Login;
using System.Reflection;

namespace CUCMarca.WebSite.Controllers
{
    public class LoginController : Controller
    {
        //twst
        // GET: Login
        public ActionResult Index(int modulo = 0)
        {
            ViewBag.modulo = modulo;
            Session["user"] = "";
            Session["pass"] = "";
            Session["token"] = "";
            Session["admin"] = "";

            ViewBag.url = ConfigurationManager.AppSettings["APIurl"];


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Login login)
        {
            ViewBag.url = ConfigurationManager.AppSettings["APIurl"];
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Verifique los datos");
                return View(login);
            }
            try {
                if (validarUsuario(login))
                {
                    Session["user"] = login.identificacion.Trim();
                    Session["pass"] = login.Contraseña;
                    bool admin = validarAdmin(login.identificacion.Trim());
                    Session["admin"] = admin;
                    
                    int mod = login.Modulo;
                    if (mod == 1)
                    {
                        return RedirectToAction("PassChange", "Login");
                    }
                    else if (mod == 2)
                    {
                        return RedirectToAction("Index", "Justificar");
                    }
                    else if (mod == 3 && admin)
                    {
                        return RedirectToAction("Index", "Aprobar");
                    }
                    else if (mod == 4)
                    {
                        return RedirectToAction("Index", "Exepcion");
                    }
                    else
                    {
                        return View(login);
                    }
                    
                }
                else {
                    ModelState.AddModelError("", "Usuario y Contraseña incorrectos");
                    return View(login);
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError("", "Verifique los datos");
                return View(login);
            }
            
        }
        public ActionResult PassChange() {

            ViewBag.url = ConfigurationManager.AppSettings["APIurl"];

            string user = (string)Session["user"];
            string pass = (string)Session["pass"];

            if ((user == "") || (pass == "") || (user == null)||(pass==null)){
                return RedirectToAction("Index", "Login", new { modulo = 1 });
            }
            else{
                return View();
            }


            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PassChange(Clave clave)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Verifique los datos");
                return View(clave);
            }

            string user = (string)Session["user"];


            ViewBag.url = ConfigurationManager.AppSettings["APIurl"];

            if (cambiarclave(user.Trim(), clave)){

                TempData["Success"] = "Modificacion correcta";
                return View(new Clave());
            }
            else {


                ModelState.AddModelError("", "Datos no cambiados");
                return View(clave);
            }

            
        }

        public ActionResult Olvido() {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Olvido(Login login)
        {
           
          


            ViewBag.url = ConfigurationManager.AppSettings["APIurl"];

            if (sendmail(login.identificacion.Trim()))
            {

                TempData["Success"] = "Correo enviado correctamente";
                return RedirectToAction("Index", "Login", login);
            }
            else
            {


                ModelState.AddModelError("", "Datos no cambiados");
                return View(login);
            }


        }


        public bool cambiarclave(string user, Clave clave) {

            string url = ConfigurationManager.AppSettings["APIurl"] + "/api/Login?id=" + user + "&pass=" + clave.Contrasena + "&newpass="+ clave.newContrasena;

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
                else if (message.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //Console.WriteLine("Respuesta obtenida {0}", response.StatusCode);
                    //string s = response.Content.ReadAsStringAsync();
                    var task2 = Task<string>.Run(async () =>
                    {
                        return await message.Content.ReadAsStringAsync();
                    });
                    string resultStr = task2.Result;
                    Session["token"] = resultStr;
                    res = true;
                }
                else
                {
                    res = false;
                }
            }

            return res;
        }

        public bool validarUsuario(Login usuario)
        {
            string url = ConfigurationManager.AppSettings["APIurl"] + "/api/Login/";

            string objeto = usuario.ToJsonString();

            bool res = false;

            using (var client = new HttpClient())
            {
                var task = Task.Run(async () =>
                {
                    return await client.PostAsync(url,
                        new StringContent (objeto, Encoding.UTF8, "application/json")
                        );
                });
                HttpResponseMessage message = task.Result;
                if (message.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    res = false;
                }
                else if (message.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //Console.WriteLine("Respuesta obtenida {0}", response.StatusCode);
                    //string s = response.Content.ReadAsStringAsync();
                    var task2 = Task<string>.Run(async () =>
                    {
                        return await message.Content.ReadAsStringAsync();
                    });
                    string resultStr = task2.Result;
                    Session["token"] = resultStr;
                    res = true;
                }
                else
                {
                    res = false;
                }
            }

            return res;
        }

        public string validarUsuarioN(Login usuario)
        {
            string url = ConfigurationManager.AppSettings["APIurl"] + "/api/Login/";

            string objeto = usuario.ToJsonString();

            string res = "";

            using (var client = new HttpClient())
            {
                var task = Task.Run(async () =>
                {
                    return await client.PostAsync(url,
                        new StringContent(objeto, Encoding.UTF8, "application/json")
                        );
                });
                HttpResponseMessage message = task.Result;
                if (message.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    res = "";
                }
                else if (message.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //Console.WriteLine("Respuesta obtenida {0}", response.StatusCode);
                    //string s = response.Content.ReadAsStringAsync();
                    var task2 = Task<string>.Run(async () =>
                    {
                        return await message.Content.ReadAsStringAsync();
                    });
                    string resultStr = task2.Result;
                    //Session["token"] = resultStr;
                    res =resultStr;
                }
                else
                {
                    res = "";
                }
            }

            return res;
        }
        public bool validarAdmin(string user)
        {
            string url = ConfigurationManager.AppSettings["APIurl"] + "/api/Jefes?id=" + user ;

            bool res = false;

            using (var client = new HttpClient())
            {
                var task = Task.Run(async () =>
                {
                    return await client.GetAsync(url);
                });
                HttpResponseMessage message = task.Result;
                if (message.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    res = false;
                }
                else if (message.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    res = true;
                }
                else
                {
                    res = false;
                }
            }

            return res;
        }

        public bool sendmail(string id) {

            string url = ConfigurationManager.AppSettings["APIurl"] + "/api/RecuperarPass?id=" + id;

            bool res = false;

            using (var client = new HttpClient())
            {
                var task = Task.Run(async () =>
                {
                    return await client.GetAsync(url);
                });
                HttpResponseMessage message = task.Result;
                if (message.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    res = false;
                }
                else if (message.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //Console.WriteLine("Respuesta obtenida {0}", response.StatusCode);
                    //string s = response.Content.ReadAsStringAsync();
                    var task2 = Task<string>.Run(async () =>
                    {
                        return await message.Content.ReadAsStringAsync();
                    });
                    string resultStr = task2.Result;
                    //Session["token"] = resultStr;
                    res = true;
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