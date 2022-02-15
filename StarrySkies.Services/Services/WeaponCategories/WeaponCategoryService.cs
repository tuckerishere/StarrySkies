using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StarrySkies.Data.Models;
using StarrySkies.Data.Repositories.WeaponCategoryRepo;
using StarrySkies.Services.DTOs.WeaponCategoryDTOs;
using StarrySkies.Services.ResponseModels;

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
        public ServiceResponse<WeaponCategoryResponseDto> CreateWeaponCategory(CreateWeaponCategoryDto weaponCategory)
        {
            ServiceResponse<WeaponCategoryResponseDto> categoryResponseDto = new ServiceResponse<WeaponCategoryResponseDto>();
            WeaponCategory categoryToCreate = _mapper.Map<CreateWeaponCategoryDto, WeaponCategory>(weaponCategory);
            if (categoryToCreate.Name != null && categoryToCreate.Name.Trim() != "")
            {
                _weaponCategoryRepo.CreateWeaponCategory(categoryToCreate);
                _weaponCategoryRepo.SaveChanges();
                categoryResponseDto.Data = _mapper.Map<WeaponCategory, WeaponCategoryResponseDto>(categoryToCreate);
            }
            else
            {
                categoryResponseDto.Success = false;
                categoryResponseDto.Message = "Missing Weapon Category Name.";
            }

            return categoryResponseDto;
        }

        public ServiceResponse<WeaponCategoryResponseDto> DeleteWeaponCategory(int id)
        {
            ServiceResponse<WeaponCategoryResponseDto> categoryResponseDto = new ServiceResponse<WeaponCategoryResponseDto>();
            WeaponCategory categoryToDelete = _weaponCategoryRepo.GetWeaponCategoryById(id);
            if (categoryToDelete != null)
            {
                _weaponCategoryRepo.DeleteWeaponCategory(categoryToDelete);
                _weaponCategoryRepo.SaveChanges();
                categoryResponseDto.Data = _mapper.Map<WeaponCategory, WeaponCategoryResponseDto>(categoryToDelete);
            }
            else
            {
                categoryResponseDto.Message = "Weapon Category Not Found.";
                categoryResponseDto.Success = false;
            }

            return categoryResponseDto;
        }

        public ServiceResponse<ICollection<WeaponCategoryResponseDto>> GetWeaponCategories()
        {
            ServiceResponse<ICollection<WeaponCategoryResponseDto>> categoryResponse = new ServiceResponse<ICollection<WeaponCategoryResponseDto>>();
            ICollection<WeaponCategory> allCategroies = _weaponCategoryRepo.GetAllWeaponCategories();
            categoryResponse.Data = _mapper.Map<ICollection<WeaponCategory>, ICollection<WeaponCategoryResponseDto>>(allCategroies);

            return categoryResponse;
        }

        public ServiceResponse<WeaponCategoryResponseDto> GetWeaponCategoryById(int id)
        {
            ServiceResponse<WeaponCategoryResponseDto> categoryResponse = new ServiceResponse<WeaponCategoryResponseDto>();
            WeaponCategory category = _weaponCategoryRepo.GetWeaponCategoryById(id);
            if (category != null)
            {
                    categoryResponse.Data = _mapper.Map<WeaponCategory, WeaponCategoryResponseDto>(category);
            }
            else
            {
                categoryResponse.Success = false;
                categoryResponse.Message = "Weapon Category Not Found.";
            }

            return categoryResponse;
        }

        public ServiceResponse<WeaponCategoryResponseDto> UpdateWeaponCategory(int id, CreateWeaponCategoryDto weaponCategoryDto)
        {
            ServiceResponse<WeaponCategoryResponseDto> categoryResponse = new ServiceResponse<WeaponCategoryResponseDto>();
            WeaponCategory categoryToUpdate = _weaponCategoryRepo.GetWeaponCategoryById(id);
            if(categoryToUpdate == null)
            {
                categoryResponse.Success = false;
                categoryResponse.Message = "Weapon Category Not Found.";
            }
            if(weaponCategoryDto.Name == null || weaponCategoryDto.Name.Trim() == "")
            {
                categoryResponse.Success = false;
                categoryResponse.Message = "Please enter name for Weapon Category.";
            }
            else
            {
                categoryToUpdate.Name = weaponCategoryDto.Name;
                _weaponCategoryRepo.UpdateWeaponCategory(categoryToUpdate);
                _weaponCategoryRepo.SaveChanges();
                categoryResponse.Data = _mapper.Map<WeaponCategory, WeaponCategoryResponseDto>(categoryToUpdate);
            }

            return categoryResponse;

        }
    }
}