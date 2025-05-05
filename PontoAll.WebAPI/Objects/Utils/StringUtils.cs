using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace PontoAll.WebAPI.Objects.Utils;

public static class StringUtils
{
    public static string NormalizeText(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return text;

        var normalizedString = text.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }

    public static string ExtractNumbers(this string text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        return new string(text.Where(char.IsDigit).ToArray());
    }

    public static bool CompareString(string str1, string str2)
	{
		return string.Equals(str1.NormalizeText(), str2.NormalizeText(), StringComparison.OrdinalIgnoreCase);
	}

    public static string HashString(this string text)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}