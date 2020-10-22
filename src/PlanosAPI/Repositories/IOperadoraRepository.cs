using PlanosAPI.Models;
using System.Linq;

namespace PlanosAPI.Repositories
{
    public interface IOperadoraRepository
    {
        Operadora Find(int id);
        IQueryable<Operadora> GetAll();

    }
}
