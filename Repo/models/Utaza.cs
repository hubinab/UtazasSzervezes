using System;
using System.Collections.Generic;

namespace Repo.models;

public partial class Utaza
{
    public int UtazasId { get; set; }

    public int UticelId { get; set; }

    public int MegrendeloId { get; set; }

    public DateOnly Datum { get; set; }

    public int Utasszam { get; set; }

    public virtual Megrendelo Megrendelo { get; set; } = null!;

    public virtual Uticel Uticel { get; set; } = null!;
}
