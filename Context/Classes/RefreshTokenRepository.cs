using DataBase;
namespace Context;

// Класс RefreshTokenRepository
public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
{
    private readonly VoluntaryWorkingDbContext Db;
    public RefreshTokenRepository(VoluntaryWorkingDbContext db) : base(db)
    {
        Db = db;
    }
}
