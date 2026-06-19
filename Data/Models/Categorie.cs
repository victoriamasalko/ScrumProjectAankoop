using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Categorie
{
    public int CategorieId { get; set; }

    public string Naam { get; set; } = null!;

    public int? HoofdCategorieId { get; set; }

    public virtual Categorie? HoofdCategorie { get; set; }

    public virtual ICollection<Categorie> InverseHoofdCategorie { get; set; } = new List<Categorie>();

    public virtual ICollection<Artikel> Artikels { get; set; } = new List<Artikel>();
}
