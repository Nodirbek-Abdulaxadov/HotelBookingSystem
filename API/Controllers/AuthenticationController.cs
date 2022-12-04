﻿using API.Interfaces;
using API.ViewModels.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register-user")]
        public async Task<IActionResult> Register([FromBody] RegisterUserViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _userService.CreateUserAsync(viewModel);
            if (result.Item1 == true)
            {
                return Ok(result.Item2);
            }

            return BadRequest(result.Item2);
        }

        [HttpPost("login-user")]
        public async Task<IActionResult> Login([FromBody] LoginUserViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.LoginUserAsync(viewModel);
            if (result.Item1 == true)
            {
                return Ok(result.Item2);
            }

            return Unauthorized(result.Item2);
        }

        [HttpPost("refresh-user")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequstViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.VerifyAndGenerateTokenAsync(viewModel);
            return Ok(result);
        }
    }
}
