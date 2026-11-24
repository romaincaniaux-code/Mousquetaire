using System.Windows;
using Mousquetaire.Models;

namespace Mousquetaire;

public partial class FenetrePrincipale : Window
{
    private const int ResponsableRoleId = 1;
    private const int CaristeRoleId = 2;
    private const int PreparateurRoleId = 3;

    private readonly Utilisateur utilisateur;

    public FenetrePrincipale(Utilisateur utilisateur)
    {
        InitializeComponent();

        this.utilisateur = utilisateur;
        UserStatusText.Text = $"Connecté : {utilisateur.Prenom} {utilisateur.Nom}";
        ConfigurerMenu();
    }

    private void ConfigurerMenu()
    {
        ResponsableCommandesMenu.Visibility = utilisateur.IdRole == ResponsableRoleId
            ? Visibility.Visible
            : Visibility.Collapsed;
        ResponsableUtilisateursMenu.Visibility = utilisateur.IdRole == ResponsableRoleId
            ? Visibility.Visible
            : Visibility.Collapsed;
        PreparateurCommandesMenu.Visibility = utilisateur.IdRole == PreparateurRoleId
            ? Visibility.Visible
            : Visibility.Collapsed;
        PreparateurReapproMenu.Visibility = utilisateur.IdRole == PreparateurRoleId
            ? Visibility.Visible
            : Visibility.Collapsed;
        CaristeDemandesMenu.Visibility = utilisateur.IdRole == CaristeRoleId
            ? Visibility.Visible
            : Visibility.Collapsed;
        CaristePickingMenu.Visibility = utilisateur.IdRole == CaristeRoleId
            ? Visibility.Visible
            : Visibility.Collapsed;
        CaristeStockageMenu.Visibility = utilisateur.IdRole == CaristeRoleId
            ? Visibility.Visible
            : Visibility.Collapsed;
    }

    private void SuiviCommandes_Click(object sender, RoutedEventArgs e)
    {
        ContentFrame.Navigate(new CommandesPage());
    }

    private void MesCommandes_Click(object sender, RoutedEventArgs e)
    {
        ContentFrame.Navigate(new CommandesPage(utilisateur.IdZone, utilisateur.IdUtilisateur));
    }

    private void AccueilMenu_Click(object sender, RoutedEventArgs e)
    {
        ContentFrame.Content = null;
    }

    private void ImporterCommandes_Click(object sender, RoutedEventArgs e) => ContentFrame.Content = null;
    private void CommandesImportees_Click(object sender, RoutedEventArgs e) => ContentFrame.Content = null;
    private void RepartitionCommandes_Click(object sender, RoutedEventArgs e) => ContentFrame.Content = null;
    private void AssignerPreparateurs_Click(object sender, RoutedEventArgs e) => ContentFrame.Content = null;
    private void ComptesUtilisateurs_Click(object sender, RoutedEventArgs e) => ContentFrame.Content = null;
    private void RolesEtZones_Click(object sender, RoutedEventArgs e) => ContentFrame.Content = null;
    private void DetailsCommandes_Click(object sender, RoutedEventArgs e) => ContentFrame.Content = null;
    private void StatutPreparation_Click(object sender, RoutedEventArgs e) => ContentFrame.Content = null;
    private void DemanderPalette_Click(object sender, RoutedEventArgs e) => ContentFrame.Content = null;
    private void PalettesDisponibles_Click(object sender, RoutedEventArgs e) => ContentFrame.Content = null;
    private void ListeDemandes_Click(object sender, RoutedEventArgs e) => ContentFrame.Content = null;
    private void DetailsDemande_Click(object sender, RoutedEventArgs e) => ContentFrame.Content = null;
    private void ConfirmerDeplacement_Click(object sender, RoutedEventArgs e) => ContentFrame.Content = null;
    private void EtatRacks_Click(object sender, RoutedEventArgs e) => ContentFrame.Content = null;

    private void LogoutMenu_Click(object sender, RoutedEventArgs e)
    {
        var loginWindow = new MainWindow();
        loginWindow.Show();
        Close();
    }
}
