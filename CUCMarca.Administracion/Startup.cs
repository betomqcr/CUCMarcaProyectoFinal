using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CUCMarca.Administracion.Startup))]
namespace CUCMarca.Administracion
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
