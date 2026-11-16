namespace Lab35.Bll;

public class Teacher : Specialist
{
    public Teacher(string? lastName, string? firstName, string? subject) : base(lastName, firstName)
    {
        Subject = Guard.NotEmpty(subject, "Предмет");
    }

    public string Subject { get; }
}
