namespace Lab35.Bll;

/// <summary>Контракт DAL. BLL залежить лише від інтерфейсу (Dependency Injection).</summary>
public interface IStudentRepository
{
    IReadOnlyList<Student> GetAll();

    void SaveAll(IEnumerable<Student> students);
}
