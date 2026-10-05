using CalculateurAge.Views;

namespace CalculateurAge;

public partial class MainPage : ContentPage
{
    public MainPage() => InitializeComponent();

    private async void OnCalculerClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(entryNom.Text))
        {
            await DisplayAlert("Erreur", "Entrez un nom", "OK");
            return;
        }

        DateTime d = pickerDate.Date;
        int age = DateTime.Today.Year - d.Year;
        if (d.Date > DateTime.Today.AddYears(-age)) age--; // anniversaire pas encore passé

        await Shell.Current.GoToAsync(
            $"{nameof(ResultatPage)}?nom={Uri.EscapeDataString(entryNom.Text)}&age={age}");
    }
}
