using System;
using System.Collections.Generic;
using System.Text;
using Data.Models;

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
            throw new NotImplementedException();
        }

        public async Task<Leverancier?> GetLeverancierByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Leverancier>> GetLeveranciersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Leverancier> UpdateLeverancierAsync(Leverancier leverancier)
        {
            throw new NotImplementedException();
        }
    }
}
