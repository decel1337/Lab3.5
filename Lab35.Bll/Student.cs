using System.Globalization;

namespace Lab35.Bll;

public class Student : Person
{
    public const int MinCourse = 1;
    public const int MaxCourse = 6;
    public const string BirthDateFormat = "dd-MM yyyy";

    /// <param name="birthDate">Дата народження у форматі "ДД-ММ РРРР", наприклад "15-07 2004".</param>
    public Student(string? lastName, string? firstName, int course, string? studentId, string? birthDate)
        : base(lastName, firstName)
    {
        if (course is < MinCourse or > MaxCourse)
        {
            throw new ValidationException($"Курс має бути від {MinCourse} до {MaxCourse}");
        }

        if (!DateTime.TryParseExact(birthDate?.Trim(), BirthDateFormat, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var parsed))
        {
            throw new ValidationException("Дата народження має бути існуючою датою у форматі \"ДД-ММ РРРР\"");
        }

        Course = course;
        StudentId = Guard.NotEmpty(studentId, "Студентський квиток");
        BirthDate = parsed;
    }

    public int Course { get; private set; }

    public string StudentId { get; }

    public DateTime BirthDate { get; }

    public string BirthDateText => BirthDate.ToString(BirthDateFormat, CultureInfo.InvariantCulture);

    public bool IsBornInSummer() => BirthDate.Month is >= 6 and <= 8;

    /// <summary>Переведення на наступний курс (курсів не більше 6).</summary>
    public void PromoteToNextCourse()
    {
        if (Course >= MaxCourse)
        {
            throw new ValidationException($"Студент уже навчається на останньому курсі ({MaxCourse})");
        }

        Course++;
    }
}
