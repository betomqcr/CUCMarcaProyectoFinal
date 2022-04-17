using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CUCMarca.Models;

namespace CUCMarca.Controllers
{
    public class JefesController : ApiController
    {
        private CUCMarcaEntities db = new CUCMarcaEntities();

        C_Token token = new C_Token();
        Bitacora bitacora = new Bitacora();
        // GET: api/Jefes/5
        [ResponseType(typeof(Funcionario))]
        public IHttpActionResult GetFuncionario(string id)
        {
            try
            {
                Funcionario funcionario = db.Funcionario.FirstOrDefault(x => x.Identificacion == id);
                if (funcionario == null)
                {
                    bitacora.accion = "GetJefes";
                    bitacora.descripcion = "El usuario no existe";
                    bitacora.fecha = DateTime.Now;
                    bitacora.tipo = "Error";
                    bitacora.usuario = id;
                    bitacora.ins_bitacora();
                    return NotFound();
                }
                else
                {
                    int cant = funcionario.Area.Count;
                    if (cant > 0)
                    {
                        return Ok();
                    }
                    else
                    {
                        bitacora.accion = "GetJefes";
                        bitacora.descripcion = "El usuario no existe";
                        bitacora.fecha = DateTime.Now;
                        bitacora.tipo = "Error";
                        bitacora.usuario = id;
                        bitacora.ins_bitacora();
                        return NotFound();
                    }
                }
                
            }
            catch (Exception ex)
            {
                bitacora.accion = "GetJefes";
                bitacora.descripcion = "Error interno: " + ex.Message;
                bitacora.fecha = DateTime.Now;
                bitacora.tipo = "Error";
                bitacora.usuario = id;
                bitacora.ins_bitacora();
                return InternalServerError(ex);
            }
            
        }

       

        // POST: api/Jefes
        [ResponseType(typeof(Funcionario))]
        public IHttpActionResult PostFuncionario(bool aprobar, int idjustificacion, int idFuncionario , bool excepcion)
        {
            try
            {
                DateTime fechaaprobacion = DateTime.Now;
                string Estado = "";
                int est = 0;
                Justificacion j = new Justificacion();
                Excepcion e = new Excepcion();
                if (!excepcion)
                {
                    j = db.Justificacion.FirstOrDefault(x => x.JustificacionID == idjustificacion);
                }
                else
                {
                    e = db.Excepcion.FirstOrDefault(x => x.ExcepcionID == idjustificacion);
                }

                if (aprobar)
                {
                    est = 3;
                    Estado = "Autorizada";
                }
                else
                {
                    est = 4;
                    Estado = "Rechazada";
                }
                

                if (j == null)
                {
                    bitacora.accion = "PostFuncionario";
                    bitacora.descripcion = "No se encontraron justificaciones";
                    bitacora.fecha = DateTime.Now;
                    bitacora.tipo = "Error";
                    bitacora.usuario = idFuncionario.ToString();
                    bitacora.ins_bitacora();
                    return NotFound();
                }
                else
                {
                    if (excepcion)
                    {
                        e.AutorizadoPor = idFuncionario;
                        e.FechaAutorizacion = fechaaprobacion;
                        e.Estado = 2;
                       
                    }
                    else
                    {
                        j.AutorizadoPor = idFuncionario;
                        j.FechaAutorizacion = fechaaprobacion;
                        j.Estado = Estado;
                        j.Inconsistencia.Estado = (byte)est;
                    }
                    db.SaveChanges();
                    bitacora.accion = "PostFuncionario";
                    bitacora.descripcion = "El estado de la justificación y/o Exepción ha sido cambiado correctamente";
                    bitacora.fecha = DateTime.Now;
                    bitacora.tipo = "Accion";
                    bitacora.usuario = idFuncionario.ToString();
                    bitacora.ins_bitacora();

                    return Ok("Listo");
                }

            }
            catch (Exception ex)
            {
                bitacora.accion = "PostFuncionario";
                bitacora.descripcion = "Error interno: " + ex.Message;
                bitacora.fecha = DateTime.Now;
                bitacora.tipo = "Accion";
                bitacora.usuario = idFuncionario.ToString();
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