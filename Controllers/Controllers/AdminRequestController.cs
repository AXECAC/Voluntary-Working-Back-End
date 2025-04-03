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

            // Нет requests
            if (response.StatusCode == DataBase.StatusCodes.NoContent)
            {
                // Вернуть response (204)
                return NoContent();
            }
            // Вернуть response 200
            return Ok(response.Data);
        }

        [HttpGet]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> GetRequest(int id)
        {
            // Id валидация (Плохой ввод)
            if (id < 1)
            {
                // Вернуть StatusCode 422
                return UnprocessableEntity();
            }
            var response = await _AdminRequestServices.GetRequest(id);

            // Есть request
            if (response.StatusCode == DataBase.StatusCodes.Ok)
            {
                // Вернуть response 200
                return Ok(response.Data);
            }

            // Нет request
            // Вернуть response (404)
            return NotFound();
        }

        [HttpGet]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRequestsByAdminId(int id)
        {
            var response = await _AdminRequestServices.GetRequestsByAdminId(id);

            // Есть requests
            if (response.StatusCode == DataBase.StatusCodes.Ok)
            {
                // Вернуть response 200
                return Ok(response.Data);
            }

            // Нет requests
            // Вернуть response (404)
            return NotFound();
        }

        [HttpGet]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRequestsByPointNumber(int pointNumber)
        {
            var response = await _AdminRequestServices.GetRequestsByPointNumber(pointNumber);

            // Есть requests
            if (response.StatusCode == DataBase.StatusCodes.Ok)
            {
                // Вернуть response 200
                return Ok(response.Data);
            }

            // Нет requests
            // Вернуть response (404)
            return NotFound();
        }

        [HttpGet]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRequestsByNeededPeopleNumber(int neededPeopleNumber)
        {
            var response = await _AdminRequestServices.GetRequestsByNeededPeopleNumber(neededPeopleNumber);

            // Есть requests
            if (response.StatusCode == DataBase.StatusCodes.Ok)
            {
                // Вернуть response 200
                return Ok(response.Data);
            }

            // Нет requests
            // Вернуть response (404)
            return NotFound();
        }

        // Some Post method
        [HttpPost]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status201Created)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateRequest(Request request)
        {
            // Проверка request на валидность
            if (!request.IsValid())
            {
                return UnprocessableEntity();
            }

            // Задаем Id админа/Dev-а создавашего это запрос
            request.AdminId = _AdminUserServices.GetAdminId();

            await _AdminRequestServices.CreateRequest(request);

            // Return response 200
            return CreatedAtAction(nameof(request), "Successed");
        }
    }
}
