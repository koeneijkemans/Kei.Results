using System;

namespace Kei.Results;

public class Result
{
    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;
    public string Error { get; }

    protected Result(bool isSuccess, string error)
    {
        if (isSuccess && error != string.Empty)
            throw new InvalidOperationException("A successful result cannot have an error.");
        if (!isSuccess && error == string.Empty)
            throw new InvalidOperationException("A failed result must have an error.");

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new Result(true, string.Empty);
    public static Result Failure(string error) => new Result(false, error);

    public static Result<T> Success<T>(T value) => Result<T>.Success(value);
    public static Result<T> Failure<T>(string error) => Result<T>.Failure(error);
}

public class Result<T> : Result
{
    private readonly T _value;

    public T Value => IsSuccess
        ? _value
        : throw new InvalidOperationException("Cannot access Value on a failed result.");

    private Result(bool isSuccess, T value, string error)
        : base(isSuccess, error)
    {
        _value = value;
    }

    public static Result<T> Success(T value) => new Result<T>(true, value, string.Empty);
    public static new Result<T> Failure(string error) => new Result<T>(false, default!, error);
}
