using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using hogwarts_core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hogwarts_core.Entities;
using AutoMapper;
using hogwarts_core.DTOs;
using hogwarts_api.Responses;

namespace hogwarts_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IRepository<Application> applicationRepository;
        private readonly IMapper mapper;
        private readonly ApiResponse response;

        public ApplicationController(IRepository<Application> applicationRepository, IMapper mapper, ApiResponse response)
        {
            this.applicationRepository = applicationRepository;
            this.mapper = mapper;
            this.response = response;
        }

        [HttpGet]
        public async Task<IActionResult> GetApplications()
        {
            var applications = await applicationRepository.GetAll();
            var applicationsDto = mapper.Map<IEnumerable<ApplicationDto>>(applications);

            response.Data = applicationsDto;
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetApplication(int id)
        {
            var application = await applicationRepository.GetById(id);
            if (application == null)
            {
                response.Message = "La solicitud no fue encontrada.";
                return BadRequest(response);
            }

            var applicationDto = mapper.Map<ApplicationDto>(application);
            response.Data = applicationDto;
            response.Message = "Solicitud encontrada.";
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> InsertApplication(ApplicationDto applicationDto)
        {
            var application = mapper.Map<Application>(applicationDto);
            try
            {
                await applicationRepository.Add(application);
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

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateApplication(int id, ApplicationDto applicationDto)
        {
            var application = mapper.Map<Application>(applicationDto);
            application.ApplicationId = id; //Forzar que Dto tenga el mismo id
            try
            {
                bool updated = await applicationRepository.Update(application);

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

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            try
            {
                bool deleted = await applicationRepository.Delete(id);
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
