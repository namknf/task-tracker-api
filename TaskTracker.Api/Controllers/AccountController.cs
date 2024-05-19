using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Api.ActionFilters;
using TaskTracker.Contract.Service;
using TaskTracker.Entities.DataTransferObjects;
using TaskTracker.Entities.Models;
using TaskTracker.Entities.RequestFeatures;

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
        private readonly IFileService _fileService;

        public AccountController(
            ILogger<AccountController> logger,
            IMapper mapper,
            UserManager<User> userManager,
            IAuthenticationService authService,
            IDataContextService dataContextService,
            IEmailService emailService,
            IFileService fileService)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _authService = authService;
            _dataContextService = dataContextService;
            _emailService = emailService;
            _fileService = fileService;
        }

        /// <summary>
        /// New user registration
        /// </summary>
        /// <param name="userForRegistration">User model for registration</param>
        /// <response code="201">New user was registered</response>
        /// <response code="400">Incorrect registration parameters</response>
        /// <returns>Registered user</returns>
        [HttpPost("register"), AllowAnonymous]
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

            var userForAuth = _mapper.Map<UserForAuthorizeDto>(userForRegistration);
            if (await _authService.IsValidUser(userForAuth))
            {
                var refresh = await _authService.GenerateRefreshToken();
                user.RefreshToken = refresh;
                user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(10);
                await _userManager.UpdateAsync(user);
                return Ok(new
                {
                    Session = new
                    {
                        AccessToken = _authService.CreateToken(user.Id, user.Email, 20),
                        RefreshToken = refresh,
                    }
                });
            } 

            return BadRequest(ModelState);
        }

        #region LogInLogic
        /// <summary>
        /// Log in by email and password
        /// </summary>
        /// <param name="user">Authorized user</param>
        /// <response code="200">Authorization token</response>
        /// <response code="401">Wrong login parameters</response>
        /// <returns>Token</returns>
        [HttpPost("login"), AllowAnonymous]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthorizeDto user)
        {
            if (!await _authService.IsValidUser(user))
            {
                _logger.LogWarning($"{nameof(Authenticate)}: Authentication failed. Wrong email or password.");
                return Unauthorized();
            }

            var userFromDb = await _userManager.FindByEmailAsync(user.EmailOrUserName);
            userFromDb ??= await _userManager.FindByNameAsync(user.EmailOrUserName);
            var refresh = await _authService.GenerateRefreshToken();
            userFromDb.RefreshToken = refresh;
            userFromDb.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(10);
            await _userManager.UpdateAsync(userFromDb);

            return Ok(new
            {
                Session = new
                {
                    AccessToken = _authService.CreateToken(userFromDb.Id, userFromDb.Email, 20),
                    RefreshToken = refresh,
                }
            });
        }

        /// <summary>
        /// Send confirmation email
        /// </summary>
        /// <response code="204">Code was sent on email</response>
        /// <response code="400">Account was not found</response>
        /// <returns></returns>
        [HttpPost("send_login_code"), AllowAnonymous]
        public async Task<IActionResult> SendCodeEmail([FromBody] UserLogInByCodeDto userDto)
        {
            var email = userDto.Email;
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest($"User with email {email} not found");

            var code = _emailService.GenerateCode();
            await _emailService.SendEmailAsync(user.Email, "Код подтверждения для входа в приложение", $"Введите следующий код, чтобы войти: {code}");
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
                return BadRequest($"Пользователь с электронной почтой {userDto.Email} не найден");

            if (!string.IsNullOrEmpty(user.EmailCode) && user.EmailCode.Equals(userDto.Code))
            {
                var refresh = await _authService.GenerateRefreshToken();
                user.RefreshToken = refresh;
                user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(10);
                await _userManager.UpdateAsync(user);

                return Ok(new
                {
                    Session = new
                    {
                        AccessToken = _authService.CreateToken(user.Id, user.Email, 20),
                        RefreshToken = refresh,
                    }
                });
            }
            else return BadRequest("Некорректный код");
        }

        /// <summary>
        /// Обновление токена доступа. Если не отправлять свойство LifeTime, то время жизни токена 5 минут.
        /// </summary>
        /// <param name="refreshTokenParams">Параметры для обновления</param>
        /// <returns></returns>
        [HttpPost]
        [Route("login/refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenParams refreshTokenParams)
        {
            if (refreshTokenParams.RefreshToken.Length == 0)
                return BadRequest("Некорректный токен обновления");

            var user = _userManager.Users.FirstOrDefault(p => p.RefreshToken.Contains(refreshTokenParams.RefreshToken));

            if (user == null || user.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Некорректный токен доступа или токен обновления");

            var newAccessToken = _authService.CreateToken(user.Id, user.Email, refreshTokenParams.LifeTime);
            var newRefreshToken = await _authService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(refreshTokenParams.LifeTime);
            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                Session = new
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken,
                }
            });
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
            await CountTasksAsync(userDto, userFromDb.Id);
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

        /// <summary>
        /// Set profile photo
        /// </summary>
        /// <param name="photo">photo file</param>
        /// <returns></returns>
        [HttpPost("set_photo"), Authorize]
        public async Task<IActionResult> SetPhoto(IFormFile photo)
        {
            var fileExt = ("." + photo.FileName.Split('.')[^1]).ToLower();
            if (!fileExt.Equals(".png") && !fileExt.Equals(".jpeg") && !fileExt.Equals(".jpg"))
                return BadRequest($"{fileExt} is incorrect file format");

            var user = await _userManager.FindByIdAsync(UserId);
            _fileService.UploadPhoto(photo, user);
            await _dataContextService.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Обновление пароля
        /// </summary>
        /// <returns></returns>
        [HttpPatch("update_password/{code}"), Authorize]
        public async Task<IActionResult> UpdatePassword([FromBody] UserForPasswordUpdateDto passwordForUpdateDto)
        {
            if (passwordForUpdateDto == null)
            {
                _logger.LogError("Отсутствет пароль для изменения");
                return BadRequest("Отсутствет пароль для изменения");
            }

            var user = await _userManager.FindByEmailAsync(Email);

            if (!string.IsNullOrEmpty(user.PasswordCode) && user.PasswordCode.Equals(passwordForUpdateDto.ConfirmationCode))
            {
                // Generate the reset token (this would generally be sent out as a query parameter as part of a 'reset' link in an email)
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                // Use the reset token to verify the provenance of the reset request and reset the password.
                var updateResult = await _userManager.ResetPasswordAsync(user, resetToken, passwordForUpdateDto.NewPassword);

                if (!updateResult.Succeeded)
                {
                    foreach (var error in updateResult.Errors)
                        ModelState.TryAddModelError(error.Code, error.Description);

                    return BadRequest(ModelState);
                }

                return NoContent();
            }
            else
                return BadRequest("Не найден код для подтверждения сброса");
        }

        /// <summary>
        /// Подтверждение сброса пароля
        /// </summary>
        /// <returns></returns>
        [HttpPost("confirm_password_reset"), Authorize]
        public async Task<IActionResult> ConfirmPasswordReset()
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
                return BadRequest($"Пользователь с электронной почтой {Email} не найден");

            var code = _emailService.GenerateCode();
            await _emailService.SendEmailAsync(user.Email, "Код подтверждения для изменения пароля", $"Введите следующий код, чтобы изменить пароль: {code}");
            user.PasswordCode = code;
            await _userManager.UpdateAsync(user);

            return Ok();
        }

        [NonAction]
        private async Task<UserDto> CountTasksAsync(UserDto userDto, string userId)
        {
            var userTasks = await _dataContextService.GetUserTasksAsync(userId);
            userDto.InProgressTasks = userTasks.Where(t => t.Status.StatusName == "InProgress").ToList().Count;
            userDto.FrozenTasks = userTasks.Where(t => t.Status.StatusName == "Frozen").ToList().Count;
            userDto.ClosedTasks = userTasks.Where(t => t.Status.StatusName == "Closed").ToList().Count;
            return userDto;
        }
    }
}
