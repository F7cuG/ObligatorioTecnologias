using ObligatorioTecnologias.Data;
using ObligatorioTecnologias.Modelos;

namespace ObligatorioTecnologias.Ventanas;

public partial class PatrocinadoresOffline : ContentPage
{
    private readonly PatrocinadorService _service;
    private string _logoPath = string.Empty;

    public PatrocinadoresOffline()
    {
        InitializeComponent();

        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "patrocinadores.db3");
        _service = new PatrocinadorService(dbPath);
    }

    private async void OnSubirLogoClicked(object sender, EventArgs e)
    {
        try
        {
            var file = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Seleccione un logo",
                FileTypes = FilePickerFileType.Images
            });

            if (file != null)
            {
                _logoPath = file.FullPath;
                LogoPreview.Source = ImageSource.FromFile(_logoPath);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo cargar la imagen: {ex.Message}", "OK");
        }
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        var patrocinador = new Patrocinador
        {
            Nombre = NombreEntry.Text ?? string.Empty,
            Direccion = DireccionEntry.Text ?? string.Empty,
            LogoPath = _logoPath
        };

        await _service.AddPatrocinadorAsync(patrocinador);
        await DisplayAlert("Éxito", "Patrocinador guardado en SQLite", "OK");

        NombreEntry.Text = string.Empty;
        DireccionEntry.Text = string.Empty;
        LogoPreview.Source = null;
        _logoPath = string.Empty;
    }

    private async void OnPointerElDoradoClicked(object sender, EventArgs e)
    {
        var patrocinadores = await _service.GetPatrocinadoresAsync();
        var elDorado = patrocinadores.FirstOrDefault(p => p.Nombre.Contains("Dorado", StringComparison.OrdinalIgnoreCase));

        if (elDorado != null)
            await DisplayAlert("Patrocinador", $"{elDorado.Nombre}\nDirección: {elDorado.Direccion}", "OK");
        else
            await DisplayAlert("Patrocinador", "Shopping El Dorado\nDirección: Av. Roosevelt, Punta del Este", "OK");
    }

    private async void OnPointerPepsiClicked(object sender, EventArgs e)
    {
        var patrocinadores = await _service.GetPatrocinadoresAsync();
        var pepsi = patrocinadores.FirstOrDefault(p => p.Nombre.Contains("Pepsi", StringComparison.OrdinalIgnoreCase));

        if (pepsi != null)
            await DisplayAlert("Patrocinador", $"{pepsi.Nombre}\nDirección: {pepsi.Direccion}", "OK");
        else
            await DisplayAlert("Patrocinador", "Pepsi Uruguay\nDirección: Montevideo", "OK");
    }

    private async void OnPointerBBVAClicked(object sender, EventArgs e)
    {
        var patrocinadores = await _service.GetPatrocinadoresAsync();
        var bbva = patrocinadores.FirstOrDefault(p => p.Nombre.Contains("BBVA", StringComparison.OrdinalIgnoreCase));

        if (bbva != null)
            await DisplayAlert("Patrocinador", $"{bbva.Nombre}\nDirección: {bbva.Direccion}", "OK");
        else
            await DisplayAlert("Patrocinador", "BBVA Uruguay\nDirección: 18 de Julio 1234, Montevideo", "OK");
    }
}
