using Lab35.Bll;

namespace Lab35.Tests;

public class StudentTests
{
    private static Student Create(
        string? lastName = "Коваленко",
        string? firstName = "Олег",
        int course = 3,
        string? studentId = "КВ-001",
        string? birthDate = "15-07 2004") =>
        new(lastName, firstName, course, studentId, birthDate);

    [Fact]
    public void Constructor_ValidData_SetsProperties()
    {
        // Arrange + Act
        var student = Create();

        // Assert
        Assert.Equal("Коваленко", student.LastName);
        Assert.Equal("Олег", student.FirstName);
        Assert.Equal(3, student.Course);
        Assert.Equal("КВ-001", student.StudentId);
        Assert.Equal(new DateTime(2004, 7, 15), student.BirthDate);
        Assert.Equal("15-07 2004", student.BirthDateText);
    }

    [Fact]
    public void Constructor_TrimsSpaces()
    {
        // Arrange + Act
        var student = Create(lastName: "  Бондар ", firstName: " Ян ", studentId: " КВ-9 ", birthDate: " 01-02 2000 ");

        // Assert
        Assert.Equal("Бондар", student.LastName);
        Assert.Equal("Ян", student.FirstName);
        Assert.Equal("КВ-9", student.StudentId);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_EmptyLastName_Throws(string? lastName)
    {
        // Act
        var act = () => Create(lastName: lastName);

        // Assert
        Assert.Throws<ValidationException>(act);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_EmptyFirstName_Throws(string? firstName)
    {
        // Act
        var act = () => Create(firstName: firstName);

        // Assert
        Assert.Throws<ValidationException>(act);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_EmptyStudentId_Throws(string? studentId)
    {
        // Act
        var act = () => Create(studentId: studentId);

        // Assert
        Assert.Throws<ValidationException>(act);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(7)]
    public void Constructor_CourseOutOfRange_Throws(int course)
    {
        // Act
        var act = () => Create(course: course);

        // Assert
        Assert.Throws<ValidationException>(act);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(6)]
    public void Constructor_BoundaryCourse_IsAccepted(int course)
    {
        // Act
        var student = Create(course: course);

        // Assert
        Assert.Equal(course, student.Course);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("15.07.2004")]
    [InlineData("2004-07-15")]
    [InlineData("1-7 2004")]
    [InlineData("31-02 2004")]
    [InlineData("29-02 2003")]
    [InlineData("10-13 2004")]
    public void Constructor_InvalidBirthDate_Throws(string? birthDate)
    {
        // Act
        var act = () => Create(birthDate: birthDate);

        // Assert
        Assert.Throws<ValidationException>(act);
    }

    [Fact]
    public void Constructor_LeapDay_IsAccepted()
    {
        // Act
        var student = Create(birthDate: "29-02 2004");

        // Assert
        Assert.Equal(new DateTime(2004, 2, 29), student.BirthDate);
    }

    [Theory]
    [InlineData("31-05 2004", false)]
    [InlineData("01-06 2004", true)]
    [InlineData("15-07 2004", true)]
    [InlineData("31-08 2004", true)]
    [InlineData("01-09 2004", false)]
    [InlineData("20-12 2004", false)]
    public void IsBornInSummer_ReturnsExpected(string birthDate, bool expected)
    {
        // Arrange
        var student = Create(birthDate: birthDate);

        // Act
        var result = student.IsBornInSummer();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1, 2)]
    [InlineData(5, 6)]
    public void PromoteToNextCourse_IncreasesCourseByOne(int before, int after)
    {
        // Arrange
        var student = Create(course: before);

        // Act
        student.PromoteToNextCourse();

        // Assert
        Assert.Equal(after, student.Course);
    }

    [Fact]
    public void PromoteToNextCourse_OnSixthCourse_ThrowsAndKeepsCourse()
    {
        // Arrange
        var student = Create(course: Student.MaxCourse);

        // Act
        var act = () => student.PromoteToNextCourse();

        // Assert
        Assert.Throws<ValidationException>(act);
        Assert.Equal(Student.MaxCourse, student.Course);
    }

    [Fact]
    public void Sing_ReturnsStringWithName()
    {
        // Arrange
        var student = Create();

        // Act
        var result = student.Sing();

        // Assert
        Assert.Equal("Олег Коваленко співає", result);
    }
}
