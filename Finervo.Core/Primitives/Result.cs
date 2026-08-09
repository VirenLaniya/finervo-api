using Finervo.Core.Errors;

namespace Finervo.Core.Primitives
{
    public class Result
    {
        public bool IsSuccess { get; }
        public Error Error { get; }

        protected Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != CommonErrors.None)
                throw new InvalidOperationException();

            if (!isSuccess && error == CommonErrors.None)
                throw new InvalidOperationException();

            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new(true, CommonErrors.None);
        public static Result Failure(Error error) => new(false, error);
    }

    public class Result<T> : Result
    {
        private readonly T _value;

        protected Result(T value, bool isSuccess, Error error) : base(isSuccess, error)
        {
            _value = value;
        }

        public T Data { get => _value; }

        public static Result<T> Success(T value) => new(value, true, CommonErrors.None);
        public static new Result<T> Failure(Error error) => new(default!, false, error);
    }
}
