﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Api.ActionFilters;
using TaskTracker.Contract;
using TaskTracker.Entities.DataTransferObjects;
using TaskTracker.Entities.Models;

namespace TaskTracker.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationService _authService;

        public UserController(ILogger<UserController> logger, IMapper mapper, UserManager<User> userManager, IAuthenticationService authService)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _authService = authService;
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="userForRegistration">Модель пользователя для регистрации</param>
        /// <returns>Зарегистрированный пользователь</returns>
        [HttpPost("register")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrerDto userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);
            user.UserName = userForRegistration.Email;
            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.TryAddModelError(error.Code, error.Description);

                return BadRequest(ModelState);
            }
            return StatusCode(201);
        }

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="user">Авторизованный пользователь</param>
        /// <returns>Токен</returns>
        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthorizeDto user)
        {
            if (!await _authService.ValidateUser(user))
            {
                _logger.LogWarning($"{nameof(Authenticate)}: Authentication failed. Wrong email or password.");
                return Unauthorized();
            }
            return Ok(new { Token = await _authService.CreateToken() });
        }

        /// <summary>
        /// Выход
        /// </summary>
        /// <returns>NoContent</returns>
        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            Response.Headers.Remove("Authorization");
            return NoContent();
        }
    }
}
