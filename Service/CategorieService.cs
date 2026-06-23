using Data.Models;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;


namespace Service;

public class CategorieService
{
    private readonly ICategorieRepository categorieRepository;

    public CategorieService(ICategorieRepository categorieRepository)
    {
        this.categorieRepository = categorieRepository;
    }

    public async Task<IEnumerable<Categorie>> GetCategorieenAsync() => await categorieRepository.GetCategorieenAsync();

    public async Task<Categorie?> GetHoofdcategorieByCategorieIdAsync(int id)
        => await categorieRepository.GetHoofdcategorieByCategorieIdAsync(id);
}
