using DataBase;
namespace Context;

// Класс UserRepository
public class RespondedPeopleRepository : BaseRepository<RespondedPeople>, IRespondedPeopleRepository
{
    private readonly VoluntaryWorkingDbContext Db;
    public RespondedPeopleRepository(VoluntaryWorkingDbContext db) : base(db)
    {
        Db = db;
    }
}
