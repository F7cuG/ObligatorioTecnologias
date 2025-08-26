using Microsoft.Maui.Controls;

namespace ObligatorioTecnologias.Ventanas;

public partial class PatrocinadoresOffline : ContentPage
{
    public PatrocinadoresOffline()
    {
        InitializeComponent();
    }

    private async void OnPointerElDoradoClicked(object sender, EventArgs e)
    {
        await DisplayAlert("El Dorado",
            "DIRECCION\n\nLavalleja casi Joaquín de Viana\n", "Cerrar");
        
    }

    private async void OnPointerPepsiClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Pepsi",
            "DIRECCION\n\nSerguini Mosquera 2442\n", "Cerrar");
    }

    private async void OnPointerBBVAClicked(object sender, EventArgs e)
    {
        await DisplayAlert("BBVA",
            "DIRECCION\n\n6 de Julio casi Roosevelt\n", "Cerrar");
    }
}