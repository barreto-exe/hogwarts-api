using AutoMapper;
using hogwarts_api.Responses;
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
        private readonly ApiResponse response;

        public PersonController(IPersonRepository personRepository, IMapper mapper, ApiResponse response)
        {
            this.personRepository = personRepository;
            this.mapper = mapper;
            this.response = response;
        }

        [HttpGet]
        public async Task<IActionResult> GetPeople()
        {
            var people = await personRepository.GetPeople();
            var peopleDto = mapper.Map<IEnumerable<PersonDto>>(people);

            response.Data = peopleDto;
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson(string id)
        {
            var person = await personRepository.GetPerson(id);
            if (person == null)
            {
                response.Message = "La persona no fue encontrada.";
                return BadRequest(response);
            }

            var personDto = mapper.Map<PersonDto>(person);
            response.Data = personDto;
            response.Message = "Persona encontrada.";
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> InsertPerson(PersonDto personDto)
        {
            var person = mapper.Map<Person>(personDto);

            try
            {
                await personRepository.InsertPerson(person);
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.Message = "Inserción fallida. " + ex.InnerException.Message;
                return BadRequest(response);
            }

            response.Data = true;
            response.Message = "Inserción completada.";
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(string id, PersonDto personDto)
        {
            var person = mapper.Map<Person>(personDto);
            person.PersonId = id; //Forzar que Dto tenga el mismo id
            try
            {
                bool updated = await personRepository.UpdatePerson(person);

                string message;
                dynamic httpResult; //Variable de respuesta http

                if (updated)
                {
                    message = "Actualización completada.";
                    httpResult = Ok(response);
                }
                else
                {
                    message = "Actualización fallida. No hubo cambios, o el id no existe.";
                    httpResult = BadRequest(response);
                }

                response.Message = message;
                response.Data = updated;
                return httpResult;
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.Message = "Actualización fallida. " + ex.InnerException.Message;
                return BadRequest(response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(string id)
        {
            try
            {
                bool deleted = await personRepository.DeletePerson(id);
                string message;
                dynamic httpResult; //Variable de respuesta http

                if (deleted)
                {
                    message = "Borrado completado.";
                    httpResult = Ok(response);
                }
                else
                {
                    message = "Borrado fallido. El id no existe.";
                    httpResult = BadRequest(response);
                }

                response.Message = message;
                response.Data = deleted;
                return httpResult;
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.Message = "Borrado fallido. " + ex.InnerException.Message;
                return BadRequest(response);
            }
        }
    }
}
