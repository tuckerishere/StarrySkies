using System.Collections.Generic;
using StarrySkies.Services.DTOs.VocationDtos;
using StarrySkies.Services.ResponseModels;

namespace StarrySkies.Services.Services.Vocations
{
    public interface IVocationService
    {
        ServiceResponse<ICollection<VocationResponseDto>> GetVocations();
        ServiceResponse<VocationResponseDto> GetVocationById(int id);
        ServiceResponse<VocationResponseDto> DeleteVocation(int id);
        ServiceResponse<VocationResponseDto> CreateVocation(CreateVocationDto vocation);
        ServiceResponse<VocationResponseDto> UpdateVocation(int id, CreateVocationDto vocation);
    }
}