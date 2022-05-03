using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using CUCMarca.Models;


namespace CUCMarca.Controllers
{
    
    public class AsistenciasController : ApiController
    {

        private CUCMarcaEntities db = new CUCMarcaEntities();
        C_Token token = new C_Token();
        Bitacora bitacora = new Bitacora();

        // GET: api/Asistencias
        public IQueryable<Asistencia> GetAsistencia()
        {
            return db.Asistencia;
        }

        // GET: api/Asistencias/5
        [ResponseType(typeof(Asistencia))]
        public async Task<IHttpActionResult> GetAsistencia(int id)
        {
            Asistencia asistencia = await db.Asistencia.FindAsync(id);
            if (asistencia == null)
            {
                bitacora.accion = "Get Asistencias";
                bitacora.descripcion = "Usuario no existe ";
                bitacora.fecha = DateTime.Now;
                bitacora.tipo = "Error";
                bitacora.usuario = asistencia.CodigoFuncionario;
                bitacora.ins_bitacora();
                return NotFound();

            }

            return Ok(asistencia);
        }

        // PUT: api/Asistencias/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAsistencia(int id, Asistencia asistencia)
        {
            if (!ModelState.IsValid)
            {
                bitacora.accion = "PutAsistencia";
                bitacora.descripcion = "Modelo no es válido";
                bitacora.fecha = DateTime.Now;
                bitacora.tipo = "Error";
                bitacora.usuario = asistencia.CodigoFuncionario;
                bitacora.ins_bitacora();

                return BadRequest(ModelState);
            }

            if (id != asistencia.AsistenciaID)
            {
                bitacora.accion = "PutAsistencia";
                bitacora.descripcion = "Usuario no existe ";
                bitacora.fecha = DateTime.Now;
                bitacora.tipo = "Error";
                bitacora.usuario = asistencia.CodigoFuncionario;
                bitacora.ins_bitacora();

                return BadRequest();
            }

            db.Entry(asistencia).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
                bitacora.accion = "PutAsistencia";
                bitacora.descripcion = "Asistencia editada correctamente";
                bitacora.fecha = DateTime.Now;
                bitacora.tipo = "Acción";
                bitacora.usuario = asistencia.CodigoFuncionario;
                bitacora.ins_bitacora();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsistenciaExists(id))
                {
                    bitacora.accion = "PutAsistencia";
                    bitacora.descripcion = "Usuario no existe ";
                    bitacora.fecha = DateTime.Now;
                    bitacora.tipo = "Error";
                    bitacora.usuario = asistencia.CodigoFuncionario;
                    bitacora.ins_bitacora();

                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Asistencias/Marcar
        [Authorize]
        [HttpPost]
        [ResponseType(typeof(Marca))]
        [Route("api/Asistencias/Marcar", Name = "MarcarAsistencia")]
        public async Task<IHttpActionResult> MarcarAsistencia(string codigo, AsistenciaObj marca)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                //Funcionario temp = db.Funcionario.FirstOrDefault<Funcionario>(x => x.Identificacion == codigo);
                FuncionarioArea f = db.FuncionarioArea.FirstOrDefault<FuncionarioArea>(x => x.Funcionario.Identificacion == codigo && x.CodigoFuncionario==marca.idFuncionario );
                if (f == null)
                {
                    bitacora.accion = "Marcar";
                    bitacora.descripcion = "Usuario no existe " ;
                    bitacora.fecha = DateTime.Now;
                    bitacora.tipo = "Error";
                    bitacora.usuario = marca.CodigoFuncionario;
                    bitacora.ins_bitacora();
                    return NotFound();
                }

                DateTime fecha = DateTime.Now;

                //Verificar si existe marca:
                bool existe = db.Asistencia.Any<Asistencia>(x => x.CodigoFuncionario == f.CodigoFuncionario && x.TipoMarca == marca.TipoMarca && x.FechaAsistencia.Day == fecha.Day
                && x.FechaAsistencia.Month == fecha.Month && x.FechaAsistencia.Year == fecha.Year);



                Asistencia asistencia = new Asistencia()
                {
                   
                    FechaAsistencia = DateTime.Now,
                    CodigoFuncionario = f.CodigoFuncionario,
                    TipoMarca = marca.TipoMarca,
                    //Actividad = marca.Actividad == null ? string.Empty : marca.Actividad,
                    Actividad = marca.Actividad,
                    Comentarios = marca.Comentarios == null ? string.Empty : marca.Comentarios,
                    Latitud = marca._Latitud,
                    Longitud = marca._Longitud,
                    DireccionIP = HttpContext.Current.Request.UserHostAddress,
                    
                };
                db.Asistencia.Add(asistencia);
                await db.SaveChangesAsync();

                bitacora.accion = "Marcar";
                bitacora.descripcion = "Marca realizada correctamente ";
                bitacora.fecha = DateTime.Now;
                bitacora.tipo = "Accion";
                bitacora.usuario = marca.CodigoFuncionario;
                bitacora.ins_bitacora();

                Marca m = new Marca()
                {
                    AsistenciaID = asistencia.AsistenciaID,
                    FechaMarca = asistencia.FechaAsistencia,
                    FuncionarioId = f.FuncionarioID,
                    Actividad = asistencia.Actividad,
                    Mensaje = existe ? "La marca ya se había realizado." : string.Empty
                };

                return CreatedAtRoute("MarcarAsistencia", new { id = asistencia.AsistenciaID }, m);
                //return InternalServerError();
                //return NotFound();
            }
            catch (DbEntityValidationException e)
            {
                string result = string.Empty;
                foreach (var eve in e.EntityValidationErrors)
                {
                    result += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        result += string.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return InternalServerError(new Exception (result , e));
            }
            catch (Exception exc)
            {
                bitacora.accion = "Marcar";
                bitacora.descripcion = "Erro interno : " + exc.Message ;
                bitacora.fecha = DateTime.Now;
                bitacora.tipo = "Error";
                bitacora.usuario = marca.CodigoFuncionario;
                bitacora.ins_bitacora();

                return InternalServerError(exc);
            }
        }

        // POST: api/Asistencias/Marcar
        [Authorize]
        [HttpPost]
        [ResponseType(typeof(Marca))]
        [Route("api/Asistencias/MarcarApp", Name = "MarcarAsistenciaApp")]
        public async Task<IHttpActionResult> MarcarAsistencia( AsistenciaObj marca)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                marca._Latitud = marca.GetDecimal(marca.Latitud);
                marca._Longitud = marca.GetDecimal(marca.Longitud);

                FuncionarioArea f = db.FuncionarioArea.FirstOrDefault<FuncionarioArea>(x => x.Funcionario.Identificacion == marca.CodigoFuncionario && x.AreaID==marca.AreaID);
                if (f == null)
                {
                    bitacora.accion = "Marcar";
                    bitacora.descripcion = "Usuario no existe ";
                    bitacora.fecha = DateTime.Now;
                    bitacora.tipo = "Error";
                    bitacora.usuario = marca.CodigoFuncionario;
                    bitacora.ins_bitacora();
                    return NotFound();
                }

                DateTime fecha = DateTime.Now;

                //Verificar si existe marca:
                bool existe = db.Asistencia.Any<Asistencia>(x => x.CodigoFuncionario == f.CodigoFuncionario && x.TipoMarca == marca.TipoMarca && x.FechaAsistencia.Day == fecha.Day
                && x.FechaAsistencia.Month == fecha.Month && x.FechaAsistencia.Year == fecha.Year);



                Asistencia asistencia = new Asistencia()
                {

                    FechaAsistencia = DateTime.Now,
                    CodigoFuncionario = f.CodigoFuncionario,
                    TipoMarca = marca.TipoMarca,
                    //Actividad = marca.Actividad == null ? string.Empty : marca.Actividad,
                    Actividad = marca.Actividad,
                    Comentarios = marca.Comentarios == null ? string.Empty : marca.Comentarios,
                    Latitud = marca._Latitud,
                    Longitud = marca._Longitud,
                    DireccionIP = HttpContext.Current.Request.UserHostAddress,

                };
                db.Asistencia.Add(asistencia);
                await db.SaveChangesAsync();

                bitacora.accion = "Marcar";
                bitacora.descripcion = "Marca realizada correctamente ";
                bitacora.fecha = DateTime.Now;
                bitacora.tipo = "Accion";
                bitacora.usuario = marca.CodigoFuncionario;
                bitacora.ins_bitacora();

                Marca m = new Marca()
                {
                    AsistenciaID = asistencia.AsistenciaID,
                    FechaMarca = asistencia.FechaAsistencia,
                    FuncionarioId = f.FuncionarioID,
                    Actividad = asistencia.Actividad,
                    Mensaje = existe ? "La marca ya se había realizado." : string.Empty
                };

                return CreatedAtRoute("MarcarAsistencia", new { id = asistencia.AsistenciaID }, m);
                //return InternalServerError();
                //return NotFound();
            }
            catch (DbEntityValidationException e)
            {
                string result = string.Empty;
                foreach (var eve in e.EntityValidationErrors)
                {
                    result += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        result += string.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return InternalServerError(new Exception(result, e));
            }
            catch (Exception exc)
            {
                bitacora.accion = "Marcar";
                bitacora.descripcion = "Erro interno : " + exc.Message;
                bitacora.fecha = DateTime.Now;
                bitacora.tipo = "Error";
                bitacora.usuario = marca.CodigoFuncionario;
                bitacora.ins_bitacora();

                return InternalServerError(exc);
            }
        }

        private void EnviarEmail(Marca m, Funcionario f)
        {

        }

        // POST: api/Asistencias
        [ResponseType(typeof(Asistencia))]
        public async Task<IHttpActionResult> PostAsistencia(Asistencia asistencia)
        {
            if (!ModelState.IsValid)
            {
                bitacora.accion = "PostAsistencia";
                bitacora.descripcion = "Algún elemento falta en el modelo ";
                bitacora.fecha = DateTime.Now;
                bitacora.tipo = "Error";
                bitacora.usuario = asistencia.CodigoFuncionario;
                bitacora.ins_bitacora();
               return BadRequest(ModelState);
            }

            db.Asistencia.Add(asistencia);
            await db.SaveChangesAsync();
            bitacora.accion = "PostAsistencia";
            bitacora.descripcion = "Asistencia creada correctamente";
            bitacora.fecha = DateTime.Now;
            bitacora.tipo = "Accion";
            bitacora.usuario = asistencia.CodigoFuncionario;
            bitacora.ins_bitacora();


            return CreatedAtRoute("DefaultApi", new { id = asistencia.AsistenciaID }, asistencia);
        }

        // DELETE: api/Asistencias/5
        [ResponseType(typeof(Asistencia))]
        public async Task<IHttpActionResult> DeleteAsistencia(int id)
        {
            Asistencia asistencia = await db.Asistencia.FindAsync(id);
            if (asistencia == null)
            {
                bitacora.accion = "DeleteAsistencia";
                bitacora.descripcion = "El usuario no existe";
                bitacora.fecha = DateTime.Now;
                bitacora.tipo = "Error";
                bitacora.usuario = asistencia.CodigoFuncionario;
                bitacora.ins_bitacora();
                return NotFound();
                
            }

            db.Asistencia.Remove(asistencia);
            await db.SaveChangesAsync();

            bitacora.accion = "DeleteAsistencia";
            bitacora.descripcion = "Asistencia eliminada correctamente";
            bitacora.fecha = DateTime.Now;
            bitacora.tipo = "Acción";
            bitacora.usuario = asistencia.CodigoFuncionario;
            bitacora.ins_bitacora();

            return Ok(asistencia);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AsistenciaExists(int id)
        {
            return db.Asistencia.Count(e => e.AsistenciaID == id) > 0;
           


        }
    }
}