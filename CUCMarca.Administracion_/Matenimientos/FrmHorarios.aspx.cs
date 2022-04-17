using CUCMarca.BusinessServices;
using CUCMarca.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CUCMarca.Administracion.Matenimientos
{
    public partial class FrmHorarios : System.Web.UI.Page
    {

        private Funcionario funcionario;
        protected void Page_Load(object sender, EventArgs e)
        {
            ErrorMessage.Visible = false;
            SuccessMessage.Visible = false;
            if (Session["FuncionarioID"] == null)
            {
                Response.Redirect("FrmFuncionario", true);
            }
            CargarFuncionario();
            if (!IsPostBack) 
            {
                ltlFuncData.Text = string.Format("{0} {1} {2}", funcionario.Identificacion, funcionario.Nombre, funcionario.Apellido);
                mvMain.SetActiveView(viewDatos);
                CargarHorarios();
                CargarCodigosFuncionario();
            }
            CheckSelectedIndex();
        }

        protected string ObtenerNombreDia(int dia)
        {
            switch (dia)
            {
                case 1: return "Lunes";
                case 2: return "Martes";
                case 3: return "Miércoles";
                case 4: return "Jueves";
                case 5: return "Viernes";
                case 6: return "Sábado";
                case 7: return "Domingo";
                default: return string.Empty;
            }
        }

        public int ObtenerHorarioSeleccionado()
        {
            if (gvMain.SelectedIndex > -1)
            {
                string id = gvMain.Rows[gvMain.SelectedIndex].Cells[1].Text;
                return int.Parse(id);
            }
            else
            {
                return -1;
            }
        }

        protected string ObtenerNombreEstado(int id)
        {
            switch (id)
            {
                case 1: return "Vigente";
                default: return "Vencido";
            }    
        }

        private void CargarCodigosFuncionario()
        {
            try
            {
                FuncionarioService service = new FuncionarioService();
                List<FuncionarioArea> codigos = service.ObtenerAreasFuncionario(funcionario.FuncionarioID);
                FuncionarioArea a = new FuncionarioArea()
                { 
                    CodigoFuncionario = "Seleccione el código"
                };
                codigos.Insert(0, a);
                ddlCodigoFuncionario.DataSource = codigos;
                ddlCodigoFuncionario.DataTextField = "CodigoFuncionario";
                ddlCodigoFuncionario.DataValueField = "CodigoFuncionario";
                ddlCodigoFuncionario.DataBind();
            }
            catch (Exception exc)
            {
                DesplegarMensajeError("Ha ocurrido un error cargando los códigos de funcionario: " + exc.Message);
            }
        }

        private void CargarHorarios()
        {
            try
            {
                FuncionarioService service = new FuncionarioService();
                List<HorarioFuncionario> horarios = service.ObtenerHorarios(funcionario.FuncionarioID);
                gvMain.DataSource = horarios;
                gvMain.DataBind();
            }
            catch (Exception exc)
            {
                DesplegarMensajeError("Ha ocurrido un error cargando los horarios: " + exc.Message);
            }
        }

        private void CargarFuncionario()
        {
            try
            {
                FuncionarioService service = new FuncionarioService();
                int id = int.Parse(Session["FuncionarioID"].ToString());
                funcionario = service.ObtenerFuncionario(id);
            }
            catch (Exception exc)
            {
                DesplegarMensajeError("Ha ocurrido un error cargando los datos del funcionario" + exc.Message);
            }
        }

        private void DesplegarMensajeExito(string mensaje)
        {
            SuccessMessage.Visible = true;
            SuccessText.Text = mensaje;
        }

        private void DesplegarMensajeError(string mensaje)
        {
            ErrorMessage.Visible = true;
            FailureText.Text = mensaje;
        }

        protected void gvMain_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            gvMain.SelectedIndex = e.NewSelectedIndex;
            CheckSelectedIndex();
            CargarHorarios();
        }

        private void CheckSelectedIndex()
        {
            try
            {
                if (gvMain.SelectedIndex > -1)
                {
                    phDetalle.Visible = true;
                    ltlHorarioID.Text = ObtenerHorarioSeleccionado().ToString();
                    FuncionarioService service = new FuncionarioService();
                    List<HorarioDetalle> detalles = service.ObtenerDetalle(ObtenerHorarioSeleccionado());
                    gvDetail.DataSource = detalles;
                    gvDetail.DataBind();
                }
            }
            catch (Exception exc)
            {
                DesplegarMensajeError("No se pudo cargar el detalle del horario: " + exc.Message);
            }
        }

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
            CargarHorarios();
        }

        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList dropDown = e.Row.Cells[6].FindControl("ddlEstadoGrid") as DropDownList;
                if (dropDown != null)
                    dropDown.SelectedValue = DataBinder.Eval(e.Row.DataItem, "Estado").ToString();
            }

        }

        protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Habilitar")
            {
                int horarioID = int.Parse(e.CommandArgument.ToString());
                FuncionarioService service = new FuncionarioService();
                int result = service.HabilitarHorario(horarioID, funcionario.FuncionarioID);
                if (result > 0)
                {
                    DesplegarMensajeExito("Horario habilitado con éxito.");
                }
                else
                {
                    DesplegarMensajeError("No se ha podido habilitar el horario.");
                }
                CargarHorarios();
            }
            else if (e.CommandName == "Deshabilitar")
            {
                int horarioID = int.Parse(e.CommandArgument.ToString());
                FuncionarioService service = new FuncionarioService();
                int result = service.DeshabilitarHorario(horarioID, funcionario.FuncionarioID);
                if (result > 0)
                {
                    DesplegarMensajeExito("Horario deshabilitado con éxito.");
                }
                else
                {
                    DesplegarMensajeError("No se ha podido deshabilitar el horario.");
                }
                CargarHorarios();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrmFuncionario", true);
        }

        //Cancelar la creación 
        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            mvMain.SetActiveView(viewDatos);
            CargarHorarios();
        }

        //Guarda nuevo horario
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string codigoFuncionario = ddlCodigoFuncionario.SelectedValue;
                string anio = txtAnio.Text;
                byte periodo = byte.Parse(ddlPeriodo.SelectedValue);
                byte estado = byte.Parse(ddlEstadoForm.SelectedValue);
                if (string.IsNullOrEmpty(anio.Trim()) || !int.TryParse(anio, out int n))
                {
                    DesplegarMensajeError("Debe indicar el año como un valor numérico");
                }
                else
                {
                    FuncionarioService service = new FuncionarioService();
                    int result = service.NuevoHorario(periodo, int.Parse(anio), estado, codigoFuncionario);
                    if (result > 0)
                    {
                        DesplegarMensajeExito("Datos guardados con éxito.");
                    }
                    else
                    {
                        DesplegarMensajeError("Ha ocurrido un error almacenando la información");
                    }
                }
                mvMain.SetActiveView(viewDatos);
                CargarHorarios();
                LimpiarFormulario();
            }
            catch (Exception exc)
            {
                DesplegarMensajeError("Ha ocurrido un error guardando el nuevo horario: "+exc.Message);
            }
        }

        private void LimpiarFormulario()
        {
            txtAnio.Text = string.Empty;
            ddlCodigoFuncionario.SelectedIndex = 0;
            ddlPeriodo.SelectedIndex = 0;
            ddlEstadoForm.SelectedIndex = 0;
            ddlDia.SelectedIndex = 0;
            txtHoraIngreso.Text = string.Empty;
            txtHoraSalida.Text = string.Empty;
            txtMinutoSalida.Text = string.Empty;
            txtMinutoIngreso.Text = string.Empty;
        }

        //Agregar nuevo horario
        protected void btnNew_Click(object sender, EventArgs e)
        {
            mvMain.SetActiveView(viewForm);
        }

        protected void btnAddSchedule_Click(object sender, EventArgs e)
        {
            try
            {
                int dia = int.Parse(ddlDia.SelectedValue.ToString());
                string horaIngreso = txtHoraIngreso.Text.Trim();
                string minutoIngreso = txtMinutoIngreso.Text.Trim();
                string horaSalida = txtHoraSalida.Text.Trim();
                string minutoSalida = txtMinutoSalida.Text.Trim();
                if (dia < 1)
                {
                    DesplegarMensajeError("Debe seleccionar el día");
                }
                else if (string.IsNullOrEmpty(horaIngreso))
                {
                    DesplegarMensajeError("Debe digitar la hora de ingreso");
                }
                else if (string.IsNullOrEmpty(minutoIngreso))
                {
                    DesplegarMensajeError("Debe digitar el minuto de ingreso");
                }
                else if (string.IsNullOrEmpty(horaSalida))
                {
                    DesplegarMensajeError("Debe digitar la hora de salida");
                }
                else if (string.IsNullOrEmpty(minutoSalida))
                {
                    DesplegarMensajeError("Debe digitar el minuto de salida");
                }
                else
                {
                    FuncionarioService service = new FuncionarioService();
                    HorarioDetalle detalle = new HorarioDetalle()
                    {
                        HorarioID = ObtenerHorarioSeleccionado(),
                        Dia = dia,
                        HoraIngreso = byte.Parse(horaIngreso),
                        MinutoIngreso = byte.Parse(minutoIngreso),
                        HoraSalida = byte.Parse(horaSalida),
                        MinutoSalida = byte.Parse(minutoSalida)
                    };
                    int result = service.AgregarHorarioDetalle(detalle);
                    CargarHorarios();
                    CheckSelectedIndex();
                    LimpiarFormulario();
                }
            }
            catch (Exception exc)
            {
                DesplegarMensajeError("Ha ocurrido un error agregando el día: " + exc.Message);
            }
        }

        protected void gvMain_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int rowid = e.RowIndex;
                int horarioID = int.Parse(gvMain.Rows[rowid].Cells[1].Text);
                FuncionarioService service = new FuncionarioService();
                int result = service.BorrarHorario(horarioID);
                if (result > 0)
                {
                    DesplegarMensajeExito("Datos eliminados con éxito");
                }
                else
                {
                    DesplegarMensajeError("Ha ocurrido un error eliminando el horario.");
                }
                CargarHorarios();
                CheckSelectedIndex();
            }
            catch (Exception exc)
            {
                DesplegarMensajeError("Ha ocurrido un error eliminando el horario: " + exc.Message);
            }
        }

        protected void gvDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int rowid = e.RowIndex;
                int horarioID = int.Parse(gvDetail.Rows[rowid].Cells[0].Text);
                int dia = int.Parse(gvDetail.Rows[rowid].Cells[1].Text);
                FuncionarioService service = new FuncionarioService();
                int result = service.BorrarHorarioDetalle(horarioID, dia);
                if (result > 0)
                {
                    DesplegarMensajeExito("Datos eliminados con éxito");
                }
                else
                {
                    DesplegarMensajeError("Ha ocurrido un error eliminando el horario.");
                }
                CargarHorarios();
                CheckSelectedIndex();
            }
            catch (Exception exc)
            {
                DesplegarMensajeError("Ha ocurrido un error eliminando el horario: " + exc.Message);
            }
        }

        protected void gvDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
            CheckSelectedIndex();
        }
    }
}
