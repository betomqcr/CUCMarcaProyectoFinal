using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CUCMarca.DataAccess;

namespace CUCMarca.BusinessServices
{
    public class AreaService
    {

        private CUCMarca.DataAccess.CUCMarcaEntities entities;

        public AreaService()
        {
            entities = new CUCMarca.DataAccess.CUCMarcaEntities();
        }

        public int BorrarArea(int areaID)
        {
            CUCMarca.DataAccess.Area area = entities.Area.FirstOrDefault<Area>(x => x.AreaID == areaID);
            entities.Area.Remove(area);
            return entities.SaveChanges();
        }

        public int BorrarTipoArea(int tipoAreaID)
        {
            TipoArea area = entities.TipoArea.FirstOrDefault<TipoArea>(x => x.TipoAreaID == tipoAreaID);
            entities.TipoArea.Remove(area);
            return entities.SaveChanges();
        }

        public int ActualizarArea(int areaID, string nombre, int tipoAreaID, int jefe)
        {
            Area area = entities.Area.FirstOrDefault<Area>(x => x.AreaID == areaID);
            area.Nombre = nombre;
            area.TipoAreaID = tipoAreaID;
            area.Jefe = jefe;
            return entities.SaveChanges();
        }

        public int ActualizarTipoArea(int tipoAreaID, string nombre)
        {
            TipoArea t = entities.TipoArea.FirstOrDefault<TipoArea>(x => x.TipoAreaID == tipoAreaID);
            t.Nombre = nombre;
            return entities.SaveChanges();
        }

        public int NuevaArea(string nombre, int tipoAreaID, int jefe)
        {
            Area area = new Area()
            {
                Nombre = nombre,
                TipoAreaID = tipoAreaID,
                Jefe = jefe
            };
            entities.Area.Add(area);
            return entities.SaveChanges();
        }

        public int NuevoTipoArea(string nombre)
        {
            CUCMarca.DataAccess.TipoArea area = new CUCMarca.DataAccess.TipoArea()
            {
                Nombre = nombre
            };
            entities.TipoArea.Add(area);
            return entities.SaveChanges();
        }

        public List<Area> ObtenerAreas()
        {
            List<CUCMarca.DataAccess.Area> resp = entities.Area.ToList();
            return resp;
        }

        public List<CUCMarca.DataAccess.TipoArea> ObtenerTiposArea()
        {
            List<CUCMarca.DataAccess.TipoArea> resp = entities.TipoArea.ToList();
            return resp;
        }
        public List<Resultado> ObtenerJefes()
        {
            List<Resultado> lista = new List<Resultado>();

            List<Funcionario> resp = entities.Funcionario.ToList<Funcionario>();
            foreach(Funcionario funcionario in resp)
            {
                Resultado r = new Resultado();
                r.nombrecompleto = funcionario.Nombre + " " + funcionario.Apellido;
                r.identificacion = funcionario.Identificacion;
                r.codigofuncionario = funcionario.FuncionarioID;
                lista.Add(r);
            }
            return lista;
        }

    }
    public class Resultado
    {
        public string nombrecompleto { get; set; }
        public int codigofuncionario { get; set; }
        public string identificacion { get; set; }
    }
}
