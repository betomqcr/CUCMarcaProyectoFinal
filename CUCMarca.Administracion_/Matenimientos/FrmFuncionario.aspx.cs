using CUCMarca.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CUCMarca.BusinessServices;
using Microsoft.Ajax.Utilities;

namespace CUCMarca.Administracion.Matenimientos
{
    public partial class FrmFuncionario : System.Web.UI.Page
    {
        public List<TipoIdentificacion> tiposId;
        public List<TipoFuncionario> tiposFuncionario;
        public List<Area> listaAreas;

        private class FuncionarioInfo
        {
            public int Id { get; set; }
            public string Identificacion {get;set;}
            public string Nombre {get;set;}
            public string Apellidos {get;set;}
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            ErrorMessage.Visible = false;
            SuccessMessage.Visible = false;
            IniciarID();
            IniciarTipoF();
            IniciarAreas();
            if (!IsPostBack)
            {
                mvMain.SetActiveView(viewDatos);
                CargarTipoID();
                CargarTipoF();
                CargarFuncionarios();
                CargarAreas();
            }
            CheckSelectedIndex();
        }

        private void LimpiarFormulario()
        {
            txtApellidos.Text = string.Empty;
            txtContrasena.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtIdentificacion.Text = string.Empty;
            txtNombre.Text = string.Empty;
            ddlTipoFuncionario.SelectedIndex = 0;
            ddlTipoIdentificacion.SelectedIndex = 0;
            txtNewPassword.Text = string.Empty;
            ddlAreaFuncionario.SelectedIndex = 0;
            txtCodigoFuncionario.Text = string.Empty;
        }

        private void CargarFuncionarios()
        {
            try
            {
                FuncionarioService service = new FuncionarioService();
                List<Funcionario> funcionarios = service.ObtenerFuncionarios();
                gvMain.DataSource = funcionarios;
                gvMain.DataBind();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error cargando los funcionario: " + exc.Message;
            }
        }

        public string ObtenerNombreTipoFuncionario(int tipoID)
        {
            IEnumerable<string> nombre = from l in tiposFuncionario
                                         where l.TipoFuncionarioID == tipoID
                                         select l.Nombre;
            return nombre.FirstOrDefault();
        }

        public string ObtenerNombreArea(int areaID)
        {
            IEnumerable<string> nombre = from l in listaAreas
                                         where l.AreaID == areaID
                                         select l.Nombre;
            return nombre.FirstOrDefault();
        }

        public string ObtenerNombreEstado(int estadoid)
        {
            switch (estadoid)
            {
                case 1: return "Activo";
                default: return "Inactivo";
            }
        }

        public string ObtenerNombreTipoIdentificacion(int tipoID)
        {
            IEnumerable<string> nombre = from l in tiposId
                                         where l.TipoIdentificacionID == tipoID
                                         select l.Nombre;
            return nombre.FirstOrDefault();
        }

        private void CargarAreas()
        {
            try
            {
                listaAreas.Insert(0, new Area()
                {
                    AreaID = 0,
                    Nombre = "Seleccione el área"
                });
                ddlAreaFuncionario.DataSource = listaAreas;
                ddlAreaFuncionario.DataTextField = "Nombre";
                ddlAreaFuncionario.DataValueField = "AreaID";
                ddlAreaFuncionario.DataBind();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error cargando las áreas: " + exc.Message;
            }
        }

        private void CargarTipoF()
        {
            try
            {
                tiposFuncionario.Insert(0, new TipoFuncionario()
                {
                    TipoFuncionarioID = 0,
                    Nombre = "Seleccione el tipo de funcionario"
                });
                ddlTipoFuncionario.DataSource = tiposFuncionario;
                ddlTipoFuncionario.DataTextField = "Nombre";
                ddlTipoFuncionario.DataValueField = "TipoFuncionarioID";
                ddlTipoFuncionario.DataBind();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error cargando los tipos de funcionario: " + exc.Message;
            }
        }

        private void CargarTipoID()
        {
            try
            {
                tiposId.Insert(0, new TipoIdentificacion()
                {
                    TipoIdentificacionID = 0,
                    Nombre = "Seleccione el tipo de identificación"
                });
                ddlTipoIdentificacion.DataSource = tiposId;
                ddlTipoIdentificacion.DataTextField = "Nombre";
                ddlTipoIdentificacion.DataValueField = "TipoIdentificacionID";
                ddlTipoIdentificacion.DataBind();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error cargando los tipos de identificación: " + exc.Message;
            }
        }

        private void IniciarAreas()
        {
            try
            {
                AreaService service = new AreaService();
                listaAreas = service.ObtenerAreas();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error cargando las áreas: " + exc.Message;
            }
        }

        private void IniciarTipoF()
        {
            try
            {
                FuncionarioService funcService = new FuncionarioService();
                tiposFuncionario = funcService.ObtenerTipoFuncionario();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error cargando los tipos de funcionario: " + exc.Message;
            }
        }

        private void IniciarID()
        {
            try
            {
                FuncionarioService uService = new FuncionarioService();
                tiposId = uService.ObtenerTiposIdentificacion();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error cargando los tipos de identificación: " + exc.Message;
            }
        }

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
            CargarFuncionarios();
        }

        protected void gvMain_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMain.EditIndex = -1;
            CargarFuncionarios();
        }

        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList dropDown = e.Row.Cells[2].FindControl("ddlTipoIdentificacionGrid") as DropDownList;
                if (dropDown != null)
                    dropDown.SelectedValue = DataBinder.Eval(e.Row.DataItem, "TipoIdentificacionID").ToString();
                DropDownList dropDownF = e.Row.Cells[2].FindControl("ddlTipoFuncionarioGrid") as DropDownList;
                if (dropDownF != null)
                    dropDownF.SelectedValue = DataBinder.Eval(e.Row.DataItem, "TipoFuncionarioID").ToString();
                //ddlEstadoFuncionarioGrid
                DropDownList dropDownEstado = e.Row.Cells[2].FindControl("ddlEstadoFuncionarioGrid") as DropDownList;
                if (dropDownEstado != null)
                    dropDownEstado.SelectedValue = DataBinder.Eval(e.Row.DataItem, "Estado").ToString();
            }
        }

        protected void gvMain_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int rowid = e.RowIndex;
                int fid = int.Parse(gvMain.Rows[rowid].Cells[1].Text);
                FuncionarioService service = new FuncionarioService();
                int result = service.BorrarFuncionario(fid);
                if (result > 0)
                {
                    SuccessMessage.Visible = true;
                    SuccessText.Text = "Datos eliminados con éxito.";
                }
                else
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Ha ocurrido un error eliminando el funcionario.";
                }
                CargarFuncionarios();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error borrando los funcionarios" + exc.Message;
            }
        }

        protected void gvMain_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMain.EditIndex = e.NewEditIndex;
            CargarFuncionarios();
        }

        protected void gvMain_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int rowid = e.RowIndex;

                int id = int.Parse(gvMain.Rows[rowid].Cells[1].Text);

                DropDownList ddlTipoID = gvMain.Rows[rowid].FindControl("ddlTipoIdentificacionGrid") as DropDownList;
                int tipoIdent = int.Parse(ddlTipoID.SelectedValue);

                string identificacion = e.NewValues["Identificacion"] == null ? string.Empty : e.NewValues["Identificacion"].ToString();
                string nombre = e.NewValues["Nombre"] == null ? string.Empty : e.NewValues["Nombre"].ToString();
                string apellidos = e.NewValues["Apellido"] == null ? string.Empty : e.NewValues["Apellido"].ToString();

                DropDownList ddlTipoFunc = gvMain.Rows[rowid].FindControl("ddlTipoFuncionarioGrid") as DropDownList;
                int tipoFunc = int.Parse(ddlTipoFunc.SelectedValue);

                string correo = e.NewValues["Correo"] == null ? string.Empty : e.NewValues["Correo"].ToString();

                DropDownList ddlEstado = gvMain.Rows[rowid].FindControl("ddlEstadoFuncionarioGrid") as DropDownList;
                byte estado = byte.Parse(ddlEstado.SelectedValue);

                if (id == 0)
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "No se pudo determinar el funcionario.";
                }
                else if (tipoIdent < 1)
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Debe indicar tipo de identificación.";
                }
                else if (string.IsNullOrEmpty(identificacion.Trim()))
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Debe indicar la identificación del funcionario";
                }
                else if (string.IsNullOrEmpty(nombre.Trim()))
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Debe indicar el nombre funcionario";
                }
                else if (string.IsNullOrEmpty(apellidos.Trim()))
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Debe indicar los apellidos del funcionario";
                }
                else if (tipoFunc < 1)
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Debe indicar el tipo del funcionario";
                }
                else if (string.IsNullOrEmpty(correo.Trim()))
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Debe indicar el correo del funcionario";
                }
                else
                {
                    Funcionario f = new Funcionario()
                    {
                        Apellido = apellidos,
                        Correo = correo,
                        Estado = estado,
                        FuncionarioID = id,
                        Identificacion = identificacion,
                        Nombre = nombre,
                        TipoFuncionarioID = tipoFunc,
                        TipoIdentificacionID = tipoIdent
                    };
                    FuncionarioService service = new FuncionarioService();
                    int result = service.ActualizarFuncionario(f);
                    if (result > 0)
                    {
                        SuccessMessage.Visible = true;
                        SuccessText.Text = "Datos actualizados con éxito.";
                    }
                    else
                    {
                        ErrorMessage.Visible = true;
                        FailureText.Text = "Ha ocurrido un error actualizando los funcionarios";
                    }
                }
                gvMain.EditIndex = -1;
                CargarFuncionarios();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error actualizando los funcionarios: " + exc.Message;
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            mvMain.SetActiveView(viewForm);
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
                int tipoIdent = int.Parse(ddlTipoIdentificacion.SelectedValue);
                string identificacion = txtIdentificacion.Text;
                string nombre = txtNombre.Text;
                string apellidos = txtApellidos.Text;
                int tipoFunc = int.Parse(ddlTipoFuncionario.SelectedValue);
                string correo = txtCorreo.Text;
                string contrasena = txtContrasena.Text;

                if (tipoIdent < 1)
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Debe indicar tipo de identificación.";
                }
                else if (string.IsNullOrEmpty(identificacion.Trim()))
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Debe indicar la identificación del funcionario";
                }
                else if (string.IsNullOrEmpty(nombre.Trim()))
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Debe indicar el nombre funcionario";
                }
                else if (string.IsNullOrEmpty(apellidos.Trim()))
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Debe indicar los apellidos del funcionario";
                }
                else if (tipoFunc < 1)
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Debe indicar el tipo del funcionario";
                }
                else if (string.IsNullOrEmpty(correo.Trim()))
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Debe indicar el correo del funcionario";
                }
                else if (string.IsNullOrEmpty(contrasena.Trim()))
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Debe indicar la contraseña del funcionario";
                }
                else
                {
                    FuncionarioService service = new FuncionarioService();
                    Funcionario f = new Funcionario()
                    { 
                        Apellido = apellidos,
                        Contrasena = contrasena,
                        Correo = correo,
                        Estado = 1,
                        Identificacion = identificacion,
                        Nombre = nombre,
                        TipoFuncionarioID = tipoFunc,
                        TipoIdentificacionID = tipoIdent
                    };
                    int result = service.NuevoFuncionario(f);
                    if (result > 0)
                    {
                        SuccessMessage.Visible = true;
                        SuccessText.Text = "Datos guardados con éxito.";
                    }
                    else
                    {
                        ErrorMessage.Visible = true;
                        FailureText.Text = "Ha ocurrido un error guardando el funcionario";
                    }
                }
                mvMain.SetActiveView(viewDatos);
                CargarFuncionarios();
                LimpiarFormulario();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error guardando el funcionario: " + exc.Message;
            }
        }

        protected void lnkAutogenerate_Click(object sender, EventArgs e)
        {
            try
            {
                txtContrasena.Text = UtilsService.RandomPassword(10);
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error generando la contraseña: " + exc.Message;
            }
        }

        protected void gvMain_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            gvMain.SelectedIndex = e.NewSelectedIndex;
            CheckSelectedIndex();
            CargarFuncionarios();
        }

        private void CheckSelectedIndex()
        {
            if (gvMain.SelectedIndex > -1)
            {
                string identificacion = gvMain.Rows[gvMain.SelectedIndex].Cells[3].Text;
                string nombre = gvMain.Rows[gvMain.SelectedIndex].Cells[4].Text;
                string apellidos = gvMain.Rows[gvMain.SelectedIndex].Cells[5].Text;
                SelectedMessage.Visible = true;
                SelectedText.Text = string.Format("Dato seleccionado: {0} {1} {2}", identificacion, nombre, apellidos);
            }
        }

        private FuncionarioInfo GetInfo()
        {
            if (gvMain.SelectedIndex > -1)
            {
                string id = gvMain.Rows[gvMain.SelectedIndex].Cells[1].Text;
                string identificacion = gvMain.Rows[gvMain.SelectedIndex].Cells[3].Text;
                string nombre = gvMain.Rows[gvMain.SelectedIndex].Cells[4].Text;
                string apellidos = gvMain.Rows[gvMain.SelectedIndex].Cells[5].Text;
                return new FuncionarioInfo()
                {
                    Id = int.Parse(id),
                    Identificacion = identificacion,
                    Nombre = nombre,
                    Apellidos = apellidos
                };
            }
            else
            {
                return null;
            }
        }

        private bool IsSelected()
        {
            return gvMain.SelectedIndex > -1;
        }

        private bool IsAreaSelected()
        {
            return gvAreas.SelectedIndex > -1;
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (IsSelected())
            {
                mvMain.SetActiveView(viewChangePassword);
                FuncionarioInfo info = GetInfo();
                ltlFData.Text = string.Format("{0} {1} {2}", info.Identificacion, info.Nombre, info.Apellidos);
            }
            else
            {
                ShowSelectMessage();
            }
        }

        protected void lnkArea_Click(object sender, EventArgs e)
        {
            if (IsSelected())
            {
                mvMain.SetActiveView(viewAreas);
                FuncionarioInfo info = GetInfo();
                CargarAreasFuncionario(info.Id);
                ltlAreasUsuario.Text = string.Format("{0} {1} {2}", info.Identificacion, info.Nombre, info.Apellidos);
            }
            else
            {
                ShowSelectMessage();
            }
        }

        private void CargarAreasFuncionario(int funcionarioID)
        {
            try
            {
                FuncionarioService service = new FuncionarioService();
                gvAreas.DataSource = service.ObtenerAreasFuncionario(funcionarioID);
                gvAreas.DataBind();

            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error cargando las áreas " + exc.Message;
            }
        }

        private void ShowSelectMessage()
        {
            ErrorMessage.Visible = true;
            FailureText.Text = "Debe seleccionar un funcionario";
        }

        private void ShowSelectAreaMessage()
        {
            ErrorMessage.Visible = true;
            FailureText.Text = "Debe seleccionar un área";
        }

        protected void lnkCancelPassword_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            mvMain.SetActiveView(viewDatos);
        }

        protected void lnkSavePassword_Click(object sender, EventArgs e)
        {
            try
            {
                FuncionarioInfo info = GetInfo();
                FuncionarioService service = new FuncionarioService();
                string clave = txtNewPassword.Text;
                if (string.IsNullOrEmpty(clave))
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Debe digitar la nueva clave.";
                }
                else
                {
                    int result = service.ActualizarClave(info.Id, clave);
                    if (result > 0)
                    {
                        SuccessMessage.Visible = true;
                        SuccessText.Text = "Datos guardados con éxito.";
                    }
                    else
                    {
                        ErrorMessage.Visible = true;
                        FailureText.Text = "Ha ocurrido un error guardando la contraseña";
                    }
                }
                mvMain.SetActiveView(viewDatos);
                CargarFuncionarios();
                LimpiarFormulario();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error guardando la contraseña: " + exc.Message;
            }
        }

        protected void lnkBtnGenerateNewPassword_Click(object sender, EventArgs e)
        {
            try
            {
                txtNewPassword.Text = UtilsService.RandomPassword(10);
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error generando la contraseña: " + exc.Message;
            }
        }

        //Volver
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            mvMain.SetActiveView(viewDatos);
        }

        //Agregar area a funcionario
        protected void LinkButton1_Click1(object sender, EventArgs e)
        {
            try
            {
                string codigo = txtCodigoFuncionario.Text;
                int areaID = int.Parse(ddlAreaFuncionario.SelectedValue);
                FuncionarioInfo info = GetInfo();
                if (string.IsNullOrEmpty(codigo.Trim()))
                {
                    DesplegarMensajeError("Debe digitar el código del funcionario");
                }
                else if (areaID <= 0)
                {
                    DesplegarMensajeError("Debe seleccionar el área a asociar.");
                }
                else
                {
                    
                    FuncionarioService service = new FuncionarioService();
                    int result = service.AsociarArea(info.Id, areaID, codigo);
                    if (result <= 0)
                    {
                        DesplegarMensajeError("Ha ocurrido un error asociando el funcionario al área.");
                    }
                    else
                    {
                        DesplegarMensajeExito("Datos asociados correctamente.");
                    }
                }
                CargarAreasFuncionario(info.Id);
                LimpiarFormulario();
            }
            catch (Exception exc)
            {
                DesplegarMensajeError("Ha ocurrido un error asociando el funcionario al área: " + exc.Message);
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

        protected void gvAreas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList dropDown = e.Row.Cells[2].FindControl("ddlAreaFuncionarioGrid") as DropDownList;
                if (dropDown != null)
                    dropDown.SelectedValue = DataBinder.Eval(e.Row.DataItem, "AreaID").ToString();
            }
        }

        protected void gvAreas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
            FuncionarioInfo info = GetInfo();
            CargarAreasFuncionario(info.Id);
        }

        protected void gvAreas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int rowid = e.RowIndex;
                string codigo = gvAreas.Rows[rowid].Cells[1].Text;
                FuncionarioService service = new FuncionarioService();
                FuncionarioInfo info = GetInfo();
                int result = service.DeasociarArea(codigo);
                if (result > 0)
                {
                    DesplegarMensajeExito("Datos eliminados con éxito.");
                }
                else
                {
                    DesplegarMensajeError("Ha ocurrido un error eliminando el área asociada.");
                }
                CargarAreasFuncionario(info.Id);
            }
            catch (Exception exc)
            {
                DesplegarMensajeError("Ha ocurrido un error eliminando el área asociada: " + exc.Message);
            }
        }

        protected void gvAreas_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            gvAreas.SelectedIndex = e.NewSelectedIndex;
            CheckSelectedIndexArea();
            FuncionarioInfo info = GetInfo();
            CargarAreasFuncionario(info.Id);
        }

        private string ObtenerCodigoSeleccionado()
        {
            return gvAreas.Rows[gvAreas.SelectedIndex].Cells[1].Text;
        }

        private void CheckSelectedIndexArea()
        {
            if (gvAreas.SelectedIndex > -1)
            {
                string codigo = gvAreas.Rows[gvAreas.SelectedIndex].Cells[1].Text;
                SelectedAreaMessage.Visible = true;
                ltlAreaSelected.Text = string.Format("Dato seleccionado: {0}", codigo);
            }
        }

        protected void lnkSchedule_Click(object sender, EventArgs e)
        {
            if (IsSelected())
            {
                FuncionarioInfo info = GetInfo();
                Session["FuncionarioID"] = info.Id;
                Response.Redirect("FrmHorarios", true);
            }
            else
            {
                ShowSelectMessage();
            }
        }
    }
}
