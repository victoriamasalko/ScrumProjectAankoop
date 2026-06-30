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
}
