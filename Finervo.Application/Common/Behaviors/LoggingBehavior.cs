using Finervo.Application.Common.Logging;
using Finervo.Core.Primitives;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Finervo.Application.Common.Behaviors
{
    public sealed class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
        {
            var requestName = typeof(TRequest).Name;

            ApplicationLogMessages.HandlingRequest(logger, requestName);

            var sw = Stopwatch.StartNew();

            try
            {
                var response = await next(ct);
                sw.Stop();

                if (response is Result result && !result.IsSuccess)
                {
                    ApplicationLogMessages.RequestFailed(
                        logger,
                        requestName,
                        sw.ElapsedMilliseconds,
                        result.Error.Code,
                        result.Error.Message);
                }
                else
                {
                    ApplicationLogMessages.RequestSucceeded(
                        logger,
                        requestName,
                        sw.ElapsedMilliseconds);
                }

                return response;
            }
            catch (Exception ex)
            {
                sw.Stop();

                ApplicationLogMessages.RequestException(
                    logger,
                    requestName,
                    sw.ElapsedMilliseconds,
                    ex);

                throw;
            }
        }
    }
}
