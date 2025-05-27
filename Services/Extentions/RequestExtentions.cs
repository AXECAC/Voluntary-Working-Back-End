using DataBase;

namespace Extentions;

// Класс RequestExtentions
public static class RequestExtentions
{
    // Валидация Request
    public static bool IsValid(this Request request)
    {
        if (request.Id < 0 || request.AdminId < 0 || request.Address == "" ||
                !request.Date.IsValidDate(request.DeadLine) || !request.PointNumber.IsValidPointNumber() ||
                request.NeededPeopleNumber < 0 || request.Description == "")
        {
            return false;
        }
        return true;
    }

    // Валидация полей сущности Request: Date и DeadLine
    private static bool IsValidDate(this DateTime date, DateTime deadLine){
        if(date == null || deadLine == null || deadLine <= date){
            return false;
        }
        return true;
    }

    // Валидация поля сущности Request: PointNumber
    private static bool IsValidPointNumber(this int pointNumber){
        if(pointNumber < 1 || pointNumber > 3){
            return false;
        }
        return true;
    }
}
