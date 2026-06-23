using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class CategorieRepository : ICategorieRepository
{
    private readonly PrulariacomContext context;

    public CategorieRepository(PrulariacomContext context)
    {
        this.context = context;
    }

    public Task<IEnumerable<Categorie>> GetCategorieenAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Categorie?> GetHoofdcategorieByCategorieIdAsync(int id)
    {
        return await context.Categorieen
            .Where(x => x.CategorieId == id)
            .Select(x => x.HoofdCategorie)
            .FirstOrDefaultAsync();
    }
}
