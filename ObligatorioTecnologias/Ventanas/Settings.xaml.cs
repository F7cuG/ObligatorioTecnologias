using ObligatorioTecnologias.Models;
using ObligatorioTecnologias.Services;
using Microsoft.Maui.Storage;


namespace ObligatorioTecnologias.Ventanas;

public partial class Settings : ContentPage
{
	public Settings()
	{
		InitializeComponent();
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();

		// Recupera el nombre de usuario guardado en Preferences
		var nombreUsuario = Preferences.Get("UsuarioActual", string.Empty);
		if (string.IsNullOrEmpty(nombreUsuario))
			return;

		// Obtiene el usuario desde la base de datos o servicio
		var usuario = await UsuarioService.GetUsuarioByNombreAsync(nombreUsuario);
		if (usuario == null)
			return;

		// Asigna los valores a los controles
		lblNombreCompleto.Text = usuario.NombreCompleto;
		lblNombreUsuario.Text = "@" + usuario.NombreUsuario;
		imgFotoPerfil.Source = string.IsNullOrEmpty(usuario.FotoPerfil)
			? "usuario_defecto.png"
			: usuario.FotoPerfil;
	}
}