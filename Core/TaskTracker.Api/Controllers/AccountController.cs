using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Api.ActionFilters;
using TaskTracker.Entities.DataTransferObjects;
using TaskTracker.Entities.Models;
using IAuthenticationService = TaskTracker.Contract.IAuthenticationService;

namespace TaskTracker.Api.Controllers
{
    [Route("api/account")]
    [Produces("application/json")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthenticationService _authService;

        public AccountController(ILogger<AccountController> logger, IMapper mapper, UserManager<User> userManager, IAuthenticationService authService, SignInManager<User> signInManager)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _authService = authService;
            _signInManager = signInManager;
            _signInManager.UserManager = _userManager;
        }

        /// <summary>
        /// New user registration
        /// </summary>
        /// <param name="userForRegistration">User model for registration</param>
        /// <returns>Registered user</returns>
        [HttpPost("register")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrerDto userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userForRegistration.Password);
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
        /// Log in by email and password
        /// </summary>
        /// <param name="user">Authorized user</param>
        /// <returns>Token</returns>
        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthorizeDto user)
        {
            if (!await _authService.IsValidUser(user))
            {
                _logger.LogWarning($"{nameof(Authenticate)}: Authentication failed. Wrong email or password.");
                return Unauthorized();
            }
            return Ok(new { Token = _authService.CreateToken() });
        }

        /// <summary>
        /// Send message with code on Telegram
        /// </summary>
        /// <param name="user">User with number</param>
        /// <returns>Dispatch result message</returns>
        [HttpPost("login/sendMessage")]
        public async Task<IActionResult> SendTelegramCode([FromBody] UserForAuthorizeDto user)
        {
            if (!await _authService.IsValidNumber(user.PhoneNumber))
            {
                _logger.LogWarning($"{nameof(Authenticate)}: Authentication failed. Wrong number.");
                return BadRequest();
            }
            await _authService.SendMessageByBot(user.PhoneNumber);
            return Ok("Message sent");
        }

        /// <summary>
        /// Log in by code from Telegram bot
        /// </summary>
        /// <param name="code">code value</param>
        /// <returns>Token</returns>
        [HttpPost("login/phone")]
        public async Task<IActionResult> LogInByCode([FromBody] string code)
        {
            return Ok(new { Token = _authService.CreateToken() });
        }

        /// <summary>
        /// Log out
        /// </summary>
        /// <returns>NoContent</returns>
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.Context.SignOutAsync("Cookie");
            _logger.LogInformation("User signed out successfully");
            return NoContent();
        }
    }
}
