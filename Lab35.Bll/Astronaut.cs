namespace Lab35.Bll;

public class Astronaut : Specialist
{
    public Astronaut(string? lastName, string? firstName, int missionsCount = 0) : base(lastName, firstName)
    {
        if (missionsCount < 0)
        {
            throw new ValidationException("Кількість місій не може бути від'ємною");
        }

        MissionsCount = missionsCount;
    }

    public int MissionsCount { get; }
}
