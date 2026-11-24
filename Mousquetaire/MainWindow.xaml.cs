using System.Windows;
using Microsoft.Data.SqlClient;
using Mousquetaire.Data;
using Mousquetaire.Models;

namespace Mousquetaire
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTextBox.Text) ||
                string.IsNullOrWhiteSpace(PasswordInput.Password))
            {
                AfficherStatut("Veuillez renseigner votre identifiant et votre mot de passe.");
                return;
            }

            try
            {
                var utilisateur = await StoredProcedureExecutor.QuerySingleAsync(
                    "dbo.AuthentifierUtilisateur",
                    reader => new Utilisateur
                    {
                        IdUtilisateur = reader.GetInt32(reader.GetOrdinal("idUtilisateur")),
                        Nom = reader.GetString(reader.GetOrdinal("nom")),
                        Prenom = reader.GetString(reader.GetOrdinal("prenom")),
                        Actif = true,
                        IdRole = reader.GetInt32(reader.GetOrdinal("idRole")),
                        IdZone = reader.IsDBNull(reader.GetOrdinal("idZone"))
                            ? null
                            : reader.GetInt32(reader.GetOrdinal("idZone"))
                    },
                    new SqlParameter("@login", System.Data.SqlDbType.NVarChar, 50)
                    {
                        Value = LoginTextBox.Text.Trim()
                    },
                    new SqlParameter("@motDePasse", System.Data.SqlDbType.NVarChar, 100)
                    {
                        Value = PasswordInput.Password
                    });

                if (utilisateur is null)
                {
                    AfficherStatut("Identifiant ou mot de passe incorrect.");
                    return;
                }

                var fenetrePrincipale = new FenetrePrincipale(utilisateur);
                fenetrePrincipale.Show();
                Close();
            }
            catch (SqlException ex)
            {
                AfficherStatut($"Erreur SQL {ex.Number} : {ex.Message}");
            }
        }

        private void AfficherStatut(string message)
        {
            StatusText.Text = message;
            StatusText.Visibility = Visibility.Visible;
        }

    }
}