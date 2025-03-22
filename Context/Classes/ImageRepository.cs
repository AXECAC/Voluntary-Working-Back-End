using DataBase;
namespace Context;

// Класс ImageRepository
public class ImageRepository : BaseRepository<Image>, IImageRepository
{
    private readonly VoluntaryWorkingDbContext Db;
    public ImageRepository(VoluntaryWorkingDbContext db) : base(db)
    {
        Db = db;
    }
}
