using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CUCMarca.Models;
using CUCMarca.BusinessServices;
using System.Configuration;

namespace CUCMarca.Controllers
{
    public class RecuperarPassController : ApiController
    {
        private CUCMarcaEntities db = new CUCMarcaEntities();
        private SendMail mails = new SendMail();
        private Bitacora bitacora = new Bitacora();
        // GET: api/RecuperarPass
        public IHttpActionResult GetFuncionario(string id)
        {
            try
            {
                var cont = db.Funcionario.FirstOrDefault(x => x.Identificacion == id);
                if (cont != null)
                {
                    SendMailResponse res = new SendMailResponse();

                    string pass = UtilsService.RandomPassword(10);
                    cont.Contrasena = pass;
                    db.SaveChanges();
                     
                    string m = cont.Correo;
                    String mensaje = " <HTML><BODY> Se proceso una solicitud de olvido de contraseña. <br> La contraseña temporal del usuario " + id + " es : " + pass +
                        " <br> Para cambiar su contraseña favor ingrese a <a href='" + ConfigurationManager.AppSettings["APIurl"] + "Login?modulo=1' >Cambio de clave </a>" +
                    " <br> Si usted no realizó esta solicitud por favor" +
                        " cambie su contraseña de inmediato.</BODY></HTML>";
                   
                    res = mails.SendEmail(mensaje, "Recuperacion de clave de Sistema de Marca", m, true);
                    if (res.Result == SendMailResult.Success)
                    {
                        bitacora.accion = "Recupera Clave";
                        bitacora.descripcion = "Se proceso una solicitud de recuperacion de clave";
                        bitacora.fecha = DateTime.Now;
                        bitacora.tipo = "Accion";
                        bitacora.usuario = id;
                        bitacora.ins_bitacora();
                        return Ok("Se envió un mensaje a su correo con la clave solicitada.");
                    }
                    else {
                        bitacora.accion = "Recupera Clave";
                        bitacora.descripcion = "No se pudo enviar el correo";
                        bitacora.fecha = DateTime.Now;
                        bitacora.tipo = "Error";
                        bitacora.usuario = id;
                        bitacora.ins_bitacora();
                        return InternalServerError(new Exception("Se presentaron error al enviar el correo." ));
                    }

                }
                else
                {
                    bitacora.accion = "Recupera Clave";
                    bitacora.descripcion = "El usuario solicitado no existe en el sistema.";
                    bitacora.fecha = DateTime.Now;
                    bitacora.tipo = "Error";
                    bitacora.usuario = id;
                    bitacora.ins_bitacora();
                    return NotFound();
                }
            }
            catch (Exception ex) {
                bitacora.accion = "Recupera Clave";
                bitacora.descripcion = "SE presentaron errores la realizar la recuperacion: " + ex.Message;
                bitacora.fecha = DateTime.Now;
                bitacora.tipo = "Error";
                bitacora.usuario = id;
                bitacora.ins_bitacora();
                return InternalServerError(ex);
            }
            
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FuncionarioExists(int id)
        {
            return db.Funcionario.Count(e => e.FuncionarioID == id) > 0;
        }
    }
}