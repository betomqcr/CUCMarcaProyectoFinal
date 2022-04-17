using CUCMarca.BusinessServices;
using CUCMarca.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CUCMarca.Administracion.Matenimientos
{
    public partial class FrmTipoIdentificacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ErrorMessage.Visible = false;
            SuccessMessage.Visible = false;
            if (!IsPostBack)
            {
                mvMain.SetActiveView(viewDatos);
                CargarTipoIdentificacion();
            }
        }

        private void CargarTipoIdentificacion()
        {
            try
            {
                FuncionarioService service = new FuncionarioService();
                List<TipoIdentificacion> listaTipo = service.ObtenerTiposIdentificacion();
                gvMain.DataSource = listaTipo;
                gvMain.DataBind();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error cargando los tipos de identificación: " + exc.Message;
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            mvMain.SetActiveView(viewForm);
        }

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
            CargarTipoIdentificacion();
        }

        protected void gvMain_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMain.EditIndex = -1;
            CargarTipoIdentificacion();
        }

        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvMain_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int rowid = e.RowIndex;
                int tipoid = int.Parse(gvMain.Rows[rowid].Cells[0].Text);
                FuncionarioService service = new FuncionarioService();
                int result = service.BorrarTipoIdentificacion(tipoid);
                if (result > 0)
                {
                    SuccessMessage.Visible = true;
                    SuccessText.Text = "Datos eliminados con éxito.";
                }
                else
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Ha ocurrido un error actualizando el tipo de identificación";
                }
                CargarTipoIdentificacion();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error borrando el tipo de identificación: " + exc.Message;
            }
        }

        protected void gvMain_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMain.EditIndex = e.NewEditIndex;
            CargarTipoIdentificacion();
        }

        protected void gvMain_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int rowid = e.RowIndex;
                string nombre = e.NewValues["Nombre"] == null ? string.Empty : e.NewValues["Nombre"].ToString();
                int tipoId = int.Parse(gvMain.Rows[rowid].Cells[0].Text);
                if (string.IsNullOrEmpty(nombre))
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Debe indicar el nombre del tipo de identificación.";
                }
                else
                {
                    FuncionarioService service = new FuncionarioService();
                    int result = service.ActualizarTipoIdentificacion(tipoId, nombre);
                    if (result > 0)
                    {
                        SuccessMessage.Visible = true;
                        SuccessText.Text = "Datos actualizados con éxito.";
                    }
                    else
                    {
                        ErrorMessage.Visible = true;
                        FailureText.Text = "Ha ocurrido un error actualizando los tipos de identificación.";
                    }
                }
                gvMain.EditIndex = -1;
                CargarTipoIdentificacion();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error actualizando los tipos de identificación: " + exc.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            mvMain.SetActiveView(viewDatos);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtNombre.Text;
                if (string.IsNullOrEmpty(nombre))
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Debe indicar el nombre del tipo de identificación.";
                }
                else
                {
                    FuncionarioService service = new FuncionarioService();
                    int result = service.NuevoTipoIdentificacion(nombre);
                    if (result > 0)
                    {
                        SuccessMessage.Visible = true;
                        SuccessText.Text = "Datos guardados con éxito.";
                    }
                    else
                    {
                        ErrorMessage.Visible = true;
                        FailureText.Text = "Ha ocurrido un error guardando el tipo de identificación";
                    }
                }
                mvMain.SetActiveView(viewDatos);
                CargarTipoIdentificacion();
                LimpiarFormulario();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error guardando el tipo de identificacion: " + exc.Message;
            }
        }

        private void LimpiarFormulario()
        {
            txtNombre.Text = string.Empty;
        }
    }
}