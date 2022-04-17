using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Device.Location;
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
    public class DistanciaController : ApiController
    {

        
        private CUCMarcaEntities db = new CUCMarcaEntities();

        // GET: api/Distancias/
        [ResponseType(typeof(RespuestaDis))]
        public List<RespuestaDis> GetCoordenadas(DateTime fechai, DateTime fechaf)
        {
            List<RespuestaDis> lista = new List<RespuestaDis>();
            List<Asistencia> listat = db.Asistencia.Where(x => x.FechaAsistencia > fechai && x.FechaAsistencia < fechaf).ToList();
            foreach (Asistencia d in listat)
            {
                RespuestaDis dis = new RespuestaDis();
                dis.AsistenciaID = (int)d.AsistenciaID;
                dis.CodigoFuncionario = d.CodigoFuncionario;
                dis.FechaAsistencia = d.FechaAsistencia;
                dis.Latitud = (decimal)d.Latitud;
                dis.Longitud = (decimal)d.Longitud;
                dis.DireccionIP = d.DireccionIP;
                lista.Add(dis);
            }
            return lista;
        }
        // GET: api/Distancias/
        
        public async Task<IHttpActionResult> PostDistancias(int AssitenciaID)
        {
            Asistencia asistencia = db.Asistencia.Find(AssitenciaID);
            if (asistencia == null)
            {


                return NotFound();
            }
            else
            {
                GeoCoordinate punto1 = new GeoCoordinate();
                if (asistencia.Latitud == 0 && asistencia.Longitud == 0)
                {
                    string lat = ConfigurationManager.AppSettings["latitud"];
                    punto1.Latitude = double.Parse(lat);
                    string lon = ConfigurationManager.AppSettings["longitud"];
                    punto1.Longitude = double.Parse(lon);
                }
                else
                {
                    punto1.Latitude = (double)asistencia.Latitud;
                    punto1.Longitude = (double)asistencia.Longitud;

                }


                GeoCoordinate punto2 = new GeoCoordinate();
                string l = ConfigurationManager.AppSettings["latitud"];
                punto2.Latitude = double.Parse(l);
                string lo = ConfigurationManager.AppSettings["longitud"];
                punto2.Longitude = double.Parse(lo);
                RespuestaDis res = new RespuestaDis();
                double distancia = res.Distance(punto1, punto2) * 1000;

                return Ok(distancia);
            }

        }


       
    }



    public class RespuestaDis
    {
        public const double EarthRadius = 6371;
        public int AsistenciaID { get; set; }
        public DateTime FechaAsistencia { get; set; }
        public string CodigoFuncionario { get; set; }
        public string DireccionIP { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public double Distance(GeoCoordinate point1, GeoCoordinate point2)
        {

            double distance = 0;
            double Lat = (point2.Latitude - point1.Latitude) * (Math.PI / 180);
            double Lon = (point2.Longitude - point1.Longitude) * (Math.PI / 180);
            double a = Math.Sin(Lat / 2) * Math.Sin(Lat / 2) + Math.Cos(point1.Latitude * (Math.PI / 180)) * Math.Cos(point2.Latitude * (Math.PI / 180)) * Math.Sin(Lon / 2) * Math.Sin(Lon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            distance = EarthRadius * c;
            return distance;
        }
    }
}