using Microsoft.EntityFrameworkCore;
using Context;
using DataBase;
namespace Services;


// Класс RespondedPeopleServices
public class RespondedPeopleServices : IRespondedPeopleServices
{
    private readonly IRespondedPeopleRepository _RespondedPeopleRepository;

    public RespondedPeopleServices(IRespondedPeopleRepository respondedPeopleRepository)
    {
        _RespondedPeopleRepository = respondedPeopleRepository;
    }

    public async Task<IBaseResponse> EditRespondedPeople(List<RespondedPeople> respondedPeoples)
    {
        BaseResponse response;

        var test = respondedPeoples[0].RequestId;
        // Находим старые RespondedPeople
        var tryDel = await _RespondedPeopleRepository
            .GetQueryable()
            .Where(rp => rp.RequestId == test)
            .ToListAsync();

        if (tryDel != null)
        {
            // Удаляем старые RespondedPeople
            await _RespondedPeopleRepository.Delete(tryDel);
        }

        // Добавляем новые RespondedPeople
        await _RespondedPeopleRepository.Create(respondedPeoples);

        // NoContent (204)
        response = BaseResponse.NoContent("Successed");
        return response;
    }

}

