using DataBase;

namespace Extentions;

// Класс RespondedPeopleExtentions
public static class RespondedPeopleExtentions
{
    public static void Generate(this List<RespondedPeople> respondedPeople, List<int> users, int requestId)
    {
        // Обнуляем
        respondedPeople.Clear();

        // Заполняем List
        for (int i = 0; i < users.Count; i++)
        {
            respondedPeople.Add(new RespondedPeople()
            {
                RequestId = requestId,
                UserId = users[i]
            });
        }
    }
}
