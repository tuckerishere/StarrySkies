using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StarrySkies.Data.Models;
using StarrySkies.Services.DTOs;

namespace StarrySkies.Services.Mapping
{
    public class ModelToDto : Profile
    {
        public ModelToDto()
        {
            CreateMap<Location, LocationResponseDto>();
        }
    }
}