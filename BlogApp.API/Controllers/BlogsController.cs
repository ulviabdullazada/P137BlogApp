﻿using BlogApp.Business.Dtos.BlogDtos;
using BlogApp.Business.Dtos.CommentDtos;
using BlogApp.Business.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.Core.Enums;
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
        readonly ICommentService _commentService;
        public BlogsController(IBlogService blogService, ICommentService commentService)
        {
            _blogService = blogService;
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _blogService.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _blogService.GetByIdAsync(id));
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]BlogCreateDto dto)
        {
            await _blogService.CreateAsync(dto);
            return Ok();
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _blogService.RemoveAsync(id);
            return StatusCode(StatusCodes.Status202Accepted);
        }
        [Authorize]
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> Comment(int id, CommentCreateDto dto)
        {
            await _commentService.CreateAsync(id, dto);
            return StatusCode(StatusCodes.Status202Accepted);
        }
        [Authorize]
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> React(int id, Reactions reaction)
        {
            await _blogService.ReactAsync(id, reaction);
            return StatusCode(StatusCodes.Status202Accepted);
        }
        [Authorize]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> RemoveReact(int id)
        {
            await _blogService.RemoveReactAsync(id);
            return StatusCode(StatusCodes.Status202Accepted);
        }
        //[HttpGet("props")]
        //public IActionResult GetProp()
        //{
        //    List<string> createProps = new List<string>();
        //    foreach (var item in typeof(BlogCreateDto).GetProperties())
        //    {
        //        createProps.Add(item.Name);
        //    }
        //    List<string> blogProps = new List<string>();
        //    foreach (var item in typeof(Blog).GetProperties())
        //    {
        //        blogProps.Add(item.Name);
        //    }

        //    return Ok(new
        //    {
        //        blog = blogProps,
        //        dto = createProps
        //    });
        //}
    }
}
