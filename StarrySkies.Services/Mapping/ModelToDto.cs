using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StarrySkies.Data.Models;
using StarrySkies.Services.DTOs;
using StarrySkies.Services.DTOs.SpellDtos;
using StarrySkies.Services.DTOs.VocationDtos;
using StarrySkies.Services.DTOs.VocationSpellDtos;
using StarrySkies.Services.DTOs.WeaponCategoryDTOs;

namespace StarrySkies.Services.Mapping
{
    public class ModelToDto : Profile
    {
        public ModelToDto()
        {
            CreateMap<Location, LocationResponseDto>();
            CreateMap<WeaponCategory, WeaponCategoryResponseDto>();
            CreateMap<Vocation, VocationResponseDto>().ForMember(dto=>dto.Spells, v=>v.MapFrom(v=>v.VocationSpells.Select(s=>s.Spell)));
            CreateMap<Spell, SpellResponseDto>().ForMember(dto=>dto.Vocations, vs=>vs.MapFrom(vs =>vs.VocationsSpells.Select(v=>v.Vocation)));
            CreateMap<VocationSpell, VocationSpellResponseDto>();
            CreateMap<Vocation, GetVocation>();
        }
    }
}