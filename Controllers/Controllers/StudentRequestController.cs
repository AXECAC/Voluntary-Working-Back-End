using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Services;

namespace Controllers.StudentRequestController
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
    // StudentRequestController класс контроллер
    public class StudentRequestController : Controller
    {
        private readonly IStudentRequestServices _StudentRequestServices;

        public StudentRequestController(IStudentRequestServices studentRequestServices)
        {
            _StudentRequestServices = studentRequestServices;
        }
        // PublicFeed --- лента пользователей
        [HttpGet]
        public async Task<IActionResult> PublicFeed()
        {
            var response = await _StudentRequestServices.GetRequests();

            // Нет запросов
            if (response.StatusCode == DataBase.StatusCodes.NoContent)
            {
                // Вернуть response (204)
                return NoContent();
            }
            // Вернуть response 200
            return Ok(response.Data);
        }

        [HttpPut]
        public async Task<IActionResult> AssignMe(int requestId)
        {
            // Если requestId < 1 => не валидный Id
            if (requestId < 1)
            {
                // Вернуть response (422)
                return UnprocessableEntity();
            }
            var response = await _StudentRequestServices.AssignMe(requestId);

            // Нет запросов
            if (response.StatusCode == DataBase.StatusCodes.NotFound)
            {
                // Вернуть response (404)
                return NotFound();
            }
            if (response.StatusCode == DataBase.StatusCodes.BadRequest)
            {
                // Вернуть response (400)
                return BadRequest();
            }

            // Вернуть response 204
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> UnassignMe(int requestId)
        {
            // Если requestId < 1 => не валидный Id
            if (requestId < 1)
            {
                // Вернуть response (422)
                return UnprocessableEntity();
            }
            var response = await _StudentRequestServices.UnassignMe(requestId);

            // Нет запросов
            if (response.StatusCode == DataBase.StatusCodes.NotFound)
            {
                // Вернуть response (404)
                return NotFound();
            }
            if (response.StatusCode == DataBase.StatusCodes.BadRequest)
            {
                // Вернуть response (400)
                return BadRequest();
            }

            // Вернуть response 204
            return NoContent();
        }
    }
}
