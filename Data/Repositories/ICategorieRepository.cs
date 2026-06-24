using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories;

public interface ICategorieRepository
{
    Task<IEnumerable<Categorie>> GetCategorieenAsync();

    Task<Categorie?> GetCategorieByIdAsync(int id);

    Task<IEnumerable<Categorie>> GetCategorieenByArtikelIdAsync(int id);

    Task<Categorie> AddArtikelToCategorieAsync(Categorie categorie, int artikelId);

    Task<Categorie> RemoveArtikelFromCategorieAsync(Categorie categorie, int artikelId);

    Task<Categorie> UpdateCategorieAsync(Categorie categorie);
}