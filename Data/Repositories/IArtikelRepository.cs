using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    internal interface IArtikelRepository
    {
        Task<IEnumerable<Artikel>> GetArtikels();

        Task<Artikel?> GetArtikelById(int  id);
    }
}
