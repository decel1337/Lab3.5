using Lab35.Bll;
using Moq;

namespace Lab35.Tests;

public class StudentServiceTests
{
    private static Student S(string id, int course, string birthDate) =>
        new("Тест", "Студент", course, id, birthDate);

    private static Mock<IStudentRepository> RepositoryWith(params Student[] students)
    {
        var repository = new Mock<IStudentRepository>();
        repository.Setup(r => r.GetAll()).Returns(students);
        return repository;
    }

    [Fact]
    public void Constructor_NullRepository_Throws()
    {
        // Act
        var act = () => new StudentService(null!);

        // Assert
        Assert.Throws<ArgumentNullException>(act);
    }

    [Fact]
    public void CountThirdYearBornInSummer_CountsOnlyMatchingStudents()
    {
        // Arrange
        var repository = RepositoryWith(
            S("1", 3, "15-07 2004"), // підходить
            S("2", 3, "01-06 2004"), // підходить
            S("3", 3, "20-12 2003"), // не літо
            S("4", 2, "10-08 2005"), // не 3-й курс
            S("5", 3, "31-08 2004")); // підходить
        var service = new StudentService(repository.Object);

        // Act
        var count = service.CountThirdYearBornInSummer();

        // Assert
        Assert.Equal(3, count);
    }

    [Fact]
    public void GetThirdYearBornInSummer_ReturnsMatchingStudents()
    {
        // Arrange
        var repository = RepositoryWith(S("1", 3, "15-07 2004"), S("2", 1, "15-07 2004"));
        var service = new StudentService(repository.Object);

        // Act
        var result = service.GetThirdYearBornInSummer();

        // Assert
        Assert.Equal(new[] { "1" }, result.Select(s => s.StudentId));
    }

    [Fact]
    public void CountThirdYearBornInSummer_EmptyRepository_ReturnsZero()
    {
        // Arrange
        var service = new StudentService(RepositoryWith().Object);

        // Act
        var count = service.CountThirdYearBornInSummer();

        // Assert
        Assert.Equal(0, count);
    }

    [Fact]
    public void PromoteStudent_IncreasesCourseAndSaves()
    {
        // Arrange
        var repository = RepositoryWith(S("1", 2, "15-07 2004"), S("2", 4, "15-07 2004"));
        var service = new StudentService(repository.Object);

        // Act
        var promoted = service.PromoteStudent("1");

        // Assert
        Assert.Equal(3, promoted.Course);
        repository.Verify(r => r.SaveAll(It.Is<IEnumerable<Student>>(
            all => all.Select(s => s.Course).SequenceEqual(new[] { 3, 4 }))), Times.Once);
    }

    [Fact]
    public void PromoteStudent_UnknownId_ThrowsAndDoesNotSave()
    {
        // Arrange
        var repository = RepositoryWith(S("1", 2, "15-07 2004"));
        var service = new StudentService(repository.Object);

        // Act
        var act = () => service.PromoteStudent("999");

        // Assert
        Assert.Throws<ValidationException>(act);
        repository.Verify(r => r.SaveAll(It.IsAny<IEnumerable<Student>>()), Times.Never);
    }

    [Fact]
    public void PromoteStudent_OnSixthCourse_ThrowsAndDoesNotSave()
    {
        // Arrange
        var repository = RepositoryWith(S("1", 6, "15-07 2004"));
        var service = new StudentService(repository.Object);

        // Act
        var act = () => service.PromoteStudent("1");

        // Assert
        Assert.Throws<ValidationException>(act);
        repository.Verify(r => r.SaveAll(It.IsAny<IEnumerable<Student>>()), Times.Never);
    }
}
