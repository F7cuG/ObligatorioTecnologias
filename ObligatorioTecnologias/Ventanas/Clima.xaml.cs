using System.Net.Http;
using System.Text.Json;

namespace ObligatorioTecnologias.Ventanas
{
    public partial class Clima : ContentPage
    {
        private const string ApiKey = "4c0f8782f318be4ceb63a516ddc82aad";
        private const string Ciudad = "Montevideo";

        public Clima()
        {
            InitializeComponent();
            CargarClima();
        }

        private async void CargarClima()
        {
            try
            {
                using var http = new HttpClient();
                string url = $"https://api.openweathermap.org/data/2.5/weather?q={Ciudad}&appid={ApiKey}&units=metric&lang=es";

                var response = await http.GetStringAsync(url);
                using var jsonDoc = JsonDocument.Parse(response);
                var root = jsonDoc.RootElement;

                string descripcion = root.GetProperty("weather")[0].GetProperty("description").GetString();
                double temp = root.GetProperty("main").GetProperty("temp").GetDouble();
                string icono = root.GetProperty("weather")[0].GetProperty("icon").GetString();

                string iconUrl = $"https://openweathermap.org/img/wn/{icono}@2x.png";

                lblClima.Text = $"{descripcion.ToUpper()} - {temp}°C";

                imgIcono.Source = ImageSource.FromUri(new Uri(iconUrl));
                await imgIcono.FadeTo(1, 1000, Easing.CubicInOut);
            }
            catch (Exception ex)
            {
                lblClima.Text = $"Error: {ex.Message}";
            }
        }
    }
}
