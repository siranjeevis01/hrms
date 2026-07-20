using FluentAssertions;
using HRMS.Services.Identity.Infrastructure.Services;
using Xunit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace HRMS.Tests.Identity;

public class SmtpEmailServiceTests
{
    private readonly Mock<ILogger<SmtpEmailService>> _loggerMock = new();

    [Fact]
    public async Task Should_log_warning_and_not_send_when_SMTP_is_not_configured()
    {
        // Arrange - Host is localhost and no username configured
        var options = Options.Create(new SmtpOptions
        {
            Host = "localhost",
            Port = 587,
            UserName = string.Empty,
            Password = string.Empty,
            FromEmail = "test@test.com",
            FromName = "Test"
        });

        var service = new SmtpEmailService(options, _loggerMock.Object);

        // Act - should not throw
        await service.SendEmailVerificationAsync("user@test.com", "John", "token123");

        // Assert
        _loggerMock.Verify(
            l => l.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("SMTP not configured")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task Should_not_throw_when_all_parameters_are_valid()
    {
        // Arrange - configured SMTP (non-localhost with username)
        var options = Options.Create(new SmtpOptions
        {
            Host = "smtp.example.com",
            Port = 587,
            UserName = "user",
            Password = "pass",
            FromEmail = "test@test.com",
            FromName = "Test"
        });

        var service = new SmtpEmailService(options, _loggerMock.Object);

        // Act & Assert - should not throw (will fail to connect but that's caught internally)
        var act = async () =>
            await service.SendEmailVerificationAsync("user@test.com", "John", "token123");
        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Should_log_warning_for_password_reset_email()
    {
        // Arrange - unconfigured SMTP
        var options = Options.Create(new SmtpOptions
        {
            Host = "localhost",
            Port = 587,
            UserName = string.Empty,
            Password = string.Empty,
            FromEmail = "test@test.com",
            FromName = "Test"
        });

        var service = new SmtpEmailService(options, _loggerMock.Object);

        // Act
        await service.SendPasswordResetAsync("user@test.com", "John", "reset_token");

        // Assert
        _loggerMock.Verify(
            l => l.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("SMTP not configured")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }
}
