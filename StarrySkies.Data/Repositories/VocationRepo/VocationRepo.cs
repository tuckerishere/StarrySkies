using System.Collections.Generic;
using System.Linq;
using StarrySkies.Data.Data;
using StarrySkies.Data.Models;

namespace StarrySkies.Data.Repositories.VocationRepo
{
    public class VocationRepo : IVocationRepo
    {
        private readonly ApplicationDbContext _context;
        public VocationRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public void CreateVocation(Vocation vocation)
        {
            _context.Vocations.Add(vocation);
        }

        public void DeleteVocation(Vocation vocation)
        {
            _context.Vocations.Remove(vocation);
        }

        public Vocation GetVocationById(int id)
        {
           return _context.Vocations.Find(id);
        }

        public ICollection<Vocation> GetVocations()
        {
            return _context.Vocations.OrderBy(x => x.Name).ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void UpdateVocation(Vocation vocation)
        {
            _context.Vocations.Update(vocation);
        }
    }
}