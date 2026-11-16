namespace Lab35.Bll;

internal static class Guard
{
    public static string NotEmpty(string? value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ValidationException($"{fieldName} не може бути порожнім");
        }

        return value.Trim();
    }
}
