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

    public Task<Actiecode?> GetActiecodeByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Actiecode>> GetActiecodesAsync()
    {
        return await context.Actiecodes.ToListAsync();
    }

    public Task<Actiecode> UpdateActiecodeAsync(Actiecode actiecode)
    {
        throw new NotImplementedException();
    }
}
