using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Api.ActionFilters;
using TaskTracker.Contract;
using TaskTracker.Entities.DataTransferObjects;
using TaskTracker.Entities.Models;

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
        private readonly Contract.IAuthenticationService _authService;
        private readonly IDataContextService _dataContextService;

        public AccountController(ILogger<AccountController> logger, IMapper mapper, UserManager<User> userManager, TaskTracker.Contract.IAuthenticationService authService, SignInManager<User> signInManager, IDataContextService dataContextService)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _authService = authService;
            _signInManager = signInManager;
            _signInManager.UserManager = _userManager;
            _dataContextService = dataContextService;
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

        #region LogInLogic
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
        #endregion

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

        /// <summary>
        /// Getting user information
        /// </summary>
        /// <returns>User information</returns>
        [HttpGet("info"), Authorize]
        public async Task<ActionResult<UserDto>> GetUserInfo()
        {
            var userFromDb = await _dataContextService.GetUserInformationAsync(UserId);
            if (userFromDb == null)
            {
                _logger.LogError("User not found");
                return BadRequest("User not found");
            }
            var userDto = _mapper.Map<UserDto>(userFromDb);
            return Ok(userDto);
        }
    }
}
