using Microsoft.EntityFrameworkCore;
using PlanosAPI.Controllers;
using PlanosAPI.Models;
using PlanosAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PlanosAPI.Test
{
    public class TipoPlanoControllerTest : IDisposable
    {
        TipoPlanoController _controller;
        PlanoDBContext _contextTest;
        public TipoPlanoControllerTest()
        {
            var options = new DbContextOptionsBuilder<PlanoDBContext>()
                .UseInMemoryDatabase(databaseName: "TipoPlanoTeste").Options;

            var context = new PlanoDBContext(options);
            if (!context.TiposPlanos.Any())
            {
                context.TiposPlanos.Add(new TipoPlano { Nome = "Pos" });
                context.TiposPlanos.Add(new TipoPlano { Nome = "Controle" });
                context.TiposPlanos.Add(new TipoPlano { Nome = "Pre" });
                context.SaveChanges();
            }

            _contextTest = context;
            var _repo = new TipoPlanoRepository(context);
            _controller = new TipoPlanoController(_repo);
        }
        public void Dispose()
        {
            _contextTest.Database.EnsureDeleted();
            _contextTest.Dispose();
        }
        [Fact]
        public void GetAllTest()
        {
            IEnumerable<TipoPlano> response = _controller.GetAll();
            Assert.True(response.Count() > 0);
        }
    }
}
