using System.Collections.Generic;
using StarrySkies.Services.DTOs.VocationDtos;

namespace StarrySkies.Services.Services.Vocations
{
    public interface IVocationService
    {
        ICollection<VocationResponseDto> GetVocations();
        VocationResponseDto GetVocationById(int id);
        VocationResponseDto DeleteVocation(int id);
        VocationResponseDto CreateVocation(CreateVocationDto vocation);
        VocationResponseDto UpdateVocation(int id, CreateVocationDto vocation);
    }
}