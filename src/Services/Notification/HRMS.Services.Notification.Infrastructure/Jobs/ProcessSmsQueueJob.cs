using HRMS.Services.Notification.Application.Commands.ProcessEmailQueue;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HRMS.Services.Notification.Infrastructure.Jobs;

public class ProcessEmailQueueJob
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProcessEmailQueueJob> _logger;

    public ProcessEmailQueueJob(IMediator mediator, ILogger<ProcessEmailQueueJob> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [AutomaticRetry(Attempts = 3)]
    [Queue("email")]
    public async Task Execute(int batchSize = 50)
    {
        _logger.LogInformation("Starting email queue processing. Batch size: {BatchSize}", batchSize);
        var processed = await _mediator.Send(new ProcessEmailQueueCommand { BatchSize = batchSize });
        _logger.LogInformation("Email queue processing completed. Processed: {Processed}", processed);
    }
}
