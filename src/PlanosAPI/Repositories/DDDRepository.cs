using PlanosAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace PlanosAPI.Repositories
{
    public class DDDRepository : IDDDRepository
    {
        private readonly PlanoDBContext _context;
        public DDDRepository(PlanoDBContext ctx)
        {
            _context = ctx;
        }

        public DDD Find(string id)
        {
            return _context.DDDs.FirstOrDefault(i => i.Codigo == id);
        }

        public IEnumerable<DDD> GetAll()
        {
            return _context.DDDs.ToList();
        }

    }
}
