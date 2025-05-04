using DataBase;

namespace Extentions;

// Класс RespondedPeopleExtentions
public static class RespondedPeopleExtentions
{
    public static void Generate(this List<RespondedPeople> respondedPeople, List<int> users, int requestId)
    {
        // Обнуляем
        respondedPeople = new List<RespondedPeople>();

        
        RespondedPeople tempRP = new RespondedPeople();

        tempRP.RequestId = requestId;
        for (int i = 0; i < users.Count; i++)
        {
            tempRP.UserId = users[i];
            respondedPeople.Add(tempRP);
        }
    }
}
