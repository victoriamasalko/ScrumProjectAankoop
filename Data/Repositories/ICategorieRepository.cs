using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories;

public interface ICategorieRepository
{
    Task<IEnumerable<Categorie>> GetCategorieenAsync();
    Task<Categorie?> GetHoofdcategorieByCategorieIdAsync(int id);
}