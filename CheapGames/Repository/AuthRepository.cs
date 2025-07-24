using CheapGames.Data;
using CheapGames.Interfaces;
using CheapGames.Models;
using Microsoft.EntityFrameworkCore;
using CheapGames.Dtos.Auth;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CheapGames.Repository
{
    public class AuthRepository: IAuthRepository
    {
        private readonly ApplicationDBContext _context;
        public AuthRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<User?> AuthenticateUserAsync(string email, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (user == null) return null;

            // Hashlenmiş şifreyle karşılaştır
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!isPasswordValid) return null;

            return user;
        }

        public async Task<User?> ChangePasswordAsync(int userId, ChangePasswordDto passwordDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return null ;

            // Mevcut şifre doğru mu?
            var isValid = BCrypt.Net.BCrypt.Verify(passwordDto.password, user.PasswordHash);
            if (!isValid)
                return null;

            if(passwordDto.newPassword != passwordDto.passwordConfirmation) return null;

            // Yeni şifreyi hashle
            string newHashedPassword = BCrypt.Net.BCrypt.HashPassword(passwordDto.newPassword);

            // Veritabanına kaydet
            user.PasswordHash = newHashedPassword;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return user;
        }

    }
}
