using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DataBase;
using Services;
using Extentions;

namespace Controllers.AdminRequestController
{
    [Route("api/[controller]")]
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
        [HttpGet("[action]")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AdminFeed()
        {
            var response = await _AdminRequestServices.Get();

            // Нет requests
            if (response.StatusCode == DataBase.StatusCodes.NoContent)
            {
                // Вернуть response (204)
                return NoContent();
            }
            // Вернуть response 200
            return Ok(response.Data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Get(int id)
        {
            // Id валидация (Плохой ввод)
            if (id < 1)
            {
                // Вернуть StatusCode 422
                return UnprocessableEntity();
            }
            var response = await _AdminRequestServices.Get(id);

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

        [HttpGet("[action]")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByAdminId(int id)
        {
            var response = await _AdminRequestServices.GetByAdminId(id);

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

        [HttpGet("[action]")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByPointNumber(int pointNumber)
        {
            var response = await _AdminRequestServices.GetByPointNumber(pointNumber);

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

        [HttpGet("[action]")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByNeededPeopleNumber(int neededPeopleNumber)
        {
            var response = await _AdminRequestServices.GetByNeededPeopleNumber(neededPeopleNumber);

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

        [HttpGet("[action]")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByAddress(string address)
        {
            var response = await _AdminRequestServices.GetByAddress(address);

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

        [HttpGet("[action]")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDTBegin(DateTime dateOfBegin)
        {
            var response = await _AdminRequestServices.GetDTBegin(dateOfBegin);

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

        [HttpGet("[action]")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDT(DateTime dateOfBegin)
        {
            var response = await _AdminRequestServices.GetDT(dateOfBegin);

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

        [HttpGet("[action]")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDTDeadLine(DateTime dateOfDeadLine)
        {
            var response = await _AdminRequestServices.GetDTDeadLine(dateOfDeadLine);

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

        [HttpGet("[action]")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCompleted()
        {
            var response = await _AdminRequestServices.GetCompleted();

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

        [HttpGet("[action]")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFailed()
        {
            var response = await _AdminRequestServices.GetFailed();

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
        public async Task<IActionResult> Create(PrivateRequest request)
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
            await _AdminRequestServices.Create(crRequest);

            // Return response 200
            return CreatedAtAction(nameof(crRequest), "Successed");
        }

        [HttpPut]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status201Created)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Update(PrivateRequest request)
        {
            Request crRequest = request.ToRequest();
            // Проверка request на валидность
            if (!crRequest.IsValid() || !(crRequest.NeededPeopleNumber >= request.RespondedPeople.Count)
                    || request.RespondedPeople.Exists(x => x < 1))
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
            response = await _AdminRequestServices.Update(crRequest);

            // Получилось изменить
            if (response.StatusCode == DataBase.StatusCodes.Created)
            {
                // Меняем RespondedPeople
                if (request.RespondedPeople.Count > 0)
                {
                    List<RespondedPeople> respondedPeoples = new List<RespondedPeople>();
                    respondedPeoples.Generate(request.RespondedPeople, request.Id);

                    response = await _RespondedPeopleServices.Update(respondedPeoples);
                }

                // Вернуть response 200
                return CreatedAtAction(nameof(crRequest), "Successed");
            }

            // Нет такого запроса
            // Вернуть response (404)
            return NotFound();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Delete(int id)
        {
            // Id валидация (Плохой ввод)
            if (id < 1)
            {
                // Вернуть StatusCode 422
                return UnprocessableEntity();
            }
            var response = await _AdminRequestServices.Delete(id);

            // Есть request
            if (response.StatusCode == DataBase.StatusCodes.NoContent)
            {
                await _RespondedPeopleServices.Delete(id);
                // Вернуть response 204
                return NoContent();
            }

            // Нет request
            // Вернуть response (404)
            return NotFound();
        }

        [HttpPut("[action]")]
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
