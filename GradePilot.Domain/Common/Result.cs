namespace GradePilot.Domain.Common;

public abstract record Result<T>
{
    public sealed record Success(T Value) : Result<T>;

    public sealed record Failure(string Error) : Result<T>;

    public bool IsSuccess => this is Success;

    public bool IsFailure => this is Failure;

    public TOut Match<TOut>(
        Func<T, TOut> onSuccess,
        Func<string, TOut> onFailure) =>
        this switch
        {
            Success success => onSuccess(success.Value),
            Failure failure => onFailure(failure.Error),
            _ => throw new InvalidOperationException("Unknown result state.")
        };

    public Result<TOut> Map<TOut>(Func<T, TOut> map) =>
        Match<Result<TOut>>(
            value => new Result<TOut>.Success(map(value)),
            error => new Result<TOut>.Failure(error));

    public Result<TOut> Bind<TOut>(Func<T, Result<TOut>> bind) =>
        Match<Result<TOut>>(
            bind,
            error => new Result<TOut>.Failure(error));
}

public static class Result
{
    public static Result<Unit> Ok() => Ok(Unit.Value);

    public static Result<T> Ok<T>(T value) => new Result<T>.Success(value);

    public static Result<T> Fail<T>(string error) => new Result<T>.Failure(error);
}
