namespace HRMS.Shared.Kernel.Common;

public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public Error Error { get; } = Error.None;
    public IReadOnlyList<Error> Errors { get; }

    private Result(T value)
    {
        IsSuccess = true;
        Value = value;
        Error = Error.None;
        Errors = Array.Empty<Error>();
    }

    private Result(Error error)
    {
        IsSuccess = false;
        Value = default;
        Error = error;
        Errors = new[] { error };
    }

    private Result(IReadOnlyList<Error> errors)
    {
        IsSuccess = false;
        Value = default;
        Error = errors.FirstOrDefault()!;
        Errors = errors;
    }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(Error error) => new(error);
    public static Result<T> Failure(IReadOnlyList<Error> errors) => new(errors);

    public TResult Match<TResult>(
        Func<T, TResult> onSuccess,
        Func<Error, TResult> onFailure)
    {
        return IsSuccess ? onSuccess(Value!) : onFailure(Error);
    }
}

public class Result
{
    public bool IsSuccess { get; }
    public Error Error { get; } = Error.None;
    public IReadOnlyList<Error> Errors { get; }

    private Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
        Errors = isSuccess ? Array.Empty<Error>() : new[] { error };
    }

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
    public static Result Failure(IReadOnlyList<Error> errors) =>
        new(false, errors.FirstOrDefault()!);

    public TResult Match<TResult>(
        Func<TResult> onSuccess,
        Func<Error, TResult> onFailure)
    {
        return IsSuccess ? onSuccess() : onFailure(Error);
    }
}
