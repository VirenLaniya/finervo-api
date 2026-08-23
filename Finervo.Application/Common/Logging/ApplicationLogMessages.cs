using Microsoft.Extensions.Logging;

namespace Finervo.Application.Common.Logging
{
    public static partial class ApplicationLogMessages
    {
        #region Logging Behavior

        [LoggerMessage(
            Level = LogLevel.Information,
            Message = "Handling {RequestName}")]
        public static partial void HandlingRequest(
            ILogger logger,
            string requestName);

        [LoggerMessage(
            Level = LogLevel.Information,
            Message = "Handled {RequestName} successfully in {ElapsedMs}ms")]
        public static partial void RequestSucceeded(
            ILogger logger,
            string requestName,
            long elapsedMs);

        [LoggerMessage(
            Level = LogLevel.Warning,
            Message = "Handled {RequestName} with failure in {ElapsedMs}ms — {ErrorCode}: {ErrorMessage}")]
        public static partial void RequestFailed(
            ILogger logger,
            string requestName,
            long elapsedMs,
            string errorCode,
            string errorMessage);

        [LoggerMessage(
            Level = LogLevel.Error,
            Message = "Failed {RequestName} in {ElapsedMs}ms")]
        public static partial void RequestException(
            ILogger logger,
            string requestName,
            long elapsedMs,
            Exception ex);

        #endregion
    }
}
