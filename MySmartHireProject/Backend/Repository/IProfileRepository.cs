using SmartHire.Models;
using System.Collections.Generic;

namespace SmartHire.Repositories
{
    public interface IProfileRepository
    {
        Profile GetById(long id);
        IEnumerable<Profile> GetAll();
        Profile Create(Profile profile);
        Profile Update(Profile profile);
        void Delete(long id);
    }
}
