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
        private readonly IEmailService _emailService;

        public AccountController(ILogger<AccountController> logger, IMapper mapper, UserManager<User> userManager, IAuthenticationService authService, IDataContextService dataContextService, IEmailService emailService)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _authService = authService;
            _dataContextService = dataContextService;
            _emailService = emailService;
        }

        /// <summary>
        /// New user registration
        /// </summary>
        /// <param name="userForRegistration">User model for registration</param>
        /// <response code="201">New user was registered</response>
        /// <response code="400">Incorrect registration parameters</response>
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
        /// <response code="200">Authorization token</response>
        /// <response code="401">Wrong login parameters</response>
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
        /// <response code="204">Code was sent on email</response>
        /// <response code="400">Account was not found</response>
        /// <returns></returns>
        [HttpPost("sendCode"), AllowAnonymous]
        public async Task<IActionResult> SendCodeEmail([FromBody] UserLogInByCodeDto userDto)
        {
            var email = userDto.Email;
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest($"User with email {email} not found");

            var code = _emailService.GenerateCode();
            await _emailService.SendEmailAsync(user.Email, "Verification code to log in Task Tracker", $"Enter this code to confirm log in: {code}");
            user.EmailCode = code;
            await _userManager.UpdateAsync(user);

            return Ok();
        }

        /// <summary>
        /// Authentication by code
        /// </summary>
        /// <param name="userDto">user model</param>
        /// <response code="200">Successfully authorized by code</response>
        /// <response code="400">Account was not found or code is incorrect</response>
        /// <returns>token</returns>
        [HttpPost("loginCode"), AllowAnonymous]
        public async Task<IActionResult> AuthorizeByCode([FromBody] UserEmailCodeDto userDto)
        {
            var user = await _userManager.FindByEmailAsync(userDto.Email);
            if (user == null)
                return BadRequest($"User with email {userDto.Email} not found");
            if (!string.IsNullOrEmpty(user.EmailCode) && user.EmailCode.Equals(userDto.Code))
                return Ok(new { Token = _authService.CreateToken(user.Id) });
            else return BadRequest("Incorrect code");
        }
        #endregion

        /// <summary>
        /// Getting user information
        /// </summary>
        /// <response code="200">Account info</response>
        /// <response code="400">User was not found</response>
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

        /// <summary>
        /// Delete account
        /// </summary>
        /// <response code="204">Account was successfully deleted</response>
        /// <response code="404">Account was not found</response>
        /// <returns></returns>
        [HttpDelete("delete"), Authorize]
        public async Task<IActionResult> DeleteAccount()
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
                return NotFound("User not found");
            else
            {
                await _userManager.DeleteAsync(user);
                return NoContent();
            }
        }
    }
}
