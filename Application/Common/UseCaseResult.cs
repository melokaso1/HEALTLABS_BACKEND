namespace Application.Common;

public sealed class UseCaseResult<T>
{
    private UseCaseResult(bool succeeded, T? value, string? error, int statusCode)
    {
        Succeeded = succeeded;
        Value = value;
        Error = error;
        StatusCode = statusCode;
    }

    public bool Succeeded { get; }
    public T? Value { get; }
    public string? Error { get; }
    public int StatusCode { get; }

    public static UseCaseResult<T> Success(T value, int statusCode = 200)
        => new(true, value, null, statusCode);

    public static UseCaseResult<T> Fail(string error, int statusCode = 400)
        => new(false, default, error, statusCode);
}

public sealed class UseCaseResult
{
    private UseCaseResult(bool succeeded, string? error, int statusCode)
    {
        Succeeded = succeeded;
        Error = error;
        StatusCode = statusCode;
    }

    public bool Succeeded { get; }
    public string? Error { get; }
    public int StatusCode { get; }

    public static UseCaseResult Success(int statusCode = 204)
        => new(true, null, statusCode);

    public static UseCaseResult Fail(string error, int statusCode = 400)
        => new(false, error, statusCode);
}
