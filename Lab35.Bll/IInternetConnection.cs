namespace Lab35.Bll;

/// <summary>Інтернет-з'єднання. Передається ззовні, щоб у тестах його можна було підмінити заглушкою.</summary>
public interface IInternetConnection
{
    bool IsAvailable { get; }
}
