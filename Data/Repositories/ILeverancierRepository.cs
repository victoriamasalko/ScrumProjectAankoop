using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface ILeverancierRepository
    {
        Task<Leverancier?> GetLeverancierByIdAsync(int id);

        Task<IEnumerable<Leverancier>> GetLeveranciersAsync();
        Task<Leverancier> AddLeverancierAsync(Leverancier leverancier);
        Task<Leverancier> UpdateLeverancierAsync(Leverancier leverancier);
    }
}
