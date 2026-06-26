using Data.Models;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class PlaatsService(IPlaatsRepository plaatsRepository)
    {
        public async Task<IEnumerable<Plaats>> GetPlaatsenAsync() => await plaatsRepository.GetPlaatsenAsync();
    }
}