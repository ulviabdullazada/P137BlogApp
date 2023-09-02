using BlogApp.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            return Ok(await _service.GetAllAsync());
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddRole(string role, string userName)
        {
            await _service.AddRole(role, userName);
            return Ok();
        }
    }
}
