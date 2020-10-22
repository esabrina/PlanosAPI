using Microsoft.AspNetCore.Mvc;
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
    public class PlanoControllerTest
    {
        PlanoController _controller;

        public PlanoControllerTest()
        {
            var options = new DbContextOptionsBuilder<PlanoDBContext>()
                .UseInMemoryDatabase(databaseName: "PlanosDB").Options;

            //Mock DBContext
            var context = new PlanoDBContext(options);
            if (!context.Planos.Any())
            {
                CargaTeste.Populate(ref context);
            }

            var _repo = new PlanoRepository(context);
            _controller = new PlanoController(_repo, context);
        }
        [Fact]
        public void InsertOKTest()
        {
            bool valid = false;
            List<PlanoDDD> list_planoddd = new List<PlanoDDD>();
            list_planoddd.Add(new PlanoDDD { CodigoDDD = "011" });
            list_planoddd.Add(new PlanoDDD { CodigoDDD = "027" });
            var _data = new Plano() {
                Minutos = 100, Franquia = 5, UnidadeFranquia = "GB",
                Valor = 100, IdTipoPlano = 1, IdOperadora = 1,
                PlanoDDD = list_planoddd
            };

            var response = (ObjectResult)_controller.Insert(_data);
            try
            {
                Plano p  = (Plano)response.Value;
                if (p.PlanoDDD.Count() == 2)
                {
                    //plano inserido com mais de um ddd
                    valid = true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            Assert.True(valid);
        }
 
        [Theory(DisplayName = "Validação de dados.")]
        [InlineData(100, 1, "011")]
        [InlineData(1, 100, "011")]
        [InlineData(1, 2, "02111")]
        [InlineData(1, 2, "021")]
        public void InsertErrorTest(int idOperadora, int idTipoPlano, string ddd)
        {
            // tipo de plano ou operadora inexistente
            // ddd inexistente ou repetido
            bool valid = false;
            List<PlanoDDD> list_planoddd = new List<PlanoDDD>();
            list_planoddd.Add(new PlanoDDD { CodigoDDD = "021" });
            list_planoddd.Add(new PlanoDDD { CodigoDDD = ddd });
            var _data = new Plano()
            {
                Minutos = 90, Franquia = 5, 
                UnidadeFranquia = "GB", Valor = 100,
                IdTipoPlano = idTipoPlano, IdOperadora = idOperadora,
                PlanoDDD = list_planoddd
            };

            var response = (ObjectResult)_controller.Insert(_data);
            if (response.StatusCode == 404)
            {
                valid = true;
            }
            Assert.True(valid);
        }
        [Fact]
        public void GetAllTest()
        {
            IQueryable<Plano> response = _controller.GetAll();
            Assert.True(response.Count() > 0);
        }
        [Fact]
        public void GetByIdOKTest()
        {
            var response = (ObjectResult) _controller.GetById(2);
            Assert.IsType<Plano>(response.Value);
        }
        [Fact]
        public void GetById404Test()
        {
            bool valid = false;
            var response = _controller.GetById(200);
            if (response == null || response.GetType() == typeof(NotFoundResult))
            {
                valid = true;
            }
            Assert.True(valid);
        }
        [Fact]
        public void DeleteOKTest()
        {
            var response = _controller.Delete(1) as ObjectResult;
            Assert.True(response != null);
        }
        [Fact]
        public void Delete404Test()
        {
            var response = _controller.Delete(200) as ObjectResult;
            Assert.True(response is null);
        }
        [Fact]
        public void GetByDDDOKTest()
        {
            IQueryable<Plano> response = _controller.GetByDDD("011");
            Assert.True(response.Count() > 0);
        }
        [Fact]
        public void GetByDDDErrorTest()
        {
            IQueryable<Plano> response = _controller.GetByDDD("200");
            Assert.True(response.Count() == 0);
        }
        [Fact]
        public void UpdateOKTest()
        {
            int id = 2;
            var response = _controller.GetById(id) as ObjectResult;
            bool valid = false;
            List<PlanoDDD> list_planoddd = new List<PlanoDDD>();
            list_planoddd.Add(new PlanoDDD { CodigoDDD = "031" });
            list_planoddd.Add(new PlanoDDD { CodigoDDD = "011" });

            if (response != null)
            {
                try
                {
                    Plano _obj = (Plano)response.Value;
                    _obj.Franquia = 20;
                    _obj.IdOperadora = 2;
                    _obj.IdTipoPlano = 2;
                    _obj.Minutos = 60;
                    _obj.UnidadeFranquia = "GB";
                    _obj.PlanoDDD = list_planoddd;

                    OkObjectResult response_update = (OkObjectResult)_controller.Update(id, _obj);
                    if (response_update.StatusCode == 200)
                    {
                        valid = true;
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            Assert.True(valid);
        }
        [Theory(DisplayName = "Validação de dados.")]
        [InlineData(100, 1, "011")]
        [InlineData(1, 100, "011")]
        [InlineData(1, 2, "02111")]
        [InlineData(1, 2, "031")]
        public void UpdateErrorTest(int idOperadora, int idTipoPlano, string ddd)
        {
            // tipo de plano ou operadora inexistente
            // ddd inexistente ou repetido
            int id = 2;
            var response = _controller.GetById(id) as ObjectResult;
            bool valid = false;
            List<PlanoDDD> list_planoddd = new List<PlanoDDD>();
            list_planoddd.Add(new PlanoDDD { CodigoDDD = "031" });
            list_planoddd.Add(new PlanoDDD { CodigoDDD = ddd });

            if (response != null)
            {
                try
                {
                    Plano _obj = (Plano)response.Value;
                    _obj.Franquia = 20;
                    _obj.IdOperadora = idOperadora;
                    _obj.IdTipoPlano = idTipoPlano;
                    _obj.Minutos = 60;
                    _obj.UnidadeFranquia = "GB";
                    _obj.PlanoDDD = list_planoddd;

                    var response_update = _controller.Update(id, _obj);
                    if (response_update == null || 
                        response_update.GetType() == typeof(NotFoundObjectResult))
                    {
                        valid = true;
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            Assert.True(valid);
        }
        [Fact]
        public void DataValidateOKTest()
        {
            List<string> list_planoddd = new List<string>();
            list_planoddd.Add("011");
            list_planoddd.Add("027");
            string result = _controller.DataValidate(1, 1, list_planoddd);

            Assert.True(string.IsNullOrEmpty(result));
        }
        [Theory(DisplayName = "Validação de dados.")]
        [InlineData(100, 1, "011")]
        [InlineData(1, 100, "011")]
        [InlineData(1, 2, "011111")]
        public void DataValidateErrorTest(int idOperadora, int idTipoPlano, string ddd)
        {
            List<string> list_planoddd = new List<string>();
            list_planoddd.Add(ddd);
            list_planoddd.Add("027");
            string result = _controller.DataValidate(idOperadora, idTipoPlano, list_planoddd);

            Assert.True(!string.IsNullOrEmpty(result));
        }
        [Fact]
        public void DataValidateSemDDDTest()
        {
            // assumindo que o plano pode ficar em stand-by sem nenhum DDD
            List<string> list_planoddd = new List<string>();
            string result = _controller.DataValidate(1, 1, list_planoddd);

            Assert.True(string.IsNullOrEmpty(result));
        }
    }
}
