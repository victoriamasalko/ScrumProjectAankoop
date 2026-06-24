using Data.Models;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service;

public class CategorieService(ICategorieRepository categorieRepository)
{
    public async Task<IEnumerable<Categorie>> GetCategorieenAsync() => await categorieRepository.GetCategorieenAsync();

    public async Task<Categorie?> GetCategorieByIdAsync(int id) => await categorieRepository.GetCategorieByIdAsync(id);

    public async Task<IEnumerable<Categorie>> GetCategorieenByArtikelIdAsync(int id) => await categorieRepository.GetCategorieenByArtikelIdAsync(id);

    public async Task<Categorie> AddArtikelToCategorieAsync(Categorie categorie, int artikelId) => await categorieRepository.AddArtikelToCategorieAsync(categorie, artikelId);

    public async Task<Categorie> RemoveArtikelFromCategorieAsync(Categorie categorie, int artikelId) => await categorieRepository.RemoveArtikelFromCategorieAsync(categorie, artikelId);

    public async Task<Categorie> UpdateCategorieAsync(Categorie categorie) => await categorieRepository.UpdateCategorieAsync(categorie);
}
