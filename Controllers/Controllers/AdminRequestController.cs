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
        private readonly IUserServices _UserServices;
        private readonly IAdminRequestServices _AdminRequestServices;
        private readonly IRespondedPeopleServices _RespondedPeopleServices;

        public AdminRequestController(IUserServices userServices, IAdminRequestServices adminRequestServices,
                IRespondedPeopleServices respondedPeopleServices)
        {
            _UserServices = userServices;
            _AdminRequestServices = adminRequestServices;
            _RespondedPeopleServices = respondedPeopleServices;
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

        [HttpGet]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRequestsByAddress(string address)
        {
            var response = await _AdminRequestServices.GetRequestsByAddress(address);

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
        public async Task<IActionResult> GetRequestsDTBegin(DateTime dateOfBegin)
        {
            var response = await _AdminRequestServices.GetRequestsDTBegin(dateOfBegin);

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
        public async Task<IActionResult> GetRequestsDT(DateTime dateOfBegin)
        {
            var response = await _AdminRequestServices.GetRequestsDT(dateOfBegin);

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
        public async Task<IActionResult> GetRequestsDTDeadLine(DateTime dateOfDeadLine)
        {
            var response = await _AdminRequestServices.GetRequestsDTDeadLine(dateOfDeadLine);

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
        public async Task<IActionResult> GetRequestsCompleted()
        {
            var response = await _AdminRequestServices.GetRequestsCompleted();

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
        public async Task<IActionResult> GetRequestsFailed()
        {
            var response = await _AdminRequestServices.GetRequestsFailed();

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

        [HttpPost]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status201Created)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateRequest(PrivateRequest request)
        {
            Request crRequest = request.ToRequest();
            // Проверка request на валидность
            if (!crRequest.IsValid())
            {
                return UnprocessableEntity();
            }

            // Задаем Id админа/Dev-а создавашего это запрос
            crRequest.AdminId = _UserServices.GetMyId();

            // Создаем Request
            await _AdminRequestServices.CreateRequest(crRequest);

            // Return response 200
            return CreatedAtAction(nameof(crRequest), "Successed");
        }

        [HttpPut]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status201Created)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> EditRequest(PrivateRequest request)
        {
            Request crRequest = request.ToRequest();
            // Проверка request на валидность
            if (!crRequest.IsValid() || !(crRequest.NeededPeopleNumber >= request.RespondedPeople.Count)
                    || (request.RespondedPeople.Exists(x => x < 1)))
            {
                return UnprocessableEntity();
            }

            request.RespondedPeople.Distinct();

            var response = await _UserServices.CheckIdsValid(request.RespondedPeople);

            // Не существует как минимум одного User из Id откликнувшихся
            if (response.StatusCode == DataBase.StatusCodes.NotFound)
            {
                // Вернуть response (404)
                return NotFound();
            }

            // Задаем Id админа/Dev-а изменившего это запрос
            crRequest.AdminId = _UserServices.GetMyId();

            // Меняем Request
            response = await _AdminRequestServices.EditRequest(crRequest);

            // Получилось изменить
            if (response.StatusCode == DataBase.StatusCodes.Created)
            {
                // Меняем RespondedPeople
                if (request.RespondedPeople.Count > 0)
                {
                    List<RespondedPeople> respondedPeoples = new List<RespondedPeople>();
                    respondedPeoples.Generate(request.RespondedPeople, request.Id);

                    response = await _RespondedPeopleServices.EditRespondedPeople(respondedPeoples);
                }

                // Вернуть response 200
                return CreatedAtAction(nameof(crRequest), "Successed");
            }

            // Нет такого запроса
            // Вернуть response (404)
            return NotFound();
        }

        [HttpDelete]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            // Id валидация (Плохой ввод)
            if (id < 1)
            {
                // Вернуть StatusCode 422
                return UnprocessableEntity();
            }
            var response = await _AdminRequestServices.DeleteRequest(id);

            // Есть request
            if (response.StatusCode == DataBase.StatusCodes.NoContent)
            {
                await _RespondedPeopleServices.DeleteRespondedPeople(id);
                // Вернуть response 204
                return NoContent();
            }

            // Нет request
            // Вернуть response (404)
            return NotFound();
        }

        [HttpPut]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status204NoContent)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> MarkAsCompleted(int requestId, List<int> usersId)
        {
            // Проверка request на валидность
            if (requestId < 0)
            {
                return UnprocessableEntity();
            }
            var response = await _UserServices.CheckIdsValid(usersId);

            // Не существует как минимум одного User из Id откликнувшихся
            if (response.StatusCode == DataBase.StatusCodes.NotFound)
            {
                // Вернуть response (404)
                return NotFound();
            }

            response = await _AdminRequestServices.MarkAsCompleted(requestId, usersId);
            
            if (response.StatusCode == DataBase.StatusCodes.BadRequest)
            {
                // Вернуть response (400)
                return BadRequest();
            }
            if (response.StatusCode == DataBase.StatusCodes.NotFound)
            {
                // Вернуть response (404)
                return NotFound();
            }
            if (response.StatusCode == DataBase.StatusCodes.UnprocessableContent)
            {
                // Вернуть response (422)
                return UnprocessableEntity();
            }
            // Вернуть response (204)
            return NoContent();
        }
    }
}
