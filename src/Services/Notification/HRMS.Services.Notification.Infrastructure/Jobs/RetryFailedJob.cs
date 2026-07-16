using HRMS.Services.Notification.Application.Commands.ProcessSmsQueue;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HRMS.Services.Notification.Infrastructure.Jobs;

public class ProcessSmsQueueJob
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProcessSmsQueueJob> _logger;

    public ProcessSmsQueueJob(IMediator mediator, ILogger<ProcessSmsQueueJob> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [AutomaticRetry(Attempts = 3)]
    [Queue("sms")]
    public async Task Execute(int batchSize = 50)
    {
        _logger.LogInformation("Starting SMS queue processing. Batch size: {BatchSize}", batchSize);
        var processed = await _mediator.Send(new ProcessSmsQueueCommand { BatchSize = batchSize });
        _logger.LogInformation("SMS queue processing completed. Processed: {Processed}", processed);
    }
}
