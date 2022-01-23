using System.Collections.Generic;
using AutoMapper;
using StarrySkies.Data.Models;
using StarrySkies.Data.Repositories.SpellRepo;
using StarrySkies.Services.DTOs.SpellDtos;

namespace StarrySkies.Services.Services.Spells
{
    public class SpellService : ISpellService
    {
        private readonly IMapper _mapper;
        private readonly ISpellRepo _spellRepo;
        public SpellService(ISpellRepo spellRepo, IMapper mapper)
        {
            _spellRepo = spellRepo;
            _mapper = mapper;
        }
        public SpellResponseDto CreateSpell(CreateSpellDto createSpell)
        {
            SpellResponseDto spellToReturn = new SpellResponseDto();
            if(!string.IsNullOrEmpty(createSpell.Name)){
                if (createSpell?.MPCost >= 0 && createSpell?.MPCost <= 99)
                {
                    Spell spellToCreate = _mapper.Map<CreateSpellDto, Spell>(createSpell);
                    _spellRepo.CreateSpell(spellToCreate);
                    _spellRepo.SaveChanges();
                    spellToReturn = _mapper.Map<Spell, SpellResponseDto>(spellToCreate);
                }
            }

            return spellToReturn;
        }

        public SpellResponseDto DeleteSpell(int id)
        {
            SpellResponseDto deletedSpellReturn = new SpellResponseDto();
            Spell spellToDelete = _spellRepo.GetSpell(id);
            if(spellToDelete != null && spellToDelete?.Id != 0)
            {
                _spellRepo.DeleteSpell(spellToDelete);
                _spellRepo.SaveChanges();
                deletedSpellReturn = _mapper.Map<Spell, SpellResponseDto>(spellToDelete);
            }

            return deletedSpellReturn;
        }

        public ICollection<SpellResponseDto> GetAllSpells()
        {
            var spellsToReturn = _spellRepo.GetSpells();
            return _mapper.Map<ICollection<Spell>, ICollection<SpellResponseDto>>(spellsToReturn);

        }

        public SpellResponseDto GetSpell(int id)
        {
            SpellResponseDto spellToReturn = new SpellResponseDto();
            var spell = _spellRepo.GetSpell(id);
            if(spell != null && spell?.Id !=0)
            {
                spellToReturn = _mapper.Map<Spell, SpellResponseDto>(spell);
            }

            return spellToReturn;
        }

        public SpellResponseDto UpdateSpell(int id, CreateSpellDto updateSpell)
        {
            SpellResponseDto spellToReturn = new SpellResponseDto();
            Spell spell = _spellRepo.GetSpell(id);
            if(spell != null 
                && spell.Id != 0
                && !string.IsNullOrWhiteSpace(updateSpell.Name))
            {
                spell.Name = updateSpell.Name;
                spell.MpCost = updateSpell.MPCost;
                spell.Description = updateSpell.Description;
                spell.SpellTarget = updateSpell.SpellTarget;
                _spellRepo.UpdateSpell(spell);
                _spellRepo.SaveChanges();
                spellToReturn = _mapper.Map<Spell, SpellResponseDto>(spell);
            }

            return spellToReturn;
        }
    }
}