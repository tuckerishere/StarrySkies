using System.Collections.Generic;
using AutoMapper;
using StarrySkies.Data.Models;
using StarrySkies.Data.Repositories.VocationRepo;
using StarrySkies.Services.DTOs.VocationDtos;
using StarrySkies.Services.ResponseModels;

namespace StarrySkies.Services.Services.Vocations
{
    public class VocationService : IVocationService
    {
        private readonly IVocationRepo _vocationRepo;
        private readonly IMapper _mapper;
        public VocationService(IVocationRepo vocationRepo, IMapper mapper)
        {
            _vocationRepo = vocationRepo;
            _mapper = mapper;
        }
        public ServiceResponse<VocationResponseDto> CreateVocation(CreateVocationDto vocation)
        {
            ServiceResponse<VocationResponseDto> createdVocation = new ServiceResponse<VocationResponseDto>();

            if (vocation != null
                && !string.IsNullOrEmpty(vocation?.Name))
            {
                var vocationToCreate = _mapper.Map<CreateVocationDto, Vocation>(vocation);
                _vocationRepo.CreateVocation(vocationToCreate);
                _vocationRepo.SaveChanges();
                createdVocation.Data = _mapper.Map<Vocation, VocationResponseDto>(vocationToCreate);
            }
            else
            {
                createdVocation.Success = false;
                createdVocation.Message = "Unable to create Vocation";
            }

            return createdVocation;
        }

        public ServiceResponse<VocationResponseDto> DeleteVocation(int id)
        {
            var vocation = _vocationRepo.GetVocationById(id);
            ServiceResponse<VocationResponseDto> deletedVocation = new ServiceResponse<VocationResponseDto>();

            if (vocation != null)
            {
                _vocationRepo.DeleteVocation(vocation);
                _vocationRepo.SaveChanges();
                deletedVocation.Data = _mapper.Map<Vocation, VocationResponseDto>(vocation);
            }
            else
            {
                deletedVocation.Success = false;
                deletedVocation.Message = "Unable to Delete Vocation";
            }

            return deletedVocation;
        }

        public ServiceResponse<VocationResponseDto> GetVocationById(int id)
        {
            var vocation = _vocationRepo.GetVocationById(id);
            ServiceResponse<VocationResponseDto> vocationToReturn = new ServiceResponse<VocationResponseDto>();
            if (vocation != null)
            {
                vocationToReturn.Data = _mapper.Map<Vocation, VocationResponseDto>(vocation);
            }

            return vocationToReturn;
        }

        public ServiceResponse<ICollection<VocationResponseDto>> GetVocations()
        {
            var serviceResponse = new ServiceResponse<ICollection<VocationResponseDto>>();
            var vocations = _vocationRepo.GetVocations();
            serviceResponse.Data = _mapper.Map<ICollection<Vocation>, ICollection<VocationResponseDto>>(vocations);
            return serviceResponse;
        }

        public ServiceResponse<VocationResponseDto> UpdateVocation(int id, CreateVocationDto vocation)
        {
            var vocationToUpdate = _vocationRepo.GetVocationById(id);
            ServiceResponse<VocationResponseDto> vocationToReturn = new ServiceResponse<VocationResponseDto>();
            if (vocationToUpdate != null
            && !string.IsNullOrEmpty(vocation?.Name))
            {
                vocationToUpdate.Name = vocation.Name;
                _vocationRepo.UpdateVocation(vocationToUpdate);
                _vocationRepo.SaveChanges();
                vocationToReturn.Data = _mapper.Map<Vocation, VocationResponseDto>(vocationToUpdate);
            }
            else
            {
                vocationToReturn.Success = false;
                vocationToReturn.Message = "Unable to update Vocation.";
            }

            return vocationToReturn;
        }
    }
}