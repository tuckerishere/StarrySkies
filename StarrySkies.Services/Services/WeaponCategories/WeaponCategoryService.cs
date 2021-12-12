using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StarrySkies.Data.Models;
using StarrySkies.Data.Repositories.WeaponCategoryRepo;
using StarrySkies.Services.DTOs.WeaponCategoryDTOs;

namespace StarrySkies.Services.Services.WeaponCategories
{
    public class WeaponCategoryService : IWeaponCategoryService
    {
        private readonly IMapper _mapper;
        private readonly IWeaponCategoryRepo _weaponCategoryRepo;

        public WeaponCategoryService(IMapper mapper, IWeaponCategoryRepo weaponCategoryRepo)
        {
            _mapper = mapper;
            _weaponCategoryRepo = weaponCategoryRepo;
        }
        public WeaponCategoryResponseDto CreateWeaponCategory(CreateWeaponCategoryDto weaponCategory)
        {
            WeaponCategoryResponseDto categoryResponseDto = new WeaponCategoryResponseDto();
            WeaponCategory categoryToCreate = _mapper.Map<CreateWeaponCategoryDto, WeaponCategory>(weaponCategory);
            if (categoryToCreate.Name != null || categoryToCreate.Name.Trim() != "")
            {
                _weaponCategoryRepo.CreateWeaponCategory(categoryToCreate);
                _weaponCategoryRepo.SaveChanges();
                categoryResponseDto = _mapper.Map<WeaponCategory, WeaponCategoryResponseDto>(categoryToCreate);
            }

            return categoryResponseDto;
        }

        public WeaponCategoryResponseDto DeleteWeaponCategory(int id)
        {
            WeaponCategoryResponseDto categoryResponseDto = new WeaponCategoryResponseDto();
            WeaponCategory categoryToDelete = _weaponCategoryRepo.GetWeaponCategoryById(id);
            if (categoryToDelete != null || categoryToDelete.Id != 0)
            {
                _weaponCategoryRepo.DeleteWeaponCategory(categoryToDelete);
                _weaponCategoryRepo.SaveChanges();
                categoryResponseDto = _mapper.Map<WeaponCategory, WeaponCategoryResponseDto>(categoryToDelete);
            }

            return categoryResponseDto;
        }

        public ICollection<WeaponCategoryResponseDto> GetWeaponCategories()
        {
            ICollection<WeaponCategoryResponseDto> categoryResponse = new List<WeaponCategoryResponseDto>();
            ICollection<WeaponCategory> allCategroies = _weaponCategoryRepo.GetAllWeaponCategories();
            categoryResponse = _mapper.Map<ICollection<WeaponCategory>, ICollection<WeaponCategoryResponseDto>>(allCategroies);

            return categoryResponse;
        }

        public WeaponCategoryResponseDto GetWeaponCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        public WeaponCategoryResponseDto UpdateWeaponCategory(int id, CreateWeaponCategoryDto weaponCategoryDto)
        {
            throw new NotImplementedException();
        }
    }
}