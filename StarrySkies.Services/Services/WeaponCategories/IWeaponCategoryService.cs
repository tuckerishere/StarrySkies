using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarrySkies.Services.DTOs.WeaponCategoryDTOs;

namespace StarrySkies.Services.Services.WeaponCategories
{
    public interface IWeaponCategoryService
    {
        ICollection<WeaponCategoryResponseDto> GetWeaponCategories();
        WeaponCategoryResponseDto GetWeaponCategoryById(int id);
        WeaponCategoryResponseDto CreateWeaponCategory(CreateWeaponCategoryDto weaponCategory);
        WeaponCategoryResponseDto UpdateWeaponCategory(int id, CreateWeaponCategoryDto weaponCategoryDto);
        WeaponCategoryResponseDto DeleteWeaponCategory(int id);
    }
}