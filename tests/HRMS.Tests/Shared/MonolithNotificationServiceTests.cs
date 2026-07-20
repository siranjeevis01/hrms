using FluentAssertions;
using HRMS.Api.Services;
using Xunit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace HRMS.Tests.Shared;

public class MonolithNotificationServiceTests
{
    private readonly Mock<ILogger<MonolithNotificationService>> _loggerMock = new();

    [Fact]
    public async Task Should_log_and_skip_when_SMTP_not_configured()
    {
        // Arrange - localhost with no username = unconfigured
        var settings = Options.Create(new SmtpSettings
        {
            Host = "localhost",
            Port = 587,
            UserName = string.Empty,
            Password = string.Empty,
            FromEmail = "test@test.com",
            FromName = "Test"
        });

        var service = new MonolithNotificationService(settings, _loggerMock.Object);

        // Act
        await service.SendEmailAsync("user@test.com", "Test Subject", "Test Body");

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
    public async Task Should_log_push_notification_info_without_throwing()
    {
        // Arrange
        var settings = Options.Create(new SmtpSettings
        {
            Host = "localhost",
            Port = 587
        });

        var service = new MonolithNotificationService(settings, _loggerMock.Object);

        // Act
        var act = async () =>
            await service.SendPushNotificationAsync(
                Guid.NewGuid(),
                "Test Title",
                "Test Body");

        // Assert
        await act.Should().NotThrowAsync();
        _loggerMock.Verify(
            l => l.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Push notification")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }
}
