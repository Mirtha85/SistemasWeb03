﻿using SistemasWeb01.Models;

namespace SistemasWeb01.Repository.Interfaces
{
    public interface ITallaRepository
    {
        IEnumerable<Talla> AllTallas { get; }

        IEnumerable<Talla> TallasFiltered(IEnumerable<Talla> filter);
        Talla GetTallaById(int id);
        void CreateTalla(Talla talla);

        void EditTalla(Talla talla);

        void DeleteTalla(Talla talla);
    }
}
