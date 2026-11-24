using System;
using System.Collections.Generic;

namespace Mousquetaire.Models;

public partial class Palette
{
    public int IdPalette { get; set; }

    public int Quantite { get; set; }

    public int IdProduit { get; set; }

    public int IdEmplacement { get; set; }

    public virtual ICollection<DemandePalette> DemandePalettes { get; set; } = new List<DemandePalette>();

    public virtual Emplacement IdEmplacementNavigation { get; set; } = null!;

    public virtual Produit IdProduitNavigation { get; set; } = null!;
}
