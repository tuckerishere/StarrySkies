using System.Collections.Generic;
using StarrySkies.Data.Models;

namespace StarrySkies.Data.Repositories.VocationSpellRepo
{
    public interface IVocationSpellRepo
    {
        VocationSpell GetVocationSpell(int vocationId, int spellId);
        ICollection<VocationSpell> GetVocationSpells();
        void CreateVocationSpell(VocationSpell vocationSpell);
        bool SaveChanges();
        void UpdateVocationSpell(VocationSpell vocationSpell);
        void DeleteVocationSpell(VocationSpell vocationSpell);
    }
}