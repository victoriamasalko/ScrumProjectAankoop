using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class PlaatsRepository(PrulariacomContext context) : IPlaatsRepository
{
    public async Task<IEnumerable<Plaats>> GetPlaatsenAsync()
        => await context.Plaatsen.ToListAsync();

    /*public async Task<Plaats?> GetPlaatsByIdAsync(int id)
        => await context.Plaatsen
            .Where(p => p.PlaatsId == id)
            .FirstOrDefaultAsync();*/
}