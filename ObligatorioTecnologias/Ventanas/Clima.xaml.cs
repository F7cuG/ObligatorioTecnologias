using System.Net.Http;
using System.Text.Json;
using System.Linq;
using System.Globalization;

namespace ObligatorioTecnologias.Ventanas
{
    public partial class Clima : ContentPage
    {

        static readonly HttpClient http = new HttpClient();
        private const string ApiKey = "4c0f8782f318be4ceb63a516ddc82aad";
        private const string Ciudad = "Punta del Este";

        public Clima()
        {
            InitializeComponent();
            CargarClima();
        }

        class DiaItem
        {
            public DateTime Fecha { get; set; }
            public double Min { get; set; }
            public double Max { get; set; }
            public string Descripcion { get; set; } = "";
            public string Icon { get; set; } = "01d";
            public string IconUrl => $"https://openweathermap.org/img/wn/{Icon}@2x.png";
        }

        static string ToTitle(string s)
        {
            var ti = CultureInfo.CurrentCulture.TextInfo;
            return ti.ToTitleCase(s);
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

        async Task CargarPronostico5DiasAsync()
        {
            var url = $"https://api.openweathermap.org/data/2.5/forecast?q={Uri.EscapeDataString(Ciudad)}&appid={ApiKey}&units=metric&lang=es";
            var json = await http.GetStringAsync(url);

            using var doc = JsonDocument.Parse(json);
            var arr = doc.RootElement.GetProperty("list"); 

            var porDia = new Dictionary<DateTime, List<JsonElement>>();
            foreach (var it in arr.EnumerateArray())
            {
                var dt = DateTimeOffset.FromUnixTimeSeconds(it.GetProperty("dt").GetInt64()).Date;
                if (!porDia.TryGetValue(dt, out var l)) { l = new List<JsonElement>(); porDia[dt] = l; }
                l.Add(it);
            }

            var items = porDia
                .OrderBy(k => k.Key)  
                .Take(5)               
                .Select(kvp =>
                {
                    var min = kvp.Value.Min(i => i.GetProperty("main").GetProperty("temp_min").GetDouble());
                    var max = kvp.Value.Max(i => i.GetProperty("main").GetProperty("temp_max").GetDouble());

                    var chosen = kvp.Value.FirstOrDefault(i =>
                    {
                        var hour = DateTimeOffset.FromUnixTimeSeconds(i.GetProperty("dt").GetInt64()).Hour;
                        return hour == 12;
                    });
                    if (chosen.ValueKind == JsonValueKind.Undefined) chosen = kvp.Value[0];

                    var wx = chosen.GetProperty("weather")[0];
                    var desc = wx.GetProperty("description").GetString() ?? "";
                    var icon = wx.GetProperty("icon").GetString() ?? "01d";

                    return new DiaItem
                    {
                        Fecha = kvp.Key,
                        Min = min,
                        Max = max,
                        Descripcion = ToTitle(desc),
                        Icon = icon
                    };
                })
                .ToList();

            collectionPronostico.ItemsSource = items;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                activity.IsVisible = activity.IsRunning = true;
                await CargarPronostico5DiasAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                activity.IsRunning = activity.IsVisible = false; 
            }
        }

    }
}
