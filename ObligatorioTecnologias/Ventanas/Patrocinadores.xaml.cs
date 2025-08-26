using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices.Sensors; // Necesario para Geolocation

namespace ObligatorioTecnologias.Ventanas;

public partial class Patrocinadores : ContentPage
{
    public Patrocinadores()
    {
        InitializeComponent();
        MostrarMapa();
    }

    private async void MostrarMapa()
    {
        var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
        if (status == PermissionStatus.Granted)
        {
            MyMap.IsShowingUser = false;

            var ubicacion = await Geolocation.Default.GetLocationAsync();
            if (ubicacion != null)
            {
                var posicion = new Location(40.7128, -74.0060); // Nueva York
                var region = MapSpan.FromCenterAndRadius(posicion, Distance.FromKilometers(3));
                MyMap.MoveToRegion(region);
            }
            else
            {
                await DisplayAlert("Ubicación", "No se pudo obtener la ubicación actual.", "OK");
            }
        }
        else
        {
            await DisplayAlert("Permiso requerido", "Se necesita permiso de ubicación para mostrar tu posición en el mapa.", "OK");
        }
    }
}