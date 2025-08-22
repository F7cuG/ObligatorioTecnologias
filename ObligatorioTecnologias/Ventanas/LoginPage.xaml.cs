using ObligatorioTecnologias.Services;
using Microsoft.Maui.Controls;
using ObligatorioTecnologias.Ventanas;
using Microsoft.Maui.Storage;

namespace ObligatorioTecnologias.Ventanas;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var usuario = entryUsuario.Text?.Trim() ?? string.Empty;
        var contrasena = entryContrasena.Text ?? string.Empty;

        var user = await UsuarioService.GetUsuarioByNombreAsync(usuario);
        if (user != null && user.Contraseña == contrasena)
        {
            //navega a la main page
            Preferences.Set("UsuarioActual", usuario);
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            await DisplayAlert("Error", "Usuario o contraseña incorrectos.", "OK");
        }
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new Usuarios());
    }

    public async void UsuarioRegistradoCorrectamente()
    {
        await DisplayAlert("Éxito", "Usuario registrado correctamente.", "OK");
        await Navigation.PopModalAsync();
    }
}

