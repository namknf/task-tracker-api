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
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="userForRegistration">Модель пользователя для регистрации</param>
        /// <response code="200">Регистрация прошла успешно</response>
        /// <response code="400">Некорректные параметры для регистрации</response>
        /// <returns>Зарегистрированный пользователь</returns>
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
        /// Авторизация "логин (почта/имя пользователя) + пароль"
        /// </summary>
        /// <param name="user">Параметры для авторизации</param>
        /// <response code="200">Авторизация прошла успешно</response>
        /// <response code="401">Некорректные параметры для авторизации</response>
        /// <returns>Токен доступа</returns>
        [HttpPost("login"), AllowAnonymous]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthorizeDto user)
        {
            if (!await _authService.IsValidUser(user))
            {
                _logger.LogWarning($"{nameof(Authenticate)}: Авторизация прошла неуспешно. Неверный логин или пароль");
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
        /// Отправка письма на почту для входа по коду
        /// </summary>
        /// <response code="200">Код был успешно отправлен</response>
        /// <response code="400">Пользователь не был найден</response>
        /// <returns></returns>
        [HttpPost("send_login_code"), AllowAnonymous]
        public async Task<IActionResult> SendCodeEmail([FromBody] UserLogInByCodeDto userDto)
        {
            var email = userDto.Email;
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest($"Пользователь с электронной почтой {email} не найден");

            var code = _emailService.GenerateCode();
            await _emailService.SendEmailAsync(user.Email, "Код подтверждения для входа в приложение", $"Введите следующий код, чтобы войти: {code}");
            user.EmailCode = code;
            await _userManager.UpdateAsync(user);

            return Ok();
        }

        /// <summary>
        /// Авторизация по коду, отправленному на почту
        /// </summary>
        /// <param name="userDto">Параметры для авторизации: почта + отправленны код</param>
        /// <response code="200">Авторизация прошла успешно</response>
        /// <response code="400">Были введены некорретные параметры</response>
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
        /// <response code="200">Обновление сессии прошло успешно</response>
        /// <response code="400">Некорректные параметры для обновления токена</response>
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
        /// Получение информации о пользователе
        /// </summary>
        /// <response code="200">Информация была успешно получена из БД</response>
        /// <response code="400">Пользователь не был найден</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <returns>Информация о пользователе</returns>
        [HttpGet("info"), Authorize]
        public async Task<ActionResult<UserDto>> GetUserInfo()
        {
            var userFromDb = await _dataContextService.GetUserInformationAsync(UserId);
            if (userFromDb == null)
            {
                _logger.LogError("Пользователь не найден");
                return BadRequest("Пользователь не найден");
            }
            var userDto = _mapper.Map<UserDto>(userFromDb);
            await CountTasksAsync(userDto, userFromDb.Id);
            return Ok(userDto);
        }

        /// <summary>
        /// Удаление аккаунта
        /// </summary>
        /// <response code="204">Аккаунт был успешно удален</response>
        /// <response code="200">Аккаунт был успешно удален</response>
        /// <response code="404">Аккаунт не найден</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <returns></returns>
        [HttpDelete("delete"), Authorize]
        public async Task<IActionResult> DeleteAccount()
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
                return NotFound("Пользователь не найден");
            else
            {
                await _userManager.DeleteAsync(user);
                return NoContent();
            }
        }

        /// <summary>
        /// Добавить фото профиля
        /// </summary>
        /// <param name="photo">Файл фотографии</param>
        /// <response code="200">Загрузка файла прошла успешно</response>
        /// <response code="400">Некорректный формат файла</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <returns></returns>
        [HttpPost("set_photo"), DisableRequestSizeLimit, Authorize]
        public async Task<IActionResult> SetPhoto([FromForm] FileForUploadDto photo)
        {
            var fileExt = ("." + photo.File.FileName.Split('.')[^1]).ToLower();
            if (!fileExt.Equals(".png") && !fileExt.Equals(".jpeg") && !fileExt.Equals(".jpg"))
                return BadRequest($"{fileExt} является некорретным форматом для фотографии");

            var user = await _userManager.FindByIdAsync(UserId);
            _fileService.UploadPhoto(photo.File, user);
            await _dataContextService.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Обновление пароля
        /// </summary>
        /// <response code="204">Пароль успешно обновлен</response>
        /// <response code="400">Отсутствет пароль для изменения или не найден код для подтверждения сброса</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <returns></returns>
        [HttpPatch("update_password"), Authorize]
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
        /// Обновление информации о пользователе
        /// </summary>
        /// <response code="200">Информация обновлена успешно</response>
        /// <response code="400">Отсутствет информация для обновления данных</response>
        /// <response code="401">Пользователь не авторизован</response>
        /// <returns></returns>
        [HttpPut("update_user_info"), Authorize]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UserForUpdateDto userForUpdateDto)
        {
            if (userForUpdateDto == null)
            {
                _logger.LogError("Отсутствет информация для обновления данных");
                return BadRequest("Отсутствет информация для обновления данных");
            }

            var user = await _userManager.FindByEmailAsync(Email);
            var updatedUser = _mapper.Map(userForUpdateDto, user);
            await _userManager.UpdateAsync(updatedUser);
            return Ok();
        }

        /// <summary>
        /// Подтверждение сброса пароля
        /// </summary>
        /// <response code="200">Письмо с кодом для подтверждения отправлено</response>
        /// <response code="400">Пользователь не был найден</response>
        /// <response code="401">Пользователь не авторизован</response>
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
