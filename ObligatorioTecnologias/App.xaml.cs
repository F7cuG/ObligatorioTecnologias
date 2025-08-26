using ObligatorioTecnologias.Models;
using ObligatorioTecnologias.Services;

namespace ObligatorioTecnologias
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Ventanas.LoginPage());
        }
        public async Task CrearUsuarioPorDefecto()
        {
            var user = await UsuarioService.GetUsuarioByNombreAsync("AgustinEtchepare");
            if (user == null)
            {
                var usuario = new Usuario
                {
                    NombreUsuario = "demo",
                    Contraseña = "demo",
                    NombreCompleto = "Usuario Demo",
                    Direccion = "Dirección Demo",
                    Telefono = "099000000",
                    Email = "demo@demo.com",
                    FotoPerfil = ""
                };
                await UsuarioService.SaveUsuarioAsync(usuario);
            }
        }
    }



}
