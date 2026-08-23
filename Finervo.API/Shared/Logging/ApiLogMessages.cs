namespace Finervo.API.Shared.Logging
{
    /// <summary>
    /// Defines strongly-typed log messages for API middleware layer.
    /// Uses LoggerMessage source generator for zero-allocation structured logging.
    /// </summary>
    public static partial class ApiLogMessages
    {
        #region Exception Handling Middleware

        [LoggerMessage(
            Level = LogLevel.Warning,
            Message = "Validation failed - {Errors}")]
        public static partial void ValidationFailed(
            ILogger logger,
            IEnumerable<string> errors);

        [LoggerMessage(
            Level = LogLevel.Error,
            Message = "Unhandled exception - {ErrorMessage}")]
        public static partial void UnhandledException(
            ILogger logger,
            string errorMessage,
            Exception ex);

        #endregion
    }
}
