using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarrySkies.Data.Data;
using StarrySkies.Data.Models;

namespace StarrySkies.Data.Repositories.WeaponCategoryRepo
{
    public class WeaponCategoryRepo : IWeaponCategoryRepo
    {
        private readonly ApplicationDbContext _dbContext;
        public WeaponCategoryRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void CreateWeaponCategory(WeaponCategory weaponCategory)
        {
            _dbContext.WeaponCategories.Add(weaponCategory);
        }

        public void DeleteWeaponCategory(WeaponCategory weaponCategory)
        {
            _dbContext.WeaponCategories.Remove(weaponCategory);
        }

        public ICollection<WeaponCategory> GetAllWeaponCategories()
        {
            return _dbContext.WeaponCategories.OrderBy(x => x.Name).ToList();
        }

        public WeaponCategory GetWeaponCategoryById(int id)
        {
            return _dbContext.WeaponCategories.Find(id);
        }

        public bool SaveChanges()
        {
            return _dbContext.SaveChanges() >= 0;
        }

        public void UpdateWeaponCategory(WeaponCategory weaponCategory)
        {
            _dbContext.WeaponCategories.Update(weaponCategory);
        }
    }
}