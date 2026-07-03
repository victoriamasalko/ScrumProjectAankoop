using Data.Models;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service;

public class SecurityService
{
    private readonly ISecurityRepository _securityRepository;

    public SecurityService(ISecurityRepository securityRepository)
    {
        _securityRepository = securityRepository;
    }

    public async Task<Personeelslidaccount> GetPersoneelslidaccountAsync(int id) => await _securityRepository.GetPersoneelslidaccountAsync(id);

    public async Task<IEnumerable<Personeelslidaccount>> GetPersoneelslidaccountsAsync() => await _securityRepository.GetPersoneelslidaccountsAsync();

    public async Task<Personeelslidaccount> UpdatePersoneelslidaccountAsync(Personeelslidaccount personeelslidaccount) => await _securityRepository.UpdatePersoneelslidaccountAsync(personeelslidaccount);

    public async Task<Personeelslidaccount>? GetPersoneelslidaccountByEmailAsync(string email) => await _securityRepository.GetPersoneelslidaccountByEmailAsync(email);
}
