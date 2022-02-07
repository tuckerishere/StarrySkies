using System.Collections.Generic;
using StarrySkies.Services.DTOs.VocationSpellDtos;

namespace StarrySkies.Services.Services.VocationSpells
{
    public interface IVocationSpellService
    {
        VocationSpellResponseDto GetVocationSpell(int vocationId, int spellId);
        VocationSpellResponseDto CreateVocationSpell(VocationSpellResponseDto createdVocationSpell);
        ICollection<VocationSpellResponseDto> GetVocationSpells();
        VocationSpellResponseDto DeleteVocationSpell(int vocationId, int spellId);
        VocationSpellResponseDto UpdateVocationSpell(int vocationId, int spellId, VocationSpellResponseDto updateVocationSpell);
    }
}