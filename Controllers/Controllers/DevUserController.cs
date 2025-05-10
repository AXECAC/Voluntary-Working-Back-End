using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Extentions;

namespace Controllers.DevUserController
{
    [Route("api/[controller]/[action]")]
    [Authorize(Roles = "Dev")]
    [ApiController]
    [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden)]
    [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
    // DevUserController класс controller
    public class DevUserController : Controller
    {
        private readonly IDevUserServices _DevUserServices;
        public DevUserController(IDevUserServices devUserServices)
        {
            _DevUserServices = devUserServices;
        }

        [HttpPut]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status204NoContent)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PromoteToAdmin(string userEmail)
        {
            // userEmail not ValidEmail (Плохой ввод)
            if (!userEmail.IsValidEmail())
            {
                return UnprocessableEntity();
            }

            // Выдаем роль Admin по почте
            var response = await _DevUserServices.PromoteToAdmin(userEmail);

            if (response.StatusCode == DataBase.StatusCodes.BadRequest)
            {
                return BadRequest();
            }

            if (response.StatusCode == DataBase.StatusCodes.NotFound)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status204NoContent)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DemoteToStudent(string userEmail)
        {
            // userEmail not ValidEmail (Плохой ввод)
            if (!userEmail.IsValidEmail())
            {
                return UnprocessableEntity();
            }

            // Меняем роль на Student по почте
            var response = await _DevUserServices.DemoteToStudent(userEmail);

            if (response.StatusCode == DataBase.StatusCodes.BadRequest)
            {
                return BadRequest();
            }

            if (response.StatusCode == DataBase.StatusCodes.NotFound)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
