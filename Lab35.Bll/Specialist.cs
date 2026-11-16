namespace Lab35.Bll;

/// <summary>Спільна поведінка Teacher та Astronaut: навчання онлайн можливе лише за наявності інтернету.</summary>
public abstract class Specialist : Person
{
    protected Specialist(string? lastName, string? firstName) : base(lastName, firstName)
    {
    }

    public string StudyOnline(IInternetConnection connection)
    {
        if (!connection.IsAvailable)
        {
            throw new InvalidOperationException("Немає інтернет-з'єднання: навчання онлайн неможливе");
        }

        return $"{FirstName} {LastName} навчається онлайн";
    }
}
