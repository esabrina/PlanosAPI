using Microsoft.EntityFrameworkCore;
using PlanosAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace PlanosAPI.Repositories
{
    public class PlanoRepository : IPlanoRepository
    {
        private readonly PlanoDBContext _context;
        public PlanoRepository(PlanoDBContext ctx)
        {
            _context = ctx;
        }
        public void Add(Plano plano)
        {
            _context.Planos.Add(plano);
            _context.SaveChanges();
        }

        public  Plano Find(int id)
        {
            var result =  _context.Planos
                .Include(i => i.Operadora)
                .Include(i => i.TipoPlano)
                .Include(i => i.PlanoDDD)
                .FirstOrDefault(i => i.Id == id);
            return result;
        }

        public IQueryable<Plano> GetAll()
        {
            var p =  _context.Planos.
                Include(i => i.Operadora).
                Include(i => i.TipoPlano).
                Include(i => i.PlanoDDD);
            return (IQueryable<Plano>)p;
        }

        public void Remove(int id)
        {
            var entity = _context.Planos.First(i => i.Id == id);
            _context.Planos.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Plano plano)
        {
            _context.Planos.Update(plano);
            _context.SaveChanges();
        }

        public IQueryable<Plano> GetByDDD(string ddd)
        {
            IQueryable<Plano> result;
            var _query = (from z in _context.Planos.
              Include(i => i.Operadora).Include(i => i.TipoPlano).Include(i => i.PlanoDDD)
                          join d in _context.PlanosDDDs on z.Id equals d.IdPlano
                          where d.CodigoDDD == ddd
                          select z);
            try
            {
                result = (IQueryable<Plano>) _query;
            }
            catch
            {
                // ddd not found
                result = Enumerable.Empty<Plano>().AsQueryable();
            }
            return result;
        }

    }
}
