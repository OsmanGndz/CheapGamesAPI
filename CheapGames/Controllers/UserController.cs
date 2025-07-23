using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CheapGames.Data;
using CheapGames.Dtos.User;
using Microsoft.EntityFrameworkCore;
using CheapGames.Interfaces;
using CheapGames.Mappers;
using System.Runtime.CompilerServices;

namespace CheapGames.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepo.GetAllUsersAsync();
            if(users == null || users.Count() == 0)
            {
                return NotFound("There is no user at database!!");
            }

            var usersDto = users.Select(u => u.ToUserReadDto());

            return Ok(usersDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userRepo.GetUsersByIdAsync(id);

            if (user == null) 
            {
                return NotFound($"There is no user with {id}");
            }

            return Ok(user.ToUserReadDto());
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto userDto)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var isUserExist = await _userRepo.IsUserExistByEmail(userDto);
            if (isUserExist)
            {
                return BadRequest("This email is already enrolled!!!");
            }

            var createdUser = await _userRepo.Register(userDto);
            return Ok(createdUser);
            
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUserInformation([FromRoute] int id, [FromBody] UserUpdateDto updateDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isExist = await _userRepo.IsUserExistById(id);

            if (!isExist)
            {
                return BadRequest($"There is no user with {id}!!");

            }

            var updatedUser = await _userRepo.UpdateUserAsync(id, updateDto);

            return Ok(updatedUser?.ToUserReadDto());

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isExist = await _userRepo.IsUserExistById(id);

            if (!isExist)
            {
                return NotFound($"There is no user with id:{id}");
            }

            var deletedUser = await _userRepo.DeleteUserAsync(id);

            return Ok(deletedUser);
        }


    }
}
