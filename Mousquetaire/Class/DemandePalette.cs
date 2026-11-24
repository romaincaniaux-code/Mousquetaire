using System;
using System.Collections.Generic;

namespace Mousquetaire.Models;

public partial class DemandePalette
{
    public int IdDemande { get; set; }

    public string Statut { get; set; } = null!;

    public DateTime DateDemande { get; set; }

    public DateTime? DateConfirmation { get; set; }

    public int IdProduit { get; set; }

    public int IdPreparateur { get; set; }

    public int? IdCariste { get; set; }

    public int? IdPalette { get; set; }

    public int IdEmplacement { get; set; }

    public virtual Utilisateur? IdCaristeNavigation { get; set; }

    public virtual Emplacement IdEmplacementNavigation { get; set; } = null!;

    public virtual Palette? IdPaletteNavigation { get; set; }

    public virtual Utilisateur IdPreparateurNavigation { get; set; } = null!;

    public virtual Produit IdProduitNavigation { get; set; } = null!;
}
