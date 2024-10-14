using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartHire.DTOs;
using SmartHire.Models;

namespace SmartHire.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDTO> CreateUserAsync(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> UpdateUserAsync(long userId, UserDTO userDTO)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return null;

            _mapper.Map(userDTO, user);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDTO>(user);
        }

        public async Task DeleteUserAsync(long userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<UserDTO> GetUserByIdAsync(long userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user == null ? null : _mapper.Map<UserDTO>(user);
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO> AuthenticateUserAsync(AuthDTO dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email && u.Password == dto.Password);
            return user == null ? null : _mapper.Map<UserDTO>(user);
        }
    }
}


//*****************************************************************************************



/*using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using SmartHire.DTOs;
using SmartHire.Models;

namespace SmartHire.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDTO> CreateUserAsync(UserDTO userDTO)
        {
            // Hash the password before saving the user
            userDTO.Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);

            var user = _mapper.Map<User>(userDTO);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> UpdateUserAsync(long userId, UserDTO userDTO)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return null;

            // If updating password, hash it
            if (!string.IsNullOrEmpty(userDTO.Password))
            {
                userDTO.Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);
            }

            _mapper.Map(userDTO, user);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDTO>(user);
        }

        public async Task DeleteUserAsync(long userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<UserDTO> GetUserByIdAsync(long userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user == null ? null : _mapper.Map<UserDTO>(user);
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO> AuthenticateUserAsync(AuthDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

            // Check if user exists and verify the password
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                return null;
            }

            return _mapper.Map<UserDTO>(user);
        }
    }
}*/
