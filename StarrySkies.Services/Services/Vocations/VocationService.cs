using System.Collections.Generic;
using AutoMapper;
using StarrySkies.Data.Models;
using StarrySkies.Data.Repositories.VocationRepo;
using StarrySkies.Services.DTOs.VocationDtos;

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
        public VocationResponseDto CreateVocation(CreateVocationDto vocation)
        {
            VocationResponseDto createdVocation = new VocationResponseDto();

            if(vocation != null
                && !string.IsNullOrEmpty(vocation?.Name)){
                var vocationToCreate = _mapper.Map<CreateVocationDto, Vocation>(vocation);
                _vocationRepo.CreateVocation(vocationToCreate);
                _vocationRepo.SaveChanges();
                createdVocation = _mapper.Map<Vocation, VocationResponseDto>(vocationToCreate);
            }

            return createdVocation;
        }

        public VocationResponseDto DeleteVocation(int id)
        {
            var vocation = _vocationRepo.GetVocationById(id);
            VocationResponseDto deletedVocation = new VocationResponseDto();
            
            if(vocation != null && vocation?.Id != 0)
            {
                _vocationRepo.DeleteVocation(vocation);
                _vocationRepo.SaveChanges();
                deletedVocation = _mapper.Map<Vocation, VocationResponseDto>(vocation);
            }

            return deletedVocation;
        }

        public VocationResponseDto GetVocationById(int id)
        {
            var vocation = _vocationRepo.GetVocationById(id);
            VocationResponseDto vocationToReturn = new VocationResponseDto();
            if(vocation != null && vocation?.Id != 0)
            {
                vocationToReturn = _mapper.Map<Vocation, VocationResponseDto>(vocation);
            }

            return vocationToReturn;
        }

        public ICollection<VocationResponseDto> GetVocations()
        {
            var vocations = _vocationRepo.GetVocations();
            return _mapper.Map<ICollection<Vocation>, ICollection<VocationResponseDto>>(vocations);
        }

        public VocationResponseDto UpdateVocation(int id, CreateVocationDto vocation)
        {
            var vocationToUpdate = _vocationRepo.GetVocationById(id);
            VocationResponseDto vocationToReturn = new VocationResponseDto();
            if(vocationToUpdate != null 
            && vocationToUpdate?.Id != 0
            && !string.IsNullOrEmpty(vocation?.Name)){
                vocationToUpdate.Name = vocation.Name;
                _vocationRepo.UpdateVocation(vocationToUpdate);
                _vocationRepo.SaveChanges();
                vocationToReturn = _mapper.Map<Vocation, VocationResponseDto>(vocationToUpdate);
            }

            return vocationToReturn;
        }
    }
}