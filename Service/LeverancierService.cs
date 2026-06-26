using Data.Models;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class LeverancierService
    {
        private readonly ILeverancierRepository leverancierRepository;
        public LeverancierService(ILeverancierRepository leverancierRepository)
        {
            this.leverancierRepository = leverancierRepository;
        }
        public async Task<Leverancier> AddLeverancierAsync(Leverancier leverancier) => await leverancierRepository.AddLeverancierAsync(leverancier);

        public async Task<Leverancier?> GetLeverancierByIdAsync(int id) => await leverancierRepository.GetLeverancierByIdAsync(id);
        public async Task<Leverancier> AddLeverancier() => await leverancierRepository.AddLeverancier();
        public async Task<IEnumerable<Leverancier>> GetLeveranciersAsync() => await leverancierRepository.GetLeveranciersAsync();
        public async Task<Leverancier> UpdateLeverancierAsync(Leverancier leverancier) => await leverancierRepository.UpdateLeverancierAsync(leverancier);

    }
}
