using SmartHire.Models;
using System.Threading.Tasks;

namespace SmartHire.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(long userId);
        Task<User> AddAsync(User user);
        Task<User> UpdateAsync(User user);
        Task DeleteAsync(long userId);
        Task<IEnumerable<User>> GetAllAsync();
    }
}
