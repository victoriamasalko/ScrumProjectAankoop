using Data.Models;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class ArtikelService(IArtikelRepository artikelRepository)
    {
        public async Task AddArtikelAsync(Artikel artikel) => await artikelRepository.AddArtikelAsync(artikel);

        public async Task<Artikel?> GetArtikelAsync(int id) => await artikelRepository.GetArtikelByIdAsync(id);

        public async Task<IEnumerable<Artikel>> GetArtikelsAsync() => await artikelRepository.GetArtikelsAsync();
        public async Task UpdateArtikelAsync(Artikel artikel) => await artikelRepository.UpdateArtikelAsync(artikel);
    }
}
