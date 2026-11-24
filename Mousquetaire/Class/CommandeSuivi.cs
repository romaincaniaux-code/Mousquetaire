namespace Mousquetaire.Models;

public sealed class CommandeSuivi
{
    public int IdCommande { get; init; }

    public string NumeroCommande => $"CMD-{IdCommande:D4}";

    public string Zone { get; init; } = string.Empty;

    public string Preparateur { get; init; } = string.Empty;

    public string Avancement { get; init; } = string.Empty;

    public string Statut { get; init; } = string.Empty;

    public string Magasin { get; init; } = string.Empty;
}
