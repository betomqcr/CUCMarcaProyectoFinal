using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CUCMarca.Models;


namespace CUCMarca.Controllers
{
    public class InconsistenciasController : ApiController
    {
        private CUCMarcaEntities db = new CUCMarcaEntities();
        Bitacora bitacora = new Bitacora();

        // GET: api/Inconsistencias/5
        [ResponseType(typeof(Inconsistencia))]
        public IHttpActionResult GetInconsistencia(string id)
        {
            try
            {

                if (id.Length > 20)
                {
                    bitacora.accion = "GetInconsistencias";
                    bitacora.descripcion = "El id no tiene el tamaño correcto";
                    bitacora.fecha = DateTime.Now;
                    bitacora.tipo = "Error";
                    bitacora.usuario = id;
                    bitacora.ins_bitacora();
                    return NotFound();

                }
                else 
                {
                    if(db.Funcionario.FirstOrDefault(x => x.Identificacion == id) != null)
                    {
                        int codigof = db.Funcionario.FirstOrDefault(x => x.Identificacion == id).FuncionarioID;


                        List<FuncionarioArea> listaf = db.FuncionarioArea.Where(x => x.FuncionarioID == codigof).ToList();
                        List<ResultadoI> listai = new List<ResultadoI>();
                        foreach (FuncionarioArea fa in listaf)
                        {
                            List<Inconsistencia> l = db.Inconsistencia.Where(x => x.CodigoFuncionario == fa.CodigoFuncionario).ToList();
                            foreach (Inconsistencia inc in l)
                            {
                                ResultadoI r = new ResultadoI();

                                
                                r.CodigoFuncionario = inc.CodigoFuncionario;
                                r.AreaID = inc.FuncionarioArea.AreaID;
                                r.Estado = inc.Estado;
                                r.FechaInconsistencia = inc.FechaInconsistencia;
                                r.HorarioID = inc.HorarioID;
                                r.identificacion = id;
                                r.InconsistenciaID = inc.InconsistenciaID;
                                r.NombreArea = inc.FuncionarioArea.Area.Nombre;
                                r.TipoInconsistenciaID = inc.TipoInconsistenciaID;
                                r.TipoInconsistencia = inc.TipoInconsistencia.Nombre;

                                listai.Add(r);
                            }
                        }

                        if (listai.Count == 0)
                        {
                            return NotFound();
                            bitacora.accion = "GetInconsistencias";
                            bitacora.descripcion = "No se encontraron registros del usuario";
                            bitacora.fecha = DateTime.Now;
                            bitacora.tipo = "Error";
                            bitacora.usuario = id;
                            bitacora.ins_bitacora();
                        }

                        return Ok(listai);
                    }
                        
                    else
                    {
                        bitacora.accion = "GetInconsistencias";
                        bitacora.descripcion = "Error con la Inconsistencia";
                        bitacora.fecha = DateTime.Now;
                        bitacora.tipo = "Error";
                        bitacora.usuario = id;
                        bitacora.ins_bitacora();
                       
                        return NotFound();
                    }
                }
            }
            catch(Exception ex)
            {
                bitacora.accion = "GetInconsistencias";
                bitacora.descripcion = "Error interno: " + ex.Message;
                bitacora.fecha = DateTime.Now;
                bitacora.tipo = "Error";
                bitacora.usuario = id;
                bitacora.ins_bitacora();
                return NotFound();
                return InternalServerError(ex);
            }

        }
        // GET: api/Inconsistencias/7
        [ResponseType(typeof(Inconsistencia))]
        public IHttpActionResult GetInconsistencias(int idinconsistencia)
        {
            try
            {
                Inconsistencia inc = db.Inconsistencia.FirstOrDefault(x => x.InconsistenciaID == idinconsistencia);

                if (inc == null)
                {
                    bitacora.accion = "GetInconsistencias";
                    bitacora.descripcion = "Inconsistencia no encontrada";
                    bitacora.fecha = DateTime.Now;
                    bitacora.tipo = "Error";
                    bitacora.usuario = inc.CodigoFuncionario;
                    bitacora.ins_bitacora();
                    return NotFound();
                }
                else
                {
                    ResultadoI res = new ResultadoI();

                    res.CodigoFuncionario = inc.CodigoFuncionario;
                    res.AreaID = inc.FuncionarioArea.AreaID;
                    res.Estado = inc.Estado;
                    res.FechaInconsistencia = inc.FechaInconsistencia;
                    res.HorarioID = inc.HorarioID;
                    res.identificacion = inc.FuncionarioArea.Funcionario.Identificacion;
                    res.InconsistenciaID = inc.InconsistenciaID;
                    res.NombreArea = inc.FuncionarioArea.Area.Nombre;
                    res.TipoInconsistencia = inc.TipoInconsistencia.Nombre;
                    res.TipoInconsistenciaID = inc.TipoInconsistenciaID;

                    return Ok(res);
                }

            }catch (Exception ex)
            {
                bitacora.accion = "GetInconsistencias";
                bitacora.descripcion = "Error interno: " + ex.Message;
                bitacora.fecha = DateTime.Now;
                bitacora.tipo = "Error";
                bitacora.usuario = "Usuario no capturado";
                bitacora.ins_bitacora();
                return InternalServerError(ex);
            }
        }
        // PUT: api/Inconsistencias/5
        [ResponseType(typeof(Inconsistencia))]
        public IHttpActionResult PutInconsistencia(int id)
        {
            if (!ModelState.IsValid)
            {
                bitacora.accion = "PutInconsistencias";
                bitacora.descripcion = "El modelo no es valido";
                bitacora.fecha = DateTime.Now;
                bitacora.tipo = "Error";
                bitacora.usuario = "Usuario no capturado";
                bitacora.ins_bitacora();
                return BadRequest("Not a valid model");

            }

            try
            {
                Inconsistencia inc = db.Inconsistencia.FirstOrDefault(x => x.InconsistenciaID == id);

                if (inc != null)
                {
                    inc.Estado = 4;
                    db.SaveChanges();
                    bitacora.accion = "PostInconsistencias";
                    bitacora.descripcion = "Inconsistencia modificada corrrectamente";
                    bitacora.fecha = DateTime.Now;
                    bitacora.tipo = "Accion";
                    bitacora.usuario = inc.CodigoFuncionario;
                    bitacora.ins_bitacora();
                    return Ok(inc);
                }
                else
                {
                    bitacora.accion = "PutInconsistencias";
                    bitacora.descripcion = "La inconsistencia no existe";
                    bitacora.fecha = DateTime.Now;
                    bitacora.tipo = "Error";
                    bitacora.usuario = inc.CodigoFuncionario;
                    bitacora.ins_bitacora();
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                bitacora.accion = "PostInconsistencias";
                bitacora.descripcion = "Error interno" + ex.Message;
                bitacora.fecha = DateTime.Now;
                bitacora.tipo = "Error";
                bitacora.usuario = "Usuario no capturado";
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

        private bool InconsistenciaExists(int id)
        {
            return db.Inconsistencia.Count(e => e.InconsistenciaID == id) > 0;
        }
    }
    public class ResultadoI {
        public int InconsistenciaID { get; set; }
        public int HorarioID { get; set; }
        public string CodigoFuncionario { get; set; }
        public System.DateTime FechaInconsistencia { get; set; }
        public byte Estado { get; set; }
        public int TipoInconsistenciaID { get; set; }
        public string TipoInconsistencia { get; set; }
        public int AreaID { get; set; }
        public string NombreArea { get; set; }
        public string identificacion { get; set; }



    }

}