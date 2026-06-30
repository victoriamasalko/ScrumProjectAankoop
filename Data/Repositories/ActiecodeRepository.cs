using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories;

public class ActiecodeRepository : IActiecodeRepository
{
    private readonly PrulariacomContext context;

    public ActiecodeRepository(PrulariacomContext context)
    {
        this.context = context;
    }

    public async Task<Actiecode> AddActiecodeAsync(Actiecode actiecode)
    {
        context.Actiecodes.Add(actiecode);
        await context.SaveChangesAsync();

        return actiecode;
    }

    public async Task<Actiecode?> GetActiecodeByIdAsync(int id)
    {
        if (context?.Actiecodes == null)
            return null;
        
        return await context.Actiecodes.Where(x => x.ActiecodeId == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Actiecode>> GetActiecodesAsync()
    {
        return await context.Actiecodes.ToListAsync();
    }

    public async Task<Actiecode> UpdateActiecodeAsync(Actiecode actiecode)
    {
        var existingActiecode = await context.Actiecodes
            .FirstOrDefaultAsync(a => a.ActiecodeId == actiecode.ActiecodeId);
        
        if (existingActiecode == null)
            return null;

        existingActiecode.Naam = actiecode.Naam;
        existingActiecode.GeldigVanDatum = actiecode.GeldigVanDatum;
        existingActiecode.GeldigTotDatum = actiecode.GeldigTotDatum;
        existingActiecode.IsEenmalig = actiecode.IsEenmalig;

        await context.SaveChangesAsync();
        
        return existingActiecode;
    }
}
