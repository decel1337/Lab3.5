using Lab35.Bll;
using Moq;

namespace Lab35.Tests;

public class SpecialistTests
{
    private static IInternetConnection Connection(bool available)
    {
        var connection = new Mock<IInternetConnection>();
        connection.Setup(c => c.IsAvailable).Returns(available);
        return connection.Object;
    }

    [Fact]
    public void Teacher_ValidData_SetsSubject()
    {
        // Arrange + Act
        var teacher = new Teacher("Шевченко", "Ірина", "Математика");

        // Assert
        Assert.Equal("Математика", teacher.Subject);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Teacher_EmptySubject_Throws(string? subject)
    {
        // Act
        var act = () => new Teacher("Шевченко", "Ірина", subject);

        // Assert
        Assert.Throws<ValidationException>(act);
    }

    [Fact]
    public void Teacher_StudyOnline_WithInternet_Succeeds()
    {
        // Arrange
        var teacher = new Teacher("Шевченко", "Ірина", "Математика");

        // Act
        var result = teacher.StudyOnline(Connection(true));

        // Assert
        Assert.Equal("Ірина Шевченко навчається онлайн", result);
    }

    [Fact]
    public void Teacher_StudyOnline_WithoutInternet_Throws()
    {
        // Arrange
        var teacher = new Teacher("Шевченко", "Ірина", "Математика");

        // Act
        var act = () => teacher.StudyOnline(Connection(false));

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void Teacher_Sing_ReturnsStringWithName()
    {
        // Arrange
        var teacher = new Teacher("Шевченко", "Ірина", "Математика");

        // Act
        var result = teacher.Sing();

        // Assert
        Assert.Equal("Ірина Шевченко співає", result);
    }

    [Fact]
    public void Astronaut_DefaultMissions_IsZero()
    {
        // Arrange + Act
        var astronaut = new Astronaut("Каденюк", "Леонід");

        // Assert
        Assert.Equal(0, astronaut.MissionsCount);
    }

    [Fact]
    public void Astronaut_NegativeMissions_Throws()
    {
        // Act
        var act = () => new Astronaut("Каденюк", "Леонід", -1);

        // Assert
        Assert.Throws<ValidationException>(act);
    }

    [Fact]
    public void Astronaut_StudyOnline_WithInternet_Succeeds()
    {
        // Arrange
        var astronaut = new Astronaut("Каденюк", "Леонід", 1);

        // Act
        var result = astronaut.StudyOnline(Connection(true));

        // Assert
        Assert.Equal("Леонід Каденюк навчається онлайн", result);
    }

    [Fact]
    public void Astronaut_StudyOnline_WithoutInternet_Throws()
    {
        // Arrange
        var astronaut = new Astronaut("Каденюк", "Леонід", 1);

        // Act
        var act = () => astronaut.StudyOnline(Connection(false));

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }
}
