using CUCMarca.BusinessServices;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CUCMarca.Administracion.Reportes
{
    public partial class MarcasRealizadas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void DesplegarMensajeError(string mensaje)
        {
            ErrorMessage.Visible = true;
            FailureText.Text = mensaje;
        }

        protected void gvMain_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            gvMain.SelectedIndex = e.NewSelectedIndex;
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                string fechaIni = txtFechaIni.Text.Trim();
                string fechaFin = txtFechaFin.Text.Trim();
                if (string.IsNullOrEmpty(fechaIni))
                {
                    DesplegarMensajeError("Debe seleccionar la fecha de inicio");
                }
                else if (string.IsNullOrEmpty(fechaIni))
                {
                    DesplegarMensajeError("Debe seleccionar la fecha de fin");
                }
                else 
                {
                    CargarAsistencia(DateTime.ParseExact(fechaIni, "yyyy-MM-dd", CultureInfo.InvariantCulture), DateTime.ParseExact(fechaFin, "yyyy-MM-dd", CultureInfo.InvariantCulture));
                }
            }
            catch (Exception exc)
            {
                DesplegarMensajeError("Ha ocurrido un error ejecutando el reporte: " + exc.Message);
            }
        }

        private void CargarAsistencia(DateTime fechaIni, DateTime fechaFin)
        {
            try
            {
                MarcaService service = new MarcaService();
                List<DatosAsistencia> datos = service.ObtenerMarcas(fechaIni, fechaFin);
                gvMain.DataSource = datos;
                gvMain.DataBind();
            }
            catch (Exception exc)
            {
                DesplegarMensajeError("Ha ocurrido un error cargando los datos de asistencia: " +exc.Message);
            }
        }
    }
}