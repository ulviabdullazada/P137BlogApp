using BlogApp.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        readonly IRoleService _service;

        public RolesController(IRoleService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get() {
            return Ok(await _service.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Post(string name)
        {
            await _service.CreateAsync(name);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.RemoveAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpPut]
        public async Task<IActionResult> Put(string id, string name)
        {
            await _service.UpdateAsync(id, name);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
