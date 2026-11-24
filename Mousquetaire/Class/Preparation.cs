using System;
using System.Collections.Generic;

namespace Mousquetaire.Models;

public partial class Preparation
{
    public int IdPreparation { get; set; }

    public string Statut { get; set; } = null!;

    public DateTime? DateDebut { get; set; }

    public DateTime? DateFin { get; set; }

    public int IdCommande { get; set; }

    public int IdZone { get; set; }

    public int? IdUtilisateur { get; set; }

    public virtual Commande IdCommandeNavigation { get; set; } = null!;

    public virtual Utilisateur? IdUtilisateurNavigation { get; set; }

    public virtual Zone IdZoneNavigation { get; set; } = null!;
}
