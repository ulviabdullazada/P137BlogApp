using BlogApp.Business.Dtos.BlogDtos;
using BlogApp.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        readonly IBlogService _blogService;

        public BlogsController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _blogService.GetAllAsync());
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]BlogCreateDto dto)
        {
            try
            {
                await _blogService.CreateAsync(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
