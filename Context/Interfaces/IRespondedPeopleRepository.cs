using DataBase;

namespace Context;

// Interface IRespondedPeopleRepository
public interface IRespondedPeopleRepository : IBaseRepository<RespondedPeople>
{
    // Удалить List<RespondedPeople>
    Task<bool> Delete(List<RespondedPeople> entity);
}
