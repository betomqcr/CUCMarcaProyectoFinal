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
    public class TipoInconsistenciasController : ApiController
    {
        private CUCMarcaEntities db = new CUCMarcaEntities();
        Bitacora bitacora = new Bitacora();
        // GET: api/TipoInconsistencias
        public List<RespuestaTI> GetTipoInconsistencia()
        {
            List<RespuestaTI> lista = new List<RespuestaTI>();
            List<TipoInconsistencia> listat = db.TipoInconsistencia.ToList();
            foreach (TipoInconsistencia t in listat)
            {
                RespuestaTI tI = new RespuestaTI();
                tI.TipoInconsistenciaID = t.TipoInconsistenciaID;
                tI.Nombre = t.Nombre;
                lista.Add(tI);
            }
            bitacora.accion = "GetTipoInconsistencia";
            bitacora.descripcion = "Tipos Inconsistencias obtenidos correctamente";
            bitacora.fecha = DateTime.Now;
            bitacora.tipo = "Acción";
            bitacora.usuario = "";
            bitacora.ins_bitacora();
            return lista;
        }

       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(   disposing);
        }

        private bool TipoInconsistenciaExists(int id)
        {
            return db.TipoInconsistencia.Count(e => e.TipoInconsistenciaID == id) > 0;
        }
    }
    public class RespuestaTI
    {
        public int TipoInconsistenciaID { get; set; }
        public string Nombre { get; set; }
    }
}