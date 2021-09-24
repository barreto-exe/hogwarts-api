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
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ApiResponse response;
        public ApplicationController(IUnitOfWork unitOfWork, IMapper mapper, ApiResponse response)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.response = response;
        }

        [HttpGet]
        public async Task<IActionResult> GetApplications()
        {
            var applications = await unitOfWork.ApplicationRepository.GetAll();
            var applicationsDto = mapper.Map<IEnumerable<ApplicationDto>>(applications);
            response.Data = applicationsDto;
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetApplication(int id)
        {
            var application = await unitOfWork.ApplicationRepository.GetById(id);
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
                await unitOfWork.ApplicationRepository.Add(application);
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.Message = "Inserción fallida. " + ex.InnerException.Message;
                return BadRequest(response);
            }

            applicationDto = mapper.Map<ApplicationDto>(application);
            response.Data = applicationDto;
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
                unitOfWork.ApplicationRepository.Update(application);
                await unitOfWork.SaveChangesAsync();

                applicationDto = mapper.Map<ApplicationDto>(application);
                response.Data = applicationDto;
                response.Message = "Actualización completada.";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Message = "Actualización fallida. " + ex.InnerException.Message;
                return BadRequest(response);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            try
            {
                await unitOfWork.ApplicationRepository.Delete(id);
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
