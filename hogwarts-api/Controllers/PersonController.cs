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
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository personRepository;
        private readonly IMapper mapper;

        public PersonController(IPersonRepository personRepository, IMapper mapper)
        {
            this.personRepository = personRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPeople()
        {
            var people = await personRepository.GetPeople();
            var peopleDto = mapper.Map<IEnumerable<PersonDto>>(people);

            return Ok(peopleDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson(string id)
        {
            var person = await personRepository.GetPerson(id);
            var personDto = mapper.Map<PersonDto>(person);

            return Ok(personDto);
        }

        [HttpPost]
        public async Task<IActionResult> InsertPerson(PersonDto personDto)
        {
            var person = mapper.Map<Person>(personDto);
            await personRepository.InsertPerson(person);

            return Ok();
        }
    }
}
