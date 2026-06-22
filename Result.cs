using System;

namespace Kei.Results;

/// <summary>Represents the outcome of an operation.</summary>
public class Result
{
    /// <summary>Gets a value indicating whether the operation succeeded.</summary>
    public bool IsSuccess { get; }

    /// <summary>Gets a value indicating whether the operation failed.</summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>Gets the error message for a failed result, or an empty string for success.</summary>
    public string Error { get; }

    /// <summary>Initializes a new instance of the <see cref="Result"/> class.</summary>
    protected Result(bool isSuccess, string error)
    {
        if (isSuccess && error != string.Empty)
            throw new InvalidOperationException("A successful result cannot have an error.");
        if (!isSuccess && error == string.Empty)
            throw new InvalidOperationException("A failed result must have an error.");

        IsSuccess = isSuccess;
        Error = error;
    }

    /// <summary>Creates a successful result.</summary>
    public static Result Success() => new Result(true, string.Empty);

    /// <summary>Creates a failed result.</summary>
    public static Result Failure(string error) => new Result(false, error);

    /// <summary>Creates a successful result with a value.</summary>
    public static Result<T> Success<T>(T value) => Result<T>.Success(value);

    /// <summary>Creates a failed result with a value type.</summary>
    public static Result<T> Failure<T>(string error) => Result<T>.Failure(error);
}

/// <summary>Represents the outcome of an operation that returns a value.</summary>
public class Result<T> : Result
{
    private readonly T _value;

    /// <summary>Gets the value for a successful result.</summary>
    public T Value => IsSuccess
        ? _value
        : throw new InvalidOperationException("Cannot access Value on a failed result.");

    /// <summary>Initializes a new instance of the <see cref="Result{T}"/> class.</summary>
    private Result(bool isSuccess, T value, string error)
        : base(isSuccess, error)
    {
        _value = value;
    }

    /// <summary>Creates a successful result with a value.</summary>
    public static Result<T> Success(T value) => new Result<T>(true, value, string.Empty);

    /// <summary>Creates a failed result.</summary>
    public static new Result<T> Failure(string error) => new Result<T>(false, default!, error);
}
