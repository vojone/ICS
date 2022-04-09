using Xunit.Sdk;

namespace Carpool.Common.Tests;

public class ObjectEqualException : AssertActualExpectedException
{
    // Taken from sample project 'Cookbook'
    public ObjectEqualException(object? expected, object? actual, string message)
        : base(expected, actual, "Assert.Equal() Failure")
    {
        Message = message;
    }

    public override string Message { get; }
}
