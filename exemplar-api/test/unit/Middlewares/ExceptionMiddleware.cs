using DHCW.PD.Controllers;
using DHCW.PD.Exceptions;
using DHCW.PD.Middlewares;
using Moq;

namespace UnitTests.Middlewares
{
    public class ExceptionMiddlewareTest
    {

        private readonly Mock<RequestDelegate> _next;
        private readonly Mock<HttpContext> _httpContext;
        private readonly Mock<ILogger<ExceptionMiddleware>> _logger;
        private readonly ExceptionMiddleware _exceptionMiddleware;

        public ExceptionMiddlewareTest()
        {
            _httpContext = new Mock<HttpContext>();
            _next = new Mock<RequestDelegate>();
            _logger = new Mock<ILogger<ExceptionMiddleware>>();
            _exceptionMiddleware = new ExceptionMiddleware(_next.Object, _logger.Object);
        }

        [Fact]
        public async Task HandleException_ShouldReturnBadRequest()
        {
            _httpContext.Setup(x=> x.Response).Returns(new DefaultHttpContext().Response);
            _next.Setup(x=> x.Invoke(_httpContext.Object)).Throws(new BadRequestException());

            await _exceptionMiddleware.InvokeAsync(_httpContext.Object);

            Assert.Equal(StatusCodes.Status400BadRequest, _httpContext.Object.Response.StatusCode);
        }

        [Fact]
        public async Task HandleException_ShouldReturnUnauthorizedRequest()
        {
            _httpContext.Setup(x => x.Response).Returns(new DefaultHttpContext().Response);
            _next.Setup(x => x.Invoke(_httpContext.Object)).Throws(new UnauthorizedException());

            await _exceptionMiddleware.InvokeAsync(_httpContext.Object);

            Assert.Equal(StatusCodes.Status401Unauthorized, _httpContext.Object.Response.StatusCode);
        }

        [Fact]
        public async Task HandleException_ShouldReturnForbiddenRequest()
        {
            _httpContext.Setup(x => x.Response).Returns(new DefaultHttpContext().Response);
            _next.Setup(x => x.Invoke(_httpContext.Object)).Throws(new ForbiddenException());

            await _exceptionMiddleware.InvokeAsync(_httpContext.Object);

            Assert.Equal(StatusCodes.Status403Forbidden, _httpContext.Object.Response.StatusCode);
        }

        [Fact]
        public async Task HandleException_ShouldReturnNotFound()
        {
            _httpContext.Setup(x => x.Response).Returns(new DefaultHttpContext().Response);
            _next.Setup(x => x.Invoke(_httpContext.Object)).Throws(new NotFoundException());

            await _exceptionMiddleware.InvokeAsync(_httpContext.Object);

            Assert.Equal(StatusCodes.Status404NotFound, _httpContext.Object.Response.StatusCode);
        }

        [Fact]
        public async Task HandleException_ShouldReturnRequestTimeout()
        {
            _httpContext.Setup(x => x.Response).Returns(new DefaultHttpContext().Response);
            _next.Setup(x => x.Invoke(_httpContext.Object)).Throws(new DHCW.PD.Exceptions.TimeoutException());

            await _exceptionMiddleware.InvokeAsync(_httpContext.Object);

            Assert.Equal(StatusCodes.Status408RequestTimeout, _httpContext.Object.Response.StatusCode);
        }

    }
}
