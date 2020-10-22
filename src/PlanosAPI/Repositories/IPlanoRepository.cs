using PlanosAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace PlanosAPI.Repositories
{
    public interface IPlanoRepository
    {
        void Add(Plano plano);
        IQueryable<Plano> GetAll();
        IQueryable<Plano> GetByDDD(string ddd);
        Plano Find(int id);
        void Remove(int id);
        void Update(Plano plano);
    }
}
