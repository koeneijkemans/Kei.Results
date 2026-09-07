namespace Kei.Results.Tests;

public class ResultTests
{
    [Test]
    public async Task Success_IsSuccessful_AndCarriesNoError()
    {
        var result = Result.Success();

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.IsFailure).IsFalse();
        await Assert.That(result.Error).IsEqualTo(Error.None);
    }

    [Test]
    public async Task Failure_IsNotSuccessful_AndPreservesTheError()
    {
        var error = new Error("USER_NOT_FOUND", "User was not found.");

        var result = Result.Failure(error);

        await Assert.That(result.IsSuccess).IsFalse();
        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error).IsEqualTo(error);
        await Assert.That(result.Error.Code).IsEqualTo("USER_NOT_FOUND");
    }

    [Test]
    public async Task Failure_WithNone_Throws()
    {
        await Assert.That(() => Result.Failure(Error.None))
            .Throws<InvalidOperationException>()
            .WithMessage("A failed result must have an error.");
    }

    [Test]
    public async Task Failure_WithNull_Throws()
    {
        await Assert.That(() => Result.Failure(null!)).Throws<ArgumentNullException>();
    }

    [Test]
    public async Task Failure_WithCodeButNoMessage_IsAllowed()
    {
        var result = Result.Failure(new Error("USER_NOT_FOUND", string.Empty));

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Code).IsEqualTo("USER_NOT_FOUND");
    }

    [Test]
    public async Task Failure_FromExplicitlyConvertedString_ProducesErrorWithoutCode()
    {
        var result = Result.Failure((Error)"Something went wrong.");

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Code).IsEmpty();
        await Assert.That(result.Error.Message).IsEqualTo("Something went wrong.");
    }
}
