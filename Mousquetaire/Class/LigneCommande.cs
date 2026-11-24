using System;
using System.Collections.Generic;

namespace Mousquetaire.Models;

public partial class LigneCommande
{
    public int IdCommande { get; set; }

    public int IdProduit { get; set; }

    public int Quantite { get; set; }

    public int QuantitePreparee { get; set; }

    public string Statut { get; set; } = null!;

    public virtual Commande IdCommandeNavigation { get; set; } = null!;

    public virtual Produit IdProduitNavigation { get; set; } = null!;
}
