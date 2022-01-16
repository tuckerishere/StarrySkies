using System.Collections.Generic;
using System.Linq;
using StarrySkies.Data.Data;
using StarrySkies.Data.Models;

namespace StarrySkies.Data.Repositories.SpellRepo
{
    public class SpellRepo : ISpellRepo
    {
        private readonly ApplicationDbContext _context;
        public SpellRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public void CreateSpell(Spell spell)
        {
            _context.Spells.Add(spell);
        }

        public void DeleteSpell(Spell spell)
        {
            _context.Spells.Remove(spell);
        }

        public Spell GetSpell(int id)
        {
            return _context.Spells.Find(id);
        }

        public ICollection<Spell> GetSpells()
        {
            return _context.Spells.OrderBy(x => x.Name).ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void UpdateSpell(Spell spell)
        {
            _context.Spells.Update(spell);
        }
    }
}