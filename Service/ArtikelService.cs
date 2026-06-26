using Data.Models;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class ArtikelService
    {
        private readonly IArtikelRepository artikelRepository;

        public ArtikelService(IArtikelRepository artikelRepository)
        {
            this.artikelRepository = artikelRepository;
        }

        public async Task AddArtikelAsync(Artikel artikel, List<int> selectedCategorieIds) => await artikelRepository.AddArtikelAsync(artikel, selectedCategorieIds);

        public async Task<Artikel?> GetArtikelAsync(int id) => await artikelRepository.GetArtikelByIdAsync(id);

        public async Task<IEnumerable<Artikel>> GetArtikelsAsync() => await artikelRepository.GetArtikelsAsync();
        public async Task UpdateArtikelAsync(Artikel artikel, List<int> selectedCategorieIds) => await artikelRepository.UpdateArtikelAsync(artikel, selectedCategorieIds);
    }
}