using System;
using System.Collections.Generic;
using System.Text;

namespace Finervo.Application.Common.Interfaces
{
    /// <summary>
    /// Holds per-request contextual information available throughout
    /// the entire request lifetime including MediatR pipeline.
    /// </summary>
    public interface IRequestContext
    {
        string CorrelationId { get; }
        string UserId { get; }
        void Initialize(string correlationId, string userId);
    }
}
