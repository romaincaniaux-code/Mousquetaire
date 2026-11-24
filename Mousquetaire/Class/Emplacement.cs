using System;
using System.Collections.Generic;

namespace Mousquetaire.Models;

public partial class Emplacement
{
    public int IdEmplacement { get; set; }

    public string Code { get; set; } = null!;

    public int Allee { get; set; }

    public int Etage { get; set; }

    public int Numero { get; set; }

    public int IdZone { get; set; }

    public virtual ICollection<DemandePalette> DemandePalettes { get; set; } = new List<DemandePalette>();

    public virtual Zone IdZoneNavigation { get; set; } = null!;

    public virtual Palette? Palette { get; set; }
}
