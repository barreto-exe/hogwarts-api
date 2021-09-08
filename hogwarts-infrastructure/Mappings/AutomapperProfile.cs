using AutoMapper;
using hogwarts_core.DTOs;
using hogwarts_core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace hogwarts_infrastructure.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Person, PersonDto>();
            CreateMap<PersonDto, Person>();

            CreateMap<House, HouseDto>();
            CreateMap<HouseDto, House>();

            CreateMap<Application, ApplicationDto>();
            CreateMap<ApplicationDto, Application>();
        }
    }
}
