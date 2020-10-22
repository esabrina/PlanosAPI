using PlanosAPI.Models;
using System.Collections.Generic;

namespace PlanosAPI.Repositories
{
    public interface IDDDRepository
    {
        DDD Find(string id);
        IEnumerable<DDD> GetAll();
    }
}
