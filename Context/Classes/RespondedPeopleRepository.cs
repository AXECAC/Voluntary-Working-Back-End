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

    // Удалить сущности из Db
    public async Task<bool> Delete(List<RespondedPeople> entity)
    {
        Db.RemoveRange(entity);
        await Db.SaveChangesAsync();

        return true;
    }

    // Создать сущность в Db 
    public async Task<bool> Create(List<RespondedPeople> entity)
    {
        await Db.AddRangeAsync(entity);
        await Db.SaveChangesAsync();
        return true;
    }
}
