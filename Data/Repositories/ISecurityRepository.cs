using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories;

public interface ISecurityRepository
{
    Task<IEnumerable<Personeelslidaccount>> GetPersoneelslidaccountsAsync();

    Task<Personeelslidaccount> GetPersoneelslidaccountAsync(int id);

    Task<Personeelslidaccount> UpdatePersoneelslidaccountAsync(Personeelslidaccount personeelslidaccount);

    Task<Personeelslidaccount>? GetPersoneelslidaccountByEmailAsync(string email);
}
