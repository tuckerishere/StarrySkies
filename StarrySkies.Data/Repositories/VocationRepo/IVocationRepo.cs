using System.Collections.Generic;
using StarrySkies.Data.Models;

namespace StarrySkies.Data.Repositories.VocationRepo
{
    public interface IVocationRepo
    {
        ICollection<Vocation> GetVocations();
        Vocation GetVocationById(int id);
        void CreateVocation(Vocation vocation);
        void DeleteVocation(Vocation vocation);
        void UpdateVocation(Vocation vocation);
        bool SaveChanges();
    }
}