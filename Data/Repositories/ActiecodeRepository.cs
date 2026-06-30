using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories;

public class ActiecodeRepository : IActiecodeRepository
{
    public Task<Actiecode> AddActiecode(Actiecode actiecode)
    {
        throw new NotImplementedException();
    }

    public Task<Actiecode?> GetActiecodeById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Actiecode>> GetActiecodesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Actiecode> UpdateActiecode(Actiecode actiecode)
    {
        throw new NotImplementedException();
    }
}
