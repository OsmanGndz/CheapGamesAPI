using CheapGames.Dtos.User;
using CheapGames.Dtos.Auth;
using CheapGames.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CheapGames.Mappers;

namespace CheapGames.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        private readonly ITokenRepository _tokenRepo;
        private readonly IUserRepository _userRepo;
        public AuthController(IAuthRepository authRepo, ITokenRepository tokenRepo, IUserRepository userRepo) 
        {
            _authRepo = authRepo;
            _tokenRepo = tokenRepo;
            _userRepo = userRepo;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _authRepo.AuthenticateUserAsync(loginInfo.Email, loginInfo.Password);

            if (user == null) 
            {
                return BadRequest(ModelState);
            }

            var token = _tokenRepo.CreateToken(user);
            return Ok(new { token,
                user = new
                {
                    id = user.Id,
                    name = user.Name,
                    surname = user.Surname,
                    email = user.Email
                }
            });
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await _userRepo.GetUsersByIdAsync(int.Parse(userId));

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(new
            {
                id = user.Id,
                name = user.Name,
                surname = user.Surname,
                email = user.Email
            });
        }

        [Authorize]
        [HttpPut("me")]
        public async Task<IActionResult> UpdateMe([FromBody] UserUpdateDto updateDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await _userRepo.GetUsersByIdAsync(int.Parse(userId));

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var updatedUser = await _userRepo.UpdateUserAsync(int.Parse(userId), updateDto);

            return Ok(updatedUser);
        }

        [Authorize]
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto passwordDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await _userRepo.GetUsersByIdAsync(int.Parse(userId));

            var passwordChange = await _authRepo.ChangePasswordAsync(int.Parse(userId), passwordDto);

            if (passwordChange == null)
            {
                return NotFound("Password is wrong or user could not found!");
            }

            return Ok("Password is changed");
        }

    }
}
