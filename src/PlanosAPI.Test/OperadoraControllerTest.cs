using Microsoft.EntityFrameworkCore;
using PlanosAPI.Controllers;
using PlanosAPI.Models;
using PlanosAPI.Repositories;
using System;
using System.Linq;
using Xunit;

namespace PlanosAPI.Test
{
    public class OperadoraControllerTest : IDisposable
    {
        OperadoraController _controller;
        PlanoDBContext _contextTest;
        public OperadoraControllerTest() 
        {
            var options = new DbContextOptionsBuilder<PlanoDBContext>()
                .UseInMemoryDatabase(databaseName: "OperadoraTeste").Options;

            var context = new PlanoDBContext(options);
            if (!context.Operadoras.Any())
            {
                context.Operadoras.Add(new Operadora { Nome = "Vivo" });
                context.Operadoras.Add(new Operadora { Nome = "Tim" });
                context.Operadoras.Add(new Operadora { Nome = "Claro" });
                context.SaveChanges();
            }

            _contextTest = context;
            var _repo = new OperadoraRepository(context);
            _controller = new OperadoraController(_repo);
        }

        [Fact]
        public void GetAllTest()
        {
            IQueryable<Operadora> response = _controller.GetAll();
            Assert.True(response.Count() > 0);
        }
        public void Dispose()
        {
            _contextTest.Database.EnsureDeleted();
            _contextTest.Dispose();
        }

    }
}
