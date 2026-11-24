using System;
using System.Collections.Generic;

namespace Mousquetaire.Models;

public partial class Utilisateur
{
    public int IdUtilisateur { get; set; }

    public string Login { get; set; } = null!;

    public byte[] MotDePasseHash { get; set; } = null!;

    public Guid Sel { get; set; }

    public string Nom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public bool Actif { get; set; }

    public DateTime DateCreation { get; set; }

    public int IdRole { get; set; }

    public int? IdZone { get; set; }

    public virtual ICollection<DemandePalette> DemandePaletteIdCaristeNavigations { get; set; } = new List<DemandePalette>();

    public virtual ICollection<DemandePalette> DemandePaletteIdPreparateurNavigations { get; set; } = new List<DemandePalette>();

    public virtual Role IdRoleNavigation { get; set; } = null!;

    public virtual Zone? IdZoneNavigation { get; set; }

    public virtual ICollection<Preparation> Preparations { get; set; } = new List<Preparation>();
}
