using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.ReverseProxy.Middleware;

namespace HRMS.ApiGateway.Transforms;

public sealed class CorrelationIdTransform : ITransformer
{
    private const string CorrelationIdHeader = "X-Correlation-Id";

    public async ValueTask TransformRequestAsync(
        HttpContext httpContext,
        ClusterPartition cluster,
        CancellationToken cancellationToken)
    {
        var correlationId = httpContext.Request.Headers[CorrelationIdHeader].FirstOrDefault()
            ?? Guid.NewGuid().ToString("D");

        httpContext.Items["CorrelationId"] = correlationId;

        cluster.Request?.Headers?.Remove(CorrelationIdHeader);
        cluster.Request?.Headers?.Append(CorrelationIdHeader, correlationId);

        await ValueTask.CompletedTask;
    }

    public ValueTask TransformResponseAsync(
        HttpContext httpContext,
        ClusterPartition cluster,
        CancellationToken cancellationToken)
    {
        if (httpContext.Items["CorrelationId"] is string correlationId)
        {
            httpContext.Response.Headers[CorrelationIdHeader] = correlationId;
        }

        return ValueTask.CompletedTask;
    }

    public ValueTask TransformResponseTrailersAsync(
        HttpContext httpContext,
        ClusterPartition cluster,
        CancellationToken cancellationToken)
    {
        return ValueTask.CompletedTask;
    }
}
