using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBContext.Services;
using DBContext.Models;

namespace Space_Truckers.Controllers
{
    [Route("Planet")]
    [ApiController]
    public class PlanetController : ControllerBase
    {
        private PlanetService _service;

        public PlanetController(IServiceProvider serviceProvider)
        {
            _service = new PlanetService(serviceProvider);
        }

        [HttpGet]
        public List<Planet> Get()
        {
            return _service.GetAllPlanets();
        }

        [HttpGet("{id}")]
        public Planet Get(int id)
        {
            return _service.GetPlanetById(id);
        }

        [HttpPost]
        public void Post([FromBody] Planet planet)
        {
            _service.SaveNewPlanet(planet);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Planet planet)
        {
            planet.PlanetId = id;
            _service.UpdatePlanet(planet);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _service.DeletePlanet(id);
        }

        [HttpDelete]
        public void DeleteAll()
        {
            _service.DeleteAllPlanets();
        }

    }
}
