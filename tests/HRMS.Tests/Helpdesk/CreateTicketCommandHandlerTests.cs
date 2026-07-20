using FluentAssertions;
using HRMS.Services.Helpdesk.Application.Commands.CreateTicket;
using Xunit;
using HRMS.Services.Helpdesk.Application.Interfaces;
using HRMS.Services.Helpdesk.Domain.Enums;
using HRMS.Shared.Kernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HRMS.Tests.Helpdesk;

public class CreateTicketCommandHandlerTests
{
    private readonly Mock<IHelpdeskDbContext> _contextMock = new();
    private readonly Mock<INotificationService> _notificationServiceMock = new();
    private readonly CreateTicketCommandHandler _handler;

    public CreateTicketCommandHandlerTests()
    {
        _handler = new CreateTicketCommandHandler(_contextMock.Object, _notificationServiceMock.Object);

        var dbSetMock = new Mock<DbSet<HRMS.Services.Helpdesk.Domain.Entities.HelpdeskTicket>>();
        _contextMock.Setup(c => c.HelpdeskTickets).Returns(dbSetMock.Object);
    }

    [Fact]
    public async Task Should_create_ticket_and_return_ticket_ID()
    {
        // Arrange
        var command = new CreateTicketCommand
        {
            EmployeeId = Guid.NewGuid(),
            Subject = "Printer not working",
            Description = "The office printer on 3rd floor is jammed",
            Category = TicketCategoryType.IT,
            Priority = TicketPriority.High,
            DepartmentId = Guid.NewGuid(),
            TenantId = "tenant-1"
        };

        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        _notificationServiceMock.Setup(n =>
            n.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var ticketId = await _handler.Handle(command, CancellationToken.None);

        // Assert
        ticketId.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task Should_send_notification_email_after_ticket_creation()
    {
        // Arrange
        var employeeId = Guid.NewGuid();
        var command = new CreateTicketCommand
        {
            EmployeeId = employeeId,
            Subject = "VPN Issue",
            Description = "Cannot connect to VPN",
            Category = TicketCategoryType.IT,
            Priority = TicketPriority.Medium,
            TenantId = "tenant-1"
        };

        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        _notificationServiceMock.Setup(n =>
            n.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _notificationServiceMock.Verify(n =>
            n.SendEmailAsync(
                $"employee-{employeeId}@hrms.local",
                It.Is<string>(s => s.Contains("VPN Issue")),
                It.IsAny<string>(),
                true,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Should_save_ticket_to_database()
    {
        // Arrange
        var command = new CreateTicketCommand
        {
            EmployeeId = Guid.NewGuid(),
            Subject = "Access Request",
            Description = "Need access to admin panel",
            Category = TicketCategoryType.HR,
            Priority = TicketPriority.Low,
            TenantId = "tenant-1"
        };

        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        _notificationServiceMock.Setup(n =>
            n.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
