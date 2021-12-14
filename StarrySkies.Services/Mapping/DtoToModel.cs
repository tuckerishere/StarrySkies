using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StarrySkies.Data.Models;
using StarrySkies.Services.DTOs;
using StarrySkies.Services.DTOs.WeaponCategoryDTOs;

namespace StarrySkies.Services.Mapping
{
    public class DtoToModel : Profile
    {
        public DtoToModel()
        {
            CreateMap<CreateLocationDto, Location>();
            CreateMap<RequestLocationDto, Location>();
            CreateMap<CreateWeaponCategoryDto, WeaponCategory>();
            CreateMap<WeaponCategoryResponseDto, WeaponCategory>();
        }
    }
}