
using ApiWebToken.Controllers;
using ApiWebToken.Dtos;
using ApiWebToken.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using iText.Html2pdf.Css.Apply.Impl;
using Microsoft.AspNetCore.Mvc;


namespace ApiWebToken.Controllers;

    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;

        public  UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(RegisterDto model)
        {
            var result = await _userService.RegisterAsync(model);
            return Ok(result);
        }

        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync(LoginDto model)
        {
            var result = await _userService.GetTokenAsync(model);
            return Ok(result);
        }

        [HttpPost("addrole")]
        public async Task<IActionResult> AddRoleAsync(AddRoleDto model)
        {
            var result = await _userService.AddRoleAsync(model);
            return Ok(result);
        }

    }