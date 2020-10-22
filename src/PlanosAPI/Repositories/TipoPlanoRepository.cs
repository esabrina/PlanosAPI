using PlanosAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace PlanosAPI.Repositories
{
    public class TipoPlanoRepository : ITipoPlanoRepository
    {
        private readonly PlanoDBContext _context;
        public TipoPlanoRepository(PlanoDBContext ctx)
        {
            _context = ctx;
        }

        public TipoPlano Find(int id)
        {
            return _context.TiposPlanos.FirstOrDefault(i => i.Id == id);
        }

        public IEnumerable<TipoPlano> GetAll()
        {
            return _context.TiposPlanos.ToList();
        }
    }
}
