using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories;

public interface IActiecodeRepository
{
    Task<IEnumerable<Actiecode>> GetActiecodesAsync();

    Task<Actiecode?> GetActiecodeById(int id);

    Task<Actiecode> AddActiecode(Actiecode actiecode);

    Task<Actiecode> UpdateActiecode(Actiecode actiecode);
}