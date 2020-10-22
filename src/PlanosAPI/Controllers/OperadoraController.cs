using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using PlanosAPI.Models;
using PlanosAPI.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlanosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperadoraController : ControllerBase
    {
        private readonly IOperadoraRepository _operadoraRepository;

        public OperadoraController(IOperadoraRepository operadoraRepository)
        {
            _operadoraRepository = operadoraRepository;
        }
        // GET: api/<OperadoraController>
        [HttpGet]
        [EnableQuery]
        public IQueryable<Operadora> GetAll()
        {
            var _data = _operadoraRepository.GetAll();
            return (IQueryable<Operadora>)_data;
        }

    }
}
