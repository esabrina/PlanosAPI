using PlanosAPI.Models;
using PlanosAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanosAPI.Test.FakeRepos
{
    class OperadoraRepositoryFake : IOperadoraRepository
    {
        private readonly List<Operadora> _context;
        public OperadoraRepositoryFake()
        {
            _context = new List<Operadora>()
            {
                new Operadora { Nome = "Vivo" },
                new Operadora { Nome = "Tim" },
                new Operadora { Nome = "Claro" }
            };

        }
        public Operadora Find(int id)
        {
            return _context.FirstOrDefault(i => i.Id == id);
        }

        public IQueryable<Operadora> GetAll()
        {
            return (IQueryable<Operadora>)_context;
        }
    }
}
