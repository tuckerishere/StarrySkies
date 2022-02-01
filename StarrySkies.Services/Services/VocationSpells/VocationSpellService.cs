using System.Collections.Generic;
using AutoMapper;
using StarrySkies.Data.Models;
using StarrySkies.Data.Repositories.SpellRepo;
using StarrySkies.Data.Repositories.VocationRepo;
using StarrySkies.Data.Repositories.VocationSpellRepo;
using StarrySkies.Services.DTOs.VocationSpellDtos;

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

        public VocationSpellResponseDto GetVocationSpell(int vocationId, int spellId)
        {
            VocationSpellResponseDto vocationSpellReturn = new VocationSpellResponseDto();
            VocationSpell vocationSpell = _vocationSpellRepo.GetVocationSpell(vocationId, spellId);

            if(vocationSpell != null && vocationSpell?.VocationId != 0 && vocationSpell?.SpellId != 0)
            {
                vocationSpellReturn = _mapper.Map<VocationSpell, VocationSpellResponseDto>(vocationSpell);
            }

            return vocationSpellReturn;
        }

        public ICollection<VocationSpellResponseDto> GetVocationSpells()
        {
            ICollection<VocationSpell> vocationSpells = _vocationSpellRepo.GetVocationSpells();
            return _mapper.Map<ICollection<VocationSpell>, ICollection<VocationSpellResponseDto>>(vocationSpells); 
        }

        public VocationSpellResponseDto CreateVocationSpell(VocationSpellResponseDto createSpell)
        {
            VocationSpellResponseDto vsToReturn = new VocationSpellResponseDto();
            VocationSpell vocationSpell = VocationSpellSetValue(createSpell);
            bool vocationSpellExists = VocationSpellExists(createSpell);

            if(vocationSpell?.VocationId != 0  && vocationSpell?.SpellId != 0 && !vocationSpellExists)
            {
                _vocationSpellRepo.CreateVocationSpell(vocationSpell);
                _vocationSpellRepo.SaveChanges();
                vsToReturn = _mapper.Map<VocationSpell, VocationSpellResponseDto>(vocationSpell);
            }

            return vsToReturn;
        }

        public VocationSpellResponseDto UpdateVocationSpell(int vocationId, int spellId, 
            VocationSpellResponseDto updatedVocationSpell)
        {
            var vsToReturn = new VocationSpellResponseDto();
            var vocationSpellToUpdate = VocationToUpdate(vocationId, spellId);
            if(vocationSpellToUpdate.SpellId != 0 && vocationSpellToUpdate.VocationId != 0
                && !VocationSpellExists(updatedVocationSpell))
            {
                    var vocationSpell = VocationSpellSetValue(updatedVocationSpell);
                    _vocationSpellRepo.UpdateVocationSpell(vocationSpell);
                    _vocationSpellRepo.SaveChanges();
                    vsToReturn = _mapper.Map<VocationSpell, VocationSpellResponseDto>(vocationSpell);
            }

            return vsToReturn;
        }

        private VocationSpell VocationSpellSetValue(VocationSpellResponseDto createSpell)
        {
            VocationSpell vocationSpell = new VocationSpell();
            Vocation vocation = _vocationRepo.GetVocationById(createSpell.VocationId);
            Spell spell = _spellRepo.GetSpell(createSpell.SpellId);

            if(vocation != null && spell != null)
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
            
            if(vocationSpell != null)
            {
                vocationSpellExists = true;
            }

            return vocationSpellExists;
        }   

        private VocationSpellResponseDto VocationToUpdate(int vocationId, int spellId)
        {
            VocationSpellResponseDto vocationSpellReturn = new VocationSpellResponseDto();
            VocationSpell vocationSpell = _vocationSpellRepo.GetVocationSpell(vocationId, spellId);

            if(vocationSpell != null)
            {
                vocationSpellReturn = _mapper.Map<VocationSpell, VocationSpellResponseDto>(vocationSpell);
            }

            return vocationSpellReturn;
        }
    }
}