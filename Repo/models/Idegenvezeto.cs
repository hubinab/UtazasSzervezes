using System;
using System.Collections.Generic;

namespace Repo.models;

public partial class Idegenvezeto
{
    public int IdegenvezetoId { get; set; }

    public string Nev { get; set; } = null!;

    public int Napidij { get; set; }

    public string Telefon { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Uticel> Uticels { get; set; } = new List<Uticel>();

    public int Bevetel => Uticels.Sum(x => x.Idotartam) * Napidij;
}
