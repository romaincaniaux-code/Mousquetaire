using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;
using Mousquetaire.Data;
using Mousquetaire.Models;

namespace Mousquetaire;

public partial class CommandesPage : Page
{
    private readonly int? idZone;
    private readonly int? idPreparateur;

    public CommandesPage(int? idZone = null, int? idPreparateur = null)
    {
        InitializeComponent();
        this.idZone = idZone;
        this.idPreparateur = idPreparateur;
        Loaded += CommandesPage_Loaded;
    }

    private async void CommandesPage_Loaded(object sender, RoutedEventArgs e)
    {
        StatusText.Text = "Chargement des commandes...";
        StatusText.Visibility = Visibility.Visible;

        try
        {
            var commandes = await StoredProcedureExecutor.QueryListAsync(
                "dbo.ListerSuiviCommandes",
                reader =>
                {
                    var quantite = reader.GetInt32(reader.GetOrdinal("quantite"));
                    var quantitePreparee = reader.GetInt32(reader.GetOrdinal("quantitePreparee"));
                    var statutCommande = reader.GetString(reader.GetOrdinal("statutCommande")) switch
                    {
                        "EnCours" => "En cours",
                        "Importee" => "Importée",
                        "Terminee" => "Terminée",
                        var statut => statut
                    };
                    var avancement = quantite == 0
                        ? 0
                        : Math.Round((double)quantitePreparee / quantite * 100);

                    return new CommandeSuivi
                    {
                        IdCommande = reader.GetInt32(reader.GetOrdinal("idCommande")),
                        Zone = reader.GetString(reader.GetOrdinal("zone")),
                        Preparateur = reader.GetString(reader.GetOrdinal("preparateur")),
                        Avancement = string.Create(
                            CultureInfo.CurrentCulture,
                            $"{avancement:0}% ({quantitePreparee}/{quantite})"),
                        Statut = statutCommande,
                        Magasin = reader.GetString(reader.GetOrdinal("magasin"))
                    };
                },
                new SqlParameter("@statut", System.Data.SqlDbType.NVarChar, 20)
                {
                    Value = DBNull.Value
                },
                new SqlParameter("@idMagasin", System.Data.SqlDbType.Int)
                {
                    Value = DBNull.Value
                },
                new SqlParameter("@idZone", System.Data.SqlDbType.Int)
                {
                    Value = idZone.HasValue ? idZone.Value : DBNull.Value
                },
                new SqlParameter("@idPreparateur", System.Data.SqlDbType.Int)
                {
                    Value = idPreparateur.HasValue ? idPreparateur.Value : DBNull.Value
                });

            CommandesListView.ItemsSource = commandes;
            StatusText.Text = commandes.Count == 0
                ? "Aucune commande à afficher."
                : $"{commandes.Count} commande(s) affichée(s).";
        }
        catch (SqlException ex)
        {
            StatusText.Text = $"Erreur SQL {ex.Number} : {ex.Message}";
        }
    }
}
