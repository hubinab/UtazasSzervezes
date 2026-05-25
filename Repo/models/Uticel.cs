using System;
using System.Collections.Generic;

namespace Repo.models;

public partial class Uticel
{
    public int UticelId { get; set; }

    public int IdegenvezetoId { get; set; }

    public string Megnevezes { get; set; } = null!;

    public int Idotartam { get; set; }

    public virtual Idegenvezeto Idegenvezeto { get; set; } = null!;

    public virtual ICollection<Utaza> Utazas { get; set; } = new List<Utaza>();
}
