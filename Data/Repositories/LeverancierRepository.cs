using System;
using System.Collections.Generic;
using System.Text;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class LeverancierRepository : ILeverancierRepository
    {
        private readonly PrulariacomContext _context;

        public LeverancierRepository (PrulariacomContext context)
        {
            _context = context;
        }

        public async Task<Leverancier> AddLeverancierAsync(Leverancier leverancier)
        {
            await _context.Leveranciers.AddAsync(leverancier);
            await _context.SaveChangesAsync();
            return leverancier;
        }

        public async Task<Leverancier> AddLeverancier()
        {
            throw new NotImplementedException();
        }

        public async Task<Leverancier?> GetLeverancierByIdAsync(int id)
        {
            return await _context.Leveranciers
                .Include(l => l.Plaats)
                .Include(l => l.Artikels)
                .FirstOrDefaultAsync(l => l.LeveranciersId == id);
        }

        public async Task<IEnumerable<Leverancier>> GetLeveranciersAsync()
        {
            return await _context.Leveranciers
                .Include(l => l.Artikels)
                .ToListAsync();
        }

        public async Task<Leverancier> UpdateLeverancierAsync(Leverancier leverancier)
        {
            throw new NotImplementedException();
        }
    }
}
