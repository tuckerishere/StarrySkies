using System.Collections.Generic;
using StarrySkies.Services.DTOs.SpellDtos;

namespace StarrySkies.Services.Services.Spells
{
    public interface ISpellService
    {
        ICollection<SpellResponseDto> GetAllSpells();
        SpellResponseDto GetSpell(int id);
        SpellResponseDto CreateSpell(CreateSpellDto createSpell);
        SpellResponseDto DeleteSpell(int id);
        SpellResponseDto UpdateSpell(int id, CreateSpellDto updateSpell);

    }
}