using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class PlaatsRepository : IPlaatsRepository
{
    private readonly PrulariacomContext _context;

    public PlaatsRepository(PrulariacomContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Plaats>> GetPlaatsenAsync() => await _context.Plaatsen.ToListAsync();
}
