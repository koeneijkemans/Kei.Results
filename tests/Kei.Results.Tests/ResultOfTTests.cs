namespace Kei.Results.Tests;

public class ResultOfTTests
{
    [Test]
    public async Task Success_ExposesTheValue()
    {
        var result = Result.Success(42);

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value).IsEqualTo(42);
        await Assert.That(result.Error).IsEqualTo(Error.None);
    }

    [Test]
    public async Task Success_WithReferenceType_ExposesTheValue()
    {
        var result = Result.Success("payload");

        await Assert.That(result.Value).IsEqualTo("payload");
    }

    [Test]
    public async Task Failure_PreservesTheError()
    {
        var error = new Error("ORDER_EXPIRED", "The order has expired.");

        var result = Result.Failure<int>(error);

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error).IsEqualTo(error);
    }

    [Test]
    public async Task Failure_AccessingValue_Throws()
    {
        var result = Result.Failure<int>(new Error("ORDER_EXPIRED", "The order has expired."));

        await Assert.That(() => _ = result.Value)
            .Throws<InvalidOperationException>()
            .WithMessage("Cannot access Value on a failed result.");
    }

    [Test]
    public async Task Failure_WithNull_Throws()
    {
        await Assert.That(() => Result.Failure<int>(null!)).Throws<ArgumentNullException>();
    }

    [Test]
    public async Task Failure_IsAssignableToResult()
    {
        Result result = Result.Failure<int>(new Error("ORDER_EXPIRED", "The order has expired."));

        await Assert.That(result.IsFailure).IsTrue();
    }
}
