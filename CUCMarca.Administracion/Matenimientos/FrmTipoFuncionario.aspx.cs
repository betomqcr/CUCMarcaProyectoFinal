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
    public partial class FrmTipoFuncionario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ErrorMessage.Visible = false;
            SuccessMessage.Visible = false;
            if (!IsPostBack)
            {
                mvMain.SetActiveView(viewDatos);
                CargarTipoFuncionario();
            }
        }

        private void CargarTipoFuncionario()
        {
            try
            {
                FuncionarioService service = new FuncionarioService();
                List<TipoFuncionario> listaTipo = service.ObtenerTipoFuncionario();
                gvMain.DataSource = listaTipo;
                gvMain.DataBind();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error cargando los tipos de funcionario: " + exc.Message;
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            mvMain.SetActiveView(viewForm);
        }

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
            CargarTipoFuncionario();
        }

        protected void gvMain_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMain.EditIndex = -1;
            CargarTipoFuncionario();
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
                int result = service.BorrarTipoFuncionario(tipoid);
                if (result > 0)
                {
                    SuccessMessage.Visible = true;
                    SuccessText.Text = "Datos eliminados con éxito.";
                }
                else
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Ha ocurrido un error actualizando el tipo de funcionario";
                }
                CargarTipoFuncionario();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error borrando el tipo de funcionario: " + exc.Message;
            }
        }

        protected void gvMain_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMain.EditIndex = e.NewEditIndex;
            CargarTipoFuncionario();
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
                    FailureText.Text = "Debe indicar el nombre del tipo de funcionario.";
                }
                else
                {
                    FuncionarioService service = new FuncionarioService();
                    int result = service.ActualizarTipoFuncionario(tipoId, nombre);
                    if (result > 0)
                    {
                        SuccessMessage.Visible = true;
                        SuccessText.Text = "Datos actualizados con éxito.";
                    }
                    else
                    {
                        ErrorMessage.Visible = true;
                        FailureText.Text = "Ha ocurrido un error actualizando los tipos de funcionario.";
                    }
                }
                gvMain.EditIndex = -1;
                CargarTipoFuncionario();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error actualizando los tipos de funcionario: " + exc.Message;
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
                    FailureText.Text = "Debe indicar el nombre del tipo de funcionario.";
                }
                else
                {
                    FuncionarioService service = new FuncionarioService();
                    int result = service.NuevoTipoFuncionario(nombre);
                    if (result > 0)
                    {
                        SuccessMessage.Visible = true;
                        SuccessText.Text = "Datos guardados con éxito.";
                    }
                    else
                    {
                        ErrorMessage.Visible = true;
                        FailureText.Text = "Ha ocurrido un error guardando el tipo de funcionario";
                    }
                }
                mvMain.SetActiveView(viewDatos);
                CargarTipoFuncionario();
                LimpiarFormulario();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error guardando el tipo de funcionario: " + exc.Message;
            }
        }

        private void LimpiarFormulario()
        {
            txtNombre.Text = string.Empty;
        }
    }
}