using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using PlanosAPI.Models;
using PlanosAPI.Repositories;

namespace PlanosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanoController : ControllerBase
    {
        private readonly IPlanoRepository _planoRepository;
        private readonly PlanoDBContext _context;

        public PlanoController(IPlanoRepository planoRepository, PlanoDBContext ctx)
        {
            _planoRepository = planoRepository;
            _context = ctx;
        }

        // GET: api/<PlanoController>
        [HttpGet]
        [EnableQuery]
        public  IQueryable<Plano> GetAll()
        {
            var _data = _planoRepository.GetAll();
            return (IQueryable<Plano>)_data;
        }

        // GET api/<PlanoController>/5
        [HttpGet("{id}", Name = "GetPlano")]
        public IActionResult GetById(int id)
        {
            var plano =  _planoRepository.Find(id);
            if (plano == null)
                return NotFound();

            return new ObjectResult(plano);
        }

        // POST api/<PlanoController>
        [HttpPost]
        public IActionResult Insert([FromBody] Plano plano)
        {
            if (plano == null)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //Assumindo que os dados de DDD, Operadora e tipo de plano já devem existir no BD
            List<string> listDDD = new List<string>();
            if (plano.PlanoDDD != null)
            {
                foreach (PlanoDDD item in plano.PlanoDDD)
                {
                    listDDD.Add(item.CodigoDDD);
                }
            }

            string validacao_dados = DataValidate(plano.IdOperadora, plano.IdTipoPlano, listDDD);
            if (string.IsNullOrEmpty(validacao_dados))
            {
                _planoRepository.Add(plano);
            }
            else
            {
                return NotFound(validacao_dados);
            }
            
            return CreatedAtRoute("GetPlano", new { id = plano.Id }, plano);
        }

        // PUT api/<PlanoController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Plano plano)
        {
            if (plano == null || plano.Id != id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var _plano = _planoRepository.Find(id);
            List<string> listDDD = new List<string>();

            if (_plano == null)
                return NotFound("Plano não encontrado.");

            //Assumindo que os dados de DDD, Operadora e tipo de plano já devem existir no BD
            if (plano.PlanoDDD != null) { 
                foreach (PlanoDDD item in plano.PlanoDDD)
                {
                    listDDD.Add(item.CodigoDDD);
                }
            }
            string validar_dados = DataValidate(plano.IdOperadora, plano.IdTipoPlano, listDDD);
            if (string.IsNullOrEmpty(validar_dados))
            {
                _plano.Minutos = plano.Minutos;
                _plano.Franquia = plano.Franquia;
                _plano.UnidadeFranquia = plano.UnidadeFranquia;
                _plano.Valor = plano.Valor;
                _plano.IdTipoPlano = plano.IdTipoPlano;
                _plano.IdOperadora = plano.IdOperadora;
                _plano.PlanoDDD = plano.PlanoDDD;
                _planoRepository.Update(_plano);
            }
            else
            {
                return NotFound(validar_dados);
            }

            return Ok("");
        }

        // DELETE api/<PlanoController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var plano = _planoRepository.Find(id);
            if (plano == null)
                return NotFound();

            _planoRepository.Remove(id);
            return Ok("");
        }

        // GET: api/<PlanoController>
        [HttpGet("ddd/{id}")]
        [EnableQuery]
        public IQueryable<Plano> GetByDDD(string id)
        {
            var _data = _planoRepository.GetByDDD(id);
            return (IQueryable<Plano>)_data;
        }

        public string DataValidate(int idOperadora, int idTipoPlano, List<string> listDDD)
        {
            string error_message = string.Empty;
            IOperadoraRepository _operadoraRepo = new OperadoraRepository(_context);
            ITipoPlanoRepository _tipoplanoRepo = new TipoPlanoRepository(_context);
            IDDDRepository _dddRepo = new DDDRepository(_context);

            // Todos os dados já devem existir na base de dados
            if (_operadoraRepo.Find(idOperadora) == null)
            {
                error_message = "Operadora não encontrada. ";
            }
            if (_tipoplanoRepo.Find(idTipoPlano) == null)
            {
                error_message += "Tipo de Plano não encontrado. ";
            }

            bool tem_ddd_duplicado = listDDD.GroupBy(n => n).Any(c => c.Count() > 1);
            if (tem_ddd_duplicado)
            {
                error_message += "DDD não encontrado. ";
            }
            foreach (string itemDDD in listDDD)
            {
                if (_dddRepo.Find(itemDDD) == null)
                {
                    error_message += "DDD não encontrado. ";
                }
            }
            _operadoraRepo = null;
            _tipoplanoRepo = null;
            _dddRepo = null;
            return error_message;
        }

    }
}
