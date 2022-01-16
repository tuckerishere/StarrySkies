using System.Collections.Generic;
using StarrySkies.Data.Models;

namespace StarrySkies.Data.Repositories.SpellRepo
{
    public interface ISpellRepo
    {
        Spell GetSpell(int id);
        ICollection<Spell> GetSpells();
        void CreateSpell(Spell spell);
        void DeleteSpell(Spell spell);
        void UpdateSpell(Spell spell);
        bool SaveChanges();
    }
}