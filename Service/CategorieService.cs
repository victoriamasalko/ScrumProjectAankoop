using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;


namespace Service;

public class CategorieService
{
    private readonly ICategorieRepository categorieRepository;

    public CategorieService(ICategorieRepository categorieRepository)
    {
        this.categorieRepository = categorieRepository;
    }
}
