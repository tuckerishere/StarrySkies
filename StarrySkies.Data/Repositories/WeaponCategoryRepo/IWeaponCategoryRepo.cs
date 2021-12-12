using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarrySkies.Data.Models;

namespace StarrySkies.Data.Repositories.WeaponCategoryRepo
{
    public interface IWeaponCategoryRepo
    {
        ICollection<WeaponCategory> GetAllWeaponCategories();
        WeaponCategory GetWeaponCategoryById(int id);
        void CreateWeaponCategory(WeaponCategory weaponCategory);
        void DeleteWeaponCategory(WeaponCategory weaponCategory);
        void UpdateWeaponCategory(WeaponCategory weaponCategory);
        bool SaveChanges();
    }
}