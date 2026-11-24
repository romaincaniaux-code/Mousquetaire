using System;
using System.Collections.Generic;

namespace Mousquetaire.Models;

public partial class Magasin
{
    public int IdMagasin { get; set; }

    public string Libelle { get; set; } = null!;

    public string? Adresse { get; set; }

    public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();
}
