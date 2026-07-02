using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories;

public interface IArtikelRepository
{
    Task<IEnumerable<Artikel>> GetArtikelsAsync();

    Task<Artikel?> GetArtikelByIdAsync(int  id);

    Task<Artikel> AddArtikelAsync(Artikel artikel, List<int> selectedCategorieIds);

    Task<Artikel> UpdateArtikelAsync(Artikel artikel, List<int> selectedCategorieIds);

    Task<Artikel> DeactivateArtikelAsync(int artikelId);
}
