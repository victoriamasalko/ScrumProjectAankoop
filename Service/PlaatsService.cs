using Data.Models;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class PlaatsService
    {
        private readonly IPlaatsRepository plaatsRepository;

        public PlaatsService(IPlaatsRepository plaatsRepository)
        {
            this.plaatsRepository = plaatsRepository;
        }

        public async Task<IEnumerable<Plaats>> GetPlaatsenAsync() => await plaatsRepository.GetPlaatsenAsync();
    }
}
