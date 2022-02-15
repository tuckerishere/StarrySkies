using System.Collections.Generic;
using StarrySkies.Services.DTOs.VocationSpellDtos;
using StarrySkies.Services.ResponseModels;

namespace StarrySkies.Services.Services.VocationSpells
{
    public interface IVocationSpellService
    {
        ServiceResponse<VocationSpellResponseDto> GetVocationSpell(int vocationId, int spellId);
        ServiceResponse<VocationSpellResponseDto> CreateVocationSpell(VocationSpellResponseDto createdVocationSpell);
        ServiceResponse<ICollection<VocationSpellResponseDto>> GetVocationSpells();
        ServiceResponse<VocationSpellResponseDto> DeleteVocationSpell(int vocationId, int spellId);
        ServiceResponse<VocationSpellResponseDto> UpdateVocationSpell(int vocationId, int spellId, VocationSpellResponseDto updateVocationSpell);
    }
}