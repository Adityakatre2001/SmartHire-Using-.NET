using Microsoft.EntityFrameworkCore;
using SmartHire.Models;
using System.Collections.Generic;
using System.Linq;

namespace SmartHire.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public ProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Profile GetById(long id)
        {
            return _context.Profiles.Find(id);
        }

        public IEnumerable<Profile> GetAll()
        {
            return _context.Profiles.ToList();
        }

        public Profile Create(Profile profile)
        {
            _context.Profiles.Add(profile);
            _context.SaveChanges();
            return profile;
        }

        public Profile Update(Profile profile)
        {
            _context.Profiles.Update(profile);
            _context.SaveChanges();
            return profile;
        }

        public void Delete(long id)
        {
            var profile = _context.Profiles.Find(id);
            if (profile != null)
            {
                _context.Profiles.Remove(profile);
                _context.SaveChanges();
            }
        }
    }
}
