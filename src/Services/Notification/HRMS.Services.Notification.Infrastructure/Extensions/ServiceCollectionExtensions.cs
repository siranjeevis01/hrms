using HRMS.Services.Notification.Application.Commands.RetryFailedNotifications;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HRMS.Services.Notification.Infrastructure.Jobs;

public class RetryFailedJob
{
    private readonly IMediator _mediator;
    private readonly ILogger<RetryFailedJob> _logger;

    public RetryFailedJob(IMediator mediator, ILogger<RetryFailedJob> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [AutomaticRetry(Attempts = 2)]
    [Queue("retry")]
    public async Task Execute(int batchSize = 50)
    {
        _logger.LogInformation("Starting retry failed notifications. Batch size: {BatchSize}", batchSize);
        var retried = await _mediator.Send(new RetryFailedNotificationsCommand { BatchSize = batchSize });
        _logger.LogInformation("Retry completed. Retried: {Retried}", retried);
    }
}
