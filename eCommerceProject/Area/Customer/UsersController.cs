using eCommerceProject.BLL.Service.Interfaces;
using eCommerceProject.DAL.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceProject.PL.Area.Customer
{
    [Route("api/[Area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet(" ")]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute]string id)
        {
            var user = await _userService.GetByIdAsync(id);
            return Ok(user);
        }
        [HttpPatch("Block/{id}")]
        public async Task<IActionResult> BlockUserAsync([FromRoute] string id, [FromBody] int days)
        {
            var result = await _userService.BlockUserAsync(id, days);
            return Ok(result);
        }
        [HttpPatch("UnBlock/{id}")]
        public async Task<IActionResult> UnBlockUserAsync([FromRoute] string id)
        {
            var result = await _userService.UnBlockUserAsync(id);
            return Ok(result);
        }
        [HttpGet("IsBlocked/{id}")]
        public async Task<IActionResult> isBlockUserAsync([FromRoute] string id)
        {
            var result = await _userService.isBlockUserAsync(id);
            return Ok(result);
        }
        [HttpPatch("ChangeRole/{id}")]
        public async Task<IActionResult> ChangeRole([FromBody] string userId, [FromBody] ChangeRoleRequest request)
        {
            var result = await _userService.ChangeUserRoleAsync(userId, request.RoleName);
            return Ok(new {message = "Role Changes"});
        }
    }
}
