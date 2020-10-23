using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PlanosAPI.Models;
using PlanosAPI.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlanosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DDDController : ControllerBase
    {
        private readonly IDDDRepository _dddRepository;

        public DDDController(IDDDRepository dddRepository)
        {
            _dddRepository = dddRepository;
        }

        // GET: api/<DDDController>
        [HttpGet]
        public IEnumerable<DDD> GetAll()
        {
            var _data = _dddRepository.GetAll();
            return _data;
        }

    }
}
