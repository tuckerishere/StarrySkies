using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StarrySkies.Data.Models;
using StarrySkies.Services.DTOs;
using StarrySkies.Services.DTOs.SpellDtos;
using StarrySkies.Services.DTOs.VocationDtos;
using StarrySkies.Services.DTOs.WeaponCategoryDTOs;

namespace StarrySkies.Services.Mapping
{
    public class ModelToDto : Profile
    {
        public ModelToDto()
        {
            CreateMap<Location, LocationResponseDto>();
            CreateMap<WeaponCategory, WeaponCategoryResponseDto>();
            CreateMap<Vocation, VocationResponseDto>();
            CreateMap<Spell, SpellResponseDto>();
        }
    }
}