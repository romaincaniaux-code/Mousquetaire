using System;
using System.Collections.Generic;

namespace Mousquetaire.Models.Database;

public partial class Commande
{
    public int IdCommande { get; set; }

    public DateTime DateImport { get; set; }

    public string Statut { get; set; } = null!;

    public int IdMagasin { get; set; }

    public virtual Magasin IdMagasinNavigation { get; set; } = null!;

    public virtual ICollection<LigneCommande> LigneCommandes { get; set; } = new List<LigneCommande>();

    public virtual ICollection<Preparation> Preparations { get; set; } = new List<Preparation>();
}
