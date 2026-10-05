using CalculateurAge.Views;

namespace CalculateurAge;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        // route à déclarer sinon GoToAsync plante
        Routing.RegisterRoute(nameof(ResultatPage), typeof(ResultatPage));
    }
}
