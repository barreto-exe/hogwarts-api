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
    public class HouseController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ApiResponse response;
        public HouseController(IUnitOfWork unitOfWork, IMapper mapper, ApiResponse response)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.response = response;
        }

        [HttpGet]
        public async Task<IActionResult> GetHouses()
        {
            var houses = await unitOfWork.HouseRepository.GetAll();
            var housesDto = mapper.Map<IEnumerable<HouseDto>>(houses);

            response.Data = housesDto;
            return Ok(response);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetHouse(string name)
        {
            var house = await unitOfWork.HouseRepository.GetById(name);
            if (house == null)
            {
                response.Message = "La casa no fue encontrada.";
                return BadRequest(response);
            }

            var houseDto = mapper.Map<HouseDto>(house);
            response.Data = houseDto;
            response.Message = "Casa encontrada.";
            return Ok(response);
        }

        public async Task<IActionResult> InsertHouse(HouseDto houseDto)
        {
            var house = mapper.Map<House>(houseDto);

            try
            {
                await unitOfWork.HouseRepository.Add(house);
                await unitOfWork.SaveChangesAsync();
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

        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteHouse(string name)
        {
            try
            {
                await unitOfWork.HouseRepository.Delete(name);
                await unitOfWork.SaveChangesAsync();

                response.Data = false;
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
