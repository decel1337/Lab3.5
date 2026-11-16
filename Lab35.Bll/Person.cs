namespace Lab35.Bll;

public abstract class Person
{
    protected Person(string? lastName, string? firstName)
    {
        LastName = Guard.NotEmpty(lastName, "Прізвище");
        FirstName = Guard.NotEmpty(firstName, "Ім'я");
    }

    public string LastName { get; }

    public string FirstName { get; }

    public string Sing() => $"{FirstName} {LastName} співає";
}
