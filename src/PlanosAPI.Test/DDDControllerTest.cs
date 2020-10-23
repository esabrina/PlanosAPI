using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PlanosAPI.Controllers;
using PlanosAPI.Models;
using PlanosAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PlanosAPI.Test
{
    public class DDDControllerTest : IDisposable
    {
        DDDController _controller;
        PlanoDBContext _contextTest;
        public DDDControllerTest()
        {
            var options = new DbContextOptionsBuilder<PlanoDBContext>()
                .UseInMemoryDatabase(databaseName: "DDDTeste").Options;

            var context = new PlanoDBContext(options);
            if (!context.DDDs.Any())
            {
                context.DDDs.Add(new DDD { Codigo = "021" });
                context.DDDs.Add(new DDD { Codigo = "011" });
                context.DDDs.Add(new DDD { Codigo = "031" });
                context.SaveChanges();
            }

            _contextTest = context;
            var _repo = new DDDRepository(context);
            _controller = new DDDController(_repo);
        }
        public void Dispose()
        {
            _contextTest.Database.EnsureDeleted();
            _contextTest.Dispose();
        }
        [Fact]
        public void GetAllTest()
        {
            IEnumerable<DDD> response = _controller.GetAll();
            Assert.True(response.Count() > 0);
        }
    }
}
