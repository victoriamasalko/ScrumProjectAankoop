using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories;

public class SecurityRepository : ISecurityRepository
{
    private readonly PrulariacomContext _context;

    public SecurityRepository(PrulariacomContext context)
    {
        _context = context;
    }

    public async Task<Personeelslidaccount>? GetPersoneelslidaccountAsync(int id)
    {
        return await _context.Personeelslidaccounts
            .Include(p => p.Personeelslid)
            .FirstOrDefaultAsync(pa => pa.PersoneelslidAccountId == id);
        
    }

    public async Task<IEnumerable<Personeelslidaccount>> GetPersoneelslidaccountsAsync()
    {
        return await _context.Personeelslidaccounts.ToListAsync();
    }

    public async  Task<Personeelslidaccount> UpdatePersoneelslidaccountAsync(Personeelslidaccount personeelslidaccount)
    {
        _context.Personeelslidaccounts.Update(personeelslidaccount);
        await _context.SaveChangesAsync();

        return personeelslidaccount;
    }

    public async Task<Personeelslidaccount>? GetPersoneelslidaccountByEmailAsync(string email)
    {
        email = email ?? string.Empty;
        return await _context.Personeelslidaccounts
            .Include(p => p.Personeelslid)
            .FirstOrDefaultAsync(p => p.Emailadres == email);
    }
}
