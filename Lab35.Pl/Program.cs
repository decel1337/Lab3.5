using Lab35.Bll;
using Lab35.Dal;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var filePath = args.Length > 0 ? args[0] : Path.Combine(AppContext.BaseDirectory, "students.json");
var service = new StudentService(new JsonStudentRepository(filePath));

var students = service.GetThirdYearBornInSummer();
Console.WriteLine($"Студентів 3-го курсу, народжених улітку: {students.Count}");
foreach (var s in students)
{
    Console.WriteLine($"  {s.LastName} {s.FirstName}, квиток {s.StudentId}, {s.BirthDateText}");
}

var online = new AlwaysOnline();
var teacher = new Teacher("Шевченко", "Ірина", "Математика");
var astronaut = new Astronaut("Каденюк", "Леонід", 1);
Console.WriteLine(teacher.StudyOnline(online));
Console.WriteLine(astronaut.StudyOnline(online));
Console.WriteLine(teacher.Sing());

internal sealed class AlwaysOnline : IInternetConnection
{
    public bool IsAvailable => true;
}
