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

    public async Task<IBaseResponse> Update(List<RespondedPeople> respondedPeople)
    {
        BaseResponse response;

        var requestId = respondedPeople[0].RequestId;
        // Находим старые RespondedPeople
        var tryDel = await _RespondedPeopleRepository
            .GetQueryable()
            .Where(rp => rp.RequestId == requestId)
            .ToListAsync();

        if (tryDel != null)
        {
            // Удаляем старые RespondedPeople
            await _RespondedPeopleRepository.Delete(tryDel);
        }

        // Добавляем новые RespondedPeople
        await _RespondedPeopleRepository.Create(respondedPeople);

        // NoContent (204)
        response = BaseResponse.NoContent("Successed");
        return response;
    }

    public async Task<IBaseResponse> Delete(int requestId)
    {
        BaseResponse response;

        // Находим старые RespondedPeople
        var tryDel = await _RespondedPeopleRepository
            .GetQueryable()
            .Where(rp => rp.RequestId == requestId)
            .ToListAsync();

        if (tryDel != null)
        {
            // Удаляем старые RespondedPeople
            await _RespondedPeopleRepository.Delete(tryDel);
        }
        // NoContent (204)
        response = BaseResponse.NoContent("Successed");
        return response;
    }

}

