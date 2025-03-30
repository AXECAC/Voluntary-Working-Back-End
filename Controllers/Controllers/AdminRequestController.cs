using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DataBase;
using Services;
using Extentions;

namespace Controllers.AdminRequestController
{
    [Route("api/[controller]/[action]")]
    [Authorize(Roles = "Dev, Admin")]
    [ApiController]
    [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden)]
    [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
    // AdminRequestController класс контроллер
    public class AdminRequestController : Controller
    {
        private readonly IAdminUserServices _AdminUserServices;
        private readonly IAdminRequestServices _AdminRequestServices;

        public AdminRequestController(IAdminUserServices adminUserServices, IAdminRequestServices adminRequestServices)
        {
            _AdminUserServices = adminUserServices;
            _AdminRequestServices = adminRequestServices;
        }

        // AdminFeed --- лента админов
        [HttpGet]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AdminFeed()
        {
            var response = await _AdminRequestServices.GetRequests();

            // Нет запросов
            if (response.StatusCode == DataBase.StatusCodes.NoContent)
            {
                // Вернуть response (204)
                return NoContent();
            }
            // Вернуть response 200
            return Ok(response.Data);
        }

    }
}
