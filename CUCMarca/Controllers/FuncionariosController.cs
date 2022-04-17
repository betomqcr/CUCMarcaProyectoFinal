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
    public class FuncionariosController : ApiController
    {
        private CUCMarcaEntities db = new CUCMarcaEntities();
        Bitacora bitacora = new Bitacora();
        // GET: api/Funcionarios
        public IQueryable<Funcionario> GetFuncionario()
        {
            return db.Funcionario;
        }

        // GET: api/Funcionarios/5
        [ResponseType(typeof(Funcionario))]
        public async Task<IHttpActionResult> GetFuncionario(int id)
        {
            Funcionario funcionario = await db.Funcionario.FindAsync(id);
            if (funcionario == null)
            {
                bitacora.accion = "GetFuncionario";
                bitacora.descripcion = "El usuario no existe";
                bitacora.fecha = DateTime.Now;
                bitacora.tipo = "Error";
                bitacora.usuario = id.ToString();
                bitacora.ins_bitacora();

                return NotFound();
            }

            return Ok(funcionario);
        }

        // PUT: api/Funcionarios/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFuncionario(int id, Funcionario funcionario)
        {
            if (!ModelState.IsValid)
            {
                bitacora.accion = "PutFuncionario";
                bitacora.descripcion = "Modelo no es válido";
                bitacora.fecha = DateTime.Now;
                bitacora.tipo = "Error";
                bitacora.usuario = id.ToString();
                bitacora.ins_bitacora();

                return BadRequest(ModelState);
            }

            if (id != funcionario.FuncionarioID)
            {
                bitacora.accion = "PutFuncionario";
                bitacora.descripcion = "Usuario no existe";
                bitacora.fecha = DateTime.Now;
                bitacora.tipo = "Error";
                bitacora.usuario = id.ToString();
                return BadRequest();
            }

            db.Entry(funcionario).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
                bitacora.accion = "PutFuncionario";
                bitacora.descripcion = "Funcionario modificado correctamente";
                bitacora.fecha = DateTime.Now;
                bitacora.tipo = "Accion";
                bitacora.usuario = id.ToString();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuncionarioExists(id))
                {
                    bitacora.accion = "PutFuncionario";
                    bitacora.descripcion = "El usuario no existe";
                    bitacora.fecha = DateTime.Now;
                    bitacora.tipo = "Error";
                    bitacora.usuario = id.ToString();
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Funcionarios
        [ResponseType(typeof(Funcionario))]
        public async Task<IHttpActionResult> PostFuncionario(Funcionario funcionario)
        {
            if (!ModelState.IsValid)
            {
                bitacora.accion = "PostFuncionario";
                bitacora.descripcion = "El modelo no es válido";
                bitacora.fecha = DateTime.Now;
                bitacora.tipo = "Error";
                bitacora.usuario = funcionario.Identificacion;
                return BadRequest(ModelState);
            }

            db.Funcionario.Add(funcionario);
            await db.SaveChangesAsync();
            bitacora.accion = "PostFuncionario";
            bitacora.descripcion = "Funcionario creado correctamente";
            bitacora.fecha = DateTime.Now;
            bitacora.tipo = "Accion";
            bitacora.usuario = funcionario.Identificacion;

            return CreatedAtRoute("DefaultApi", new { id = funcionario.FuncionarioID }, funcionario);
        }

        // DELETE: api/Funcionarios/5
        [ResponseType(typeof(Funcionario))]
        public async Task<IHttpActionResult> DeleteFuncionario(int id)
        {
            Funcionario funcionario = await db.Funcionario.FindAsync(id);
            if (funcionario == null)
            {
                bitacora.accion = "DeleteFuncionario";
                bitacora.descripcion = "El usuario no existe";
                bitacora.fecha = DateTime.Now;
                bitacora.tipo = "Error";
                bitacora.usuario = funcionario.Identificacion;
                return NotFound();
            }

            db.Funcionario.Remove(funcionario);
            await db.SaveChangesAsync();
            bitacora.accion = "DeleteFuncionario";
            bitacora.descripcion = "Funcionario eliminado correctamente";
            bitacora.fecha = DateTime.Now;
            bitacora.tipo = "Accion";
            bitacora.usuario = funcionario.Identificacion;
            return Ok(funcionario);
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