using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices.Sensors;
using ObligatorioTecnologias.Models;
using ObligatorioTecnologias.Services;

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
        if (status != PermissionStatus.Granted)
        {
            await DisplayAlert("Permiso requerido", "Se necesita permiso de ubicación para mostrar el mapa.", "OK");
            return;
        }

        MyMap.IsShowingUser = true;

        var ubicacion = await Geolocation.Default.GetLocationAsync();
        Location posicion;

        if (ubicacion != null)
        {
            posicion = new Location(ubicacion.Latitude, ubicacion.Longitude);
        }
        else
        {
            posicion = new Location(-34.9011, -56.1645); // Montevideo por defecto
        }

        var region = MapSpan.FromCenterAndRadius(posicion, Distance.FromKilometers(5));
        MyMap.MoveToRegion(region);

        await CargarPatrocinadoresEnMapa();
    }

    private async Task CargarPatrocinadoresEnMapa()
    {
        var patrocinadores = await PatrocinadorService.GetPatrocinadoresAsync();

        foreach (var p in patrocinadores)
        {
            if (p.Latitud == 0 && p.Longitud == 0)
                continue;

            var pin = new Pin
            {
                Label = p.Nombre,
                Address = p.Direccion,
                Location = new Location(p.Latitud, p.Longitud),
                Type = PinType.Place
            };

            pin.MarkerClicked += async (s, args) =>
            {
                args.HideInfoWindow = true;
                await DisplayAlert("Patrocinador", $"{p.Nombre}\n{p.Direccion}", "OK");
            };

            MyMap.Pins.Add(pin);
        }
    }
}
