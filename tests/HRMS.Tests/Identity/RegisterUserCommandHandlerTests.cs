using FluentAssertions;
using HRMS.Services.Identity.Application.Commands.Register;
using Xunit;
using HRMS.Services.Identity.Application.DTOs;
using HRMS.Services.Identity.Application.Events;
using HRMS.Services.Identity.Application.Interfaces;
using HRMS.Services.Identity.Domain.Entities;
using HRMS.Shared.Kernel.Common;
using MediatR;
using Moq;

namespace HRMS.Tests.Identity;

public class RegisterUserCommandHandlerTests
{
    private readonly Mock<IIdentityDbContext> _contextMock = new();
    private readonly Mock<IPasswordHasher> _passwordHasherMock = new();
    private readonly Mock<ITokenService> _tokenServiceMock = new();
    private readonly Mock<IEmailService> _emailServiceMock = new();
    private readonly Mock<IPublisher> _publisherMock = new();

    private readonly RegisterUserCommandHandler _handler;

    public RegisterUserCommandHandlerTests()
    {
        _handler = new RegisterUserCommandHandler(
            _contextMock.Object,
            _passwordHasherMock.Object,
            _tokenServiceMock.Object,
            _emailServiceMock.Object,
            _publisherMock.Object);
    }

    [Fact]
    public async Task Should_register_new_user_successfully_and_return_auth_response()
    {
        // Arrange
        var command = new RegisterUserCommand(
            "test@example.com",
            "Password123!",
            "John",
            "Doe",
            "+1234567890",
            null);

        _contextMock.Setup(c => c.EmailExistsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _passwordHasherMock.Setup(p => p.HashPassword(It.IsAny<string>()))
            .Returns("hashed_password");

        _tokenServiceMock.Setup(t => t.GenerateRefreshToken()).Returns("refresh_token_value");
        _tokenServiceMock.Setup(t => t.GenerateAccessToken(
            It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<IReadOnlyList<string>>(),
            It.IsAny<IReadOnlyList<string>>(), It.IsAny<Guid?>()))
            .Returns("access_token_value");
        _tokenServiceMock.Setup(t => t.GetAccessTokenExpiration())
            .Returns(DateTime.UtcNow.AddHours(1));
        _tokenServiceMock.Setup(t => t.GeneratePasswordResetToken()).Returns("verification_token");

        _contextMock.Setup(c => c.GetUserRoleNamesAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<string> { "User" });
        _contextMock.Setup(c => c.GetUserPermissionsAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<string> { "read" });

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.AccessToken.Should().Be("access_token_value");
        result.Value.RefreshToken.Should().Be("refresh_token_value");
        result.Value.User.Email.Should().Be("test@example.com");
        result.Value.User.FirstName.Should().Be("John");
        result.Value.User.LastName.Should().Be("Doe");
    }

    [Fact]
    public async Task Should_return_failure_when_email_already_exists()
    {
        // Arrange
        var command = new RegisterUserCommand(
            "existing@example.com",
            "Password123!",
            "John",
            "Doe",
            null,
            null);

        _contextMock.Setup(c => c.EmailExistsAsync("existing@example.com", It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Code.Should().Be("Auth.EmailAlreadyExists");
        _contextMock.Verify(c => c.AddUser(It.IsAny<ApplicationUser>()), Times.Never);
    }

    [Fact]
    public async Task Should_send_verification_email_after_registration()
    {
        // Arrange
        var command = new RegisterUserCommand(
            "test@example.com",
            "Password123!",
            "John",
            "Doe",
            null,
            null);

        _contextMock.Setup(c => c.EmailExistsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        _passwordHasherMock.Setup(p => p.HashPassword(It.IsAny<string>())).Returns("hashed");
        _tokenServiceMock.Setup(t => t.GenerateRefreshToken()).Returns("rt");
        _tokenServiceMock.Setup(t => t.GenerateAccessToken(
            It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<IReadOnlyList<string>>(),
            It.IsAny<IReadOnlyList<string>>(), It.IsAny<Guid?>()))
            .Returns("at");
        _tokenServiceMock.Setup(t => t.GetAccessTokenExpiration()).Returns(DateTime.UtcNow.AddHours(1));
        _tokenServiceMock.Setup(t => t.GeneratePasswordResetToken()).Returns("verify_token");
        _contextMock.Setup(c => c.GetUserRoleNamesAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<string>());
        _contextMock.Setup(c => c.GetUserPermissionsAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<string>());

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _emailServiceMock.Verify(e =>
            e.SendEmailVerificationAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                "verify_token",
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Should_hash_password_before_saving()
    {
        // Arrange
        var command = new RegisterUserCommand(
            "test@example.com",
            "PlainPassword123!",
            "John",
            "Doe",
            null,
            null);

        _contextMock.Setup(c => c.EmailExistsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        _passwordHasherMock.Setup(p => p.HashPassword("PlainPassword123!"))
            .Returns("HASHED_PASSWORD");
        _tokenServiceMock.Setup(t => t.GenerateRefreshToken()).Returns("rt");
        _tokenServiceMock.Setup(t => t.GenerateAccessToken(
            It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<IReadOnlyList<string>>(),
            It.IsAny<IReadOnlyList<string>>(), It.IsAny<Guid?>()))
            .Returns("at");
        _tokenServiceMock.Setup(t => t.GetAccessTokenExpiration()).Returns(DateTime.UtcNow.AddHours(1));
        _tokenServiceMock.Setup(t => t.GeneratePasswordResetToken()).Returns("vt");
        _contextMock.Setup(c => c.GetUserRoleNamesAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<string>());
        _contextMock.Setup(c => c.GetUserPermissionsAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<string>());

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _passwordHasherMock.Verify(p => p.HashPassword("PlainPassword123!"), Times.Once);
        _contextMock.Verify(c =>
            c.SetUserPasswordHashAsync(It.IsAny<Guid>(), "HASHED_PASSWORD", It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
