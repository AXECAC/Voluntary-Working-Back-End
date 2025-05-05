using DataBase;
namespace Services;

// Интерфейс IRespondedPeopleServices
public interface IRespondedPeopleServices
{
    Task<IBaseResponse> EditRespondedPeople(List<RespondedPeople> respondedPeoples);
}
