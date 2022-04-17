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

namespace CUCMarca.Controllers
{
    public class JustificacionsController : ApiController
    {
        private CUCMarcaEntities db = new CUCMarcaEntities();
        Bitacora bitacora = new Bitacora();

        // GET: api/Jefes/5
        [ResponseType(typeof(Funcionario))]
        public IHttpActionResult GetJustificacion(string id)
        {
            try
            {
                Funcionario f = db.Funcionario.FirstOrDefault(x => x.Identificacion == id);

                List<respuestaf> lista = new List<respuestaf>();

                if(f == null)
                {
                    bitacora.accion = "GetJustificacion";
                    bitacora.descripcion = "EL usuario no existe";
                    bitacora.fecha = DateTime.Now;
                    bitacora.tipo = "Error";
                    bitacora.usuario = id;
                    bitacora.ins_bitacora();
                    return NotFound();
                }
                else
                {
                    foreach(Area a in f.Area.Where(x => x.Jefe == f.FuncionarioID))
                    {
                        
                        foreach(FuncionarioArea funA in a.FuncionarioArea)
                        {
                            if(funA.Funcionario.Identificacion != id)
                            {
                                foreach (Inconsistencia inc in funA.Inconsistencia)
                                {
                                    if (inc.Justificacion.Count > 0)
                                    {
                                        if (inc.Justificacion.First().AutorizadoPor == null && inc.Justificacion.First().FechaAutorizacion == null)
                                        {
                                            respuestaf res = new respuestaf();
                                            res.identificacion = funA.Funcionario.Identificacion;
                                            res.JustificacionID = inc.Justificacion.First().JustificacionID;
                                            res.InconsistenciaID = inc.InconsistenciaID;
                                            res.CodigoFuncionario = funA.CodigoFuncionario;
                                            res.ReponeTiempo = inc.Justificacion.First().ReponeTiempo;
                                            res.FechaReposicion = (DateTime)inc.Justificacion.First().FechaReposicion;
                                            res.Observaciones = inc.Justificacion.First().Observaciones;
                                            res.FechaJustificacion = (DateTime)inc.Justificacion.First().FechaJustificacion;
                                            res.idmotivo = inc.Justificacion.First().MotivoID;
                                            res.Motivo = inc.Justificacion.First().Motivo.Nombre;
                                            res.nombrefuncionario = funA.Funcionario.Nombre + " " + funA.Funcionario.Apellido;
                                            res.fechainconsistencia = (DateTime)inc.FechaInconsistencia;
                                            res.tipo = "Justificación";
                                            lista.Add(res);
                                        }

                                    }


                                }
                                foreach (Excepcion exc in funA.Excepcion)
                                {
                                    if (exc.AutorizadoPor == null && exc.FechaAutorizacion == null)
                                    {
                                        respuestaf res = new respuestaf();
                                        res.identificacion = funA.Funcionario.Identificacion;
                                        res.JustificacionID = exc.ExcepcionID;
                                        res.InconsistenciaID = exc.ExcepcionID;
                                        res.CodigoFuncionario = funA.CodigoFuncionario;

                                        res.ReponeTiempo = exc.ReponeTiempo;
                                        res.FechaReposicion = (DateTime)exc.FechaReposicion;
                                        res.Observaciones = exc.Observaciones;
                                        res.FechaJustificacion = (DateTime)exc.FechaExcepcion;
                                        res.idmotivo = exc.MotivoID;
                                        res.Motivo = exc.Motivo.Nombre;
                                        res.nombrefuncionario = funA.Funcionario.Nombre + " " + funA.Funcionario.Apellido;
                                        res.fechainconsistencia = (DateTime)exc.FechaExcepcion;
                                        res.tipo = "Excepción";
                                        lista.Add(res);
                                    }

                                }
                            }
                            

                        }
                    }
                }
                if(lista.Count > 0)
                {
                    List<respuestaf> resultado = new List<respuestaf>();
                    resultado = lista.OrderBy(x => x.fechainconsistencia).ToList();
                    return Ok(resultado);
                }
                else
                {
                    bitacora.accion = "GetJustificacion";
                    bitacora.descripcion = "EL usuario no existe";
                    bitacora.fecha = DateTime.Now;
                    bitacora.tipo = "Error";
                    bitacora.usuario = id;
                    bitacora.ins_bitacora();
                    return NotFound();
                }
                
            }
            catch (Exception ex) {
                bitacora.accion = "GetJustificacion";
                bitacora.descripcion = "Error interno: " + ex.Message;
                bitacora.fecha = DateTime.Now;
                bitacora.tipo = "Error";
                bitacora.usuario = id;
                bitacora.ins_bitacora();
                return InternalServerError(ex);
            }
        }
        // POST: api/Justificacions
        [ResponseType(typeof(Justificacion))]
        public IHttpActionResult PostJustificacion(string Identificacion, int InconsistenciaID, string CodigoFuncionario, bool ReponeTiempo, DateTime FechaReposicion, string Observaciones, int MotivoID)
        {
            try
            {
                List<Inconsistencia> incoID = db.Inconsistencia.Where(x => x.InconsistenciaID == InconsistenciaID).ToList();
                if (incoID.Count > 0)
                {
                    string idc = incoID.First().CodigoFuncionario;

                    FuncionarioArea f = db.FuncionarioArea.First(x => x.CodigoFuncionario == idc);
                    Funcionario fun = db.Funcionario.First(x => x.FuncionarioID == f.FuncionarioID);

                    if ((incoID.Count > 0) && (fun.Identificacion == Identificacion))
                    {
                        int justID = db.Justificacion.Where(x => x.InconsistenciaID == InconsistenciaID).ToList().Count;
                        if (justID == 0)
                        {
                            DateTime FechaJustificacion = DateTime.Now;
                            Justificacion j = new Justificacion();
                            

                            if (!ModelState.IsValid)
                            {
                                bitacora.accion = "PostJustificacion";
                                bitacora.descripcion = "Modelo invalido";
                                bitacora.fecha = DateTime.Now;
                                bitacora.tipo = "Error";
                                bitacora.usuario = Identificacion;
                                bitacora.ins_bitacora();
                                return BadRequest(ModelState);
                            }
                            j.InconsistenciaID = InconsistenciaID;

                            j.CodigoFuncionario = CodigoFuncionario;
                            j.ReponeTiempo = ReponeTiempo;
                            j.FechaReposicion = FechaReposicion;
                            j.Observaciones = Observaciones;
                            j.MotivoID = MotivoID;
                            j.FechaJustificacion = FechaJustificacion;
                            j.Estado = "Nueva";

                            db.Justificacion.Add(j);
                            db.SaveChanges();

                            Inconsistencia inc = db.Inconsistencia.FirstOrDefault(x => x.InconsistenciaID == InconsistenciaID);
                            inc.Estado = 2;
                            db.SaveChanges();

                            bitacora.accion = "PostJustificacion";
                            bitacora.descripcion = "Justificacion creada correctamente";
                            bitacora.fecha = DateTime.Now;
                            bitacora.tipo = "accion";
                            bitacora.usuario = Identificacion;
                            bitacora.ins_bitacora();

                            respuestaj resj = new respuestaj();


                           resj.InconsistenciaID = InconsistenciaID;

                            resj.CodigoFuncionario = CodigoFuncionario;
                            resj.ReponeTiempo = ReponeTiempo;
                            resj.FechaReposicion = FechaReposicion;
                            resj.Observaciones = Observaciones;
                            resj.motivo = MotivoID;
                            resj.FechaJustificacion = FechaJustificacion;



                            return CreatedAtRoute("DefaultApi", new { id = resj.JustificacionID }, resj);
                        }
                        else
                        {
                            bitacora.accion = "PostJustificacion";
                            bitacora.descripcion = "La justificación ya existe";
                            bitacora.fecha = DateTime.Now;
                            bitacora.tipo = "Error";
                            bitacora.usuario = Identificacion;
                            bitacora.ins_bitacora();
                            return Conflict();
                        }
                    }
                    else
                    {
                        bitacora.accion = "PostJustificacion";
                        bitacora.descripcion = "Justifiacion no creada";
                        bitacora.fecha = DateTime.Now;
                        bitacora.tipo = "Error";
                        bitacora.usuario = Identificacion;
                        bitacora.ins_bitacora();
                        
                        return NotFound();
                    }
                }
                else
                {
                    bitacora.accion = "PostJustificacion";
                    bitacora.descripcion = "Justifiacion no creada";
                    bitacora.fecha = DateTime.Now;
                    bitacora.tipo = "Error";
                    bitacora.usuario = Identificacion;
                    bitacora.ins_bitacora();
                    return NotFound();
                }
                
            }
            catch (Exception ex)
            {
                bitacora.accion = "PostJustificacion";
                bitacora.descripcion = "Error interno: " + ex.Message;
                bitacora.fecha = DateTime.Now;
                bitacora.tipo = "Error";
                bitacora.usuario = Identificacion;
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

        private bool JustificacionExists(int id)
        {
            return db.Justificacion.Count(e => e.JustificacionID == id) > 0;
        }

        public class respuestaj
        {
            public int JustificacionID { get; set; }
            public int InconsistenciaID { get; set; }
            public string CodigoFuncionario { get; set; }
            public bool ReponeTiempo { get; set; }
            public DateTime FechaReposicion { get; set; }
            public string Observaciones { get; set; }
            public DateTime FechaJustificacion { get; set; }
            public int motivo { get; set; }

            public int AutorizadoPor { get; set; }

            public DateTime FechaAutorizacion { get; set; }
        }
        public class respuestaf
        {
            public int JustificacionID { get; set; }
            public int InconsistenciaID { get; set; }
            public string CodigoFuncionario { get; set; }
            public bool ReponeTiempo { get; set; }
            public DateTime FechaReposicion { get; set; }
            public string Observaciones { get; set; }
            public DateTime FechaJustificacion { get; set; }
            public int idmotivo { get; set; }

            public string Motivo { get; set; }

            public string identificacion { get; set; }
            
            public string nombrefuncionario { get; set; }

            public DateTime fechainconsistencia { get; set; }

            public string tipo { get; set; }

        }

    }
}