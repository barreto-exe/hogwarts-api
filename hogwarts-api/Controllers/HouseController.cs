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
        private readonly IHouseRepository houseRepository;
        private readonly IMapper mapper;
        private readonly ApiResponse response;

        public HouseController(IHouseRepository houseRepository, IMapper mapper, ApiResponse response)
        {
            this.houseRepository = houseRepository;
            this.mapper = mapper;
            this.response = response;
        }

        [HttpGet]
        public async Task<IActionResult> GetHouses()
        {
            var houses = await houseRepository.GetHouses();
            var housesDto = mapper.Map<IEnumerable<HouseDto>>(houses);

            response.Data = housesDto;
            return Ok(response);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetHouse(string name)
        {
            var house = await houseRepository.GetHouse(name);
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
                await houseRepository.InsertHouse(house);
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
                bool deleted = await houseRepository.DeleteHouse(name);
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
