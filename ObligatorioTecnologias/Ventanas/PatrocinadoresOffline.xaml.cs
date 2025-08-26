namespace ObligatorioTecnologias.Ventanas;

public partial class PatrocinadoresOffline : ContentPage
{
    public PatrocinadoresOffline()
    {
        InitializeComponent();
    }

    private async void OnPointerElDoradoClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Patrocinador", "Shopping El Dorado\nDirección: Av. Roosevelt, Punta del Este", "OK");
    }

    private async void OnPointerPepsiClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Patrocinador", "Pepsi Uruguay\nDirección: Montevideo", "OK");
    }

    private async void OnPointerBBVAClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Patrocinador", "BBVA Uruguay\nDirección: 18 de Julio 1234, Montevideo", "OK");
    }
}
