using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PlanosAPI.Models;
using PlanosAPI.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlanosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoPlanoController : ControllerBase
    {
        private readonly ITipoPlanoRepository _tipoplanoRepository;

        public TipoPlanoController(ITipoPlanoRepository tipoplanoRepository)
        {
            _tipoplanoRepository = tipoplanoRepository;
        }

        // GET: api/<TipoPlanoController>
        [HttpGet]
        public IEnumerable<TipoPlano> GetAll()
        {
            var _data = _tipoplanoRepository.GetAll();
            return _data;
        }

    }
}
