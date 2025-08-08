using ObligatorioTecnologias.Ventanas;
using System.Threading.Tasks;

namespace ObligatorioTecnologias
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private async void btnPagina1_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Usuarios());
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

        private async Task btnPaginaClima_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Clima());
        }
    }

}
