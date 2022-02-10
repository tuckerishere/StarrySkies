using System.Collections.Generic;
using AutoMapper;
using StarrySkies.Data.Models;
using StarrySkies.Data.Repositories.SpellRepo;
using StarrySkies.Services.DTOs.SpellDtos;
using StarrySkies.Services.ResponseModels;

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
        public ServiceResponse<SpellResponseDto> CreateSpell(CreateSpellDto createSpell)
        {
            ServiceResponse<SpellResponseDto> spellToReturn = new ServiceResponse<SpellResponseDto>();
            if (!string.IsNullOrEmpty(createSpell.Name))
            {
                if (createSpell?.MPCost >= 0 && createSpell?.MPCost <= 99)
                {
                    Spell spellToCreate = _mapper.Map<CreateSpellDto, Spell>(createSpell);
                    _spellRepo.CreateSpell(spellToCreate);
                    _spellRepo.SaveChanges();
                    spellToReturn.Data = _mapper.Map<Spell, SpellResponseDto>(spellToCreate);
                }
            }
            else
            {
                spellToReturn.Success = false;
                spellToReturn.Message = "Unable to create Spell.";
            }

            return spellToReturn;
        }

        public ServiceResponse<SpellResponseDto> DeleteSpell(int id)
        {
            ServiceResponse<SpellResponseDto> deletedSpellReturn = new ServiceResponse<SpellResponseDto>();
            Spell spellToDelete = _spellRepo.GetSpell(id);
            if (spellToDelete != null)
            {
                _spellRepo.DeleteSpell(spellToDelete);
                _spellRepo.SaveChanges();
                deletedSpellReturn.Data = _mapper.Map<Spell, SpellResponseDto>(spellToDelete);
            }
            else
            {
                deletedSpellReturn.Success = false;
                deletedSpellReturn.Message = "Spell does not exist.";
            }

            return deletedSpellReturn;
        }

        public ServiceResponse<ICollection<SpellResponseDto>> GetAllSpells()
        {
            var serviceResponse = new ServiceResponse<ICollection<SpellResponseDto>>();
            var spellsToReturn = _spellRepo.GetSpells();
            serviceResponse.Data = _mapper.Map<ICollection<Spell>, ICollection<SpellResponseDto>>(spellsToReturn);

            return serviceResponse;
        }

        public ServiceResponse<SpellResponseDto> GetSpell(int id)
        {
            ServiceResponse<SpellResponseDto> spellToReturn = new ServiceResponse<SpellResponseDto>();
            var spell = _spellRepo.GetSpell(id);
            if (spell != null && spell?.Id != 0)
            {
                spellToReturn.Data = _mapper.Map<Spell, SpellResponseDto>(spell);
            }
            else
            {
                spellToReturn.Success = false;
                spellToReturn.Message = "Spell not found.";
            }

            return spellToReturn;
        }

        public ServiceResponse<SpellResponseDto> UpdateSpell(int id, CreateSpellDto updateSpell)
        {
            ServiceResponse<SpellResponseDto> spellToReturn = new ServiceResponse<SpellResponseDto>();
            Spell spell = _spellRepo.GetSpell(id);
            if (spell != null
                && !string.IsNullOrWhiteSpace(updateSpell.Name))
            {
                spell.Name = updateSpell.Name;
                spell.MpCost = updateSpell.MPCost;
                spell.Description = updateSpell.Description;
                spell.SpellTarget = updateSpell.SpellTarget;
                _spellRepo.UpdateSpell(spell);
                _spellRepo.SaveChanges();
                spellToReturn.Data = _mapper.Map<Spell, SpellResponseDto>(spell);
            }
            else
            {
                spellToReturn.Success = false;
                spellToReturn.Message = "Unable to update spell.";
            }

            return spellToReturn;
        }
    }
}