using AutoMapper;
using hogwarts_core.DTOs;
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
        private readonly IMapper mapper;

        public HouseController(IHouseRepository houseRepository, IMapper mapper)
        {
            this.houseRepository = houseRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetHouses()
        {
            var houses = await houseRepository.GetHouses();
            var housesDto = mapper.Map<IEnumerable<HouseDto>>(houses);
            return Ok(housesDto);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetHouse(string name)
        {
            var house = await houseRepository.GetHouse(name);
            var houseDto = mapper.Map<HouseDto>(house);

            return Ok(houseDto);
        }
    }
}
