using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
}
