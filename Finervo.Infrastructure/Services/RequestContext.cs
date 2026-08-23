using Finervo.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Infrastructure.Services
{
    /// <summary>
    /// Scoped service that carries request context through the entire
    /// request pipeline including MediatR handlers and behaviors.
    /// </summary>
    public sealed class RequestContext : IRequestContext
    {
        public string CorrelationId { get; private set; } = "none";

        public string UserId { get; private set; } = "anonymous";

        public void Initialize(string correlationId, string userId)
        {
            CorrelationId = correlationId;
            UserId = userId;
        }
    }
}
