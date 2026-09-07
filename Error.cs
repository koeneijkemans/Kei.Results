namespace Kei.Results;

/// <summary>Represents a specific failure reason, identified by a stable code and a human-readable message.</summary>
/// <param name="Code">The stable identifier for this error, used to distinguish failure cases in calling code.</param>
/// <param name="Message">The human-readable description of the failure.</param>
public sealed record Error(string Code, string Message)
{
    /// <summary>Gets the error that represents the absence of a failure, used by successful results.</summary>
    public static readonly Error None = new(string.Empty, string.Empty);

    /// <summary>Converts a message into an <see cref="Error"/> with an empty <see cref="Code"/>.</summary>
    /// <param name="message">The human-readable description of the failure.</param>
    public static explicit operator Error(string message) => new(string.Empty, message);
}
