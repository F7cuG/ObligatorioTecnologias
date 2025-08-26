using ObligatorioTecnologias.Services;
using Microsoft.Maui.Controls;
using ObligatorioTecnologias.Ventanas;
using Microsoft.Maui.Storage;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;

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

    private async void OnHuellaClicked(object sender, EventArgs e)
    {
        var result = await CrossFingerprint.Current.AuthenticateAsync(
            new AuthenticationRequestConfiguration("Autenticación requerida", "Usa tu huella para ingresar"));

        if (result.Authenticated)
        {
            var user = await UsuarioService.GetUsuarioByNombreAsync("AgustinEtchepare");
            if (user != null)
            {
                Preferences.Set("UsuarioActual", user.NombreUsuario);
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await DisplayAlert("Error", "No existe el usuario demo.", "OK");
            }
        }
        else
        {
            await DisplayAlert("Error", "No se pudo autenticar con huella.", "OK");
        }
    }
}

