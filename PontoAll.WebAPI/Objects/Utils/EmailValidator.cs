namespace PontoAll.WebAPI.Objects.Utils;

public class EmailValidator
{
    public static int CheckValidEmail(string email)
    {
        int atCount = email.Count(c => c == '@');
        bool hasTextBeforeAt = email.IndexOf('@') > 0;
        bool hasTextAfterAt = email.LastIndexOf('@') < email.Length - 1;

        int atPosition = email.IndexOf('@');
        bool hasDotAfterAt = atPosition >= 0 && email.IndexOf('.', atPosition) > atPosition;
        bool endsWithDot = email.EndsWith('.');

        if (atCount != 1) return -1;
        else if (!hasTextBeforeAt) return -1;
        else if (!hasTextAfterAt) return -2;
        else if (!hasDotAfterAt) return -2;
        else if (endsWithDot) return -1;

        return 1;
    }
}