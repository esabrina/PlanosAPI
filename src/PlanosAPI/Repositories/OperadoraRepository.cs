using PlanosAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlanosAPI.Repositories
{
    public class OperadoraRepository : IOperadoraRepository
    {
        private readonly PlanoDBContext _context;
        public OperadoraRepository(PlanoDBContext ctx)
        {
            _context = ctx;
        }

        public Operadora Find(int id)
        {
            return _context.Operadoras.FirstOrDefault(i => i.Id == id);
        }

        public IQueryable<Operadora> GetAll()
        {
            return (IQueryable<Operadora>) _context.Operadoras;
        }
    }
}
