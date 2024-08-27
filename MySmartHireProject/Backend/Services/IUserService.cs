using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHire.DTOs;

namespace SmartHire.Services
{
    public interface IUserService
    {
        Task<UserDTO> CreateUserAsync(UserDTO userDTO);
        Task<UserDTO> UpdateUserAsync(long userId, UserDTO userDTO);
        Task DeleteUserAsync(long userId);
        Task<UserDTO> GetUserByIdAsync(long userId);
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> AuthenticateUserAsync(AuthDTO dto);
    }
}
