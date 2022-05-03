using CUCMarca.Models;
using CUCMarca.Servicios;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;


namespace CUCMarca.Controllers
{
    public class LoginController : ApiController
    {
        private CUCMarcaEntities db = new CUCMarcaEntities();
        

        C_Token token = new C_Token();
        Bitacora bitacora = new Bitacora();
        // GET: api/Login/5
        [HttpPost]
        //[ResponseType(typeof(Funcionario))]
        public IHttpActionResult Funcionario(LoginUser usuario)
        {
            try
            {
                
                Funcionario funcionario = db.Funcionario.FirstOrDefault(x => x.Identificacion == usuario.Identificacion && x.Contrasena == usuario.Contraseña);
                if (funcionario == null)
                {
                    bitacora.accion = "Login";
                    bitacora.descripcion = "El usuario/clave no existe, error  de login ";
                    bitacora.fecha = DateTime.Now;
                    bitacora.tipo = "Error";
                    bitacora.usuario = usuario.Identificacion;
                    bitacora.ins_bitacora();
                    return NotFound();
                }
                else
                {
                    string t = token.generarToken();
                    var TokenN = TokenGenerator.GenerateTokenJwt(usuario.Identificacion);

                    bitacora.accion = "Login";
                    bitacora.descripcion = "Login correcto. Token generado : " + TokenN;
                    bitacora.fecha = DateTime.Now;
                    bitacora.tipo = "Accion";
                    bitacora.usuario = usuario.Identificacion;
                    bitacora.ins_bitacora();
                    //var TokenN = TokenGenerator.GenerateTokenJwt(usuario.Identificacion);
                    // return Ok(t);
                    return Ok(TokenN);
                }


            }
            catch (Exception ex)
            {
                bitacora.accion = "Login";
                bitacora.descripcion = "Error al realizar login: " + ex.Message;
                bitacora.tipo = "Error";
                bitacora.fecha = DateTime.Now;
                bitacora.usuario = usuario.Identificacion;
                bitacora.ins_bitacora();
                return InternalServerError(ex);
            }

        }

       
        // POST: api/Login
        [ResponseType(typeof(Funcionario))]
        public IHttpActionResult PostFuncionario(string id, string pass, string newpass)
        {
            try
            {
                Funcionario func = db.Funcionario.FirstOrDefault(x => x.Identificacion == id && x.Contrasena == pass);

                if (func != null)
                {
                    func.Contrasena = newpass;

                    db.SaveChanges();

                    string tok = token.generarToken();
                    bitacora.accion = "Login";
                    bitacora.descripcion = "Cambio de contraseña realizado correctamente con token: " + tok;
                    bitacora.tipo = "Accion";
                    bitacora.fecha = DateTime.Now;
                    bitacora.usuario = id;
                    bitacora.ins_bitacora();
                    return Ok(tok);

                }
                else{
                    bitacora.accion = "Login";
                    bitacora.descripcion = "Usuario no encontrado";
                    bitacora.tipo = "Error";
                    bitacora.fecha = DateTime.Now;
                    bitacora.usuario = id;
                    bitacora.ins_bitacora();
                    return NotFound();
                }


                
            }catch(Exception ex)
            {
                bitacora.accion = "Login";
                bitacora.descripcion = "Error interno: " + ex.Message;
                bitacora.tipo = "Error";
                bitacora.fecha = DateTime.Now;
                bitacora.usuario = id;
                bitacora.ins_bitacora();
                return InternalServerError(ex);
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FuncionarioExists(int id)
        {
            return db.Funcionario.Count(e => e.FuncionarioID == id) > 0;
        }
    }
}