using PontoAll.WebAPI.Objects.Dtos.Entities;

namespace PontoAll.WebAPI.Objects.Utils
{
    public static class EmailValidator
    {
        public static bool IsValidEmail(string email)
        {
            if(string.IsNullOrWhiteSpace(email)) { return false; }

            int atPosition = email.IndexOf('@');


            bool hasTextBeforeAt = atPosition > 0;
            bool hasTextAfterAt = atPosition >= 0 && email.LastIndexOf('@') < email.Length - 1;
            bool hasDotAfterAt = atPosition >= 0 && email.LastIndexOf('.') > atPosition;
            bool endsWithDot = email.EndsWith('.');
            bool hasSingleAt = email.Count(c => c == '@') == 1;

            return hasSingleAt &&
                   hasTextBeforeAt &&
                   hasTextAfterAt &&
                   hasDotAfterAt &&
                   !endsWithDot;
        }
    }
}
