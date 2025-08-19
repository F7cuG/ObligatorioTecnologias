using ObligatorioTecnologias.Models;
using ObligatorioTecnologias.Services;

namespace ObligatorioTecnologias.Ventanas;

public partial class Usuarios : ContentPage
{
    private readonly DatabaseService _db;

    // Constructor recibe DatabaseService
    public Usuarios(DatabaseService db)
    {
        InitializeComponent();
        _db = db;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CargarUsuarios();
    }

    private async void btnGuardar_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(entryNombre.Text) && int.TryParse(entryEdad.Text, out int edad))
        {
            var nuevoUsuario = new Usuario
            {
                Nombre = entryNombre.Text,
                Edad = edad
            };

            await _db.SaveUsuarioAsync(nuevoUsuario);

            entryNombre.Text = string.Empty;
            entryEdad.Text = string.Empty;

            await CargarUsuarios();
        }
        else
        {
            await DisplayAlert("Error", "Ingrese un nombre y una edad válida.", "OK");
        }
    }

    private async Task CargarUsuarios()
    {
        var usuarios = await _db.GetUsuariosAsync();
        usuariosCollectionView.ItemsSource = usuarios;
    }
}
