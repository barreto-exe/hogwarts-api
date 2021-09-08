using hogwarts_core.Entities;
using hogwarts_core.Interfaces;
using hogwarts_infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hogwarts_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        private readonly IHouseRepository houseRepository;

        public HouseController(IHouseRepository repository)
        {
            houseRepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetHouses()
        {
            var house = await houseRepository.GetHouses();
            return Ok(house);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetHouse(string name)
        {
            var house = await houseRepository.GetHouse(name);
            return Ok(house);
        }
    }
}
