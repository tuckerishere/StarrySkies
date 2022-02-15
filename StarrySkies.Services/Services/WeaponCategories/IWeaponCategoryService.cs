using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarrySkies.Services.DTOs.WeaponCategoryDTOs;
using StarrySkies.Services.ResponseModels;

namespace StarrySkies.Services.Services.WeaponCategories
{
    public interface IWeaponCategoryService
    {
        ServiceResponse<ICollection<WeaponCategoryResponseDto>> GetWeaponCategories();
        ServiceResponse<WeaponCategoryResponseDto> GetWeaponCategoryById(int id);
        ServiceResponse<WeaponCategoryResponseDto> CreateWeaponCategory(CreateWeaponCategoryDto weaponCategory);
        ServiceResponse<WeaponCategoryResponseDto> UpdateWeaponCategory(int id, CreateWeaponCategoryDto weaponCategoryDto);
        ServiceResponse<WeaponCategoryResponseDto> DeleteWeaponCategory(int id);
    }
}