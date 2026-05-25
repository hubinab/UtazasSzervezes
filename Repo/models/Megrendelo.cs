using System;
using System.Collections.Generic;

namespace Repo.models;

public partial class Megrendelo
{
    public int MegrendeloId { get; set; }

    public string Nev { get; set; } = null!;

    public string Telefon { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Utaza> Utazas { get; set; } = new List<Utaza>();
}
