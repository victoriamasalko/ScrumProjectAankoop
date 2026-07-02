using Data.Models;
using Microsoft.EntityFrameworkCore;
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

    public async Task<Categorie?> GetCategorieByIdAsync(int id)
    {
        return await context.Categorieen
            .Include(c => c.HoofdCategorie)
            .Include(c => c.SubCategorieen)
            .Include(c => c.Artikels)
            .FirstOrDefaultAsync(c => c.CategorieId == id);
    }

    public async Task<IEnumerable<Categorie>> GetCategorieenAsync()
    {
        return await context.Categorieen
            .Include(c => c.HoofdCategorie)
            .Include(c => c.SubCategorieen)
            .Include(c => c.Artikels)
            .ToListAsync();
    }

    public async Task<IEnumerable<Categorie>> GetCategorieenByArtikelIdAsync(int id)
    {
        return await context.Categorieen
            .Include(c => c.HoofdCategorie)
            .Include(c => c.SubCategorieen)
            .Where(c => c.Artikels.Any(a => a.ArtikelId == id))
            .ToListAsync();
    }

    public async Task<Categorie> UpdateCategorieAsync(Categorie categorie)
    {
        context.Categorieen.Update(categorie);
        await context.SaveChangesAsync();
        return categorie;
    }

    public async Task<Categorie> AddArtikelToCategorieAsync(Categorie categorie, int artikelId)
    {
        if (categorie.Artikels.Any(a => a.ArtikelId == artikelId))
        {
            return categorie;
        }

        var artikel = await context.Artikels.FindAsync(artikelId);
        if (artikel == null)
        {
            throw new KeyNotFoundException($"Het artikel met ArtikelId = {artikelId} bestaat niet");
        }

        categorie.Artikels.Add(artikel);
        await context.SaveChangesAsync();

        return categorie;
    }

    public async Task<Categorie> RemoveArtikelFromCategorieAsync(Categorie categorie, int artikelId)
    {
        var gekoppeldArtikel = categorie.Artikels.FirstOrDefault(a => a.ArtikelId == artikelId);

        if (gekoppeldArtikel == null)
        {
            throw new KeyNotFoundException($"Het artikel met ArtikelId = {artikelId} is niet gekoppeld aan de categorie {categorie.Naam} en kon dus niet ontkoppeld worden");
        }

        categorie.Artikels.Remove(gekoppeldArtikel);
        await context.SaveChangesAsync();

        return categorie;
    }

    public async Task<Categorie> UpdateCategorie(Categorie categorie)
    {
        context.Categorieen.Update(categorie);
        await context.SaveChangesAsync();
        return categorie;
    }
    public async Task<Categorie> GetCategorieByNaamAsync(string naam)
    {
        naam = naam ?? "";
        return await context.Categorieen
            .Include(c => c.HoofdCategorie)
            .Include(c => c.SubCategorieen)
            .Include(c => c.Artikels)
            .FirstOrDefaultAsync(c => c.Naam.ToUpper() == naam.ToUpper());
    }

}
