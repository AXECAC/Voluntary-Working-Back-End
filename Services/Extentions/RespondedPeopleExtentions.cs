using DataBase;

namespace Extentions;

// Класс RespondedPeopleExtentions
public static class RespondedPeopleExtentions
{
    public static List<RespondedPeople> Generate(this RespondedPeople rP, List<int> users, int requestId)
    {
        List<RespondedPeople> respondedPeople = new List<RespondedPeople>();

        rP.RequestId = requestId;
        for (int i = 0; i < users.Count; i++)
        {
            rP.UserId = users[i];
            respondedPeople.Add(rP);
        }

        return respondedPeople;
    }
}
