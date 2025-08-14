using Application.DTO.BankProductDto;
using Application.DTO.PersonDto;
using AutoMapper;
using Domain.Entities.Banks;
using Domain.Entities.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            //Banks
            CreateMap<BankEntity, BankDto>();
            CreateMap<BankDto, BankEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            //CardTariffs
            CreateMap<CardTariffsEntity, CardTariffsDto>();
            CreateMap<CardTariffsDto, CardTariffsEntity>()
                .ForMember(dest => dest.Id, opt=> opt.Ignore());

            //Users
            CreateMap<UserEntity, UserDto>();
            CreateMap<UserDto, UserEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
