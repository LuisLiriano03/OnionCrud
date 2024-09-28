using AutoMapper;
using OnionCrud.Application.Users.DTOs;
using OnionCrud.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionCrud.Application.Users.AutoMappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<User, GetUser>()
                .ForMember(destination =>
                    destination.IsActive,
                    options => options.MapFrom(origin => origin.IsActive == true ? 1 : 0)
                );

           CreateMap<GetUser, User>()
                .ForMember(destination =>
                    destination.IsActive,
                    options => options.MapFrom(origin => origin.IsActive == 1 ? true : false)
                );

            CreateMap<User, CreateUser>().ReverseMap();
            CreateMap<User, UpdateUser>().ReverseMap();

        }
    }
}
