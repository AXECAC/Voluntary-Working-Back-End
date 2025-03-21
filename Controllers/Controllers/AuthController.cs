using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using DataBase;
using Extentions;

namespace Controllers.AuthController
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    // Класс контроллер AuthController
    public class AuthController : Controller
    {
        private readonly IAuthServices _AuthServices;
        private readonly string? _secretKey;

        public AuthController(IAuthServices authServices, IConfiguration configuration)
        {
            _AuthServices = authServices;
            _secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status201Created)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status409Conflict)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        // метод Registration
        public async Task<IActionResult> Registration(User user)
        {
            // Secret key пуст
            if (_secretKey == "")
            {
                // Вернуть StatusCode 500
                return StatusCode(statusCode: 500);
            }
            // User not Valid (Плохой ввод)
            if (!user.IsValid())
            {
                // Вернуть StatusCode 422
                return UnprocessableEntity();
            }
            // Попытка Registration
            var response = await _AuthServices.TryRegister(user, _secretKey);
            // Registration успешна
            if (response.StatusCode == DataBase.StatusCodes.Created)
            {
                // Вернуть токен (201)
                return CreatedAtAction(nameof(user), new { response.Data });
            }
            // Такой email уже существует
            else
            {
                // Вернуть Conflict (409)
                return Conflict();
            }

        }
        [HttpPost]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
        // Метод Login
        public async Task<IActionResult> Login(LoginUser form)
        {
            // Secret key пуст
            if (_secretKey == "")
            {
                // Вернуть StatusCode 500
                return StatusCode(statusCode: 500);
            }
            // User not Valid (Плохой ввод)
            if (!form.IsValid())
            {
                // Вернуть StatusCode 422
                return UnprocessableEntity();
            }
            // Попытка Login
            var response = await _AuthServices.TryLogin(form, _secretKey);
            // Login успешна
            if (response.StatusCode == DataBase.StatusCodes.Ok)
            {
                // Вернуть токен (200)
                return Ok(new { response.Data });
            }
            // неправильные Email или Password 
            else
            {
                // Вернуть Conflict (401)
                return Unauthorized();
            }

        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized)]
        // Проверить токен (на валидность, не является ли он старым)(максимум 3 часа жизни)
        public IActionResult Check()
        {
            // Токен валидный, а иначе вернуть Unauthorized
            return Ok();
        }
    }
}
