using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Shop.Api.Middlewares;

namespace Shop.Api.Tests.Middlewares;

public class ErrorHandlingMiddlewareTests
{
    [Fact]
    public async Task InvokeAsync_WithNoExceptionThrown_CallNextDelegate()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
        var context = new DefaultHttpContext();
        var nextDelegateMock = new Mock<RequestDelegate>();
        var middleware = new ErrorHandlingMiddleware(loggerMock.Object);

        // Act
        await middleware.InvokeAsync(context, nextDelegateMock.Object);

        // Assert
        nextDelegateMock.Verify(next => next.Invoke(context), Times.Once);
    }
}
