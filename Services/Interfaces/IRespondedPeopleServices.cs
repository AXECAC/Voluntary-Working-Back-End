using DataBase;
namespace Services;

// Интерфейс IRespondedPeopleServices
public interface IRespondedPeopleServices
{
    Task<IBaseResponse> Update(List<RespondedPeople> respondedPeople);

    Task<IBaseResponse> Delete(int requestId);
}
