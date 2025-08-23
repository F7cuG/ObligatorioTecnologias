using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System.Linq;

namespace ObligatorioTecnologias
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        public void AplicarPreferencias()
        {
            foreach (var item in Items)
            {
                switch (item.Route)
                {
                    case "clima":
                        item.IsVisible = Preferences.Get("Pref_Clima", true);
                        break;
                    case "cotizaciones":
                        item.IsVisible = Preferences.Get("Pref_Cotizaciones", true);
                        break;
                    case "noticias":
                        item.IsVisible = Preferences.Get("Pref_Noticias", true);
                        break;
                    case "cine":
                        item.IsVisible = Preferences.Get("Pref_Cine", true);
                        break;
                    case "patrocinadores":
                        item.IsVisible = Preferences.Get("Pref_Patrocinadores", true);
                        break;
                    // Home y Settings siempre visibles
                    case "home":
                    case "settings":
                        item.IsVisible = true;
                        break;
                }
            }
        }
    }
}
