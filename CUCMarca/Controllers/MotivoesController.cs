using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using System.Web.Http.Description;

using CUCMarca.Models;

namespace CUCMarca.Controllers
{
    public class MotivoesController : ApiController
    {
        // GET: api/Motivoes
        private CUCMarcaEntities db = new CUCMarcaEntities();
        Bitacora bitacora = new Bitacora();
        // GET: Motivo
        public List<respuesta> GetMotivo()
        {
            List<respuesta> lista = new List<respuesta>();
            foreach(Motivo m in db.Motivo.ToList())
            {
                respuesta r = new respuesta();
                r.MotivoID = m.MotivoID;
                r.nombre = m.Nombre;
                lista.Add(r);
            }
            bitacora.accion = "GetMotivos";
            bitacora.descripcion = "Motivos obtenidos correctamente";
            bitacora.fecha = DateTime.Now;
            bitacora.tipo = "Acción";
            bitacora.usuario = "";
            bitacora.ins_bitacora();

            return lista;
        }

        // GET: Motivo/Details/5
        [ResponseType(typeof(Motivo))]
        public async Task<IHttpActionResult> GetMotivo(int id)
        {
            Motivo motivo = await db.Motivo.FindAsync(id);
            if (motivo == null)
            {
                bitacora.accion = "GetMotivos";
                bitacora.descripcion = "No existen motivos";
                bitacora.fecha = DateTime.Now;
                bitacora.tipo = "Error";
                bitacora.usuario = "";
                bitacora.ins_bitacora();
                return NotFound();
            }
            bitacora.accion = "GetMotivos";
            bitacora.descripcion = "Motivos obtenidos correctamente";
            bitacora.fecha = DateTime.Now;
            bitacora.tipo = "Acción";
            bitacora.usuario = "";
            bitacora.ins_bitacora();
            return Ok(motivo);
        }

    }
    public class respuesta {
        public int MotivoID { get; set; }
        public string nombre { get; set; }
}
}
