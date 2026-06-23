using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories;

public class ArtikelRepository : IArtikelRepository
{
    private readonly PrulariacomContext _context;

    public ArtikelRepository(PrulariacomContext context)
    {
        _context = context;
    }

    public async Task<Artikel> AddArtikelAsync(Artikel artikel)
    {
        throw new NotImplementedException();
    }

    public async Task<Artikel?> GetArtikelByIdAsync(int id)
    {
        return await _context.Artikels
            .Where(x => x.ArtikelId == id)
            .Include(x => x.Categories)
            .Include(x => x.Leveranciers)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Artikel>> GetArtikelsAsync()
    {
        return await _context.Artikels
            .Include(artikel => artikel.Leveranciers)
            .Include(artikel => artikel.Categories)
            .ToListAsync();
    }

    public async Task<Artikel> UpdateArtikelAsync(Artikel artikel)
    {
        throw new NotImplementedException();
    }
}
