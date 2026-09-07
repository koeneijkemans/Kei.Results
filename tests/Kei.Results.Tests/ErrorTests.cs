namespace Kei.Results.Tests;

public class ErrorTests
{
    [Test]
    public async Task None_HasEmptyCodeAndMessage()
    {
        await Assert.That(Error.None.Code).IsEmpty();
        await Assert.That(Error.None.Message).IsEmpty();
    }

    [Test]
    public async Task Errors_WithSameCodeAndMessage_AreEqual()
    {
        var first = new Error("USER_NOT_FOUND", "User was not found.");
        var second = new Error("USER_NOT_FOUND", "User was not found.");

        await Assert.That(first).IsEqualTo(second);
    }

    [Test]
    public async Task Errors_WithDifferentCodes_AreNotEqual()
    {
        var notFound = new Error("USER_NOT_FOUND", "Not found.");
        var forbidden = new Error("USER_FORBIDDEN", "Not found.");

        await Assert.That(notFound).IsNotEqualTo(forbidden);
    }

    [Test]
    public async Task ExplicitConversion_FromString_ProducesEmptyCode()
    {
        var error = (Error)"Something went wrong.";

        await Assert.That(error.Code).IsEmpty();
        await Assert.That(error.Message).IsEqualTo("Something went wrong.");
    }

    [Test]
    public async Task ExplicitConversion_FromEmptyString_EqualsNone()
    {
        var error = (Error)string.Empty;

        await Assert.That(error).IsEqualTo(Error.None);
    }
}
