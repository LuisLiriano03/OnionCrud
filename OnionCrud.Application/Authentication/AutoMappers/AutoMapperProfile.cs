using AutoMapper;
using OnionCrud.Application.Authentication.DTOs;
using OnionCrud.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionCrud.Application.Authentication.AutoMappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<User, LoginRequest>().ReverseMap();
            CreateMap<User, LoginResponse>().ReverseMap();
        }
    }
}
