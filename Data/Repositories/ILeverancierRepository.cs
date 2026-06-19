using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface ILeverancierRepository
    {
        Task<Leverancier?> GetLeverancierById(int id);

        Task<IEnumerable<Leverancier>> GetLeveranciers();
    }
}
