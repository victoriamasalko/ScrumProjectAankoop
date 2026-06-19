using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories;

public class SQLCategorieRepository : ICategorieRepository
{
    private readonly PrulariacomContext context;

    public SQLCategorieRepository(PrulariacomContext context)
    {
        this.context = context;
    }

    public Task<IEnumerable<Categorie>> GetCategorieen()
    {
        throw new NotImplementedException();
    }
}
