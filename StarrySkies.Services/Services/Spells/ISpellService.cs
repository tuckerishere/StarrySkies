using System.Collections.Generic;
using StarrySkies.Services.DTOs.SpellDtos;
using StarrySkies.Services.ResponseModels;

namespace StarrySkies.Services.Services.Spells
{
    public interface ISpellService
    {
        ServiceResponse<ICollection<SpellResponseDto>> GetAllSpells();
        ServiceResponse<SpellResponseDto> GetSpell(int id);
        ServiceResponse<SpellResponseDto> CreateSpell(CreateSpellDto createSpell);
        ServiceResponse<SpellResponseDto> DeleteSpell(int id);
        ServiceResponse<SpellResponseDto> UpdateSpell(int id, CreateSpellDto updateSpell);

    }
}