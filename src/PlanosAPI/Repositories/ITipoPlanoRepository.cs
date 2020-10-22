using PlanosAPI.Models;
using System.Collections.Generic;

namespace PlanosAPI.Repositories
{
    public interface ITipoPlanoRepository
    {
        TipoPlano Find(int id);
        IEnumerable<TipoPlano> GetAll();
    }
}
