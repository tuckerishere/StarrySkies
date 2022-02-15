using System.Collections.Generic;
using AutoMapper;
using StarrySkies.Data.Models;
using StarrySkies.Data.Repositories.SpellRepo;
using StarrySkies.Data.Repositories.VocationRepo;
using StarrySkies.Data.Repositories.VocationSpellRepo;
using StarrySkies.Services.DTOs.VocationSpellDtos;
using StarrySkies.Services.ResponseModels;

namespace StarrySkies.Services.Services.VocationSpells
{
    public class VocationSpellService : IVocationSpellService
    {
        private readonly IVocationSpellRepo _vocationSpellRepo;
        private readonly IVocationRepo _vocationRepo;
        private readonly ISpellRepo _spellRepo;
        private readonly IMapper _mapper;
        public VocationSpellService(IVocationSpellRepo vocationSpellRepo,
            IVocationRepo vocationRepo,
            ISpellRepo spellRepo,
            IMapper mapper)
        {
            _vocationSpellRepo = vocationSpellRepo;
            _vocationRepo = vocationRepo;
            _spellRepo = spellRepo;
            _mapper = mapper;
        }

        public ServiceResponse<VocationSpellResponseDto> GetVocationSpell(int vocationId, int spellId)
        {
            ServiceResponse<VocationSpellResponseDto> vocationSpellReturn = new ServiceResponse<VocationSpellResponseDto>();
            VocationSpell vocationSpell = _vocationSpellRepo.GetVocationSpell(vocationId, spellId);

            if (vocationSpell != null)
            {
                vocationSpellReturn.Data = _mapper.Map<VocationSpell, VocationSpellResponseDto>(vocationSpell);
            }
            else
            {
                vocationSpellReturn.Success = false;
                vocationSpellReturn.Message = "VocationSpell not found.";
            }

            return vocationSpellReturn;
        }

        public ServiceResponse<ICollection<VocationSpellResponseDto>> GetVocationSpells()
        {
            ServiceResponse<ICollection<VocationSpellResponseDto>> vsToReturn = new ServiceResponse<ICollection<VocationSpellResponseDto>>();
            var vocationSpells = _vocationSpellRepo.GetVocationSpells();
            vsToReturn.Data = _mapper.Map<ICollection<VocationSpell>, ICollection<VocationSpellResponseDto>>(vocationSpells);

            return vsToReturn;
        }

        public ServiceResponse<VocationSpellResponseDto> CreateVocationSpell(VocationSpellResponseDto createSpell)
        {
            ServiceResponse<VocationSpellResponseDto> vsToReturn = new ServiceResponse<VocationSpellResponseDto>();
            VocationSpell vocationSpell = VocationSpellSetValue(createSpell);
            bool vocationSpellExists = VocationSpellExists(createSpell);

            if (vocationSpell?.VocationId != 0 && vocationSpell?.SpellId != 0 && !vocationSpellExists)
            {
                _vocationSpellRepo.CreateVocationSpell(vocationSpell);
                _vocationSpellRepo.SaveChanges();
                vsToReturn.Data = _mapper.Map<VocationSpell, VocationSpellResponseDto>(vocationSpell);
            }
            else
            {
                vsToReturn.Success = false;
                vsToReturn.Message = "Unable to add VocationSpell.";
            }

            return vsToReturn;
        }

        public ServiceResponse<VocationSpellResponseDto> UpdateVocationSpell(int vocationId, int spellId,
            VocationSpellResponseDto updatedVocationSpell)
        {
            var vsToReturn = new ServiceResponse<VocationSpellResponseDto>();
            var vocationSpellToUpdate = GetVocationByIds(vocationId, spellId);
            if (vocationSpellToUpdate != null
            && vocationSpellToUpdate.SpellId != 0
            && vocationSpellToUpdate.VocationId != 0
            && !VocationSpellExists(updatedVocationSpell))
            {
                var vocationSpell = VocationSpellSetValue(updatedVocationSpell);
                _vocationSpellRepo.UpdateVocationSpell(vocationSpell);
                _vocationSpellRepo.SaveChanges();
                vsToReturn.Data = _mapper.Map<VocationSpell, VocationSpellResponseDto>(vocationSpell);
            }
            else
            {
                vsToReturn.Success = false;
                vsToReturn.Message = "Unable to update Vocation Spell.";
            }

            return vsToReturn;
        }

        public ServiceResponse<VocationSpellResponseDto> DeleteVocationSpell(int vocationId, int spellId)
        {
            ServiceResponse<VocationSpellResponseDto> vsToReturn = new ServiceResponse<VocationSpellResponseDto>();
            var deleteSpell = GetVocationByIds(vocationId, spellId);
            if (VocationSpellExists(deleteSpell))
            {
                VocationSpell vocationSpell = _mapper.Map<VocationSpellResponseDto, VocationSpell>(deleteSpell);
                _vocationSpellRepo.DeleteVocationSpell(vocationSpell);
                _vocationSpellRepo.SaveChanges();
                vsToReturn.Data = _mapper.Map<VocationSpell, VocationSpellResponseDto>(vocationSpell);
            }

            return vsToReturn;
        }

        private VocationSpell VocationSpellSetValue(VocationSpellResponseDto createSpell)
        {
            VocationSpell vocationSpell = new VocationSpell();
            Vocation vocation = _vocationRepo.GetVocationById(createSpell.VocationId);
            Spell spell = _spellRepo.GetSpell(createSpell.SpellId);

            if (vocation != null && spell != null)
            {
                vocationSpell = _mapper.Map<VocationSpellResponseDto, VocationSpell>(createSpell);
                vocationSpell.Spell = spell;
                vocationSpell.Vocation = vocation;
            }

            return vocationSpell;
        }

        private bool VocationSpellExists(VocationSpellResponseDto createSpell)
        {
            bool vocationSpellExists = false;

            VocationSpell vocationSpell = _vocationSpellRepo.GetVocationSpell(createSpell.VocationId, createSpell.SpellId);

            if (vocationSpell != null)
            {
                vocationSpellExists = true;
            }

            return vocationSpellExists;
        }

        private VocationSpellResponseDto GetVocationByIds(int vocationId, int spellId)
        {
            VocationSpellResponseDto vocationSpellReturn = new VocationSpellResponseDto();
            VocationSpell vocationSpell = _vocationSpellRepo.GetVocationSpell(vocationId, spellId);

            if (vocationSpell != null)
            {
                vocationSpellReturn = _mapper.Map<VocationSpell, VocationSpellResponseDto>(vocationSpell);
            }

            return vocationSpellReturn;
        }
    }
}