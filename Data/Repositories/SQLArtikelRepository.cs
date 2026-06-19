using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories;

public class SQLArtikelRepository : IArtikelRepository
{
    private readonly PrulariacomContext _context;

    public SQLArtikelRepository(PrulariacomContext context)
    {
        _context = context;
    }

    public async Task<Artikel> AddArtikelAsync(Artikel artikel)
    {
        throw new NotImplementedException();
    }

    public async Task<Artikel?> GetArtikelByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Artikel>> GetArtikelsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Artikel> UpdateArtikelAsync(Artikel artikel)
    {
        throw new NotImplementedException();
    }
}
