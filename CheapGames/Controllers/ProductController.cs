using CheapGames.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CheapGames.Controllers
{
    [Route("api/product")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepo;
        private readonly IUserRepository _userRepo;

        public ProductController(IProductRepository productRepo, IUserRepository userRepo) 
        {
            _productRepo = productRepo;
            _userRepo = userRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyProducts() 
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
            {
                return Unauthorized("User could not recognized");
            }

            var userId = int.Parse(userIdClaim);

            var user = await _userRepo.IsUserExistById(userId);

            if (user == false) return NotFound("User not found");

            var products = await _productRepo.GetOrderItems(userId);

            return Ok(products);


        }
    }
}
