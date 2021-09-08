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

        public PersonController(IPersonRepository repository)
        {
            personRepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPeople()
        {
            var people = await personRepository.GetPeople();
            return Ok(people);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson(string id)
        {
            var person = await personRepository.GetPerson(id);
            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> InsertPerson(Person person)
        {
            await personRepository.InsertPerson(person);
            return Ok();
        }
    }
}
