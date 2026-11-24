using System;
using System.Collections.Generic;

namespace Mousquetaire.Models;

public partial class Produit
{
    public int IdProduit { get; set; }

    public string Libelle { get; set; } = null!;

    public int IdZone { get; set; }

    public virtual ICollection<DemandePalette> DemandePalettes { get; set; } = new List<DemandePalette>();

    public virtual Zone IdZoneNavigation { get; set; } = null!;

    public virtual ICollection<LigneCommande> LigneCommandes { get; set; } = new List<LigneCommande>();

    public virtual ICollection<Palette> Palettes { get; set; } = new List<Palette>();
}
