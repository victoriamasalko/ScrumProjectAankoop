using Data.Models;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service;

public class ActiecodeService
{
    private readonly IActiecodeRepository actiecodeRepository;

    public ActiecodeService(IActiecodeRepository actiecodeRepository)
    {
        this.actiecodeRepository = actiecodeRepository;
    }

    public async Task<IEnumerable<Actiecode>> GetActiecodesAsync() => await actiecodeRepository.GetActiecodesAsync();

    public async Task<Actiecode?> GetActiecodeByIdAsync(int id) => await actiecodeRepository.GetActiecodeByIdAsync(id);

    public async Task<Actiecode> AddActiecodeAsync(Actiecode actiecode) => await actiecodeRepository.AddActiecodeAsync(actiecode);

    public async Task<Actiecode> UpdateActiecodeAsync(Actiecode actiecode) => await actiecodeRepository.UpdateActiecodeAsync(actiecode);
}