using FluentAssertions;
using Shop.Application.Constants;
using Shop.Application.Users;

namespace Shop.Application.Tests.Users;

public class CurrentUserTests
{
    [Theory]
    [InlineData(UserRoles.Admin)]
    [InlineData(UserRoles.User)]
    public void IsInRole_WithMatchingRole_ShouldReturnTrue(string userRole)
    {
        // Arrange
        var currentUser = new CurrentUser(
            "1", "test@gmail.com", [UserRoles.Admin, UserRoles.User]);

        // Act
        var isInRole = currentUser.IsInRole(userRole);

        // Assert
        isInRole.Should().BeTrue();
    }

    [Fact]
    public void IsInRole_WithNotMatchingRole_ShouldReturnFalse()
    {
        // Arrange
        var currentUser = new CurrentUser("1", "test@gmail.com", []);

        // Act 
        var isInRole = currentUser.IsInRole(UserRoles.Admin);

        // Assert
        isInRole.Should().BeFalse();
    }
}
