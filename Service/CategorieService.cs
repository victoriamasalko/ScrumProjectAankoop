using Data.Models;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service;

public class CategorieService(ICategorieRepository categorieRepository)
{
    public async Task<IEnumerable<Categorie>> GetCategorieenAsync() => await categorieRepository.GetCategorieenAsync();
}
