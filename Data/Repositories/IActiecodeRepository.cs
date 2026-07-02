using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories;

public interface IActiecodeRepository
{
    Task<IEnumerable<Actiecode>> GetActiecodesAsync();

    Task<Actiecode?> GetActiecodeByIdAsync(int id);

    Task<Actiecode> AddActiecodeAsync(Actiecode actiecode);

    Task<Actiecode> UpdateActiecodeAsync(Actiecode actiecode);
}