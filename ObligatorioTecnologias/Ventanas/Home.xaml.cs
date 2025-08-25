using Microsoft.Maui.Controls;

namespace ObligatorioTecnologias.Ventanas;

public partial class Home : ContentPage
{
    private const string RadioUrl = "https://cast1.asurahosting.com/proxy/btsasalv/stream"; // Concierto FM

    public Home()
    {
        InitializeComponent();
        CargarReproductor();
    }

    private void CargarReproductor()
    {
        string html = $@"
            <html>
                <body style='background:#1A237E;display:flex;justify-content:center;align-items:center;height:100%;margin:0;'>
                    <audio controls style='width:280px;'>
                        <source src='{RadioUrl}' type='audio/mpeg'>
                        Tu navegador no soporta audio HTML5.
                    </audio>
                </body>
            </html>";
        webRadio.Source = new HtmlWebViewSource { Html = html };
    }
}