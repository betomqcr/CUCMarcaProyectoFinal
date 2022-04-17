using CUCMarca.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUCMarca.BusinessServices
{
    public class MarcaService
    {

        private CUCMarcaEntities entities;

        public MarcaService()
        {
            entities = new CUCMarcaEntities();
        }

        public List<DatosAsistencia> ObtenerMarcas(DateTime fechaInicio, DateTime fechaFin)
        {
            fechaFin = fechaFin.AddDays(1);
            List<Asistencia> datos = entities.Asistencia.Where<Asistencia>(x => x.FechaAsistencia >= fechaInicio && x.FechaAsistencia <= fechaFin).OrderByDescending(x => x.FechaAsistencia).ToList<Asistencia>();
            List<DatosAsistencia> result = new List<DatosAsistencia>();
            foreach (Asistencia asistencia in datos)
            {
                DatosAsistencia asis = new DatosAsistencia()
                { 
                    AsistenciaID = asistencia.AsistenciaID,
                    CodigoFuncionario = asistencia.CodigoFuncionario,
                    Identificacion = asistencia.FuncionarioArea.Funcionario.Identificacion,
                    Nombre = asistencia.FuncionarioArea.Funcionario.Nombre,
                    Apellidos = asistencia.FuncionarioArea.Funcionario.Apellido,
                    FechaAsistencia = asistencia.FechaAsistencia,
                    TipoMarca = asistencia.TipoMarca,
                    DireccionIP = asistencia.DireccionIP,
                    Latitud = asistencia.Latitud.Value,
                    Longitud = asistencia.Longitud.Value
                };
                result.Add(asis);
            }
            return result;
        }
    }
}
