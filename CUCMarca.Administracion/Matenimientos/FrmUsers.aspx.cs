using CUCMarca.Administracion.Models;
using CUCMarca.BusinessServices;
using CUCMarca.DataAccess;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace CUCMarca.Administracion.Matenimientos
{
    public partial class FrmUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ErrorMessage.Visible = false;
            SuccessMessage.Visible = false;
            if (!IsPostBack)
            {
                mvMain.SetActiveView(viewDatos);
                CargarUsuarios();
            }
        }

        private void CargarUsuarios()
        {
            try
            {
                UsuariosService service = new UsuariosService();
                List<AspNetUsers> listaUsuarios = service.ObtenerUsuarios();
                gvMain.DataSource = listaUsuarios;
                gvMain.DataBind();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error cargando los usuarios: " + exc.Message;
            }
        }

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
            CargarUsuarios();
        }

        protected void gvMain_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvMain_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int rowid = e.RowIndex;
                string userid = gvMain.Rows[rowid].Cells[0].Text;
                IOwinContext context = Context.GetOwinContext();
                var manager = context.GetUserManager<ApplicationUserManager>();
                var user = manager.Users.FirstOrDefault(x => x.Id == userid);
                IdentityResult result = manager.Delete(user);
                if (result == IdentityResult.Success)
                {
                    SuccessMessage.Visible = true;
                    SuccessText.Text = "Datos eliminados con éxito.";
                }
                else
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Ha ocurrido un error eliminando el usuario" + result.Errors.FirstOrDefault();
                }
                CargarUsuarios();
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error borrando el usuario: " + exc.Message;
            }
        }

        protected void gvMain_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvMain_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

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
                string contra = txtPassword.Text;
                string username = txtNombre.Text;

                if (string.IsNullOrEmpty(username.Trim()) || !UtilsService.IsValidEmail(username))
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Debe indicar un correo eletrónico válido.";
                }
                else if (string.IsNullOrEmpty(contra.Trim()) || contra.Length < 8 || contra.Length > 20)
                {
                    ErrorMessage.Visible = true;
                    FailureText.Text = "Debe indicar una contraseña válida. No puede ser en blanco, menor a 8 caracteres ni mayor a 20.";
                }
                else
                {
                    var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
                    var user = new ApplicationUser() { UserName = username, Email = username };
                    IdentityResult result = manager.Create(user, contra);
                    if (result.Succeeded)
                    {
                        // Para obtener más información sobre cómo habilitar la confirmación de cuentas y el restablecimiento de contraseña, visite https://go.microsoft.com/fwlink/?LinkID=320771
                        //string code = manager.GenerateEmailConfirmationToken(user.Id);
                        //string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                        //manager.SendEmail(user.Id, "Confirmar cuenta", "Para confirmar la cuenta, haga clic <a href=\"" + callbackUrl + "\">aquí</a>.");

                        //signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                        //IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                        
                        mvMain.SetActiveView(viewDatos);
                        CargarUsuarios();
                        SuccessMessage.Visible = true;
                        bool enableMail = false;
                        bool.TryParse(ConfigurationManager.AppSettings["enableMail"], out enableMail);
                        if (enableMail)
                        {
                            SendMail correo = new SendMail();
                            string mensaje = string.Format("Estimado usuario administrador, se ha registrado con éxito su usuario {0} su contraseña de ingreso es: {1}. Por favor proceda a ingresar y a realizar el cambio de contraseña.", username, contra);
                            SendMailResponse envio = correo.SendEmail(mensaje, "Registro en control de asistencia", username, false);

                            SuccessText.Text = "Usuario registrado con éxito. " + envio.Message;
                        }
                        else
                        {
                            SuccessText.Text = "Usuario registrado con éxito. Envío de correo deshabilitado en configuración";
                        }
                        LimpiarFormulario();
                    }
                    else
                    {
                        ErrorMessage.Visible = true;
                        FailureText.Text = "Ha ocurrido un error creando el usuario: " + result.Errors.FirstOrDefault();
                    }
                }
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error creando el usuario: " + exc.Message;
            }
        }

        private void LimpiarFormulario()
        {
            txtNombre.Text = string.Empty;
            txtPassword.Text = string.Empty;
        }

        protected void lnkAutogenerate_Click(object sender, EventArgs e)
        {
            try
            {
                txtPassword.Text = UtilsService.RandomPassword(10);
            }
            catch (Exception exc)
            {
                ErrorMessage.Visible = true;
                FailureText.Text = "Ha ocurrido un error cargando generando la contraseña: " + exc.Message;
            }
        }
    }
}