using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Microsoft.Maui.ApplicationModel;


namespace ObligatorioTecnologias.Ventanas;

public partial class Cine : ContentPage
{
    private const string ApiKey = "99bbb1f2b1b88e748de40a58cf89b444";
    private const string ImagenBaseUrl = "https://image.tmdb.org/t/p/w500";
    private ObservableCollection<Pelicula> listaPeliculas = new ObservableCollection<Pelicula>();

    public Cine()
    {
        InitializeComponent();
        PeliculasCollection.ItemsSource = listaPeliculas;
        CargarProximosEstrenos();
    }

    private async void CargarProximosEstrenos(string filtro = "")
    {
        try
        {
            string url;

            if (string.IsNullOrWhiteSpace(filtro))
            {
                // Próximos estrenos
                url = $"https://api.themoviedb.org/3/movie/upcoming?api_key={ApiKey}&language=es-ES&page=1&region=UY";
            }
            else
            {
                // Búsqueda por palabra clave o género
                url = $"https://api.themoviedb.org/3/search/movie?api_key={ApiKey}&language=es-ES&query={Uri.EscapeDataString(filtro)}&page=1&region=UY";
            }

            using var httpClient = new HttpClient();
            var respuesta = await httpClient.GetStringAsync(url);

            var data = JsonConvert.DeserializeObject<TMDBResponse>(respuesta);

            listaPeliculas.Clear();

            if (data != null && data.results != null)
            {
                foreach (var p in data.results)
                {
                    // Agregar URL completa de poster
                    p.poster_full_url = string.IsNullOrEmpty(p.poster_path) ? null : ImagenBaseUrl + p.poster_path;
                    listaPeliculas.Add(p);
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudieron cargar las películas: {ex.Message}", "OK");
        }
    }

    private void OnBuscarClicked(object sender, EventArgs e)
    {
        // Buscamos el Entry por nombre dentro del árbol visual
        var entry = this.FindByName<Entry>("BusquedaEntry");
        var filtro = entry?.Text?.Trim() ?? "";
        CargarProximosEstrenos(filtro);
    }

    public class TMDBResponse
    {
        public List<Pelicula> results { get; set; }
    }

    public class Pelicula
    {
        public string title { get; set; }
        public string overview { get; set; }
        public string release_date { get; set; }
        public string poster_path { get; set; }

        [JsonIgnore]
        public string poster_full_url { get; set; }
    }
    private async void OnPeliculaSeleccionada(object sender, SelectionChangedEventArgs e)
    {
        // Permite toques sucesivos en el mismo item
        var seleccion = PeliculasCollection.SelectedItem as Pelicula;
        PeliculasCollection.SelectedItem = null;

        if (seleccion == null)
            return;

        // Acción simple: abrir la ficha en TMDB
        // ID no está en tu modelo actual; abrimos la búsqueda por título como fallback.
        // Si luego agregás el Id de TMDB, cambiamos a la URL directa /movie/{id}.
        var query = Uri.EscapeDataString(seleccion.title ?? string.Empty);
        var url = $"https://www.themoviedb.org/search/movie?query={query}";

        var opcion = await DisplayActionSheet(seleccion.title, "Cancelar", null, "Abrir en TMDB");
        if (opcion == "Abrir en TMDB")
            await Launcher.OpenAsync(url);
    }
}
