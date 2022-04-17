using CUCMarca.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CUCMarca.BusinessServices
{
    public class FuncionarioService
    {

        private CUCMarcaEntities entities;

        public FuncionarioService()
        {
            entities = new CUCMarcaEntities();
        }

        #region Horarios
        public int BorrarHorarioDetalle(int horarioID, int dia)
        {
            HorarioDetalle horario = entities.HorarioDetalle.FirstOrDefault<HorarioDetalle>(x => x.HorarioID == horarioID && x.Dia == dia);
            entities.HorarioDetalle.Remove(horario);
            return entities.SaveChanges();
        }

        public int BorrarHorario(int horarioID)
        {
            Horario horario = entities.Horario.FirstOrDefault<Horario>(x => x.HorarioId == horarioID);
            entities.Horario.Remove(horario);
            return entities.SaveChanges();
        }

        public int AgregarHorarioDetalle(HorarioDetalle detalle)
        {
            if (detalle.HoraSalida <= detalle.HoraIngreso)
            {
                throw new Exception("El valor de la hora de ingreso debe ser menor al de la hora de salida.");
            }

            entities.HorarioDetalle.Add(detalle);
            return entities.SaveChanges();
        }

        public List<HorarioDetalle> ObtenerDetalle(int horarioID)
        {
            Horario horario = entities.Horario.FirstOrDefault<Horario>(x => x.HorarioId == horarioID);
            return horario.HorarioDetalle.OrderBy(h => h.Dia).ToList<HorarioDetalle>();
        }

        public int NuevoHorario(byte periodo, int anio, byte estado, string codigoFuncionario)
        {
            //Si el horario se asigna como vigente, deshabilitamos los demás
            if (estado == 1)
            {
                List<Horario> horarios = entities.Horario.Where<Horario>(x => x.CodigoFuncionario == codigoFuncionario).ToList();
                foreach (Horario horario in horarios)
                {
                        horario.Estado = 2;
                }
            }
            Horario h = new Horario()
            { 
                Anio = anio,
                CodigoFuncionario = codigoFuncionario,
                Cuatrimestre = periodo,
                Estado = estado,
            };

            entities.Horario.Add(h);
            return entities.SaveChanges();
        }

        public List<FuncionarioArea> ObtenerCodigosFuncionario(int funcionarioID)
        {
            List<FuncionarioArea> result = entities.FuncionarioArea.Where<FuncionarioArea>(x => x.FuncionarioID == funcionarioID).OrderBy(x => x.CodigoFuncionario).ToList<FuncionarioArea>();
            return result;
        }

        public int HabilitarHorario(int horarioid, int funcionarioID)
        {
            Horario horario = entities.Horario.FirstOrDefault<Horario>(x=> x.HorarioId == horarioid);
            List<Horario> horarios = entities.Horario.Where<Horario>(x => x.CodigoFuncionario == horario.CodigoFuncionario && x.FuncionarioArea.AreaID == horario.FuncionarioArea.AreaID).ToList();
            foreach (Horario h in horarios)
            {
                if (h.HorarioId == horarioid)
                {
                    h.Estado = 1;
                }
                else
                {
                    h.Estado = 2;
                }
            }
            return entities.SaveChanges();
        }

        public int DeshabilitarHorario(int horarioid, int funcionarioID)
        {
            Horario horario = entities.Horario.FirstOrDefault<Horario>(x => x.HorarioId == horarioid);
            horario.Estado = 2;
            return entities.SaveChanges();
        }

        public List<HorarioFuncionario> ObtenerHorarios(int funcionarioID)
        {
            List<Horario> horarios = entities.Horario.Where<Horario>(x => x.FuncionarioArea.FuncionarioID == funcionarioID).ToList();
            List<HorarioFuncionario> resultado = new List<HorarioFuncionario>();
            foreach (var horario in horarios)
            {
                HorarioFuncionario hf = new HorarioFuncionario()
                { 
                    Anio = horario.Anio,
                    CodigoFuncionario = horario.CodigoFuncionario,
                    Estado = horario.Estado,
                    HorarioID = horario.HorarioId,
                    Periodo = horario.Cuatrimestre,
                    NombreArea = horario.FuncionarioArea.Area.Nombre
                };
                resultado.Add(hf);
            }
            return resultado.OrderByDescending(x => x.Anio).ThenByDescending(x => x.Periodo).ToList<HorarioFuncionario>();
        }

        #endregion

        #region Funcionario

        public int AsociarArea(int funcionarioID, int areaID, string codigo)
        {
            FuncionarioArea relacion = new FuncionarioArea()
            {
                AreaID = areaID,
                FuncionarioID = funcionarioID,
                CodigoFuncionario = codigo
            };
            entities.FuncionarioArea.Add(relacion);
            return entities.SaveChanges();
        }

        public int DeasociarArea(string codigo)
        {
            FuncionarioArea id = entities.FuncionarioArea.FirstOrDefault<FuncionarioArea>(x => x.CodigoFuncionario == codigo);
            entities.FuncionarioArea.Remove(id);
            return entities.SaveChanges();
        }

        public List<FuncionarioArea> ObtenerAreasFuncionario(int funcionarioID)
        {
            List<FuncionarioArea> funcionarioAreas = entities.FuncionarioArea.Where<FuncionarioArea>(x => x.FuncionarioID == funcionarioID).ToList<FuncionarioArea>();
            return funcionarioAreas;
        }

        public int NuevoFuncionario(Funcionario f)
        {
            entities.Funcionario.Add(f);
            return entities.SaveChanges();
        }

        public List<Funcionario> ObtenerFuncionarios()
        {
            List<Funcionario> resp = entities.Funcionario.ToList<Funcionario>();
            return resp;
        }

        public Funcionario ObtenerFuncionario(int funcionarioID)
        {
            Funcionario funcionario = entities.Funcionario.FirstOrDefault<Funcionario>(x => x.FuncionarioID == funcionarioID);
            return funcionario;
        }

        public List<string> ObtenerFuncionario(string identificacion)
        {
            var resultado = from fa in entities.FuncionarioArea
                            join f in entities.Funcionario
                            on fa.FuncionarioID equals f.FuncionarioID
                            where f.Identificacion == identificacion
                            select fa.CodigoFuncionario;
            return resultado.ToList<string>();
        }

        public int ActualizarFuncionario(Funcionario datos)
        {
            Funcionario t = entities.Funcionario.FirstOrDefault<Funcionario>(x => x.FuncionarioID == datos.FuncionarioID);
            t.Apellido = datos.Apellido;
            //t.Contrasena = datos.Contrasena;
            t.Correo = datos.Correo;
            t.Estado = datos.Estado;
            t.Nombre = datos.Nombre;
            t.Identificacion = datos.Identificacion;
            t.TipoIdentificacionID = datos.TipoIdentificacionID;
            t.TipoFuncionarioID = datos.TipoFuncionarioID;
            return entities.SaveChanges();
        }

        public int ActualizarClave(int funcionarioID, string newPassword)
        {
            Funcionario t = entities.Funcionario.FirstOrDefault<Funcionario>(x => x.FuncionarioID == funcionarioID);
            t.Contrasena = newPassword;
            return entities.SaveChanges();
        }

        public int BorrarFuncionario(int ID)
        {
            Funcionario id = entities.Funcionario.FirstOrDefault<Funcionario>(x => x.FuncionarioID == ID);
            entities.Funcionario.Remove(id);
            return entities.SaveChanges();
        }
        #endregion

        #region Tipo de identificación
        public int NuevoTipoIdentificacion(string nombre)
        {
            TipoIdentificacion t = new TipoIdentificacion()
            {
                Nombre = nombre
            };
            entities.TipoIdentificacion.Add(t);
            return entities.SaveChanges();
        }

        public List<TipoIdentificacion> ObtenerTiposIdentificacion()
        {
            List<TipoIdentificacion> resp = entities.TipoIdentificacion.ToList<TipoIdentificacion>();
            return resp;
        }

        public int ActualizarTipoIdentificacion(int tipoID, string nombre)
        {
            TipoIdentificacion t = entities.TipoIdentificacion.FirstOrDefault<TipoIdentificacion>(x => x.TipoIdentificacionID == tipoID);
            t.Nombre = nombre;
            return entities.SaveChanges();
        }

        public int BorrarTipoIdentificacion(int tipoID)
        {
            TipoIdentificacion id = entities.TipoIdentificacion.FirstOrDefault<TipoIdentificacion>(x => x.TipoIdentificacionID == tipoID);
            entities.TipoIdentificacion.Remove(id);
            return entities.SaveChanges();
        }
        #endregion

        #region Tipo de funcionario
        public int NuevoTipoFuncionario(string nombre)
        {
            TipoFuncionario t = new TipoFuncionario()
            {
                Nombre = nombre
            };
            entities.TipoFuncionario.Add(t);
            return entities.SaveChanges();
        }

        public List<TipoFuncionario> ObtenerTipoFuncionario()
        {
            List<TipoFuncionario> resp = entities.TipoFuncionario.ToList<TipoFuncionario>();
            return resp;
        }

        public int ActualizarTipoFuncionario(int tipoID, string nombre)
        {
            TipoFuncionario t = entities.TipoFuncionario.FirstOrDefault<TipoFuncionario>(x => x.TipoFuncionarioID == tipoID);
            t.Nombre = nombre;
            return entities.SaveChanges();
        }

        public int BorrarTipoFuncionario(int tipoID)
        {
            TipoFuncionario id = entities.TipoFuncionario.FirstOrDefault<TipoFuncionario>(x => x.TipoFuncionarioID == tipoID);
            entities.TipoFuncionario.Remove(id);
            return entities.SaveChanges();
        }
        #endregion

    }
}
