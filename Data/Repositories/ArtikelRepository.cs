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

    public async Task<Artikel> AddArtikelAsync(Artikel artikel, List<int> selectedCategorieIds)
    {
        if(selectedCategorieIds != null && selectedCategorieIds.Any())
        {
            var categorieen = await _context.Categorieen
                .Where(c => selectedCategorieIds.Contains(c.CategorieId))
                .ToArrayAsync();

            artikel.Categorieen = categorieen;
        }

        _context.Artikels.Add(artikel);
        await _context.SaveChangesAsync();

        return artikel;
    }

    public async Task<Artikel?> GetArtikelByIdAsync(int id)
    {
        return await _context.Artikels
            .Where(x => x.ArtikelId == id)
            .Include(x => x.Categorieen)
            .Include(x => x.Leverancier)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Artikel>> GetArtikelsAsync()
    {
        return await _context.Artikels
            .Include(artikel => artikel.Leverancier)
            .Include(artikel => artikel.Categorieen)
            .ToListAsync();
    }

    public async Task<Artikel> UpdateArtikelAsync(Artikel artikel, List<int> selectedCategorieIds)
    {
        var existingArtikel = await _context.Artikels
            .Include(a => a.Categorieen)
            .FirstOrDefaultAsync(a => a.ArtikelId == artikel.ArtikelId);

        if(existingArtikel == null)
        {
            throw new InvalidOperationException("Artikel niet gevonden");
        }

        existingArtikel.Naam = artikel.Naam;
        existingArtikel.Beschrijving = artikel.Beschrijving;
        existingArtikel.Prijs = artikel.Prijs;
        existingArtikel.GewichtInGram = existingArtikel.GewichtInGram;
        existingArtikel.Bestelpeil = artikel.Bestelpeil;
        existingArtikel.MinimumVoorraad = artikel.MinimumVoorraad;
        existingArtikel.MaximumVoorraad = artikel.MaximumVoorraad;
        existingArtikel.Levertijd = artikel.Levertijd;
        existingArtikel.AantalBesteldLeverancier = artikel.AantalBesteldLeverancier;
        existingArtikel.MaxAantalInMagazijnPlaats = artikel.MaxAantalInMagazijnPlaats;
        existingArtikel.LeveranciersId = artikel.LeveranciersId;

        var geselecteerdeCategorieen = await _context.Categorieen
            .Where(c => selectedCategorieIds.Contains(c.CategorieId))
            .ToListAsync();

        existingArtikel.Categorieen = geselecteerdeCategorieen;

        await _context.SaveChangesAsync();

        return existingArtikel;
    }
}
