using CheapGames.Data;
using CheapGames.Dtos.User;
using CheapGames.Interfaces;
using CheapGames.Mappers;
using CheapGames.Models;
using Microsoft.EntityFrameworkCore;

namespace CheapGames.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;
        public UserRepository(ApplicationDBContext context)
        { 
            _context = context;
        }

        public async Task<UserReadDto> Register(UserCreateDto userDto)
        {
            var passwordHash = EncryptPassword(userDto.Password);
            var newUser = new UserCreateDto
            {
                Name = userDto.Name,
                Surname = userDto.Surname,
                Email = userDto.Email,
                Password = passwordHash
            };
            var addedData = newUser.ToUserCreateDto();
            await _context.AddAsync(addedData);
            await _context.SaveChangesAsync();
            return addedData.ToUserReadDto();
        }

        public string EncryptPassword(string password)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            return passwordHash;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<User?> GetUsersByIdAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<bool> IsUserExistByEmail(UserCreateDto userDto)
        {
            var isUserExist = await _context.Users.AnyAsync(u => u.Email == userDto.Email);
            return isUserExist;
        }

        public async Task<bool> IsUserExistById(int id)
        {
            var isUserExist = await _context.Users.AnyAsync(u => u.Id == id);
            return isUserExist;
        }

        public async Task<User?> UpdateUserAsync(int id, UserUpdateDto userDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u=> u.Id == id);
            
            user.Name = userDto.Name;
            user.Surname = userDto.Surname;
            user.Email = userDto.Email;

            await _context.SaveChangesAsync();

            return user;   
        }

        public async Task<UserReadDto> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u=>u.Id == id);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user.ToUserReadDto();
        }
    }
}
