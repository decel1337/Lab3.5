namespace Lab35.Bll;

public class StudentService
{
    private const int TargetCourse = 3;

    private readonly IStudentRepository _repository;

    public StudentService(IStudentRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public IReadOnlyList<Student> GetThirdYearBornInSummer() =>
        _repository.GetAll()
            .Where(s => s.Course == TargetCourse && s.IsBornInSummer())
            .ToList();

    public int CountThirdYearBornInSummer() => GetThirdYearBornInSummer().Count;

    public Student PromoteStudent(string studentId)
    {
        var students = _repository.GetAll();
        var student = students.FirstOrDefault(s => s.StudentId == studentId)
                      ?? throw new ValidationException($"Студента з квитком {studentId} не знайдено");

        student.PromoteToNextCourse();
        _repository.SaveAll(students);
        return student;
    }
}
