using DataBase;
namespace Context;

// Класс UserRepository
public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly VoluntaryWorkingDbContext Db;
    public UserRepository(VoluntaryWorkingDbContext db) : base(db)
    {
        Db = db;
    }
}
