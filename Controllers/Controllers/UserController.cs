using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DataBase;
using Services;

namespace Controllers.UserController
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
    // UserController класс контроллер
    public class UserController : Controller
    {
        private readonly IUserServices _UserServices;
        public UserController(IUserServices userServices)
        {
            _UserServices = userServices;
        }

        [HttpGet]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMyProfile()
        {
            var response = await _UserServices.GetMyProfile();

            if (response.StatusCode == DataBase.StatusCodes.NotFound)
            {
                // Вернуть response (404)
                return NotFound();
            }

            // Вернуть response 200
            return Ok(response.Data);
        }

        [HttpGet]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMyCurrentRequests()
        {
            var response = await _UserServices.GetMyCurrentRequests();

            if (response.StatusCode == DataBase.StatusCodes.NotFound)
            {
                // Вернуть response (404)
                return NotFound();
            }

            // Вернуть response 200
            return Ok(response.Data);
        }

        [HttpPut]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeMyPassword(string newPassword)
        {
            var response = await _UserServices.ChangeMyPassword(newPassword);

            if (response.StatusCode == DataBase.StatusCodes.NotFound)
            {
                // Вернуть response (404)
                return NotFound();
            }
            // Вернуть response (204)
            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EditMyProfile(User editedUser)
        {
            var response = await _UserServices.EditMyProfile(editedUser);

            if (response.StatusCode == DataBase.StatusCodes.NotFound)
            {
                // Вернуть response (404)
                return NotFound();
            }
            // Вернуть response (204)
            return NoContent();
        }
    }
}
