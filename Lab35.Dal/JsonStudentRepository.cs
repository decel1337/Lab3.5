using System.Text.Encodings.Web;
using System.Text.Json;
using Lab35.Bll;

namespace Lab35.Dal;

public sealed record StudentRecord(string LastName, string FirstName, int Course, string StudentId, string BirthDate);

/// <summary>DAL: зберігання студентів у JSON-файлі.</summary>
public class JsonStudentRepository : IStudentRepository
{
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    };

    private readonly string _filePath;

    public JsonStudentRepository(string filePath)
    {
        _filePath = filePath;
    }

    public IReadOnlyList<Student> GetAll()
    {
        var records = JsonSerializer.Deserialize<List<StudentRecord>>(File.ReadAllText(_filePath), Options)
                      ?? new List<StudentRecord>();

        return records
            .Select(r => new Student(r.LastName, r.FirstName, r.Course, r.StudentId, r.BirthDate))
            .ToList();
    }

    public void SaveAll(IEnumerable<Student> students)
    {
        var records = students.Select(s =>
            new StudentRecord(s.LastName, s.FirstName, s.Course, s.StudentId, s.BirthDateText));

        File.WriteAllText(_filePath, JsonSerializer.Serialize(records, Options));
    }
}
