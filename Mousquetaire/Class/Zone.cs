using System;
using System.Collections.Generic;

namespace Mousquetaire.Models;

public partial class Zone
{
    public int IdZone { get; set; }

    public string Libelle { get; set; } = null!;

    public virtual ICollection<Emplacement> Emplacements { get; set; } = new List<Emplacement>();

    public virtual ICollection<Preparation> Preparations { get; set; } = new List<Preparation>();

    public virtual ICollection<Produit> Produits { get; set; } = new List<Produit>();

    public virtual ICollection<Utilisateur> Utilisateurs { get; set; } = new List<Utilisateur>();
}
