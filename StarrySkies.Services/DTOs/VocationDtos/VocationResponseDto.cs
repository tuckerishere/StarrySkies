using System.Collections.Generic;
using StarrySkies.Services.DTOs.SpellDtos;

namespace StarrySkies.Services.DTOs.VocationDtos
{
    public class VocationResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }   
        public List<SpellResponseDto> Spells { get; set; }
        
         
    }
}