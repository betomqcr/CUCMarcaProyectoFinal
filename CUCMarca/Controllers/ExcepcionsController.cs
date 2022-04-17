using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CUCMarca.Models;

namespace CUCMarca.Controllers
{
    public class ExcepcionsController : ApiController
    {
        private CUCMarcaEntities db = new CUCMarcaEntities();

        Bitacora bitacora = new Bitacora();
// GET: api/Excepcions/5
        [ResponseType(typeof(Excepcion))]
        public IHttpActionResult GetExepciones(string id)
        {
            try
            {

                
                if (id.Length > 20)
                {
                    bitacora.accion = "GetExcepciones";
                    bitacora.descripcion = "El id no tiene el tamaño correcto: ";
                    bitacora.tipo = "Error";
                    bitacora.usuario = id;
                    bitacora.ins_bitacora();
                    return NotFound();

                }
                else
                {
                    if (db.Funcionario.FirstOrDefault(x => x.Identificacion == id) != null)
                    {
                       
                        int codigof = db.Funcionario.FirstOrDefault(x => x.Identificacion == id).FuncionarioID;
                        string estado;

                        List<FuncionarioArea> listaf = db.FuncionarioArea.Where(x => x.FuncionarioID == codigof).ToList(); 
                        List<resp> listai = new List<resp>();
                        foreach (FuncionarioArea fa in listaf)
                        {
                            List<Excepcion> l = db.Excepcion.Where(x => x.CodigoFuncionario == fa.CodigoFuncionario && x.Estado == 1 ).ToList();
                            foreach (Excepcion ex in l)
                            {
                                resp r = new resp();
                                r.FechaExcepcion = ex.FechaExcepcion;
                                if (ex.ReponeTiempo == true)
                                {
                                    r.ReponeTiempo = "SI";
                                }
                                else if (ex.ReponeTiempo == false)
                                {
                                    r.ReponeTiempo = "NO";
                                }
                                
                                r.FechaReposicion = ex.FechaReposicion;
                                r.Observaciones = ex.Observaciones;
                                r.Motivo = (db.Motivo.FirstOrDefault(x => x.MotivoID == ex.MotivoID).Nombre);
                                if (ex.Estado == 1)

                                {
                                    estado = "Pendiente";
                                    r.Estado = estado;
                                }
                              
                                

                                listai.Add(r);
                            }
                        }

                      

                        return Ok(listai);
                    }

                    else
                    {
                        bitacora.accion = "GetExcepciones";
                        bitacora.descripcion = "Modelo incorrecto";
                        bitacora.tipo = "Error";
                        bitacora.usuario = id;
                        bitacora.ins_bitacora();
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                bitacora.accion = "GetExcepciones";
                bitacora.descripcion = "Error interno: " + ex.Message;
                bitacora.tipo = "Error";
                bitacora.usuario = id;
                bitacora.ins_bitacora();
                return InternalServerError(ex);
            }

        }



        // POST: api/Excepcions
        [HttpPost]
        [ResponseType(typeof(Excepcion))]
        public IHttpActionResult PostExcepcion(string id,  DateTime FechaExcepcion, bool ReponeTiempo,
         Nullable<System.DateTime> FechaReposicion,  string Observaciones, int Motivo)
        {
            int Estado = 1;
            int autorizadoPor = 2;
            DateTime fechaAutoriza = new DateTime(1753,01, 01,8, 30, 52);
            try
            {
                if (!ModelState.IsValid)
            {
                    bitacora.accion = "PostExcepciones";
                    bitacora.descripcion = "El modelo es invalido";
                    bitacora.tipo = "Error";
                    bitacora.usuario = id;
                    bitacora.ins_bitacora();
                    return BadRequest(ModelState);
            }
                int identifica = db.Funcionario.FirstOrDefault(x => x.Identificacion == id).FuncionarioID;

               
              
                string cod = db.FuncionarioArea.FirstOrDefault(x => x.FuncionarioID == identifica).CodigoFuncionario;

                FuncionarioArea f = db.FuncionarioArea.FirstOrDefault<FuncionarioArea>(x => x.CodigoFuncionario == cod);
                if (f == null)
                {
                    bitacora.accion = "PostExcepciones";
                    bitacora.descripcion = "El usuario no existe";
                    bitacora.tipo = "Error";
                    bitacora.usuario = id;
                    bitacora.ins_bitacora();
                    return NotFound();
                }

                Excepcion excep = new Excepcion()
                {
                    CodigoFuncionario = cod,
                    FechaExcepcion = FechaExcepcion,
                    ReponeTiempo = ReponeTiempo,
                    FechaReposicion = FechaReposicion,
                    Observaciones = Observaciones,
                    MotivoID = Motivo,
                    Estado = Estado,
                    //AutorizadoPor = autorizadoPor,
                    //FechaAutorizacion = fechaAutoriza
                   
                };
                
                db.Excepcion.Add(excep);
                 db.SaveChangesAsync();

                bitacora.accion = "PostExcepciones";
                bitacora.descripcion = "Excepción creada correctamente";
                bitacora.tipo = "Acción";
                bitacora.usuario = id;
                bitacora.ins_bitacora();
                responseExcep r = new responseExcep();
                r.ExcepcionID = excep.ExcepcionID;

                return CreatedAtRoute("DefaultApi", new { id = r.ExcepcionID }, r);


            }
          
            catch (Exception exc)
            {
                bitacora.accion = "PostExcepciones";
                bitacora.descripcion = "Error interno: " + exc.Message;
                bitacora.tipo = "Error";
                bitacora.usuario = id;
                bitacora.ins_bitacora();
                return InternalServerError(exc);
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

        private bool ExcepcionExists(int id)
        {
            return db.Excepcion.Count(e => e.ExcepcionID == id) > 0;
        }

    }

    public class resp
    {
        public DateTime FechaExcepcion { get; set; }
        public string ReponeTiempo { get; set; }

        public Nullable<System.DateTime> FechaReposicion { get; set; }

        public string Observaciones { get; set; }

        public string  Motivo { get; set; }

        public string Estado { get; set; }
    }
    public class responseExcep
    {
        public int ExcepcionID { get; set; }
        
    }
}