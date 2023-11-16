using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IAuthenticationService _authService;
        private readonly IDataContextService _dataContextService;

        public AccountController(ILogger<AccountController> logger, IMapper mapper, UserManager<User> userManager, TaskTracker.Contract.IAuthenticationService authService, IDataContextService dataContextService)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _authService = authService;
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

        /// <summary>
        /// Send confirmation email
        /// </summary>
        /// <returns></returns>
        [HttpPost("sendEmail"), AllowAnonymous]
        public async Task<IActionResult> SendEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest($"User with email {email} not found");

            return Ok(new { Token = _authService.CreateToken() });
        }
        #endregion

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
