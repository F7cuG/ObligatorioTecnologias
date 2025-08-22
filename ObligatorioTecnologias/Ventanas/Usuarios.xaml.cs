using ObligatorioTecnologias.Models;
using ObligatorioTecnologias.Services;
using Microsoft.Maui.Controls;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Storage;

namespace ObligatorioTecnologias.Ventanas;

public partial class Usuarios : ContentPage
{
    private string _fotoPerfilPath = string.Empty;

    public Usuarios()
    {
        InitializeComponent();
    }

    private async void OnTomarSelfieClicked(object sender, EventArgs e)
    {
        try
        {
            var photo = await MediaPicker.CapturePhotoAsync();
            if (photo != null)
            {
                var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                    await stream.CopyToAsync(newStream);
                _fotoPerfilPath = newFile;
                imgFotoPerfil.Source = ImageSource.FromFile(newFile);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "No se pudo tomar la foto: " + ex.Message, "OK");
        }
    }

    private async void OnSeleccionarFotoClicked(object sender, EventArgs e)
    {
        try
        {
            var photo = await MediaPicker.PickPhotoAsync();
            if (photo != null)
            {
                var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                    await stream.CopyToAsync(newStream);
                _fotoPerfilPath = newFile;
                imgFotoPerfil.Source = ImageSource.FromFile(newFile);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "No se pudo seleccionar la foto: " + ex.Message, "OK");
        }
    }

    private async void OnRegistrarClicked(object sender, EventArgs e)
    {
        var usuario = new Usuario
        {
            NombreUsuario = entryNombreUsuario.Text?.Trim() ?? string.Empty,
            Contraseña = entryContrasena.Text ?? string.Empty,
            NombreCompleto = entryNombreCompleto.Text ?? string.Empty,
            Direccion = entryDireccion.Text ?? string.Empty,
            Telefono = entryTelefono.Text ?? string.Empty,
            Email = entryEmail.Text ?? string.Empty,
            FotoPerfil = _fotoPerfilPath
        };

        if (string.IsNullOrWhiteSpace(usuario.NombreUsuario) || string.IsNullOrWhiteSpace(usuario.Contraseña))
        {
            await DisplayAlert("Error", "El nombre de usuario y la contraseña son obligatorios.", "OK");
            return;
        }

        var existente = await UsuarioService.GetUsuarioByNombreAsync(usuario.NombreUsuario);
        if (existente != null)
        {
            await DisplayAlert("Error", "El nombre de usuario ya existe.", "OK");
            return;
        }

        await UsuarioService.SaveUsuarioAsync(usuario);
        await DisplayAlert("Éxito", "Usuario registrado correctamente.", "OK");
        // Limpiar formulario
        entryNombreUsuario.Text = entryContrasena.Text = entryNombreCompleto.Text = entryDireccion.Text = entryTelefono.Text = entryEmail.Text = string.Empty;
        imgFotoPerfil.Source = null;
        _fotoPerfilPath = string.Empty;
        Preferences.Set("UsuarioActual", usuario.NombreUsuario);
    }

    private async void OnVolverClicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new LoginPage());
    }
}