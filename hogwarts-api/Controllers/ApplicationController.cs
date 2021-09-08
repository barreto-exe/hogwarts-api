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

namespace hogwarts_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationRepository applicationRepository;
        private readonly IMapper mapper;

        public ApplicationController(IApplicationRepository applicationRepository, IMapper mapper)
        {
            this.applicationRepository = applicationRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetApplications()
        {
            var applications = await applicationRepository.GetApplications();
            var applicationsDto = mapper.Map<IEnumerable<ApplicationDto>>(applications);

            return Ok(applicationsDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetApplication(int id)
        {
            var application = await applicationRepository.GetApplication(id);
            var applicationDto = mapper.Map<ApplicationDto>(application);

            return Ok(applicationDto);
        }

        [HttpPost]
        public async Task<IActionResult> InsertApplication(ApplicationDto applicationDto)
        {
            var application = mapper.Map<Application>(applicationDto);
            await applicationRepository.InsertApplication(application);

            return Ok();
        }
    }
}
