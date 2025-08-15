using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace ObligatorioTecnologias.Ventanas
{
    public partial class Noticias : ContentPage
    {
        private const string ApiKey = "pub_938aa4610d214af4a29ee862345b5376";
        private ObservableCollection<Noticia> listaNoticias = new ObservableCollection<Noticia>();

        public Noticias()
        {
            InitializeComponent();
            NoticiasCollection.ItemsSource = listaNoticias;
            CargarNoticias(); 
        }

        private async void CargarNoticias(string filtro = "")
        {
            try
            {
                // Construir URL
                string url = $"https://newsdata.io/api/1/news?apikey={ApiKey}&country=uy&language=es";

                if (!string.IsNullOrWhiteSpace(filtro))
                    url += $"&q={Uri.EscapeDataString(filtro)}";

                using var httpClient = new HttpClient();
                var respuesta = await httpClient.GetStringAsync(url);

                var data = JsonConvert.DeserializeObject<NewsApiResponse>(respuesta);

                listaNoticias.Clear();

                if (data != null && data.results != null)
                {
                    foreach (var item in data.results)
                    {
                        listaNoticias.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudieron cargar las noticias: {ex.Message}", "OK");
            }
        }

        private void OnBuscarClicked(object sender, EventArgs e)
        {
            var filtro = BusquedaEntry.Text?.Trim() ?? "";
            CargarNoticias(filtro);
        }

        private async void OnAbrirEnlaceClicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.CommandParameter is string enlace)
            {
                await Launcher.Default.OpenAsync(enlace);
            }
        }
    }

    public class NewsApiResponse
    {
        public List<Noticia> results { get; set; }
    }

    public class Noticia
    {
        public string title { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public string image_url { get; set; }
    }
}
