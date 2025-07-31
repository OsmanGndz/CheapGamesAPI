using Microsoft.AspNetCore.Authorization;
using CheapGames.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CheapGames.Dtos.Order;
using System.Security.Claims;
using CheapGames.Mappers;

namespace CheapGames.Controllers
{
    [Route("api/order")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IUserRepository _userRepo;
        public OrderController(IOrderRepository orderRepo, IUserRepository userRepo)
        {
            _orderRepo = orderRepo;
            _userRepo = userRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized("User could not recognized");
            }

            var userId = int.Parse(userIdClaim);

            var user = await _userRepo.IsUserExistById(userId);

            if (user == false) return NotFound("User not found");

            var orders = await _orderRepo.GetOrdersAsync(userId);

            if (orders == null) 
            {
                return NotFound("There is no order in database!!!");
            }
            var ordersDto = orders.Select(o => o.ToOrderReadDto()).ToList();

            return Ok(ordersDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized("User could not recognized");
            }

            var userId = int.Parse(userIdClaim);

            var user = await _userRepo.IsUserExistById(userId);

            if (user == false) return NotFound("User not found");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _orderRepo.GetOrderAsync(id, userId);

            if (order == null)
            {
                return NotFound($"There is no order with {id}");
            }

            return Ok(order.ToOrderReadDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderCreateDto orderDto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null) 
            {
                return Unauthorized("User could not recognized");
            }

            var userId = int.Parse(userIdClaim);

            var user = await _userRepo.IsUserExistById(userId);

            if (user == false) return NotFound("User not found");

            var order = await _orderRepo.CreateOrderAsync(orderDto, userId);

            if (order == null) 
            {
                return BadRequest("Order could not taken succesfully, it can be because of product already exist for user.");
            }

            return Ok(order.ToOrderReadDto());
        }

    }
}
