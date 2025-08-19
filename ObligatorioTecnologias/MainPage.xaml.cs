using ObligatorioTecnologias.Ventanas;
using ObligatorioTecnologias.Services;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;

namespace ObligatorioTecnologias
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private readonly DatabaseService _db;

        // Inyectamos DatabaseService
        public MainPage(DatabaseService db)
        {
            InitializeComponent();
            _db = db;
        }

        // Navegación a páginas
        private async void btnPagina1_Clicked(object sender, EventArgs e)
        {
            // Pasamos el servicio a la página de usuarios
            await Navigation.PushAsync(new Usuarios(_db));
        }

        private async void btnPagina2_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Cine());
        }

        private async void btnPaginaCotizaciones_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Cotizaciones());
        }

        private async void btnPaginaNoticias_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Noticias());
        }

        private async void btnPaginaPatrocinadores_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Patrocinadores());
        }

        private async void btnPaginaClima_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Clima());
        }

        private async void btnPaginaLogin_Clicked(object sender, EventArgs e)
        {
            try
            {
                var request = new AuthenticationRequestConfiguration("obligatorio", "Para probar la huella");

                var resul = await CrossFingerprint.Current.AuthenticateAsync(request);

                if (resul.Authenticated)
                {
                    await DisplayAlert("Éxito", "La operación se completó correctamente.", "OK");
                }
                else
                {
                    await DisplayAlert("Error de autenticación", "No se pudo autenticar.", "Cerrar");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Activar Login", "Se produjo un error al activar el login.", "Cerrar");
            }
        }
    }
}
