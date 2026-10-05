using System.Collections.ObjectModel;

namespace CalculateurAge.ViewModels;

public class CalculateurViewModel : BaseViewModel
{
    private string _nom = "";
    private DateTime _dateNaissance = DateTime.Today.AddYears(-20);
    private string _resultat = "";
    private bool _resultatVisible;
    private string _message = "";
    private string _joursAnniversaire = "";
    private string _erreur = "";

    public string Nom
    {
        get => _nom;
        set { if (SetField(ref _nom, value)) CalculerCommand.Rafraichir(); }
    }
    public DateTime DateNaissance
    {
        get => _dateNaissance;
        set
        {
            if (!SetField(ref _dateNaissance, value)) return;
            Erreur = value.Date > DateTime.Today
                ? "La date de naissance ne peut pas être dans le futur." : "";
            CalculerCommand.Rafraichir();
        }
    }
    public string Resultat { get => _resultat; set => SetField(ref _resultat, value); }
    public bool ResultatVisible { get => _resultatVisible; set => SetField(ref _resultatVisible, value); }
    public string Message { get => _message; set => SetField(ref _message, value); }
    public string JoursAnniversaire { get => _joursAnniversaire; set => SetField(ref _joursAnniversaire, value); }
    public string Erreur { get => _erreur; set => SetField(ref _erreur, value); }

    public ObservableCollection<string> Historique { get; } = new();

    public RelayCommand CalculerCommand { get; }
    public RelayCommand EffacerCommand { get; }

    public CalculateurViewModel()
    {
        CalculerCommand = new RelayCommand(Calculer,
            () => !string.IsNullOrWhiteSpace(Nom) && DateNaissance.Date <= DateTime.Today);
        EffacerCommand = new RelayCommand(Effacer);
    }

    private static DateTime Anniversaire(DateTime naissance, int annee)
    {
        int jour = naissance.Day;
        if (naissance.Month == 2 && jour == 29 && !DateTime.IsLeapYear(annee)) jour = 28;
        return new DateTime(annee, naissance.Month, jour);
    }

    private void Calculer()
    {
        var today = DateTime.Today;
        int age = today.Year - DateNaissance.Year;
        if (DateNaissance.Date > today.AddYears(-age)) age--;

        Resultat = $"{Nom}, vous avez {age} ans";
        Message = age >= 18 ? "Majeur" : "Mineur";

        var prochain = Anniversaire(DateNaissance, today.Year);
        if (prochain < today) prochain = Anniversaire(DateNaissance, today.Year + 1);
        int jours = (prochain - today).Days;
        JoursAnniversaire = jours == 0
            ? "C'est votre anniversaire aujourd'hui !"
            : $"Prochain anniversaire dans {jours} jour(s)";

        Historique.Insert(0, $"{DateTime.Now:HH:mm:ss} - {Nom} - {age} ans - {Message}");
        ResultatVisible = true;
    }

    private void Effacer()
    {
        Nom = "";
        DateNaissance = DateTime.Today.AddYears(-20);
        Resultat = Message = JoursAnniversaire = Erreur = "";
        ResultatVisible = false;
        Historique.Clear();
    }
}
