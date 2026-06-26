using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface IPlaatsRepository
    {
        Task<IEnumerable<Plaats>> GetPlaatsenAsync();
    }
}