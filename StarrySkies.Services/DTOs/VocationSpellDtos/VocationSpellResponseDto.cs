using StarrySkies.Data.Models;

namespace StarrySkies.Services.DTOs.VocationSpellDtos
{
    public class VocationSpellResponseDto
    {
        public int VocationId { get; set; }     
        public int SpellId { get; set; }
        public int LevelLearned { get; set; }
    }
}