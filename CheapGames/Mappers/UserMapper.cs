using CheapGames.Dtos.User;
using CheapGames.Models;

namespace CheapGames.Mappers
{
    public static class UserMapper
    {
        public static User ToUserCreateDto(this UserCreateDto createDto)
        {

            return new User
            {
                Name = createDto.Name,
                Surname = createDto.Surname,
                Email = createDto.Email,
                PasswordHash = createDto.Password,
            };

        }
        public static UserReadDto ToUserReadDto(this User user)
        {

            return new UserReadDto
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
            };

        }
    }
}
