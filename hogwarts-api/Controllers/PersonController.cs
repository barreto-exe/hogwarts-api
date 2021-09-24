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
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ApiResponse response;
        public PersonController(IUnitOfWork unitOfWork, IMapper mapper, ApiResponse response)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.response = response;
        }

        [HttpGet]
        public async Task<IActionResult> GetPeople()
        {
            var people = await unitOfWork.PersonRepository.GetAll();
            var peopleDto = mapper.Map<IEnumerable<PersonDto>>(people);

            response.Data = peopleDto;
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson(string id)
        {
            var person = await unitOfWork.PersonRepository.GetById(id);
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
                await unitOfWork.PersonRepository.Add(person);
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.Message = "Inserción fallida. " + ex.InnerException.Message;
                return BadRequest(response);
            }

            personDto = mapper.Map<PersonDto>(person);
            response.Data = personDto;
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
                unitOfWork.PersonRepository.Update(person);
                await unitOfWork.SaveChangesAsync();

                personDto = mapper.Map<PersonDto>(person);
                response.Data = personDto;
                response.Message = "Actualización completada.";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Message = "Actualización fallida. " + ex.InnerException.Message;
                return BadRequest(response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(string id)
        {
            try
            {
                await unitOfWork.PersonRepository.Delete(id);
                await unitOfWork.SaveChangesAsync();

                response.Data = true;
                response.Message = "Borrado completado.";
                return Ok(response);
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
