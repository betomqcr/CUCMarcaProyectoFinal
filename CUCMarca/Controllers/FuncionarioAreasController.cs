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
    public class FuncionarioAreasController : ApiController
    {
        private CUCMarcaEntities db = new CUCMarcaEntities();

        Bitacora bitacora = new Bitacora();

        // GET: api/FuncionarioAreas/5

        [ResponseType(typeof(FuncionarioArea))]
        [HttpGet]
        public IHttpActionResult GetFuncionarioArea(string id)
        {
            int idf = 0;
            try
            {
                var listarf = db.Funcionario.Select(x => x.Identificacion == id);
                int cantif = listarf.Count();
                if (cantif == 0)
                {
                    bitacora.accion = "GetFuncionarioArea";
                    bitacora.descripcion = "El usuario no existe";
                    bitacora.tipo = "Error";
                    bitacora.usuario = id;
                    bitacora.ins_bitacora();
                    return NotFound();
                }
                else
                {
                    if (db.Funcionario.FirstOrDefault(x => x.Identificacion == id) != null)
                    {
                        idf = db.Funcionario.FirstOrDefault(x => x.Identificacion == id).FuncionarioID;
                        List<FuncionarioArea> funcionarioArea = db.FuncionarioArea.Where(x => x.FuncionarioID == idf).ToList();
                        if (funcionarioArea == null)
                        {
                            bitacora.accion = "GetFuncionarioArea";
                            bitacora.descripcion = "El usuario no existe";
                            bitacora.tipo = "Error";
                            bitacora.usuario = id;
                            bitacora.ins_bitacora();
                            return NotFound();
                        }
                        List<Respuesta> res = new List<Respuesta>();
                        foreach (FuncionarioArea fa in funcionarioArea)
                        {
                            Respuesta datos = new Respuesta();
                            datos.CodigoFuncionario = fa.CodigoFuncionario;
                            datos.NombreArea = fa.Area.Nombre;
                            datos.id = fa.AreaID;
                            res.Add(datos);
                        }

                        return Ok(res);

                    }
                    else
                    {
                        bitacora.accion = "GetFuncionarioArea";
                        bitacora.descripcion = "El usuario no existe";
                        bitacora.tipo = "Error";
                        bitacora.usuario = id;
                        bitacora.ins_bitacora();
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                bitacora.accion = "GetFuncionarioArea";
                bitacora.descripcion = "Error interno: " + ex.Message;
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

        private bool FuncionarioAreaExists(string id)
        {
            return db.FuncionarioArea.Count(e => e.CodigoFuncionario == id) > 0;
        }
    }

    public class Respuesta
    {
        public string CodigoFuncionario { get; set; }
        public string NombreArea { get; set; }

        public int id { get; set; }



    }
}