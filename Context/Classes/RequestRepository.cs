using DataBase;
namespace Context;

// Класс UserRepository
public class RequestRepository : BaseRepository<Request>, IRequestRepository
{
    private readonly VoluntaryWorkingDbContext Db;
    public RequestRepository(VoluntaryWorkingDbContext db) : base(db)
    {
        Db = db;
    }
}
