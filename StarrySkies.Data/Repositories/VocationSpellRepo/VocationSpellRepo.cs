using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StarrySkies.Data.Data;
using StarrySkies.Data.Models;

namespace StarrySkies.Data.Repositories.VocationSpellRepo
{    
    public class VocationSpellRepo : IVocationSpellRepo
    {
         private readonly ApplicationDbContext _context;
         public VocationSpellRepo(ApplicationDbContext context)
         {
            _context = context;
        }
        public void CreateVocationSpell(VocationSpell vocationSpell)
        {
            _context.VocationSpells.Add(vocationSpell);
        }

        public void DeleteVocationSpell(VocationSpell vocationSpell)
        {
            _context.VocationSpells.Remove(vocationSpell);
        }

        public VocationSpell GetVocationSpell(int vocationId, int spellId)
        {
            return _context.VocationSpells.SingleOrDefault(x => x.VocationId == vocationId && x.SpellId == spellId);
        }

        public ICollection<VocationSpell> GetVocationSpells()
        {
            return _context.VocationSpells.OrderBy(x => x.Vocation.Name).ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void UpdateVocationSpell(VocationSpell vocationSpell)
        {
            _context.Update(vocationSpell);
        }
    }
}