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

        public async Task AddLeverancierAsync(Leverancier leverancier)
        {
            _context.Leveranciers.Add(leverancier);
        }

        public async Task<Leverancier?> GetLeverancierByIdAsync(int id)
        {
            return _context.Leveranciers.Find(id);
        }

        public async Task<IEnumerable<Leverancier>> GetLeveranciersAsync()
        {
            return await _context.Leveranciers
                .Include(l => l.Artikels)
                .ToListAsync();
        }

        public async Task UpdateLeverancierAsync(Leverancier leverancier)
        {
            _context.Leveranciers.Update(leverancier);
        }
    }
}
