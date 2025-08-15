using System.Net.Http;
using System.Text.Json;

namespace ObligatorioTecnologias.Ventanas
{
    public partial class Cotizaciones : ContentPage
    {
        private const string ApiKey = "42052733edda395658ac8493f8dc7acb";

        public Cotizaciones()
        {
            InitializeComponent();
            CargarCotizaciones();
        }

        private async void CargarCotizaciones()
        {
            try
            {
                using var http = new HttpClient();
                string url = $"http://api.currencylayer.com/live?access_key={ApiKey}&currencies=USD,EUR,BRL,UYU&format=1";

                var response = await http.GetStringAsync(url);
                using var jsonDoc = JsonDocument.Parse(response);
                var root = jsonDoc.RootElement;  

                if (!root.GetProperty("success").GetBoolean())
                {
                    lblUSD.Text = "Error al obtener datos.";
                    return;
                }

                var quotes = root.GetProperty("quotes");

                double usd = quotes.GetProperty("USDUYU").GetDouble();
                double eur = quotes.GetProperty("USDUYU").GetDouble() / quotes.GetProperty("USDEUR").GetDouble();
                double brl = quotes.GetProperty("USDUYU").GetDouble() / quotes.GetProperty("USDBRL").GetDouble();

                lblUSD.Text = $"USD → UYU: {usd:N2}";
                lblEUR.Text = $"EUR → UYU: {eur:N2}";
                lblBRL.Text = $"BRL → UYU: {brl:N2}";
            }
            catch (Exception ex)
            {
                lblUSD.Text = $"Error: {ex.Message}";
            }
        }
    }
}
