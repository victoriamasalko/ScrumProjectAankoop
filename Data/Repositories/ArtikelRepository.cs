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
        _context.Artikels.Add(artikel);
        await _context.SaveChangesAsync();

        return artikel;
    }

    public async Task<Artikel?> GetArtikelByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Artikel>> GetArtikelsAsync()
    {
        return await _context.Artikels
            .Include(artikel => artikel.Leverancier)
            .Include(artikel => artikel.Categorieen)
            .ToListAsync();
    }

    public async Task<Artikel> UpdateArtikelAsync(Artikel artikel)
    {
        throw new NotImplementedException();
    }
}
