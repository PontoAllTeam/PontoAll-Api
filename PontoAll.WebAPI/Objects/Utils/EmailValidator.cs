namespace PontoAll.WebAPI.Objects.Utils;

public class EmailValidator
{
    public static bool IsValidEmail(string email)
    {
        int atPosition = email.IndexOf('@');
        int atCount = email.Count(c => c == '@');

        bool hasTextBeforeAt = atPosition > 0;
        bool hasTextAfterAt = email.LastIndexOf('@') < email.Length - 1;
        bool hasDotAfterAt = atPosition >= 0 && email.IndexOf('.', atPosition) > atPosition;
        bool endsWithDot = email.EndsWith('.');
        bool hasSingleAt = atCount != 1;

        return hasSingleAt ||
               hasTextBeforeAt ||
               hasTextAfterAt ||
               hasDotAfterAt ||
               !endsWithDot;
    }
}