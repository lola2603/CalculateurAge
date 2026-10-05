namespace CalculateurAge;

public partial class MainPage : ContentPage
{
    public MainPage() => InitializeComponent();

    private void OnCalculerClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(entryNom.Text))
        {
            DisplayAlert("Erreur", "Entrez un nom", "OK");
            return;
        }

        DateTime d = pickerDate.Date;
        int age = DateTime.Today.Year - d.Year;
        if (d.Date > DateTime.Today.AddYears(-age)) age--; // anniversaire pas encore passé

        lblResultat.Text = $"{entryNom.Text}, vous avez {age} ans";
        lblResultat.IsVisible = true;
    }
}
