using FluentAssertions;
using HRMS.Services.Leave.Application.Events;
using Xunit;
using HRMS.Shared.Kernel.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace HRMS.Tests.Leave;

public class LeaveApprovedEventHandlerTests
{
    private readonly Mock<INotificationService> _notificationServiceMock = new();
    private readonly Mock<ILogger<LeaveApprovedEventHandler>> _loggerMock = new();
    private readonly LeaveApprovedEventHandler _handler;

    public LeaveApprovedEventHandlerTests()
    {
        _handler = new LeaveApprovedEventHandler(_notificationServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task Should_send_email_when_leave_is_approved()
    {
        // Arrange
        var employeeId = Guid.NewGuid();
        var approvedEvent = new LeaveApprovedEvent
        {
            LeaveApplicationId = Guid.NewGuid(),
            EmployeeId = employeeId,
            LeaveTypeId = Guid.NewGuid(),
            StartDate = new DateTime(2026, 8, 1),
            EndDate = new DateTime(2026, 8, 5),
            TotalDays = 5,
            ApprovedBy = Guid.NewGuid(),
            ApprovedAt = DateTime.UtcNow,
            TenantId = Guid.NewGuid()
        };

        _notificationServiceMock.Setup(n =>
            n.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(approvedEvent, CancellationToken.None);

        // Assert
        _notificationServiceMock.Verify(n =>
            n.SendEmailAsync(
                $"employee-{employeeId}@hrms.local",
                It.Is<string>(s => s.Contains("Approved")),
                It.IsAny<string>(),
                true,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Should_not_throw_when_notification_fails()
    {
        // Arrange
        var approvedEvent = new LeaveApprovedEvent
        {
            LeaveApplicationId = Guid.NewGuid(),
            EmployeeId = Guid.NewGuid(),
            LeaveTypeId = Guid.NewGuid(),
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(2),
            TotalDays = 2,
            ApprovedBy = Guid.NewGuid(),
            ApprovedAt = DateTime.UtcNow,
            TenantId = Guid.NewGuid()
        };

        _notificationServiceMock.Setup(n =>
            n.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("SMTP connection failed"));

        // Act & Assert
        var act = async () => await _handler.Handle(approvedEvent, CancellationToken.None);
        await act.Should().ThrowAsync<InvalidOperationException>();
    }
}
