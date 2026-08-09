using Finervo.Core.Errors;

namespace Finervo.Core.Primitives
{
    public class ValidationErrorResult : Result
    {
        public Dictionary<string, string[]> ValidationErrors { get; private set; }
        private ValidationErrorResult(Dictionary<string, string[]> errors) : base(false, CommonErrors.ValidationFailed)
        {
            ValidationErrors = errors;
        }

        public static ValidationErrorResult WithErrors(Dictionary<string, string[]> errors) => new(errors);
    }

    public class ValidationErrorResult<T> : Result<T>
    {
        public Dictionary<string, string[]> ValidationErrors { get; private set; }
        private ValidationErrorResult(Dictionary<string, string[]> errors) : base(default!, false, CommonErrors.ValidationFailed)
        {
            ValidationErrors = errors;
        }

        public static ValidationErrorResult<T> WithErrors(Dictionary<string, string[]> errors) => new(errors);
    }
}
