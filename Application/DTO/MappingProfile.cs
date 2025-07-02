using Application.DTO.BankProductDto;
using AutoMapper;
using Domain.Entities.Banks;
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
            CreateMap<BankEntity, BankDto>();
            CreateMap<BankDto, BankEntity>();
        }
    }
}
